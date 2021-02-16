using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mossharbor.ActivityStreams;

namespace Mossharbor.ActivityStreams.UnitTests
{
    [TestClass]
    public class BuildActivityTypes
    {
        [TestMethod]
        public void BuildSimpleQuestion()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            QuestionActivity activity = (QuestionActivity)builder
                .Question(
                    "What is the answer?",
                    QuestionBuilder.AnswerType.OneOf,
                    i => i.AddAnswer("Option A", type: NoteObject.NoteType)
                          .AddAnswer("Option B", type: NoteObject.NoteType))
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Name, "What is the answer?");

            Assert.IsNotNull((activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Question", (activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(QuestionActivity));

            Assert.AreEqual(2, (activity as QuestionActivity).OneOf.Length, "one of count was set incorrectly.");
            Assert.IsInstanceOfType((activity as QuestionActivity).OneOf[0].Obj, typeof(NoteObject));
            Assert.AreEqual("Note", (activity as QuestionActivity).OneOf[0].Obj.Type, "one of type was set incorrectly.");
            Assert.AreEqual("Option A", (activity as QuestionActivity).OneOf[0].Obj.Name, "one of name was set incorrectly.");

            Assert.IsInstanceOfType((activity as QuestionActivity).OneOf[1].Obj, typeof(NoteObject));
            Assert.AreEqual("Note", (activity as QuestionActivity).OneOf[1].Obj.Type, "one of type was set incorrectly.");
            Assert.AreEqual("Option B", (activity as QuestionActivity).OneOf[1].Obj.Name, "one of name was set incorrectly.");
        }

        [TestMethod]
        public void BuildNoteWithAttachments()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            NoteObject activity = (NoteObject)builder
                .Note("Have you seen my cat?",
                      i => i.Attachment(
                           a => a.Type("Image")
                                 .Content("This is what he looks like.")))
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Content, "Have you seen my cat?");

