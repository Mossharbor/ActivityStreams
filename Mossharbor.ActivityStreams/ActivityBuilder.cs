﻿using System;
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
        internal static JsonDocumentOptions options = new JsonDocumentOptions
        {
            AllowTrailingCommas = true
        };

        /// <summary>
        /// This is the function that modifies the Activity we are trying to build
        /// </summary>
        private Func<Activity, IActivityObject> fn = null;

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


        public ActivityBuilder()
        {
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

                    activity = ParseActivityObject(document.RootElement);
                }

                return activity;
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

                    activity = ParseActivityObject(document.RootElement);
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
                ActivityObject activity =  ParseActivityObject(je);

                return activity;
            };

            return this;
        }

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

            foreach(var t in el.EnumerateArray())
            {
                links.Add(ParseOutActivityLink(t));
            }

            return links.ToArray();
        }

        private static ActivityObject ParseActivityObject(JsonElement el)
        {
            if (el.ValueKind == JsonValueKind.String)
            {
                return new Activity() { Url = ParseOutActivityLinks(el), Type = ActivityLink.ActivityLinkType };
            }

            ActivityObject activity = CreateCorrectActivityFrom(el);

            if (activity is ICustomParser)
            {
                (activity as ICustomParser).PerformCustomParsing(el);
            }

            if (activity is IParsesChildObjects)
            {
                (activity as IParsesChildObjects).PerformCustomObjectParsing(el, ParseActivityObject);
            }

            if (activity is IParsesChildObjectOrLinks)
            {
                (activity as IParsesChildObjectOrLinks).PerformCustomObjectOrLinkParsing(el, ParseActivityObjectOrLink);
            }

            if (activity is IParsesChildLinks)
            {
                (activity as IParsesChildLinks).PerformCustomLinkParsing(el, ParseOutActivityLink);
            }

            if (el.TryGetProperty("attributedTo", out JsonElement attributeTo))
            {
                activity.AttributedTo = ParseActivityObjectOrLink(attributeTo);
            }

            if (el.TryGetProperty("location", out JsonElement localEl))
            {
                (activity as IntransitiveActivity).Location = ParseActivityObject(localEl) as PlaceObject;
            }

            if (el.TryGetProperty("url", out JsonElement urlEl))
            {
                activity.Url = ParseOutActivityLinks(urlEl);
            }

            if (el.TryGetProperty("attachment", out JsonElement attachmentEl))
            {
                activity.Attachment = ParseActivityObjectOrLink(attachmentEl);
            }

            if (el.TryGetProperty("audience", out JsonElement audienceEl))
            {
                activity.Audience = ParseActivityObjectOrLink(audienceEl);
            }

            if (el.ContainsElement("bcc"))
            {
                activity.bcc = ParseActivityObjectOrLink(el.GetProperty("bcc"));
            }

            if (activity is Collection)
            {
                (activity as Collection).TotalItems = (uint)el.GetLongOrDefault("totalItems");
                if (el.ContainsElement("items"))
                {
                    (activity as Collection).Items = ParseActivityObjectOrLink(el.GetProperty("items"));
                }
                else if (el.ContainsElement("orderedItems"))
                {
                    (activity as Collection).OrderedItems = ParseActivityObjectOrLink(el.GetProperty("orderedItems"));
                }

                if (activity is CollectionPage)
                {
                    if (el.ContainsElement("partOf"))
                        (activity as CollectionPage).PartOf = ParseActivityObjectOrLink(el.GetProperty("partOf")).FirstOrDefault();
                    if (el.ContainsElement("next"))
                        (activity as CollectionPage).Next = ParseOutActivityLink(el.GetProperty("next"));
                    if (el.ContainsElement("prev"))
                        (activity as CollectionPage).Prev = ParseOutActivityLink(el.GetProperty("prev"));
                }
            }

            return activity;
        }

        private static ActivityObject CreateCorrectActivityFrom(JsonElement el)
        {
            string typeString = el.GetStringOrDefault("type");

            ActivityObject activity = null;

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
