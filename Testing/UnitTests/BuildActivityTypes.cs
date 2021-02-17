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
                         i.Object(o => o.Event(null).Name("A Party"))
                          .Actor(a => a.Person().Name("Sally"))
                          .Target(t => t.Person().Name("John"))
                          .Target(t => t.Person().Name("Lisa")))
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

        [TestMethod]
        public void BuildSimpleLike()
        {
            LikeActivity activity = (LikeActivity)new ActivityObjectBuilder()
                .Like(i =>
                         i.Object(null, o => o.Href("http://example.org/notes/1"))
                          .Actor(a => a.Person().Name("Sally")))
                .Summary("Sally liked a note")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally liked a note");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Like", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(LikeActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Link", (activity as Activity).Object[0].Type, "the target object type was not null");
            Assert.AreEqual("http://example.org/notes/1", (activity as Activity).Object[0].Url[0].Href, "the object link was not correct.");
        }

        [TestMethod]
        public void BuildSimpleListen()
        {
            ListenActivity activity = (ListenActivity)new ActivityObjectBuilder()
                .Listen(i =>
                         i.Object(null, o => o.Href("http://example.org/music.mp3"))
                          .Actor(a => a.Person().Name("Sally")))
                .Summary("Sally listened to a piece of music")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally listened to a piece of music");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Listen", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(ListenActivity));

            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as ListenActivity).Object[0].Type, "the object link name was incorrect");
            Assert.AreEqual("http://example.org/music.mp3", (activity as ListenActivity).Object[0].Url[0].Href, "the object link href was incorrect");
        }

        [TestMethod]
        public void BuildSimpleMove()
        {
            MoveActivity activity = (MoveActivity)new ActivityObjectBuilder()
                .Move(i =>
                         i.Object(null, o => o.Href("http://example.org/posts/1"))
                          .Actor(a => a.Person().Name("Sally"))
                          .Target(t=>t.Collection(c=>c.Name("List B")))
                          .Origin(or=>or.Collection().Name("List A")))
                .Summary("Sally moved a post from List A to List B")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally moved a post from List A to List B");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Move", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(MoveActivity));

            Assert.AreEqual("Sally", (activity as MoveActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as MoveActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as MoveActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.IsNotNull((activity as Activity).Object[0].Type, "the target object type was null");
            Assert.AreEqual("http://example.org/posts/1", (activity as Activity).Object[0].Url[0].Href, "the target object url name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(ActivityObject));

            Assert.AreEqual("Collection", (activity as Activity).Target[0].Obj.Type, "the origin object type was incorrect");
            Assert.AreEqual("List B", (activity as Activity).Target[0].Obj.Name, "the origin object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Target[0].Obj, typeof(Collection));

            Assert.AreEqual("Collection", (activity as Activity).Origin.Obj.Type, "the origin object type was incorrect");
            Assert.AreEqual("List A", (activity as Activity).Origin.Obj.Name, "the origin object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Origin.Obj, typeof(Collection));
        }

        [TestMethod]
        public void BuildSimpleOffer()
        {
            OfferActivity activity = (OfferActivity)new ActivityObjectBuilder()
                .Offer(i =>
                         i.Object(o=> o.Relationship("http://purl.org/vocab/relationship/friendOf", 
                                  r=> r.Object(o=>o.Url(u=>u.Href("acct:john@example.org")))
                                       .Subject(null, s=>s.Href("acct:sally@example.org"))
                                       .Summary("Sally and John's friendship"))
                                       .Id(new Uri("http://example.org/connections/123")))
                          .Actor(null, a => a.Href("acct:sally@example.org"))
                          .Target(null, t => t.Href("acct:john@example.org")))
                .Context()
                .Build();

            Assert.AreEqual(activity.Type, "Offer");
            Assert.IsInstanceOfType(activity, typeof(OfferActivity));

            Assert.AreEqual((activity as OfferActivity).Actor[0].Link.Href, "acct:sally@example.org");

            Assert.AreEqual((activity as OfferActivity).Object[0].Type, "Relationship");
            Assert.AreEqual(((activity as OfferActivity).Object[0] as RelationshipObject).Summary, "Sally and John's friendship");
            Assert.AreEqual(((activity as OfferActivity).Object[0] as RelationshipObject).Id, "http://example.org/connections/123");
            Assert.AreEqual(((activity as OfferActivity).Object[0] as RelationshipObject).Relationship, "http://purl.org/vocab/relationship/friendOf");
            Assert.AreEqual(((activity as OfferActivity).Object[0] as RelationshipObject).Subject[0].Link.Href, "acct:sally@example.org");
            Assert.AreEqual(((activity as OfferActivity).Object[0] as RelationshipObject).Object.Url[0].Href, "acct:john@example.org");
            Assert.AreEqual(((activity as OfferActivity).Target[0]).Link.Href, "acct:john@example.org");
        }

        [TestMethod]
        public void BuildSimpleRead()
        {
            ReadActivity activity = (ReadActivity)new ActivityObjectBuilder()
                .Read(i =>
                         i.Object(null, o => o.Href("http://example.org/posts/1"))
                          .Actor(a=>a.Person("Sally")))
                .Summary("Sally read a blog post")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally read a blog post");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Read", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(ReadActivity));

            Assert.AreEqual("Sally", (activity as ReadActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as ReadActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as ReadActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as Activity).Object[0].Type, "the target object actors href was incorrect");
            Assert.AreEqual("http://example.org/posts/1", (activity as Activity).Object[0].Url[0].Href, "the target object actors href was incorrect");
        }

        [TestMethod]
        public void BuildSimpleReject()
        {
            RejectActivity activity = (RejectActivity)new ActivityObjectBuilder()
                .Reject(i =>
                         i.Object(o=>o.Invite(i=> i.Object(ob=>ob.Event(e=>e.Name("Going-Away Party for Jim")))
                                                   .Actor(null, a=>a.Href("http://john.example.org"))))
                          .Actor(a => a.Person("Sally")))
                .Summary("Sally rejected an invitation to a party")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally rejected an invitation to a party");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Reject", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(RejectActivity));

            Assert.AreEqual("Sally", (activity as RejectActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as RejectActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as RejectActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Invite", (activity as RejectActivity).Object[0].Type, "the target object type was incorrect");
            Assert.AreEqual("http://john.example.org", ((activity as RejectActivity).Object[0] as InviteActivity).Actor[0].Link.Href, "thehref for the invte was incorrect");

            Assert.AreEqual("Event", ((activity as RejectActivity).Object[0] as InviteActivity).Object[0].Type, "The sub object type was incorrect");
            Assert.AreEqual("Going-Away Party for Jim", ((activity as RejectActivity).Object[0] as InviteActivity).Object[0].Name, "The sub object name was incorrect");
            Assert.IsInstanceOfType(((activity as RejectActivity).Object[0] as InviteActivity).Object[0], typeof(EventObject));
        }

        [TestMethod]
        public void BuildSimpleRemove()
        {
            RemoveActivity activity = (RemoveActivity)new ActivityObjectBuilder()
                .Remove(i =>
                         i.Object(null, o => o.Href("http://example.org/notes/1"))
                          .Actor(a => a.Person("Sally"))
                          .Target(t=>t.Collection(c=>c.Name("Notes Folder"))))
                .Summary("Sally removed a note from her notes folder")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally removed a note from her notes folder");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Remove", (activity as Activity).Type, "the sub object type was not correct");
            Assert.IsInstanceOfType(activity, typeof(RemoveActivity));

            Assert.AreEqual("Sally", (activity as RemoveActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as RemoveActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as RemoveActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as RemoveActivity).Object[0].Type, "the target object type was incorrect");
            Assert.AreEqual("http://example.org/notes/1", (activity as RemoveActivity).Object[0].Url[0].Href, "thehref for the invte was incorrect");

            Assert.AreEqual("Collection", (activity as Activity).Target[0].Obj.Type, "the target object type was incorrect");
            Assert.AreEqual("Notes Folder", (activity as Activity).Target[0].Obj.Name, "the target object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Target[0].Obj, typeof(Collection));
        }

        [TestMethod]
        public void BuildSimpleTentativeAccept()
        {
            TentativeAcceptActivity activity = (TentativeAcceptActivity)new ActivityObjectBuilder()
                .TentativeAccept(a =>
                         a.Object(o=>o.Invite(i=>i.Object(e=>e.Event(ev=>ev.Name("Going-Away Party for Jim")))
                                                  .Actor(null, l=>l.Href("http://john.example.org"))))
                          .Actor(a=>a.Person("Sally")))
                .Summary("Sally removed a note from her notes folder")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally removed a note from her notes folder");
            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("TentativeAccept", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(TentativeAcceptActivity));
            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));
            Assert.AreEqual("Invite", (activity as Activity).Object[0].Type, "the target object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(InviteActivity));
            Assert.IsInstanceOfType(((activity as Activity).Object[0] as InviteActivity).Object[0], typeof(EventObject));
            Assert.AreEqual("Event", (((activity as Activity).Object[0] as InviteActivity).Object[0] as EventObject).Type, "the name of the event was incorrect");
            Assert.AreEqual("Going-Away Party for Jim", (((activity as Activity).Object[0] as InviteActivity).Object[0] as EventObject).Name, "the name of the event was incorrect");
            Assert.AreEqual("http://john.example.org", ((activity as Activity).Object[0] as InviteActivity).Actor[0].Link.Href, "The link for the href was incorrect");
        }

        [TestMethod]
        public void BuildSimpleTentativeReject()
        {
            TentativeRejectActivity activity = (TentativeRejectActivity)new ActivityObjectBuilder()
                .TentativeReject(a =>
                         a.Object(o => o.Invite(i => i.Object(e => e.Event(ev => ev.Name("Going-Away Party for Jim")))
                                                    .Actor(null, l => l.Href("http://john.example.org"))))
                          .Actor(a => a.Person("Sally")))
                .Summary("Sally tentatively rejected an invitation to a party")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally tentatively rejected an invitation to a party");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("TentativeReject", (activity as Activity).Type, "the sub object type was not correct");
            Assert.IsInstanceOfType(activity, typeof(TentativeRejectActivity));

            Assert.AreEqual("Sally", (activity as TentativeRejectActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as TentativeRejectActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as TentativeRejectActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Invite", (activity as RejectActivity).Object[0].Type, "the target object type was incorrect");
            Assert.AreEqual("http://john.example.org", ((activity as RejectActivity).Object[0] as InviteActivity).Actor[0].Link.Href, "thehref for the invte was incorrect");

            Assert.AreEqual("Event", ((activity as RejectActivity).Object[0] as InviteActivity).Object[0].Type, "The sub object type was incorrect");
            Assert.AreEqual("Going-Away Party for Jim", ((activity as RejectActivity).Object[0] as InviteActivity).Object[0].Name, "The sub object name was incorrect");
            Assert.IsInstanceOfType(((activity as RejectActivity).Object[0] as InviteActivity).Object[0], typeof(EventObject));
        }

        [TestMethod]
        public void BuildSimpleTravel()
        {
            TravelActivity activity = (TravelActivity)new ActivityObjectBuilder()
                .Travel(a =>
                         a.Origin(o=>o.Place("Work"))
                          .Actor(a => a.Person("Sally"))
                          .Target(t=>t.Place("Home")))
                .Summary("Sally went home from work")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally went home from work");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Travel", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(TravelActivity));

            Assert.AreEqual("Sally", (activity as TravelActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as TravelActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as TravelActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Place", (activity as Activity).Target[0].Obj.Type, "the origin object type was incorrect");
            Assert.AreEqual("Home", (activity as Activity).Target[0].Obj.Name, "the origin object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Target[0].Obj, typeof(PlaceObject));

            Assert.AreEqual("Place", (activity as Activity).Origin.Obj.Type, "the origin object type was incorrect");
            Assert.AreEqual("Work", (activity as Activity).Origin.Obj.Name, "the origin object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Origin.Obj, typeof(PlaceObject));
        }

        [TestMethod]
        public void BuildSimpleUndo()
        {
            UndoActivity activity = (UndoActivity)new ActivityObjectBuilder()
                .Undo(a =>
                         a.Object(o=>o.Offer(f=>f.Object(null, ob=>ob.Href("http://example.org/posts/1"))
                                                 .Actor(null, ac=>ac.Href("http://sally.example.org"))
                                                 .Target(null, t=>t.Href("http://john.example.org"))))
                         .Actor(null, al=>al.Href("http://sally.example.org")))
                .Summary("Sally retracted her offer to John")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally retracted her offer to John");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Undo", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(UndoActivity));

            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as UndoActivity).Actor[0].Link.Type, "the actor link name was incorrect");
            Assert.AreEqual("http://sally.example.org", (activity as UndoActivity).Actor[0].Link.Href, "the actor link href was incorrect");

            Assert.AreEqual("Offer", (activity as Activity).Object[0].Type, "the target object type was incorrect");
            Assert.AreEqual(ActivityLink.ActivityLinkType, ((activity as Activity).Object[0] as Activity).Actor[0].Link.Type, "the target object actors href was incorrect");
            Assert.AreEqual("http://sally.example.org", ((activity as Activity).Object[0] as Activity).Actor[0].Link.Href, "the target object actors href was incorrect");

            Assert.AreEqual(ActivityLink.ActivityLinkType, ((activity as Activity).Object[0] as Activity).Object[0].Type, "the target object actors href was incorrect");
            Assert.AreEqual("http://example.org/posts/1", ((activity as Activity).Object[0] as Activity).Object[0].Url[0].Href, "the target object actors href was incorrect");

            Assert.AreEqual(ActivityLink.ActivityLinkType, ((activity as Activity).Object[0] as Activity).Target[0].Link.Type, "the origin object type was incorrect");
            Assert.AreEqual("http://john.example.org", ((activity as Activity).Object[0] as Activity).Target[0].Link.Href, "the origin object name was incorrect");
        }

        [TestMethod]
        public void BuildSimpleUpdate()
        {
            UpdateActivity activity = (UpdateActivity)new ActivityObjectBuilder()
                .Update(a => a.Object(null, lk=>lk.Href("http://example.org/notes/1")))
                .Summary("Sally updated her note")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally updated her note");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Update", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(UpdateActivity));

            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as UpdateActivity).Object[0].Type, "the object link name was incorrect");
            Assert.AreEqual("http://example.org/notes/1", (activity as UpdateActivity).Object[0].Url[0].Href, "the object link href was incorrect");
        }

        [TestMethod]
        public void BuildSimpleView()
        {
            ViewActivity activity = (ViewActivity)new ActivityObjectBuilder()
                .View(a => a.Object(ob=>ob.Article(al=>al.Name("What You Should Know About Activity Streams"))))
                .Summary("Sally read an article")
                .Context()
                .Build();

            Assert.AreEqual(activity.Summary, "Sally read an article");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("View", (activity as Activity).Type, "activity type was correct");
            Assert.IsInstanceOfType(activity, typeof(ViewActivity));

            Assert.AreEqual("Article", (activity as ViewActivity).Object[0].Type, "the object link name was incorrect");
            Assert.IsInstanceOfType((activity as ViewActivity).Object[0], typeof(ArticleObject));
            Assert.AreEqual("What You Should Know About Activity Streams", (activity as ViewActivity).Object[0].Name, "the object name was incorrect");
        }

        [TestMethod]
        public void BuildSimpleApplication()
        {
            ApplicationActor activity = (ApplicationActor)new ActivityObjectBuilder()
                .Application()
                .Name("Exampletron 3000")
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Application");
            Assert.AreEqual(activity.Name, "Exampletron 3000");
            Assert.IsInstanceOfType(activity, typeof(ApplicationActor));
        }

        [TestMethod]
        public void BuildSimpleGroup()
        {
            GroupActor activity = (GroupActor)new ActivityObjectBuilder()
                .Group()
                .Name("Big Beards of Austin")
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Group");
            Assert.AreEqual(activity.Name, "Big Beards of Austin");
            Assert.IsInstanceOfType(activity, typeof(GroupActor));
        }

        [TestMethod]
        public void BuildSimpleOrganizationActor()
        {
            OrganizationActor activity = (OrganizationActor)new ActivityObjectBuilder()
                .Organization()
                .Name("Example Co.")
                .Context()
                .Build();


            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Organization");
            Assert.AreEqual(activity.Name, "Example Co.");
            Assert.IsInstanceOfType(activity, typeof(OrganizationActor));
        }

        [TestMethod]
        public void BuildSimplePersonActor()
        {
            PersonActor activity = (PersonActor)new ActivityObjectBuilder()
                .Person()
                .Name("Sally Smith")
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Person");
            Assert.AreEqual(activity.Name, "Sally Smith");
            Assert.IsInstanceOfType(activity, typeof(PersonActor));
        }

        [TestMethod]
        public void BuildSimpleServiceActor()
        {
            ServiceActor activity = (ServiceActor)new ActivityObjectBuilder()
                .Service()
                .Name("Acme Web Service")
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Service");
            Assert.AreEqual(activity.Name, "Acme Web Service");
            Assert.IsInstanceOfType(activity, typeof(ServiceActor));
        }

        [TestMethod]
        public void BuildSimpleArticleObject()
        {
            ArticleObject activity = (ArticleObject)new ActivityObjectBuilder()
                .Article()
                .Name("What a Crazy Day I Had")
                .Content("<div>... you will never believe ...</div>")
                .AttributedTo(null, a=>a.Href("http://sally.example.org"))
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
        public void BuildSimpleAudio()
        {
            AudioObject activity = (AudioObject)new ActivityObjectBuilder()
                .Audio()
                .Name("Interview With A Famous Technologist")
                .Url(l=>l.Href("http://example.org/podcast.mp3").MediaType("audio/mp3"))
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Audio");
            Assert.AreEqual(activity.Name, "Interview With A Famous Technologist");
            Assert.IsInstanceOfType(activity, typeof(AudioObject));
            Assert.AreEqual(activity.Url[0].Href, "http://example.org/podcast.mp3");
            Assert.AreEqual(activity.Url[0].Type, "Link");
            Assert.AreEqual(activity.Url[0].MediaType, "audio/mp3");
        }

        [TestMethod]
        public void BuildSimpleDocument()
        {
            DocumentObject activity = (DocumentObject)new ActivityObjectBuilder()
                .Document()
                .Name("4Q Sales Forecast")
                .Url(l => l.Href("http://example.org/4q-sales-forecast.pdf"))
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Document");
            Assert.IsInstanceOfType(activity, typeof(DocumentObject));
            Assert.AreEqual(activity.Name, "4Q Sales Forecast");
            Assert.AreEqual(activity.Url[0].Href, "http://example.org/4q-sales-forecast.pdf");
        }

        [TestMethod]
        public void BuildSimpleEvent()
        {
            EventObject activity = (EventObject)new ActivityObjectBuilder()
                .Event()
                .Name("Going-Away Party for Jim")
                .StartTime(DateTime.Parse("2014-12-31T23:00:00-08:00"))
                .EndTime(DateTime.Parse("2015-01-01T06:00:00-08:00"))
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Event");
            Assert.IsInstanceOfType(activity, typeof(EventObject));
            Assert.AreEqual(activity.Name, "Going-Away Party for Jim");
            Assert.AreEqual(activity.StartTime, DateTime.Parse("2014-12-31T23:00:00-08:00"));
            Assert.AreEqual(activity.EndTime, DateTime.Parse("2015-01-01T06:00:00-08:00"));
        }

        [TestMethod]
        public void BuildSimpleImage()
        {
            ImageObject activity = (ImageObject)new ActivityObjectBuilder()
                .Image()
                .Name("Cat Jumping on Wagon")
                .Url(u => u.Href("http://example.org/image.jpeg").MediaType("image/jpeg"))
                .Url(u => u.Href("http://example.org/image.png").MediaType("image/png"))
                .Context()

                .Build();
            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Image");
            Assert.IsInstanceOfType(activity, typeof(ImageObject));
            Assert.AreEqual(activity.Name, "Cat Jumping on Wagon");
            Assert.AreEqual(activity.Url[0].Href, "http://example.org/image.jpeg");
            Assert.AreEqual(activity.Url[0].Type, "Link");
            Assert.AreEqual(activity.Url[0].MediaType, "image/jpeg");
            Assert.AreEqual(activity.Url[1].Href, "http://example.org/image.png");
            Assert.AreEqual(activity.Url[1].Type, "Link");
            Assert.AreEqual(activity.Url[1].MediaType, "image/png");
        }

        [TestMethod]
        public void BuildSimpleNote()
        {
            NoteObject activity = (NoteObject)new ActivityObjectBuilder()
                .Note("Looks like it is going to rain today. Bring an umbrella!")
                .Name("A Word of Warning")
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));
            Assert.AreEqual(activity.Name, "A Word of Warning");
            Assert.AreEqual(activity.Content, "Looks like it is going to rain today. Bring an umbrella!");
        }

        [TestMethod]
        public void BuildSimplePageObject()
        {
            PageObject activity = (PageObject)new ActivityObjectBuilder()
                .Page()
                .Name("Omaha Weather Report")
                .Url(u=>u.Href("http://example.org/weather-in-omaha.html"))
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Page");
            Assert.IsInstanceOfType(activity, typeof(PageObject));
            Assert.AreEqual(activity.Name, "Omaha Weather Report");
            Assert.AreEqual(activity.Url[0].Href, "http://example.org/weather-in-omaha.html");
        }

        [TestMethod]
        public void BuildSimplePlace()
        {
            PlaceObject activity = (PlaceObject)new ActivityObjectBuilder()
                .Place(119.7667, 36.75, null, null, 15, "miles")
                .Name("Fresno Area")
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Place");
            Assert.IsInstanceOfType(activity, typeof(PlaceObject));
            Assert.AreEqual(activity.Name, "Fresno Area");
            Assert.AreEqual((activity as PlaceObject).Latitude, 36.75);
            Assert.AreEqual((activity as PlaceObject).Longitude, 119.7667);
            Assert.AreEqual((activity as PlaceObject).Radius, 15);
            Assert.AreEqual((activity as PlaceObject).Units, "miles");
        }

        [TestMethod]
        public void BuildSimpleProfile()
        {
            ProfileObject activity = (ProfileObject)new ActivityObjectBuilder()
                .Profile(p=>p.Describes(d=>d.Person("Sally Smith")))
                .Summary("Sally's Profile")
                .Url(u => u.Href("http://example.org/weather-in-omaha.html"))
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Profile");
            Assert.IsInstanceOfType(activity, typeof(ProfileObject));
            Assert.AreEqual(activity.Summary, "Sally's Profile");
            Assert.AreEqual((activity as ProfileObject).Describes.Type, "Person");
            Assert.AreEqual((activity as ProfileObject).Describes.Name, "Sally Smith");
            Assert.IsInstanceOfType((activity as ProfileObject).Describes, typeof(PersonActor));
        }

        [TestMethod]
        public void BuildSimpleRelationship()
        {
            RelationshipObject activity = (RelationshipObject)new ActivityObjectBuilder()
                .Relationship("http://purl.org/vocab/relationship/acquaintanceOf",
                        r=> r.Object(o=>o.Person("John"))
                             .Subject(s=>s.Person("Sally")))
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Relationship");
            Assert.IsInstanceOfType(activity, typeof(RelationshipObject));

            Assert.AreEqual("Person", (activity as RelationshipObject).Subject[0].Obj.Type, "the subject link name was incorrect");
            Assert.IsInstanceOfType((activity as RelationshipObject).Subject[0].Obj, typeof(PersonActor));
            Assert.AreEqual("Sally", (activity as RelationshipObject).Subject[0].Obj.Name, "the subject name was incorrect");

            Assert.AreEqual("http://purl.org/vocab/relationship/acquaintanceOf", (activity as RelationshipObject).Relationship, "the relationship was incorrect");

            Assert.AreEqual("Person", (activity as RelationshipObject).Object.Type, "the object link name was incorrect");
            Assert.IsInstanceOfType((activity as RelationshipObject).Object, typeof(PersonActor));
            Assert.AreEqual("John", (activity as RelationshipObject).Object.Name, "the object name was incorrect");
        }

        [TestMethod]
        public void BuildSimpleTombstone()
        {
            TombstoneObject activity = (TombstoneObject)new ActivityObjectBuilder()
                .Tombstone("Image", null)
                .Summary("This image has been deleted")
                .Url(l=>l.Href("http://example.org/image/2"))
                .Context()
                .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Tombstone");
            Assert.IsInstanceOfType(activity, typeof(TombstoneObject));
            Assert.AreEqual((activity as TombstoneObject).FormerType, "Image");
            Assert.AreEqual(activity.Summary, "This image has been deleted");
            Assert.AreEqual(activity.Url[0].Href, "http://example.org/image/2");
        }

        [TestMethod]
        public void BuildSimpleMentionLink()
        {
            MentionLink activityLink = (MentionLink)new ActivityLinkBuilder()
                .Mention()
                .Href("http://example.org/joe")
                .Name("Joe")
                .Context()
                .Build();

            Assert.IsNotNull(activityLink.Context, "the activity stream context was null");
            Assert.AreEqual(activityLink.Type, "Mention");
            Assert.IsInstanceOfType(activityLink, typeof(MentionLink));
            Assert.AreEqual(activityLink.Name, "Joe");
            Assert.AreEqual((activityLink as MentionLink).Href, "http://example.org/joe");
        }
    }
}
