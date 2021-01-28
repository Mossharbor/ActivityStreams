using System;
using System.Text.Json;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// this builds Activities
    /// </summary>
    public class ActivityBuilder
    {
        private static JsonDocumentOptions options = new JsonDocumentOptions
        {
            AllowTrailingCommas = true
        };

        /// <summary>
        /// This is the function that modifies the Activity we are trying to build
        /// </summary>
        private Func<Activity, IActivityObject> fn = null;

        public ActivityBuilder FromJson(string json)
        {
            this.fn = (ignored) =>
            {
                ActivityObject activity = null;

                using (JsonDocument document = JsonDocument.Parse(json, options))
                {
                    if (IsLinkElment(document.RootElement))
                        throw new NotSupportedException("We dont support links being the root.  the root must be an activity");

                    activity = ParseActivityObject(document.RootElement);
                }

                return activity;
            };

            return this;
        }

        private static bool IsLinkElment(JsonElement el)
        {
            var typeElement = el.GetPropertyOrDefault("type");
            if (typeElement.ValueKind == JsonValueKind.Undefined || typeElement.ValueKind == JsonValueKind.Null)
                return false;
            return typeElement.ValueEquals("Link") || typeElement.ValueEquals("Mention");
        }

        private static ActivityObject ParseActivityObject(JsonElement el)
        {
            ActivityObject activity = new ActivityObject();
            var typeElement = el.GetStringOrDefault("type");
            var idElement = el.GetUriOrDefault("id");
            var summary = el.GetStringOrDefault("summary");
            var context = el.GetUriOrDefault("@context");

            activity.Id = idElement;
            activity.Summary = summary;
            activity.Context = context;
            activity.Type = typeElement;

            return activity;
        }

        /// <summary>
        /// This function composes the builder function
        /// </summary>
        /// <typeparam name="TA">input type to f1</typeparam>
        /// <typeparam name="TB">output type to f1 and input type to f2</typeparam>
        /// <typeparam name="TC">output type of f2</typeparam>
        /// <param name="f1">first function in the chain to call</param>
        /// <param name="f2">second function in the chain to call</param>
        /// <returns>The function chain</returns>
        private static Func<TA, TC> Compose<TA, TB, TC>(Func<TA, TB> f1, Func<TB, TC> f2)
        {
            return (a) => f2(f1(a));
        }

        /// <summary>
        /// This sets the id for the activity <see cref="IActivityObject.Id"/>
        /// </summary>
        /// <param name="activityId">the id that we are going to use for the activity id if nothing is currently set</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        private ActivityBuilder Id(Uri activityId = null)
        {
            // Create a function chain
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Id = activityId == null ?new Uri("unknown") : activityId;

                return activity;
            });
            return this;
        }

        /// <summary>
        /// Run through the function chain and actually build the IActivity.
        /// </summary>
        /// <returns>the activity that we built</returns>
        public IActivityObject Build()
        {
            IActivityObject ac = this.fn(null);
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
