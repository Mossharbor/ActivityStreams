using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// this builds Activities
    /// </summary>
    public class ActivityBuilder
    {
        public enum BuildValidationLevel { Off, Basic, Strict };

        internal static JsonDocumentOptions options = new JsonDocumentOptions
        {
            AllowTrailingCommas = true
        };

        /// <summary>
        /// This function composes the builder function
        /// </summary>
        /// <typeparam name="TA">input type to f1</typeparam>
        /// <typeparam name="TB">output type to f1 and input type to f2</typeparam>
        /// <typeparam name="TC">output type of f2</typeparam>
        /// <param name="f1">first function in the chain to call</param>
        /// <param name="f2">second function in the chain to call</param>
        /// <returns>The function chain</returns>
        protected static Func<TA, TC> Compose<TA, TB, TC>(Func<TA, TB> f1, Func<TB, TC> f2)
        {
            return (a) => f2(f1(a));
        }

        /// <summary>
        /// This is the function that modifies the Activity we are trying to build
        /// </summary>
        protected Func<IActivityObject, IActivityObject> fn = null;

        public ActivityBuilder()
        {
        }


        public ActivityBuilder(IActivityObject activity)
        {
            this.fn = (ignored) =>
            {
                return activity;
            };
        }

        /// <summary>
        /// this function builds an activity from the json text read from the stream provided
        /// </summary>
        /// <param name="jsonStream">the json stream </param>
        /// <returns>the activity builder</returns>
        /// <remarks> you must call Build to get and actual activity back</remarks>
        public ActivityBuilder FromJson(System.IO.Stream jsonStream)
        {
            this.fn = (ignored) =>
            {
                ActivityObject activity = null;

                using (JsonDocument document = JsonDocument.Parse(jsonStream, options))
                {
                    if (ActivityLinkBuilder.IsLinkElment(document.RootElement))
                        throw new NotSupportedException("We dont support links being the root.  the root must be an activity");

                    activity = ActivityStreamsParser.ParseActivityObject(document.RootElement);
                }

                return activity;
            };

            return this;
        }

        public ActivityBuilder Question(string question, QuestionBuilder.AnswerType answerType, Action<QuestionBuilder> modifier)
        {
            this.fn = (ignored) =>
            {
                QuestionActivity activity = new QuestionActivity();
                QuestionBuilder qBuilder = new QuestionBuilder(answerType, activity);
                qBuilder.Name(question);
                modifier(qBuilder);
                return qBuilder.Build();
            };

            return this;
        }

        public ActivityBuilder Note(string content, Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                NoteObject activity = new NoteObject();
                ActivityBuilder qBuilder = new ActivityBuilder(activity);
                qBuilder.Content(content);
                modifier(qBuilder);
                return qBuilder.Build();
            };

            return this;
        }

        /// <summary>
        /// this function builds an activity from the json string provided
        /// </summary>
        /// <param name="json">the json string</param>
        /// <returns>the activity builder</returns>
        /// <remarks> you must call Build to get and actual activity back</remarks>
        public ActivityBuilder FromJson(string json)
        {
            this.fn = (ignored) =>
            {
                ActivityObject activity = null;

                using (JsonDocument document = JsonDocument.Parse(json, options))
                {
                    if (ActivityLinkBuilder.IsLinkElment(document.RootElement))
                        throw new NotSupportedException("We dont support links being the root.  the root must be an activity");

                    activity = ActivityStreamsParser.ParseActivityObject(document.RootElement);
                }

                return activity;
            };

            return this;
        }

        /// <summary>
        /// this parses an object from a JsonElement
        /// </summary>
        /// <param name="je">the json element</param>
        /// <returns>an ActivityBuilder</returns>
        public ActivityBuilder FromJsonElement(JsonElement je)
        {
            this.fn = (ignored) =>
            {
                ActivityObject activity = ActivityStreamsParser.ParseActivityObject(je);

                return activity;
            };

            return this;
        }

        /// <summary>
        /// This sets the id for the activity <see cref="IActivityObject.Id"/>
        /// </summary>
        /// <param name="activityId">the id that we are going to use for the activity id if nothing is currently set</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder Id(Uri activityId)
        {   
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Id = activityId;
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the name for the activity <see cref="IActivityObject.Name"/>
        /// </summary>
        /// <param name="activityName">the name of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder Name(string activityName)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Name = activityName;
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the context for the activity <see cref="IActivityObject.Context"/>
        /// </summary>
        /// <param name="context">the context of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder Context(string context = "https://www.w3.org/ns/activitystreams")
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Context = new Uri(context);
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.Summary"/> for the activity
        /// </summary>
        /// <param name="summary">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder Summary(string summary)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Summary = summary;
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.Content"/> for the activity
        /// </summary>
        /// <param name="content">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder Content(string content)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Content = content;
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.MediaType"/> for the activity
        /// </summary>
        /// <param name="mediaType">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder MediaType(string mediaType)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.MediaType = mediaType;
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.StartTime"/> for the activity
        /// </summary>
        /// <param name="start">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder StartTime(DateTime? start)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.StartTime = start;
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.EndTime"/> for the activity
        /// </summary>
        /// <param name="end">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder EndTime(DateTime? end)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.EndTime = end;
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.Published"/> for the activity
        /// </summary>
        /// <param name="published">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder Published(DateTime? published)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Published = published;
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.Updated"/> for the activity
        /// </summary>
        /// <param name="updated">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder Updated(DateTime? updated)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Updated = updated;
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.Duration"/> for the activity
        /// </summary>
        /// <param name="duration">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder Duration(TimeSpan? duration)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Duration = duration;
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.Type"/> for the activity
        /// </summary>
        /// <param name="type">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder Type(string type)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Type = type;
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.AttributedTo"/>
        /// </summary>
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder AttributedTo(Action<ActivityBuilder> modifier)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.AttributedTo = ExpandArray(activity.AttributedTo, out int index);
                activity.AttributedTo[index].Obj = RunModifierBuilder(modifier).Build();

                return activity;
            });
            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Attachment"/>
        /// </summary>
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityBuilder Attachment(Action<ActivityBuilder> modifier)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Attachment = ExpandArray(activity.AttributedTo, out int index);
                activity.Attachment[index].Obj = RunModifierBuilder(modifier).Build();

                return activity;
            });
            return this;
        }

        private static ActivityBuilder RunModifierBuilder(Action<ActivityBuilder> modifier)
        {
            Activity ac = new Activity();
            ActivityBuilder abuilder = new ActivityBuilder(ac);
            modifier(abuilder);
            return abuilder;
        }

        private static IActivityObjectOrLink[] ExpandArray(IActivityObjectOrLink[] array,out int lastIndex)
        {
            lastIndex = 0;
            if (array == null)
            {
                array = new IActivityObjectOrLink[1];
            }
            else
            {
                var temp = array;
                array = new IActivityObjectOrLink[array.Length + 1];
                Array.Copy(temp, array, temp.Length);
                lastIndex = temp.Length;
            }

            array[lastIndex] = new ActivityObjectOrLink();

            return array;
        }

        /// <summary>
        /// Run through the function chain and actually build the IActivity.
        /// </summary>
        /// <returns>the activity that we built</returns>
        public virtual IActivityObject Build(BuildValidationLevel validationLevel = BuildValidationLevel.Off)
        {
            IActivityObject ac = this.fn(null);
            return ValidateActivity(ac, validationLevel);
        }

        protected IActivityObject ValidateActivity(IActivityObject ac, BuildValidationLevel validationLevel = BuildValidationLevel.Off)
        {
            List<string> violations = new List<string>();

            if (validationLevel == BuildValidationLevel.Off)
                return ac;

            if (validationLevel >= BuildValidationLevel.Basic)
            {
                if (ac.Id == null)
                    violations.Add("id is null");

                if (string.IsNullOrEmpty(ac.Type))
                    violations.Add("type is null or empty");

                if (ac.Context == null)
                    violations.Add("context is null or empty");

                if (violations.Any())
                    throw new BuildViolationsException(violations);

                return ac;
            }

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // catch and new exceptions to the protocol during developement and testing
                // every activity we build or modify should meet the spec
                // string violation = null;
                // TODO System.Diagnostics.Debug.Assert(ValidateActivityMeetsSpec(ac, serverGeneratedActivity, out violation));
            }
#endif
            if (violations.Any())
                throw new BuildViolationsException(violations);

            return ac;
        }
    }
}
