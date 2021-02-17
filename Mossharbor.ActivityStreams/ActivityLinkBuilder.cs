using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;

namespace Mossharbor.ActivityStreams
{
    public class ActivityLinkBuilder : BuilderBase
    {
        public ActivityLinkBuilder()
        { }

        public ActivityLinkBuilder(IActivityLink link)
        {
            this.fn = (ignored) =>
            {
                return link;
            };
        }

        /// <summary>
        /// this is the mapping from a type to an implementation
        /// </summary>
        protected static Dictionary<string, Func<ActivityLink>> TypeToObjectMap = new Dictionary<string, Func<ActivityLink>>()
        {
            {ActivityLink.ActivityLinkType, () => new ActivityLink() },
            {MentionLink.LinkType, () => new MentionLink() },
        };

        internal static JsonDocumentOptions options = new JsonDocumentOptions
        {
            AllowTrailingCommas = true
        };

        /// <summary>
        /// This is the function that modifies the Activity we are trying to build
        /// </summary>
        private Func<Activity, IActivityLink> fn = null;

        /// <summary>
        /// this function builds an activity from the json text read from the stream provided
        /// </summary>
        /// <param name="jsonStream">the json stream </param>
        /// <returns>the activity builder</returns>
        /// <remarks> you must call Build to get and actual activity back</remarks>
        public ActivityLinkBuilder FromJson(System.IO.Stream jsonStream)
        {
            this.fn = (ignored) =>
            {
                ActivityLink activity = null;

                using (JsonDocument document = JsonDocument.Parse(jsonStream, options))
                {
                    if (!IsLinkElment(document.RootElement))
                        throw new NotSupportedException("The stream did not contain a valid activity link");

                    activity = ParseLink(document.RootElement);
                }

                return activity;
            };

            return this;
        }

        /// <summary>
        /// this function builds an activity from the json text read from the stream provided
        /// </summary>
        /// <param name="jsonElement">the json element </param>
        /// <returns>the activity builder</returns>
        /// <remarks> you must call Build to get and actual activity back</remarks>
        public ActivityLinkBuilder FromJsonElement(JsonElement jsonElement)
        {
            this.fn = (ignored) =>
            {
                ActivityLink activity = null;

                if (!IsLinkElment(jsonElement))
                    throw new NotSupportedException("This is not a valid link element.");

                activity = ParseLink(jsonElement);

                return activity;
            };

            return this;
        }

        private static string[] ParseOutRelationShips(JsonElement el)
        {
            if (el.ValueKind != JsonValueKind.Array)
            {
                return new string[] { el.GetString() };
            }

            List<string> links = new List<string>();

            foreach (var t in el.EnumerateArray())
            {
                links.Add(t.ToString());
            }

            return links.ToArray();
        }

        private static ActivityLink ParseLink(JsonElement el)
        {
            var type = el.GetStringOrDefault("type");
            ActivityLink link = new ActivityLink();

            if (!String.IsNullOrEmpty(type) && TypeToObjectMap.ContainsKey(type))
            {
                link = TypeToObjectMap[type]();
            }

            if (el.ValueKind == JsonValueKind.String)
            {
                link.Href = el.GetString();
                return link;
            }

            var context = el.GetUriOrDefault("@context");
            var hrefElement = el.GetStringOrDefault("href");
            var hrefLang = el.GetStringOrDefault("hreflang");
            var mediaType = el.GetStringOrDefault("mediaType");
            var height = el.GetLongOrDefault("height");
            var width = el.GetLongOrDefault("width");
            var name = el.GetStringOrDefault("name");
            var url = el.GetUriOrDefault("url");

            link.Context = context;
            link.Href = hrefElement;
            link.HrefLang = hrefLang;
            link.Type = type;
            link.MediaType = mediaType;
            link.Height = height;
            link.Width = width;
            link.Name = name;
            link.Rel = el.ContainsElement("rel") ? ParseOutRelationShips(el.GetProperty("rel")) : null;
            link.Url = url;

            return link;
        }

