﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// this builds Activities
    /// </summary>
    public class ActivityObjectBuilder : BuilderBase
    {
        public enum BuildValidationLevel { Off, Basic, Strict };

        internal static JsonDocumentOptions options = new JsonDocumentOptions
        {
            AllowTrailingCommas = true
        };


        /// <summary>
        /// This is the function that modifies the Activity we are trying to build
        /// </summary>
        protected Func<IActivityObject, IActivityObject> fn = null;

        public ActivityObjectBuilder()
        {
        }

        public ActivityObjectBuilder(IActivityObject activity)
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
        public ActivityObjectBuilder FromJson(System.IO.Stream jsonStream)
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


        /// <summary>
        /// this function builds an activity from the json string provided
        /// </summary>
        /// <param name="json">the json string</param>
        /// <returns>the activity builder</returns>
        /// <remarks> you must call Build to get and actual activity back</remarks>
        public ActivityObjectBuilder FromJson(string json)
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
        public ActivityObjectBuilder FromJsonElement(JsonElement je)
        {
            this.fn = (ignored) =>
            {
                ActivityObject activity = ActivityStreamsParser.ParseActivityObject(je);

                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Question(string question, QuestionBuilder.AnswerType answerType, Action<QuestionBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                QuestionActivity activity = new QuestionActivity();
                QuestionBuilder qBuilder = new QuestionBuilder(answerType, activity);
                qBuilder.Name(question);
                if (modifier != null)
                    modifier(qBuilder);
                return qBuilder.Build();
            };

            return this;
        }

        public ActivityObjectBuilder Article(string content, Action<ActivityObjectBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                ArticleObject activity = (ArticleObject)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[ArticleObject.ArticleType]);
                activity.Content = content;
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Audio(Action<ActivityObjectBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                AudioObject activity = (AudioObject)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[AudioObject.AudioType]);
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Document(string content, Action<ActivityObjectBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                DocumentObject activity = (DocumentObject)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[DocumentObject.DocumentType]);
                activity.Content = content;
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Event(string content, Action<ActivityObjectBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                EventObject activity = (EventObject)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[EventObject.EventType]);
                activity.Content = content;
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Image(Action<ActivityObjectBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                ImageObject activity = (ImageObject)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[ImageObject.ImageType]);
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Note(string content, Action<ActivityObjectBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                NoteObject activity = (NoteObject)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[NoteObject.NoteType]);
                activity.Content = content;
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Page(string content, Action<ActivityObjectBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                PageObject activity = (PageObject)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[PageObject.PageType]);
                activity.Content = content;
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Place(double? longitude, double? latitude, double? altitude, double? accuracy, double? radius, string units, Action<ActivityObjectBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                PlaceObject activity = (PlaceObject)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[PlaceObject.PlaceType]);

                if (latitude != null)
                    activity.Latitude = latitude.Value;
                if (longitude != null)
                    activity.Longitude = longitude.Value;
                if (altitude != null)
                    activity.Altitude = altitude.Value;
                if (accuracy != null)
                    activity.Accuracy = accuracy.Value;
                if (radius != null)
                    activity.Radius = radius.Value;
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Profile(Action<ProfileBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                ProfileObject activity = new ProfileObject();
                ProfileBuilder qBuilder = new ProfileBuilder(activity);
                if (modifier != null)
                    modifier(qBuilder);
                return qBuilder.Build();
            };

            return this;
        }

        public ActivityObjectBuilder Relationship(string relationship, Action<RelationshipBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                RelationshipObject activity = new RelationshipObject();
                activity.Relationship = relationship;
                RelationshipBuilder qBuilder = new RelationshipBuilder(activity);
                if (modifier != null)
                    modifier(qBuilder);
                return qBuilder.Build();
            };

            return this;
        }

        public ActivityObjectBuilder Tombstone(string formerType, DateTime? deleted, Action<ActivityObjectBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                TombstoneObject activity = (TombstoneObject)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[TombstoneObject.TombstoneType]);
                activity.Deleted = deleted;
                activity.FormerType = formerType;
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Video(string title, string url, Action<ActivityObjectBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                VideoObject activity = (VideoObject)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[VideoObject.VideoType]);
                activity.Name = title;
                activity.Url = ExpandArray(activity.Url, out int index);
                activity.Url[index] = new ActivityLink();
                activity.Url[index].Href = url;
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Application(Action<ActivityObjectBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                ApplicationActor activity = (ApplicationActor)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[ApplicationActor.ApplicationActorType]);
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Group(Action<ActivityObjectBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                GroupActor activity = (GroupActor)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[GroupActor.GroupActorType]);
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Organization(Action<ActivityObjectBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                OrganizationActor activity = (OrganizationActor)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[OrganizationActor.OrganizationActorType]);
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Person(Action<ActivityObjectBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                PersonActor activity = (PersonActor)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[PersonActor.PersonActorType]);
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Service(Action<ActivityObjectBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                ServiceActor activity = (ServiceActor)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[ServiceActor.ServiceActorType]);
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Activity(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                Activity activity = new Activity();
                ActivityBuilder qBuilder = new ActivityBuilder(activity);
                if (null != modifier)
                    modifier(qBuilder);
                return qBuilder.Build();
            };

            return this;
        }

        public ActivityObjectBuilder Accept(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                AcceptActivity activity = (AcceptActivity)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[AcceptActivity.AcceptActivtyTypeString]);
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Add(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                AddActivity activity = (AddActivity)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[AddActivity.TypeString]);
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Announce(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                AnnounceActivity activity = (AnnounceActivity)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[AnnounceActivity.TypeString]);
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Arrive(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) =>
            {
                ArriveActivity activity = (ArriveActivity)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[ArriveActivity.TypeString]);
                return activity;
            };

            return this;
        }

        public ActivityObjectBuilder Block(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => (BlockActivity)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[BlockActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Create(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => (CreateActivity)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[CreateActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Delete(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => (DeleteActivity)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[DeleteActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Dislike(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => (DislikeActivity)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[DislikeActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Flag(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => (FlagActivity)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[FlagActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Follow(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => (FollowActivity)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[FollowActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Ignore(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => (IgnoreActivity)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[IgnoreActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Invite(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => (InviteActivity)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[InviteActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Join(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => (JoinActivity)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[JoinActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Leave(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => (LeaveActivity)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[LeaveActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Like(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => (LikeActivity)CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[LikeActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Listen(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[ListenActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Move(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[MoveActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Offer(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[OfferActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Read(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[ReadActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Reject(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[RejectActivity.RejectActivityTypeString]);
            return this;
        }

        public ActivityObjectBuilder Remove(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[RemoveActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder TentativeAccept(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[TentativeAcceptActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder TentativeReject(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[TentativeRejectActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Travel(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[TravelActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Undo(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[UndoActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder Update(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[UpdateActivity.TypeString]);
            return this;
        }

        public ActivityObjectBuilder View(Action<ActivityBuilder> modifier = null)
        {
            this.fn = (ignored) => CreateStreamsType(modifier, ActivityStreamsParser.TypeToObjectMap[ViewActivity.TypeString]);
            return this;
        }

        /// <summary>
        /// This sets the id for the activity <see cref="IActivityObject.Id"/>
        /// </summary>
        /// <param name="activityId">the id that we are going to use for the activity id if nothing is currently set</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Id(Uri activityId)
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
        public ActivityObjectBuilder Name(string activityName)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Name = activityName;
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.ContentMap"/> for the activity
        /// </summary>
        /// <param name="culture">The language we are adding</param>
        /// <param name="content">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Name(CultureInfo culture, string content)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                if (activity.NameMap == null)
                    activity.NameMap = new ContentMap(new Dictionary<string, string>());

                activity.NameMap.Add(culture, content);
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.ContentMap"/> for the activity
        /// </summary>
        /// <param name="langaugeCode">The language we are adding</param>
        /// <param name="content">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Name(string langaugeCode, string content)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                if (activity.NameMap == null)
                    activity.NameMap = new ContentMap(new Dictionary<string, string>());

                activity.NameMap.Add(langaugeCode, content);
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the context for the activity <see cref="IActivityObject.Context"/>
        /// </summary>
        /// <param name="context">the context of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Context(string context = "https://www.w3.org/ns/activitystreams")
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
        public ActivityObjectBuilder Summary(string summary)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Summary = summary;
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.Summary"/> for the activity
        /// </summary>
        /// <param name="culture">The language we are adding</param>
        /// <param name="content">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Summary(CultureInfo culture, string content)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                if (activity.SummaryMap == null)
                    activity.SummaryMap = new ContentMap(new Dictionary<string, string>());

                activity.SummaryMap.Add(culture, content);
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.Summary"/> for the activity
        /// </summary>
        /// <param name="langaugeCode">The language we are adding</param>
        /// <param name="content">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Summary(string langaugeCode, string content)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                if (activity.SummaryMap == null)
                    activity.SummaryMap = new ContentMap(new Dictionary<string, string>());

                activity.SummaryMap.Add(langaugeCode, content);
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.Content"/> for the activity
        /// </summary>
        /// <param name="content">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Content(string content)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Content = content;
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.ContentMap"/> for the activity
        /// </summary>
        /// <param name="culture">The language we are adding</param>
        /// <param name="content">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Content(CultureInfo culture, string content)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                if (activity.ContentMap == null)
                    activity.ContentMap = new ContentMap(new Dictionary<string, string>());

                activity.ContentMap.Add(culture, content);
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.ContentMap"/> for the activity
        /// </summary>
        /// <param name="langaugeCode">The language we are adding</param>
        /// <param name="content">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Content(string langaugeCode, string content)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                if (activity.ContentMap == null)
                    activity.ContentMap = new ContentMap(new Dictionary<string, string>());

                activity.ContentMap.Add(langaugeCode, content);
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.MediaType"/> for the activity
        /// </summary>
        /// <param name="mediaType">the content of the activity</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder MediaType(string mediaType)
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
        public ActivityObjectBuilder StartTime(DateTime? start)
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
        public ActivityObjectBuilder EndTime(DateTime? end)
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
        public ActivityObjectBuilder Published(DateTime? published)
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
        public ActivityObjectBuilder Updated(DateTime? updated)
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
        public ActivityObjectBuilder Duration(TimeSpan? duration)
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
        public ActivityObjectBuilder Type(string type)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Type = type;
                return activity;
            });
            return this;
        }

        /// <summary>
        /// This sets the <see cref="IActivityObject.Location"/> for the activity
        /// </summary>
        /// <param name="longitude">the longitute</param>
        /// <param name="latitude">the latitude</param>
        /// <param name="altitude">the altitude</param>
        /// <param name="accuracy">the accuracy</param>
        /// <param name="radius">the radius</param>
        /// <param name="units">the v</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Location(double? longitude, double? latitude,double? altitude,double? accuracy, double? radius, string units, Action<ActivityObjectBuilder> modifier)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Location = new PlaceObject();
                if (latitude != null)
                    activity.Location.Latitude = latitude.Value;
                if (longitude != null)
                    activity.Location.Longitude = longitude.Value;
                if (altitude != null)
                    activity.Location.Altitude = altitude.Value;
                if (accuracy != null)
                    activity.Location.Accuracy = accuracy.Value;
                if (radius != null)
                    activity.Location.Radius = radius.Value;
                ActivityObjectBuilder builder = new ActivityObjectBuilder(activity);
                modifier(builder);

                return builder.Build();
            });
            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.AttributedTo"/>
        /// </summary>
        /// <param name="objectModifier">the action for building objects</param>
        /// <param name="linkModifier">the action for building links</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder AttributedTo(Action<ActivityObjectBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.AttributedTo = ExpandArray(activity.AttributedTo, out int index);
                if (null != objectModifier)
                {
                    activity.AttributedTo[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    activity.AttributedTo[index].Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });
            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Attachment"/>
        /// </summary>
        /// <param name="objectModifier">the action for building objects</param>
        /// <param name="linkModifier">the action for building links</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Attachment(Action<ActivityObjectBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Attachment = ExpandArray(activity.Attachment, out int index);
                if (null != objectModifier)
                {
                    activity.Attachment[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    activity.Attachment[index].Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });
            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Audience"/>
        /// </summary>
        /// <param name="objectModifier">the action for building objects</param>
        /// <param name="linkModifier">the action for building links</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Audience(Action<ActivityObjectBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Audience = ExpandArray(activity.AttributedTo, out int index);
                if (null != objectModifier)
                {
                    activity.Audience[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    activity.Audience[index].Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });
            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.InReplyTo"/>
        /// </summary>
        /// <param name="objectModifier">the action for building objects</param>
        /// <param name="linkModifier">the action for building links</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder InReplyTo(Action<ActivityObjectBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.InReplyTo = ExpandArray(activity.AttributedTo, out int index);
                if (null != objectModifier)
                {
                    activity.InReplyTo[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    activity.InReplyTo[index].Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });
            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.bcc"/>
        /// </summary>
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder BCC(Action<ActivityObjectBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.bcc = ExpandArray(activity.AttributedTo, out int index);
                if (null != objectModifier)
                {
                    activity.bcc[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    activity.bcc[index].Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });
            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Bto"/>
        /// </summary>
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder BTO(Action<ActivityObjectBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Bto = ExpandArray(activity.AttributedTo, out int index);
                if (null != objectModifier)
                {
                    activity.Bto[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    activity.Bto[index].Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });
            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.CC"/>
        /// </summary>
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder CC(Action<ActivityObjectBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.CC = ExpandArray(activity.AttributedTo, out int index);
                if (null != objectModifier)
                {
                    activity.CC[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    activity.CC[index].Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });
            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.To"/>
        /// </summary>
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder To(Action<ActivityObjectBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.To = ExpandArray(activity.AttributedTo, out int index);
                if (null != objectModifier)
                {
                    activity.To[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    activity.To[index].Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });
            return this;
        }

        public ActivityObjectBuilder Url(Action<ActivityLinkBuilder> linkModifier)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Url = ExpandArray(activity.Url, out int index);
                activity.Url[index] = RunModifierBuilder(linkModifier).Build();

                return activity;
            });
            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Generator"/>
        /// </summary>
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Generator(Action<ActivityObjectBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Generator = ExpandArray(activity.AttributedTo, out int index);
                if (null != objectModifier)
                {
                    activity.Generator[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    activity.Generator[index].Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });
            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Preview"/>
        /// </summary>
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Preview(Action<ActivityObjectBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Preview = ExpandArray(activity.AttributedTo, out int index);
                if (null != objectModifier)
                {
                    activity.Preview[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    activity.Preview[index].Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });
            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Tag"/>
        /// </summary>
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Tag(Action<ActivityObjectBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Tag = ExpandArray(activity.AttributedTo, out int index);
                if (null != objectModifier)
                {
                    activity.Tag[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    activity.Tag[index].Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });
            return this;
        }

        /// <summary>
        /// This appends the <see cref="IActivityObject"/> to the <see cref="IActivityObject.Images"/>
        /// </summary>
        /// <param name="modifier">the builder for this type</param>
        /// <returns>A builder to be used in the builder pattern</returns>
        public ActivityObjectBuilder Images(Action<ActivityObjectBuilder> objectModifier, Action<ActivityLinkBuilder> linkModifier = null)
        {
            this.fn = Compose(this.fn, (activity) =>
            {
                activity.Images = ExpandArray(activity.AttributedTo, out int index);
                if (null != objectModifier)
                {
                    activity.Images[index].Obj = RunModifierBuilder(objectModifier).Build();
                }
                else
                {
                    activity.Images[index].Link = RunModifierBuilder(linkModifier).Build();
                }

                return activity;
            });
            return this;
        }

        protected T CreateStreamsType<T>(Action<ActivityObjectBuilder> modifier, Func<T> activityInstanitator) where T : ActivityObject
        {
            T activity = activityInstanitator();
            ActivityObjectBuilder qBuilder = new ActivityObjectBuilder(activity);
            if (modifier != null)
                modifier(qBuilder);
            return (T)qBuilder.Build();
        }

        protected T CreateStreamsType<T>(Action<ActivityBuilder> modifier, Func<T> activityInstanitator) where T : ActivityObject
        {
            T activity = activityInstanitator();
            ActivityBuilder qBuilder = new ActivityBuilder(activity);
            if (modifier != null)
                modifier(qBuilder);
            return (T)qBuilder.Build();
        }

        protected ActivityObjectBuilder RunModifierBuilder(Action<ActivityObjectBuilder> modifier)
        {
            ActivityObject ac = new ActivityObject();
            ActivityObjectBuilder abuilder = new ActivityObjectBuilder(ac);
            modifier(abuilder);
            return abuilder;
        }

        protected ActivityLinkBuilder RunModifierBuilder(Action<ActivityLinkBuilder> modifier)
        {
            ActivityLink ac = new ActivityLink();
            ActivityLinkBuilder abuilder = new ActivityLinkBuilder(ac);
            modifier(abuilder);
            return abuilder;
        }

        protected static IActivityObjectOrLink[] ExpandArray(IActivityObjectOrLink[] array,out int lastIndex)
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

        protected static IActivityObject[] ExpandArray(IActivityObject[] array, out int lastIndex)
        {
            lastIndex = 0;
            if (array == null)
            {
                array = new IActivityObject[1];
            }
            else
            {
                var temp = array;
                array = new IActivityObject[array.Length + 1];
                Array.Copy(temp, array, temp.Length);
                lastIndex = temp.Length;
            }

            return array;
        }

        protected static IActivityLink[] ExpandArray(IActivityLink[] array, out int lastIndex)
        {
            lastIndex = 0;
            if (array == null)
            {
                array = new IActivityLink[1];
            }
            else
            {
                var temp = array;
                array = new IActivityLink[array.Length + 1];
                Array.Copy(temp, array, temp.Length);
                lastIndex = temp.Length;
            }

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