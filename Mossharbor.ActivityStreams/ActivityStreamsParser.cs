using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Mossharbor.ActivityStreams
{
    public class ActivityStreamsParser
    {
        /// <summary>
        /// this is the mapping from a type to an implementation
        /// </summary>
        protected static Dictionary<string, Func<ActivityObject>> TypeToObjectMap = new Dictionary<string, Func<ActivityObject>>()
        {
            {Activity.ActivityType, () => new Activity() },
            {ServiceActor.ServiceActorType, () => new ServiceActor() },
            {ApplicationActor.ApplicationActorType, () => new ApplicationActor()},
            {GroupActor.GroupActorType, () => new GroupActor()},
            {OrganizationActor.OrganizationActorType, () => new OrganizationActor()},
            {PersonActor.PersonActorType, () => new PersonActor()},
            {Collection.OrderedCollectionType, () => new Collection()},
            {Collection.CollectionType, () => new Collection()},
            {ArticleObject.ArticleType, () => new ArticleObject()},
            {AudioObject.AudioType, () => new AudioObject()},
            {DocumentObject.DocumentType, () => new DocumentObject()},
            {EventObject.EventType, () => new EventObject()},
            {Icon.IconType, () => new Icon()},
            {ImageObject.ImageType, () => new ImageObject()},
            {NoteObject.NoteType, () => new NoteObject()},
            {PageObject.PageType, () => new PageObject()},
            {PlaceObject.PlaceType, () => new PlaceObject()},
            {ProfileObject.ProfileType, () => new ProfileObject()},
            {RelationshipObject.RelationshipType, () => new RelationshipObject()},
            {TombstoneObject.TombstoneType, () => new TombstoneObject()},
            {VideoObject.VideoType, () => new VideoObject()},
            {AcceptActivity.AcceptActivtyTypeString, () => new AcceptActivity()},
            {AddActivity.TypeString, () => new AddActivity()},
            {AnnounceActivity.TypeString, () => new AnnounceActivity()},
            {ArriveActivity.TypeString, () => new ArriveActivity()},
            {BlockActivity.TypeString, () => new BlockActivity()},
            {CreateActivity.TypeString, () => new CreateActivity()},
            {DeleteActivity.TypeString, () => new DeleteActivity()},
            {DislikeActivity.TypeString, () => new DislikeActivity()},
            {FlagActivity.TypeString, () => new FlagActivity()},
            {FollowActivity.TypeString, () => new FollowActivity()},
            {IgnoreActivity.TypeString, () => new IgnoreActivity()},
            {InviteActivity.TypeString, () => new InviteActivity()},
            {JoinActivity.TypeString, () => new JoinActivity()},
            {LeaveActivity.TypeString, () => new LeaveActivity()},
            {LikeActivity.TypeString, () => new LikeActivity()},
            {ListenActivity.TypeString, () => new ListenActivity()},
            {MoveActivity.TypeString, () => new MoveActivity()},
            {OfferActivity.TypeString, () => new OfferActivity()},
            {QuestionActivity.TypeString, () => new QuestionActivity()},
            {ReadActivity.TypeString, () => new ReadActivity()},
            {RejectActivity.RejectActivityTypeString, () => new RejectActivity()},
            {RemoveActivity.TypeString, () => new RemoveActivity()},
            {TentativeRejectActivity.TypeString, () => new TentativeRejectActivity()},
            {TentativeAcceptActivity.TypeString, () => new TentativeAcceptActivity()},
            {TravelActivity.TypeString, () => new TravelActivity()},
            {UndoActivity.TypeString, () => new UndoActivity()},
            {UpdateActivity.TypeString, () => new UpdateActivity()},
            {ViewActivity.TypeString, () => new ViewActivity()},
            {CollectionPage.CollectionPageType, () => new CollectionPage()},
            {CollectionPage.OrderedCollectionPageType, () => new CollectionPage()}
        };

        private static IActivityObjectOrLink[] ParseActivityObjectOrLink(JsonElement el)
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
                    aOrI.Obj = ParseActivityObject(toParse);
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

        private static IActivityObject[] ParseActivityObjects(JsonElement el)
        {
            if (el.ValueKind != JsonValueKind.Array)
            {
                return new IActivityObject[] { ParseActivityObject(el) };
            }

            List<IActivityObject> objects = new List<IActivityObject>();

            foreach (var t in el.EnumerateArray())
            {
                objects.Add(ParseActivityObject(t));
            }

            return objects.ToArray();
        }

        internal static ActivityObject ParseActivityObject(JsonElement el)
        {
            if (el.ValueKind == JsonValueKind.String)
            {
                // TODO this should be a reference link
                return new Activity() { Url = ParseOutActivityLinks(el), Type = ActivityLink.ActivityLinkType };
            }

            ActivityObject activity = CreateCorrectActivityFrom(el);

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

            return activity;
        }

        internal static string GetActivityType(JsonElement typeProperty)
        {
            if (typeProperty.ValueKind == JsonValueKind.Undefined || typeProperty.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            if (typeProperty.ValueKind == JsonValueKind.String)
            {
                return typeProperty.GetString();
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

                var knownTypes = types.Union(TypeToObjectMap.Keys);

                if (!knownTypes.Any())
                {
                    return types.First();
                }

                return knownTypes.First();
            }

            throw new InvalidTypeDefinitionException(typeProperty.GetString());
        }

        private static ActivityObject CreateCorrectActivityFrom(JsonElement el)
        {
            string typeString = el.ContainsElement("type") ? GetActivityType(el.GetProperty("type")) : null;

            ActivityObject activity = null;

            if (typeString != null && typeString.Equals(Collection.CollectionType))
            {
                if (el.ContainsElement("partOf") || el.ContainsElement("next") || el.ContainsElement("prev"))
                {
                    return TypeToObjectMap[CollectionPage.CollectionPageType]();
                }
            }
            else if (typeString != null && typeString.Equals(Collection.OrderedCollectionType))
            {
                if (el.ContainsElement("partOf") || el.ContainsElement("next") || el.ContainsElement("prev"))
                {
                    return TypeToObjectMap[CollectionPage.OrderedCollectionPageType]();
                }
            }

            if (typeString == ImageObject.ImageType && el.ContainsElement("width") || el.ContainsElement("height"))
            {
                return TypeToObjectMap[Icon.IconType]();
            }

            if (!String.IsNullOrEmpty(typeString) && TypeToObjectMap.ContainsKey(typeString))
            {
                return TypeToObjectMap[typeString]();
            }
            else
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

            return activity;
        }
    }
}