        /// <summary>
        /// this function builds an activity from the json string provided
        /// </summary>
        /// <param name="json">the json string</param>
        /// <returns>the activity builder</returns>
        /// <remarks> you must call Build to get and actual activity back</remarks>
        public ActivityLinkBuilder FromJson(string json)
        {
            this.fn = (ignored) =>
            {
                ActivityLink activity = null;

                using (JsonDocument document = JsonDocument.Parse(json, options))
                {
                    if (IsLinkElment(document.RootElement))
                        throw new NotSupportedException("This is not a valid link element.");

                    activity = ParseLink(document.RootElement);
                }

                return activity;
            };

            return this;
        }

        internal static bool IsLinkElment(JsonElement el)
        {
            if (el.ValueKind == JsonValueKind.String)
                return true;

            // href is required by the spec.
            var hrefElement = el.GetPropertyOrDefault("href");
            if (hrefElement.ValueKind == JsonValueKind.String)
                return true;
            else
                return false;
        }

        public ActivityLinkBuilder Href(string href)
        {
            this.fn = Compose(this.fn, (link) =>
            {
                link.Href = href;
                return link;
            });
            return this;
        }

        public ActivityLinkBuilder Hreflang(string hreflang)
        {
            this.fn = Compose(this.fn, (link) =>
            {
                link.HrefLang = hreflang;
                return link;
            });
            return this;
        }

        public ActivityLinkBuilder Name(string name)
        {
            this.fn = Compose(this.fn, (link) =>
            {
                link.Name = name;
                return link;
            });
            return this;
        }

        public ActivityLinkBuilder MediaType(string mediaType)
        {
            this.fn = Compose(this.fn, (link) =>
            {
                link.MediaType = mediaType;
                return link;
            });
            return this;
        }

        public ActivityLinkBuilder Width(long width)
        {
            this.fn = Compose(this.fn, (link) =>
            {
                link.Width = width;
                return link;
            });
            return this;
        }

        public ActivityLinkBuilder Height(long height)
        {
            this.fn = Compose(this.fn, (link) =>
            {
                link.Height = height;
                return link;
            });
            return this;
        }

        public ActivityLinkBuilder Rel(string[] rel)
        {
            this.fn = Compose(this.fn, (link) =>
            {
                link.Rel = rel;
                return link;
            });
            return this;
        }

        public ActivityLinkBuilder Uri(string url)
        {
            this.fn = Compose(this.fn, (link) =>
            {
                link.Url = new Uri(url);
                return link;
            });
            return this;
        }

        public ActivityLinkBuilder Mention(Action<ActivityLinkBuilder> modifier = null)
        {

            this.fn = (ignored) => CreateStreamsType(modifier, ActivityLinkBuilder.TypeToObjectMap[MentionLink.LinkType]);
            return this;
        }
        protected T CreateStreamsType<T>(Action<ActivityLinkBuilder> modifier, Func<T> activityInstanitator) where T : ActivityLink
        {
            T activityLink = activityInstanitator();
            ActivityLinkBuilder qBuilder = new ActivityLinkBuilder(activityLink);
            if (modifier != null)
                modifier(qBuilder);
            return (T)qBuilder.Build();
        }

        /// <summary>
        /// This sets the context for the activity <see cref="IActivityLink.Context"/>
        /// </summary>
        /// <param name="context">the context of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityLinkBuilder Context(string context = "https://www.w3.org/ns/activitystreams")
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Context = new Uri(context);
                return activity;
            });
            return this;
        }

        /// <summary>
        /// Run through the function chain and actually build the IActivity.
        /// </summary>
        /// <returns>the activity that we built</returns>
        public IActivityLink Build()
        {
            IActivityLink link = this.fn(null);
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // catch and new exceptions to the protocol during developement and testing
                // every activity we build or modify should meet the spec
                // string violation = null;
                // TODO System.Diagnostics.Debug.Assert(ValidateActivityMeetsSpec(ac, serverGeneratedActivity, out violation));
            }
#endif
            return link;
        }
    }
}
