using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    internal class ActivityStreamsParser
    {
        private static IActivityObjectOrLink[] ParseActivityObjectOrLink(JsonElement el, IActivityObject parent)
        {
            if (el.ValueKind == JsonValueKind.Undefined || el.ValueKind == JsonValueKind.Null)
                return null;

            JsonElement[] elementArray = el.ValueKind == JsonValueKind.Array ? el.EnumerateArray().ToArray() : new JsonElement[] { el };
            var parsed = new IActivityObjectOrLink[elementArray.Length];

            for (int i = 0; i < elementArray.Length; ++i)
            {
                IActivityObjectOrLink aOrI = new ActivityObjectOrLink();
                parsed[i] = aOrI;
                var toParse = elementArray[i];

                if (ActivityLinkBuilder.IsLinkElment(toParse))
                    aOrI.Link = new ActivityLinkBuilder().FromJsonElement(toParse).Build();
                else
                    aOrI.Obj = ParseActivityObject(toParse, parent);
            }

            return parsed;
        }

        private static IActivityLink ParseOutActivityLink(JsonElement el)
        {
            return new ActivityLinkBuilder().FromJsonElement(el).Build();
        }

        private static IActivityLink[] ParseOutActivityLinks(JsonElement el)
        {
            if (el.ValueKind != JsonValueKind.Array)
            {
                return new IActivityLink[] { ParseOutActivityLink(el) };
            }

            List<IActivityLink> links = new List<IActivityLink>();

            foreach (var t in el.EnumerateArray())
            {
                links.Add(ParseOutActivityLink(t));
            }

            return links.ToArray();
        }

        private static IActivityObject[] ParseActivityObjects(JsonElement el, IActivityObject parent)
        {
            if (el.ValueKind != JsonValueKind.Array)
            {
                return new IActivityObject[] { ParseActivityObject(el, parent) };
            }

            List<IActivityObject> objects = new List<IActivityObject>();

            foreach (var t in el.EnumerateArray())
            {
                objects.Add(ParseActivityObject(t, parent));
            }

            return objects.ToArray();
        }

        internal static void ParseOutExtensions(JsonElement el, IActivityObject activity)
        {
            if (activity.ExtendedContexts == null || !activity.ExtendedContexts.Any())
                return;

            var knownAttributes = activity.GetType().GetProperties(
                          BindingFlags.FlattenHierarchy |
                          BindingFlags.Instance |
                          BindingFlags.Public).Where(p =>
                          {
                              return (null != p.GetCustomAttribute(typeof(JsonPropertyNameAttribute)));
                          })
                          .Select(p => (p.GetCustomAttribute(typeof(JsonPropertyNameAttribute)) as JsonPropertyNameAttribute).Name)
                          .ToArray();

            activity.Extensions = new Dictionary<string, string>();
            activity.ExtensionsOutOfContext = new Dictionary<string, string>();
            foreach (var t in el.EnumerateObject())
            {
                if (t.Name == "@context" || t.Name == "context" || t.Name == "type" || knownAttributes.Contains(t.Name))
                    continue;

                int indexOfColon = t.Name.IndexOf(":");

                // Parse out extension objects
                if (indexOfColon > 0 && indexOfColon < t.Name.Length - 1)
                {
                    // split out the extension
                    string extensionName = t.Name.Substring(0, indexOfColon);
                    if (activity.ExtendedContexts.ContainsKey(extensionName) || activity.ExtendedIds.ContainsKey(extensionName))
                    {
                        if (!activity.Extensions.ContainsKey(t.Name))
                        {
                            activity.Extensions.Add(t.Name, t.Value.ToString());
                        }
                    }
                    else
                    {
                        activity.ExtensionsOutOfContext.Add(t.Name, t.Value.ToString());
                    }
                }
                else if (t.Value.ValueKind == JsonValueKind.Object || t.Value.ValueKind == JsonValueKind.String || t.Value.ValueKind == JsonValueKind.Number || t.Value.ValueKind == JsonValueKind.True || t.Value.ValueKind == JsonValueKind.False || t.Value.ValueKind == JsonValueKind.Undefined)
                {
                    // Make sure we ignore the standard items
                    if (t.Name.Contains(":"))
                        activity.Extensions.Add(t.Name, t.Value.ToString());
                    else if (activity.ExtendedContexts.ContainsKey(t.Name) || activity.ExtendedIds.ContainsKey(t.Name))
                        activity.Extensions.Add(t.Name, t.Value.ToString());
                    else
                        activity.ExtensionsOutOfContext.Add(t.Name, t.Value.ToString());
                }
                else if (t.Value.ValueKind == JsonValueKind.Array)
                {
                    foreach (var subEl in t.Value.EnumerateArray())
                    {
                        int foo = 0;
                        // this.Extensions.Add(subEl.Name.ToLower(), subEl.Value.ToString());
                    }
                }
            }
        }


        internal static ActivityObject CreateNewActivity(JsonElement el, Dictionary<string, Func<ActivityObject>> TypeToObjectMap)
        {
            if (el.ValueKind == JsonValueKind.String)
            {
                // TODO this should be a reference link
                return new ActivityObject() { Url = ParseOutActivityLinks(el), Type = ActivityLink.ActivityLinkType, TypeToObjectMap = TypeToObjectMap };
            }

            ActivityObject activity = CreateCorrectActivityFrom(el, null, TypeToObjectMap, null, null);

            if (activity is ICustomParser)
            {
                (activity as ICustomParser).PerformCustomParsing(el);
            }

            if (activity is IParsesChildObject)
            {
                (activity as IParsesChildObject).PerformCustomObjectParsing(el, ParseActivityObject);
            }

            if (activity is IParsesChildObjects)
            {
                (activity as IParsesChildObjects).PerformCustomObjectParsing(el, ParseActivityObjects);
            }

            if (activity is IParsesChildObjectOrLinks)
            {
                (activity as IParsesChildObjectOrLinks).PerformCustomObjectOrLinkParsing(el, ParseActivityObjectOrLink);
            }

            if (activity is IParsesChildLinks)
            {
                (activity as IParsesChildLinks).PerformCustomLinkParsing(el, ParseOutActivityLinks);
            }

            if (activity is IParsesChildObjectExtensions)
            {
                (activity as IParsesChildObjectExtensions).PerformCustomExtendedObjectParsing(el, ParseOutExtensions);
            }

            return activity;
        }

        internal static ActivityObject ParseActivityObject(JsonElement el, IActivityObject parent)
        {
            if (el.ValueKind == JsonValueKind.String)
            {
                // TODO this should be a reference link
                return new ActivityObject() { Url = ParseOutActivityLinks(el), Type = ActivityLink.ActivityLinkType, TypeToObjectMap = (parent as ActivityObject).TypeToObjectMap };
            }

            ActivityObject activity = CreateCorrectActivityFrom(el, parent?.Context, (parent as ActivityObject).TypeToObjectMap, parent?.ExtendedContexts, parent?.ExtendedIds);

            if (activity is ICustomParser)
            {
                (activity as ICustomParser).PerformCustomParsing(el);
            }

            if (activity is IParsesChildObject)
            {
                (activity as IParsesChildObject).PerformCustomObjectParsing(el, ParseActivityObject);
            }

            if (activity is IParsesChildObjects)
            {
                (activity as IParsesChildObjects).PerformCustomObjectParsing(el, ParseActivityObjects);
            }

            if (activity is IParsesChildObjectOrLinks)
            {
                (activity as IParsesChildObjectOrLinks).PerformCustomObjectOrLinkParsing(el, ParseActivityObjectOrLink);
            }

            if (activity is IParsesChildLinks)
            {
                (activity as IParsesChildLinks).PerformCustomLinkParsing(el, ParseOutActivityLinks);
            }

            if (activity is IParsesChildObjectExtensions)
            {
                (activity as IParsesChildObjectExtensions).PerformCustomExtendedObjectParsing(el, ParseOutExtensions);
            }

            return activity;
        }


        internal static string GetActivityType(JsonElement typeProperty, Dictionary<string, Func<ActivityObject>> TypeToObjectMap, out IList<string> extendedTypes)
        {
            extendedTypes = new List<string>();
            if (typeProperty.ValueKind == JsonValueKind.Undefined || typeProperty.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            if (typeProperty.ValueKind == JsonValueKind.String)
            {
                string typeVal = typeProperty.GetString();
                if (TypeToObjectMap.ContainsKey(typeVal))
                    return typeVal;
                else
                    extendedTypes.Add(typeVal);

                return typeVal;
            }

            if (typeProperty.ValueKind == JsonValueKind.Array)
            {
                List<string> types = new List<string>();

                foreach (var t in typeProperty.EnumerateArray())
                {
                    if (t.ValueKind != JsonValueKind.String)
                        throw new InvalidTypeDefinitionException(t.GetString());

                    types.Add(t.GetString());
                }

                var knownTypes = types.Intersect(TypeToObjectMap.Keys).ToArray();

                if (!knownTypes.Any())
                {
                    extendedTypes = types;
                    return types.First();
                }

                types.Remove(knownTypes.First());
                extendedTypes = types;
                return knownTypes.First();
            }

            throw new InvalidTypeDefinitionException(typeProperty.GetString());
        }

        private static ActivityObject CreateCorrectActivityFrom(JsonElement el, Uri context, Dictionary<string, Func<ActivityObject>> TypeToObjectMap, IDictionary<string, string> extendedContexts, IDictionary<string, CompactIriID> extendedIds)
        {
            string typeString = el.ContainsElement("type") ? GetActivityType(el.GetProperty("type"), TypeToObjectMap, out _) : null;

            ActivityObject activity = null;

            if (typeString != null && typeString.Equals(Collection.CollectionType))
            {
                if (el.ContainsElement("partOf") || el.ContainsElement("next") || el.ContainsElement("prev"))
                {
                    activity = TypeToObjectMap[CollectionPage.CollectionPageType]();
                }
            }
            else if (typeString != null && typeString.Equals(Collection.OrderedCollectionType))
            {
                if (el.ContainsElement("partOf") || el.ContainsElement("next") || el.ContainsElement("prev"))
                {
                    activity = TypeToObjectMap[CollectionPage.OrderedCollectionPageType]();
                }
            }

            if (activity == null && typeString == ImageObject.ImageType && el.ContainsElement("width") || el.ContainsElement("height"))
            {
                activity = TypeToObjectMap[Icon.IconType]();
            }

            if (activity == null && !String.IsNullOrEmpty(typeString) && TypeToObjectMap.ContainsKey(typeString))
            {
                activity = TypeToObjectMap[typeString]();
            }
            else if (activity == null)
            {
                bool isActivity = el.ContainsElement("actor");
                bool isIntransitiveActivity = isActivity ? el.ContainsElement("object") : false;

                if (!isActivity)
                {
                    activity = new ActivityObject();
                }
                else if (isIntransitiveActivity)
                {
                    activity = new IntransitiveActivity();
                }
                else if (!isIntransitiveActivity)
                {
                    activity = new Activity();
                }
            }

            activity.Context = context;
            activity.ExtendedContexts = extendedContexts;
            activity.ExtendedIds = extendedIds;
            activity.TypeToObjectMap = TypeToObjectMap;
            return activity;
        }
    }
}
