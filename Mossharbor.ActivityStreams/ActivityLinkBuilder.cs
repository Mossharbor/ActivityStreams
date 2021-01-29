using System;
using System.Net.Sockets;
using System.Text.Json;

namespace Mossharbor.ActivityStreams
{
    public class ActivityLinkBuilder
    {
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

        private static ActivityLink ParseLink(JsonElement el)
        {
            ActivityLink link = new ActivityLink();

            if (el.ValueKind == JsonValueKind.String)
            {
                link.Href = el.GetString();
                return link;
            }

            var hrefElement = el.GetStringOrDefault("href");
            var hrefLang = el.GetStringOrDefault("hreflang");
            var type = el.GetStringOrDefault("type");
            var mediaType = el.GetStringOrDefault("mediaType");
            var height = el.GetLongOrDefault("height");
            var width = el.GetLongOrDefault("width");
            var rel = el.GetStringOrDefault("rel");
            var name = el.GetStringOrDefault("name");
            var url = el.GetUriOrDefault("url");

            link.Href = hrefElement;
            link.HrefLang = hrefLang;
            link.Type = type;
            link.MediaType = mediaType;
            link.Height = height;
            link.Width = width;
            link.Name = name;
            link.Rel = new string[] { rel };
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

        /// <summary>
        /// Run through the function chain and actually build the IActivity.
        /// </summary>
        /// <returns>the activity that we built</returns>
        public IActivityLink Build()
        {
            IActivityLink ac = this.fn(null);
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // catch and new exceptions to the protocol during developement and testing
                // every activity we build or modify should meet the spec
                string violation = null;
                // TODO System.Diagnostics.Debug.Assert(ValidateActivityMeetsSpec(ac, serverGeneratedActivity, out violation));
            }
#endif
            return ac;
        }
    }
}