            Assert.IsNotNull(activity.Type, "the sub object was null and should not have been");
            Assert.AreEqual("Note", activity.Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));

            Assert.IsNotNull((activity as NoteObject).Attachment);
            Assert.AreEqual("Image", (activity as NoteObject).Attachment[0].Obj.Type);
            Assert.AreEqual("This is what he looks like.", (activity as NoteObject).Attachment[0].Obj.Content);
        }

        [TestMethod]
        public void BuildSimpleVideo()
        {
            VideoObject activity = (VideoObject)new ActivityObjectBuilder()
                .Video(
                    "Puppy Plays With Ball",
                    "http://example.org/video.mkv",
                    i => i.Duration(XmlConvert.ToTimeSpan("PT2H")))
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Video");
            Assert.IsInstanceOfType(activity, typeof(VideoObject));
            Assert.AreEqual(activity.Name, "Puppy Plays With Ball");
            Assert.AreEqual(activity.Url[0].Href, "http://example.org/video.mkv");
            Assert.AreEqual(activity.Duration, XmlConvert.ToTimeSpan("PT2H"));
        }

        [TestMethod]
        public void BuildSimpleArticle()
        {
            ArticleObject activity = (ArticleObject)new ActivityObjectBuilder()
                .Article("<div>... you will never believe ...</div>",
                         i => i.Name("What a Crazy Day I Had")
                               .AttributedTo(null, a => a.Href("http://sally.example.org")))
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Article");
            Assert.AreEqual(activity.Name, "What a Crazy Day I Had");
            Assert.IsInstanceOfType(activity, typeof(ArticleObject));
            Assert.AreEqual(activity.Content, "<div>... you will never believe ...</div>");
            Assert.AreEqual(activity.AttributedTo[0].Link.Type, ActivityLink.ActivityLinkType);
            Assert.AreEqual(activity.AttributedTo[0].Link.Href, "http://sally.example.org");
        }

        [TestMethod]
        public void BuildSimpleAdd()
        {
            AddActivity activity = (AddActivity)new ActivityObjectBuilder()
                .Add(i => i.Object(o =>
                                o.Activity(null)
                                .Url(u => u.Href("http://example.org/abc")))
                           .Actor(a =>
                                a.Person(p =>
                                    p.Name("Sally"))))
                .Summary("Sally added an object")
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally added an object");
            Assert.IsNotNull((activity as AddActivity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Add", (activity as AddActivity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(AddActivity));
            Assert.AreEqual("Sally", (activity as AddActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as AddActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as AddActivity).Actor[0].Obj, typeof(PersonActor));
            Assert.IsInstanceOfType((activity as AddActivity).Object[0], typeof(Activity));
            Assert.AreEqual("http://example.org/abc", (activity as Activity).Object[0].Url[0].Href, "the object url was not correct");
        }

        [TestMethod]
        public void BuildSimpleAnnounce()
        {
            AnnounceActivity activity = (AnnounceActivity)new ActivityObjectBuilder()
                .Announce(i =>
                           i.Actor(a =>
                                a.Person(p =>
                                    p.Name("Sally"))
                                .Id(new Uri("http://sally.example.org"))))
                .Summary("Sally added an object")
                .Context()
                .Build();

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Announce", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(AnnounceActivity));

            Assert.AreEqual("Sally", (activity as AnnounceActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as AnnounceActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.AreEqual(new Uri("http://sally.example.org"), (activity as AnnounceActivity).Actor[0].Obj.Id, "the actor object id was incorrect");
            Assert.IsInstanceOfType((activity as AnnounceActivity).Actor[0].Obj, typeof(PersonActor));
        }

        [TestMethod]
        public void BuildSimpleArrive()
        {
            ArriveActivity activity = (ArriveActivity)new ActivityObjectBuilder()
                .Arrive(i =>
                           i.Actor(a =>
                                a.Person(p =>
                                    p.Name("Sally"))
                                .Id(new Uri("http://sally.example.org")))
                           .Origin(o => o.Place(null, null, null, null, null, null, o => o.Name("Home"))))
                .Summary("Sally added an object")
                .Context()
                .Build();

            Assert.IsNotNull((activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Arrive", (activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(ArriveActivity));

            Assert.AreEqual("Sally", (activity as IntransitiveActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as IntransitiveActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as IntransitiveActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Place", (activity as IntransitiveActivity).Origin.Obj.Type, "the origin object type was incorrect");
            Assert.AreEqual("Home", (activity as IntransitiveActivity).Origin.Obj.Name, "the origin object name was incorrect");
            Assert.IsInstanceOfType((activity as IntransitiveActivity).Origin.Obj, typeof(PlaceObject));
        }

        [TestMethod]
        public void BuildSimpleBlock()
        {
            BlockActivity activity = (BlockActivity)new ActivityObjectBuilder()
                .Block(i =>
                           i.Object(a =>
                                        a.Activity(null)
                                         .Type(ActivityLink.ActivityLinkType)
                                         .Url(u => u.Href("http://joe.example.org")))
                            .Actor(null, a => a.Href("http://sally.example.org")))
                .Summary("Sally added an object")
                .Context()
                .Build();

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Block", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(BlockActivity));

            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as BlockActivity).Actor[0].Link.Type, "the actor object type was incorrect");
            Assert.AreEqual("http://sally.example.org", (activity as BlockActivity).Actor[0].Link.Href, "the actor object link href was incorrect");

            Assert.IsNotNull((activity as Activity).Object[0].Type, "the target object type was null");
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as Activity).Object[0].Type, "the target object url name was incorrect");
            Assert.AreEqual("http://joe.example.org", (activity as Activity).Object[0].Url[0].Href, "the target object url name was incorrect");
        }

        [TestMethod]
        public void BuildSimpleCreate()
        {
            CreateActivity activity = (CreateActivity)new ActivityObjectBuilder()
                .Create(i =>
                           i.Object(a =>
                              a.Relationship("http://purl.org/vocab/relationship/friendOf", r =>
                                 r.Object(o => o.Url(u => u.Href("http://matt.example.org")))
                                  .Subject(null, s => s.Href("http://sally.example.org")))
                                  .StartTime(DateTime.Parse("2015-04-21T12:34:56")))
                            .Actor(null, a => a.Href("http://sally.example.org")))
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Create");
            Assert.IsInstanceOfType(activity, typeof(CreateActivity));

            Assert.AreEqual((activity as CreateActivity).Actor[0].Link.Href, "http://sally.example.org");

            Assert.AreEqual((activity as CreateActivity).Object[0].Type, "Relationship");
            Assert.AreEqual(((activity as CreateActivity).Object[0] as RelationshipObject).Subject[0].Link.Href, "http://sally.example.org");
            Assert.AreEqual(((activity as CreateActivity).Object[0] as RelationshipObject).Relationship, "http://purl.org/vocab/relationship/friendOf");
            Assert.AreEqual(((activity as CreateActivity).Object[0] as RelationshipObject).Object.Url[0].Href, "http://matt.example.org");
            Assert.AreEqual(((activity as CreateActivity).Object[0] as RelationshipObject).StartTime.Value, DateTime.Parse("2015-04-21T12:34:56"));
        }

        [TestMethod]
        public void BuildSimpleDelete()
        {
            DeleteActivity activity = (DeleteActivity)new ActivityObjectBuilder()
                .Delete(i =>
                           i.Object(o => o.Activity(a => a.Url(u => u.Href("http://example.org/notes/1"))))
                            .Actor(a => a.Person(p => p.Name("Sally")))
                            .Origin(o => o.Dislike(d => d.Name("Sally's Notes"))))
                .Context()
                .Build();

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Delete", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(DeleteActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.IsNotNull((activity as Activity).Object[0].Type, "the target object type was null");
            Assert.AreEqual("http://example.org/notes/1", (activity as Activity).Object[0].Url[0].Href, "the target object url name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(Activity));

            Assert.AreEqual("Dislike", (activity as Activity).Origin.Obj.Type, "the origin object type was incorrect");
            Assert.AreEqual("Sally's Notes", (activity as Activity).Origin.Obj.Name, "the origin object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Origin.Obj, typeof(DislikeActivity));
        }

        [TestMethod]
        public void BuildSimpleDislike()
        {
            /*
            {
              "@context": "https://www.w3.org/ns/activitystreams",
              "summary": "Sally disliked a post",
              "type": "Dislike",
              "actor": "http://sally.example.org",
              "object": "http://example.org/posts/1"
            }*/

            DislikeActivity activity = (DislikeActivity)new ActivityObjectBuilder()
                .Dislike(i =>
                         i.Object(o => o.Type(ActivityLink.ActivityLinkType).Url(u => u.Href("http://example.org/posts/1")))
                          .Summary("Sally disliked a post"))
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally disliked a post");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Dislike", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(DislikeActivity));

            Assert.IsNotNull((activity as Activity).Object[0].Type, "the target object type was null");
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as Activity).Object[0].Type, "the target object url name was incorrect");
            Assert.AreEqual("http://example.org/posts/1", (activity as Activity).Object[0].Url[0].Href, "the target object url name was incorrect");
        }

        [TestMethod]
        public void BuildSimpleFlag()
        {
            FlagActivity activity = (FlagActivity)new ActivityObjectBuilder()
                .Flag(i =>
                         i.Object(o => o.Note("An inappropriate note")))
                .Context()
                .Build();

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Flag", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(FlagActivity));

            Assert.IsNotNull((activity as FlagActivity).Object[0].Type, "the target object type was null");
            Assert.AreEqual("Note", (activity as Activity).Object[0].Type, "the target object url name was incorrect");
            Assert.IsInstanceOfType((activity as FlagActivity).Object[0], typeof(NoteObject));
            Assert.AreEqual("An inappropriate note", (activity as Activity).Object[0].Content, "the target object url name was incorrect");
        }

        [TestMethod]
        public void BuildSimpleFollow()
        {

            FollowActivity activity = (FollowActivity)new ActivityObjectBuilder()
                .Follow(i =>
                         i.Object(o => o.Person().Name("John"))
                          .Actor(a => a.Person().Name("Sally")))
                .Summary("Sally followed John")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally followed John");

            Assert.IsNotNull(activity.Type, "the sub object was null and should not have been");
            Assert.AreEqual("Follow", activity.Type, "the sub object was null and should not have been");

            Assert.AreEqual("Sally", activity.Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", activity.Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType(activity.Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Person", (activity as Activity).Object[0].Type, "the target object type was not null");
            Assert.AreEqual("John", (activity as Activity).Object[0].Name, "the target object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(PersonActor));
        }

        [TestMethod]
        public void BuildSimpleIgnore()
        {
            IgnoreActivity activity = (IgnoreActivity)new ActivityObjectBuilder()
                .Ignore(i =>
                         i.Object(null, o => o.Href("http://example.org/notes/1"))
                          .Actor(a => a.Person().Name("Sally")))
                .Summary("Sally ignored a note")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally ignored a note");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Ignore", (activity as Activity).Type, "the sub object was null and should not have been");

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Link", (activity as Activity).Object[0].Type, "the target object type was not null");
            Assert.AreEqual("http://example.org/notes/1", (activity as Activity).Object[0].Url[0].Href, "the object link was not correct.");
        }

        [TestMethod]
        public void BuildSimpleInvite()
        {
            InviteActivity activity = (InviteActivity)new ActivityObjectBuilder()
                .Invite(i =>
                         i.Object(o=>o.Event(null).Name("A Party"))
                          .Actor(a => a.Person().Name("Sally"))
                          .Target(t=>t.Person().Name("John"))
                          .Target(t=>t.Person().Name("Lisa")))
                .Summary("Sally invited John and Lisa to a party")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally invited John and Lisa to a party");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Invite", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(InviteActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Event", (activity as Activity).Object[0].Type, "the target object type was incorrect");
            Assert.AreEqual("A Party", (activity as Activity).Object[0].Name, "the target object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(EventObject));

            Assert.AreEqual("Person", (activity as Activity).Target[0].Obj.Type, "the target object type was incorrect");
            Assert.AreEqual("John", (activity as Activity).Target[0].Obj.Name, "the target object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Target[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Person", (activity as Activity).Target[1].Obj.Type, "the target object 1 type was incorrect");
            Assert.AreEqual("Lisa", (activity as Activity).Target[1].Obj.Name, "the target object 1 name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Target[1].Obj, typeof(PersonActor));
        }

        [TestMethod]
        public void BuildSimpleJoin()
        {
            JoinActivity activity = (JoinActivity)new ActivityObjectBuilder()
                .Join(i =>
                         i.Object(o => o.Group().Name("A Simple Group"))
                          .Actor(a => a.Person().Name("Sally")))
                .Summary("Sally joined a group")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally joined a group");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Join", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(JoinActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Group", (activity as Activity).Object[0].Type, "the target object type was not null");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(GroupActor));
            Assert.AreEqual("A Simple Group", (activity as Activity).Object[0].Name, "the object link was not correct.");
        }

        [TestMethod]
        public void BuildSimpleLeave()
        {
            LeaveActivity activity = (LeaveActivity)new ActivityObjectBuilder()
                .Leave(i =>
                         i.Object(o => o.Place("Work"))
                          .Actor(a => a.Person().Name("Sally")))
                .Summary("Sally left work")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally left work");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Leave", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(LeaveActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Place", (activity as Activity).Object[0].Type, "the target object type was not null");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(PlaceObject));
            Assert.AreEqual("Work", (activity as Activity).Object[0].Name, "the object link was not correct.");
        }
    }
}
