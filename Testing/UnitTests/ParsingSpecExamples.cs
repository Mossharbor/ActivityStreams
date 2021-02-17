using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using System.Xml;

namespace Mossharbor.ActivityStreams.UnitTests
{
    [TestClass]
    public class ParsingSpecExamples
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ParseBasicObjectWithType62()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(@"
                {
                  ""@context"": ""https://www.w3.org/ns/activitystreams"",
                  ""summary"": ""A foo"",
                  ""type"": ""http://example.org/Foo""
                }")
                .Build();

            Assert.IsNotNull(activity.Context, "the context waas null");
            Assert.AreEqual(activity.Summary, "A foo", "The summary Did not match");
            Assert.AreEqual(activity.Context.AbsoluteUri, "https://www.w3.org/ns/activitystreams", "The Uri Did not match");
            Assert.AreEqual(activity.Type, "http://example.org/Foo", "The type did not match");

        }

        /// <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample001()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example001.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 2
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample002()
        {
            ActivityLinkBuilder builder = new ActivityLinkBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example002.json"))
                            .Build();

            Assert.IsNotNull(activity.Type, "the activity link context was null");
            Assert.IsNotNull(activity.Href, "the activity link Href was null");
            Assert.IsNotNull(activity.HrefLang, "the activity link HrefLang was null");
            Assert.IsNotNull(activity.MediaType, "the activity link MediaType was null");
            Assert.IsNotNull(activity.Name, "the activity link Name was null");

            Assert.AreEqual(activity.Type, "Link", "the type was not set");
            Assert.AreEqual(activity.Href, "http://example.org/abc", "");
            Assert.AreEqual(activity.HrefLang, "en", "the hrref lang");
            Assert.AreEqual(activity.MediaType, "text/html", "the media type");
            Assert.AreEqual(activity.Name, "An example link", "the name");
        }

        /// <summary>
        /// Testing Spec example 3
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample003()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example003.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally did something to a note", "The summary Did not match");
            Assert.AreEqual(activity.Context.AbsoluteUri, "https://www.w3.org/ns/activitystreams", "The Uri Did not match");
            Assert.AreEqual(activity.Type, "Activity", "The type did not match");
            Assert.IsInstanceOfType(activity, typeof(Activity));
            Assert.IsNotNull((activity as Activity).Actor, "the actor was null and should not have been");
            Assert.AreEqual(1, (activity as Activity).Actor.Length, "the actor was null and should not have been");
            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor name was not correct");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor type was not correct");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.IsNotNull((activity as Activity).Object[0], "the sub object was null and should not have been");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(NoteObject));
            Assert.AreEqual("A Note", (activity as Activity).Object[0].Name, "the sub object name was incorrect");
            Assert.AreEqual("Note", (activity as Activity).Object[0].Type, "the sub object type was incorrect");

        }

        /// <summary>
        /// Testing Spec example 4
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample004()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example004.json")).Build();
            
            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Travel", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(TravelActivity));
            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));
            Assert.AreEqual("Work", (activity as Activity).Target[0].Obj.Name, "the target object name was incorrect");
            Assert.AreEqual("Place", (activity as Activity).Target[0].Obj.Type, "the target object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Target[0].Obj, typeof(PlaceObject));
        }

        /// <summary>
        /// Testing Spec example 5
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample005()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example005.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.AreEqual((uint)2, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the OrderedItems were not empty");
            Assert.AreEqual(Collection.CollectionType, (activity as Collection).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as Collection).TotalItems, (activity as Collection).Items.Length, "the item count was incorrect");
            Assert.AreEqual("Note", (activity as Collection).Items[0].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual("Note", (activity as Collection).Items[1].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual("A Simple Note", (activity as Collection).Items[0].Obj.Name, "the sub object type was incorrect");
            Assert.AreEqual("Another Simple Note", (activity as Collection).Items[1].Obj.Name, "the sub object type was incorrect");
            Assert.IsInstanceOfType((activity as Collection).Items[0].Obj, typeof(NoteObject));
            Assert.IsInstanceOfType((activity as Collection).Items[1].Obj, typeof(NoteObject));
        }

        /// <summary>
        /// Testing Spec example 6
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample006()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example006.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.AreEqual((uint)2, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.AreEqual(Collection.OrderedCollectionType, (activity as Collection).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as Collection).TotalItems, (activity as Collection).OrderedItems.Length, "the item count was incorrect");
            Assert.IsNull((activity as Collection).Items, "the items were not empty");
            Assert.AreEqual("Note", (activity as Collection).OrderedItems[0].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual("Note", (activity as Collection).OrderedItems[1].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual("A Simple Note", (activity as Collection).OrderedItems[0].Obj.Name, "the sub object type was incorrect");
            Assert.AreEqual("Another Simple Note", (activity as Collection).OrderedItems[1].Obj.Name, "the sub object type was incorrect");
            Assert.IsInstanceOfType((activity as Collection).OrderedItems[0].Obj, typeof(NoteObject));
            Assert.IsInstanceOfType((activity as Collection).OrderedItems[1].Obj, typeof(NoteObject));
        }

        /// <summary>
        /// Testing Spec example 7
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample007()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example007.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsInstanceOfType(activity, typeof(CollectionPage));
            Assert.AreEqual((uint)2, (activity as CollectionPage).TotalItems, "the total items were incorrect");
            Assert.AreEqual(CollectionPage.CollectionPageType, (activity as CollectionPage).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as CollectionPage).TotalItems, (activity as CollectionPage).Items.Length, "the item count was incorrect");
            Assert.IsNull((activity as CollectionPage).OrderedItems, "the items were not empty");
            Assert.AreEqual("Note", (activity as CollectionPage).Items[0].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual("Note", (activity as CollectionPage).Items[1].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual("A Simple Note", (activity as CollectionPage).Items[0].Obj.Name, "the sub object type was incorrect");
            Assert.AreEqual("Another Simple Note", (activity as CollectionPage).Items[1].Obj.Name, "the sub object type was incorrect");
            Assert.IsInstanceOfType((activity as CollectionPage).Items[0].Obj, typeof(NoteObject));
            Assert.IsInstanceOfType((activity as CollectionPage).Items[1].Obj, typeof(NoteObject));
            Assert.IsNotNull((activity as CollectionPage).PartOf, "partof was null");
            Assert.IsNotNull((activity as CollectionPage).PartOf.Link, "partof link was null");
            Assert.IsNotNull((activity as CollectionPage).PartOf.Link.Href, "partof link href was null");
            Assert.IsNotNull(activity.Id, "the id was null");
            Assert.IsNull((activity as CollectionPage).Next, "the next was not null");
            Assert.IsNull((activity as CollectionPage).Prev, "the prev was not null");
            Assert.AreEqual("http://example.org/foo?page=1", activity.Id.AbsoluteUri, "the id was not correct");
        }

        /// <summary>
        /// Testing Spec example 8
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample008()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example008.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Page 1 of Sally's notes");
            Assert.IsInstanceOfType(activity, typeof(CollectionPage));
            Assert.AreEqual((uint)2, (activity as CollectionPage).TotalItems, "the total items were incorrect");
            Assert.AreEqual(CollectionPage.OrderedCollectionPageType, (activity as CollectionPage).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as CollectionPage).TotalItems, (activity as CollectionPage).OrderedItems.Length, "the item count was incorrect");
            Assert.IsNull((activity as CollectionPage).Items, "the items were not empty");
            Assert.AreEqual("Note", (activity as CollectionPage).OrderedItems[0].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual("Note", (activity as CollectionPage).OrderedItems[1].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual("A Simple Note", (activity as CollectionPage).OrderedItems[0].Obj.Name, "the sub object type was incorrect");
            Assert.AreEqual("Another Simple Note", (activity as CollectionPage).OrderedItems[1].Obj.Name, "the sub object type was incorrect");
            Assert.IsInstanceOfType((activity as CollectionPage).OrderedItems[0].Obj, typeof(NoteObject));
            Assert.IsInstanceOfType((activity as CollectionPage).OrderedItems[1].Obj, typeof(NoteObject));
            Assert.IsNotNull((activity as CollectionPage).PartOf, "partof was null");
            Assert.IsNotNull((activity as CollectionPage).PartOf.Link, "partof link was null");
            Assert.IsNotNull((activity as CollectionPage).PartOf.Link.Href, "partof link href was null");
            Assert.IsNotNull(activity.Id, "the id was null");
            Assert.IsNull((activity as CollectionPage).Next, "the next was not null");
            Assert.IsNull((activity as CollectionPage).Prev, "the prev was not null");
            Assert.AreEqual("http://example.org/foo?page=1", activity.Id.AbsoluteUri, "the id was not correct");
        }

        /// <summary>
        /// Testing Spec example 9
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample009()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example009.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally accepted an invitation to a party");
            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Accept", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(AcceptActivity));
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

        /// <summary>
        /// Testing Spec example 10
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample010()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example010.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally accepted Joe into the club");
            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Accept", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(AcceptActivity));
            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));
            Assert.AreEqual("Person", (activity as Activity).Object[0].Type, "the target object type was incorrect");
            Assert.AreEqual("Joe", (activity as Activity).Object[0].Name, "the target object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(PersonActor));
            Assert.AreEqual("Group", (activity as Activity).Target[0].Obj.Type, "the target object type was incorrect");
            Assert.AreEqual("The Club", (activity as Activity).Target[0].Obj.Name, "the target object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Target[0].Obj, typeof(GroupActor));
        }

        /// <summary>
        /// Testing Spec example 11
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample011()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example011.json"))
                            .Build();


            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally tentatively accepted an invitation to a party");
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

        /// <summary>
        /// Testing Spec example 12
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample012()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example012.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally added an object");
            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Add", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(AddActivity));
            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(ActivityObject));
            Assert.AreEqual("http://example.org/abc", (activity as Activity).Object[0].Url[0].Href, "the object url was not correct");
        }

        /// <summary>
        /// Testing Spec example 13
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample013()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example013.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally added a picture of her cat to her cat picture collection");
            
            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Add", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(AddActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));
            
            Assert.AreEqual("Image", (activity as Activity).Object[0].Type, "the target object type was incorrect");
            Assert.AreEqual("A picture of my cat", (activity as Activity).Object[0].Name, "the target object name was incorrect");
            Assert.AreEqual("http://example.org/img/cat.png", (activity as Activity).Object[0].Url[0].Href, "the target object url name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(ImageObject));

            Assert.AreEqual("Collection", (activity as Activity).Origin.Obj.Type, "the origin object type was incorrect");
            Assert.AreEqual("Camera Roll", (activity as Activity).Origin.Obj.Name, "the origin object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Origin.Obj, typeof(Collection));

            Assert.AreEqual("Collection", (activity as Activity).Target[0].Obj.Type, "the target object type was incorrect");
            Assert.AreEqual("My Cat Pictures", (activity as Activity).Target[0].Obj.Name, "the target object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Target[0].Obj, typeof(Collection));
        }

        /// <summary>
        /// Testing Spec example 14
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample014()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example014.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally arrived at work");

            Assert.IsNotNull((activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Arrive", (activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(ArriveActivity));

            Assert.AreEqual("Sally", (activity as IntransitiveActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as IntransitiveActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as IntransitiveActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Place", activity.Location.Type, "the target object type was incorrect");
            Assert.AreEqual("Work", activity.Location.Name, "the target object name was incorrect");
            Assert.IsInstanceOfType(activity.Location, typeof(PlaceObject));

            Assert.AreEqual("Place", (activity as IntransitiveActivity).Origin.Obj.Type, "the origin object type was incorrect");
            Assert.AreEqual("Home", (activity as IntransitiveActivity).Origin.Obj.Name, "the origin object name was incorrect");
            Assert.IsInstanceOfType((activity as IntransitiveActivity).Origin.Obj, typeof(PlaceObject));
        }

        /// <summary>
        /// Testing Spec example 15
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample015()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example015.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally created a note");

            Assert.IsNotNull((activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Create", (activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(CreateActivity));

            Assert.AreEqual("Sally", (activity as IntransitiveActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as IntransitiveActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as IntransitiveActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Note", (activity as Activity).Object[0].Type, "the target object type was incorrect");
            Assert.AreEqual("A Simple Note", (activity as Activity).Object[0].Name, "the target object name was incorrect");
            Assert.AreEqual("This is a simple note", (activity as Activity).Object[0].Content, "the target object url name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(NoteObject));

        }

        /// <summary>
        /// Testing Spec example 16
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample016()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example016.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally deleted a note");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Delete", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(DeleteActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.IsNotNull((activity as Activity).Object[0].Type, "the target object type was null");
            Assert.AreEqual("http://example.org/notes/1", (activity as Activity).Object[0].Url[0].Href, "the target object url name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(ActivityObject));

            Assert.AreEqual("Collection", (activity as Activity).Origin.Obj.Type, "the origin object type was incorrect");
            Assert.AreEqual("Sally's Notes", (activity as Activity).Origin.Obj.Name, "the origin object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Origin.Obj, typeof(Collection));
        }

        /// <summary>
        /// Testing Spec example 17
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample017()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example017.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally followed John");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Follow", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(FollowActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Person", (activity as Activity).Object[0].Type, "the target object type was not null");
            Assert.AreEqual("John", (activity as Activity).Object[0].Name, "the target object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(PersonActor));
        }

        /// <summary>
        /// Testing Spec example 18
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample018()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example018.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally ignored a note");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Ignore", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(IgnoreActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Link", (activity as Activity).Object[0].Type, "the target object type was not null");
            Assert.AreEqual("http://example.org/notes/1", (activity as Activity).Object[0].Url[0].Href, "the object link was not correct.");
        }

        /// <summary>
        /// Testing Spec example 19
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample019()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example019.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
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

        /// <summary>
        /// Testing Spec example 20
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample020()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example020.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
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

        /// <summary>
        /// Testing Spec example 21
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample021()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example021.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally left a group");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Leave", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(LeaveActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Group", (activity as Activity).Object[0].Type, "the target object type was not null");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(GroupActor));
            Assert.AreEqual("A Simple Group", (activity as Activity).Object[0].Name, "the object link was not correct.");
        }

        /// <summary>
        /// Testing Spec example 22
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample022()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example022.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
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

        /// <summary>
        /// Testing Spec example 23
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample023()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example023.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally offered 50% off to Lewis");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Offer", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(OfferActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("http://www.types.example/ProductOffer", (activity as Activity).Object[0].Type, "the target object type was incorrect");
            Assert.AreEqual("50% Off!", (activity as Activity).Object[0].Name, "the target object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(ActivityObject));

            Assert.AreEqual("Person", (activity as Activity).Target[0].Obj.Type, "the target object type was incorrect");
            Assert.AreEqual("Lewis", (activity as Activity).Target[0].Obj.Name, "the target object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Target[0].Obj, typeof(PersonActor));
        }

        /// <summary>
        /// Testing Spec example 24
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample024()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example024.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
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

        /// <summary>
        /// Testing Spec example 25
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample025()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example025.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
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

        /// <summary>
        /// Testing Spec example 26
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample026()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example026.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
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

        /// <summary>
        /// Testing Spec example 27
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample027()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example027.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
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

        /// <summary>
        /// Testing Spec example 28
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample028()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example028.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "The moderator removed Sally from a group");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Remove", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(RemoveActivity));

            Assert.AreEqual("The Moderator", (activity as RemoveActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("http://example.org/Role", (activity as RemoveActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as RemoveActivity).Actor[0].Obj, typeof(ActivityObject));

            Assert.AreEqual("Person", (activity as Activity).Object[0].Type, "the target object type was incorrect");
            Assert.AreEqual("Sally", (activity as Activity).Object[0].Name, "the target object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(PersonActor));

            Assert.AreEqual("Group", (activity as Activity).Origin.Obj.Type, "the origin object type was incorrect");
            Assert.AreEqual("A Simple Group", (activity as Activity).Origin.Obj.Name, "the origin object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Origin.Obj, typeof(GroupActor));
        }

        /// <summary>
        /// Testing Spec example 29
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample029()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example029.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
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

        /// <summary>
        /// Testing Spec example 30
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample030()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example030.json"))
                            .Build();
            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally updated her note");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Update", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(UpdateActivity));

            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as UpdateActivity).Object[0].Type, "the object link name was incorrect");
            Assert.AreEqual("http://example.org/notes/1", (activity as UpdateActivity).Object[0].Url[0].Href, "the object link href was incorrect");
        }

        /// <summary>
        /// Testing Spec example 31
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample031()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example031.json"))
                            .Build();
            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally read an article");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("View", (activity as Activity).Type, "activity type was correct");
            Assert.IsInstanceOfType(activity, typeof(ViewActivity));

            Assert.AreEqual("Article", (activity as ViewActivity).Object[0].Type, "the object link name was incorrect");
            Assert.IsInstanceOfType((activity as ViewActivity).Object[0], typeof(ArticleObject));
            Assert.AreEqual("What You Should Know About Activity Streams", (activity as ViewActivity).Object[0].Name, "the object name was incorrect");
        }

        /// <summary>
        /// Testing Spec example 32
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample032()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example032.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally listened to a piece of music");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Listen", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(ListenActivity));

            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as ListenActivity).Object[0].Type, "the object link name was incorrect");
            Assert.AreEqual("http://example.org/music.mp3", (activity as ListenActivity).Object[0].Url[0].Href, "the object link href was incorrect");
        }

        /// <summary>
        /// Testing Spec example 33
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample033()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example033.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
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

        /// <summary>
        /// Testing Spec example 34
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample034()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example034.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
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

        /// <summary>
        /// Testing Spec example 35
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample035()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example035.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
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

        /// <summary>
        /// Testing Spec example 36
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample036()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example036.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally announced that she had arrived at work");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Announce", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(AnnounceActivity));

            Assert.AreEqual("Sally", (activity as AnnounceActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as AnnounceActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.AreEqual(new Uri("http://sally.example.org"), (activity as AnnounceActivity).Actor[0].Obj.Id, "the actor object id was incorrect");
            Assert.IsInstanceOfType((activity as AnnounceActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.IsNotNull((activity as Activity).Object[0].Type, "the target object type was null");
            Assert.AreEqual("Arrive", (activity as Activity).Object[0].Type, "the target object url name was incorrect");

            Assert.AreEqual("http://sally.example.org", ((activity as Activity).Object[0] as IntransitiveActivity).Actor[0].Link.Href, "the target object url name was incorrect");
            Assert.AreEqual(ActivityLink.ActivityLinkType, ((activity as Activity).Object[0] as IntransitiveActivity).Actor[0].Link.Type, "the target object url name was incorrect");

            Assert.AreEqual("Place", ((activity as Activity).Object[0] as IntransitiveActivity).Location.Type, " the local type was not correct");
            Assert.AreEqual("Work", ((activity as Activity).Object[0] as IntransitiveActivity).Location.Name, " the local type was not correct");

        }

        /// <summary>
        /// Testing Spec example 37
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample037()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example037.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally blocked Joe");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Block", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(BlockActivity));

            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as BlockActivity).Actor[0].Link.Type, "the actor object type was incorrect");
            Assert.AreEqual("http://sally.example.org", (activity as BlockActivity).Actor[0].Link.Href, "the actor object link href was incorrect");

            Assert.IsNotNull((activity as Activity).Object[0].Type, "the target object type was null");
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as Activity).Object[0].Type, "the target object url name was incorrect");
            Assert.AreEqual("http://joe.example.org", (activity as Activity).Object[0].Url[0].Href, "the target object url name was incorrect");
        }

        /// <summary>
        /// Testing Spec example 38
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample038()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example038.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally flagged an inappropriate note");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Flag", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(FlagActivity));

            Assert.IsNotNull((activity as FlagActivity).Object[0].Type, "the target object type was null");
            Assert.AreEqual("Note", (activity as Activity).Object[0].Type, "the target object url name was incorrect");
            Assert.IsInstanceOfType((activity as FlagActivity).Object[0], typeof(NoteObject));
            Assert.AreEqual("An inappropriate note", (activity as Activity).Object[0].Content, "the target object url name was incorrect");
        }

        /// <summary>
        /// Testing Spec example 39
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample039()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example039.json"))
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

        /// <summary>
        /// Testing Spec example 40
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample040()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example040.json"))
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

        /// <summary>
        /// Testing Spec example 41
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample041()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example041.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Name, "What is the answer?");

            Assert.IsNotNull((activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Question", (activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.AreEqual(DateTime.Parse("2016-05-10T00:00:00Z").ToUniversalTime(), (activity as QuestionActivity).Closed, "the sub object was not set correctly and should not have been");
            Assert.IsInstanceOfType(activity, typeof(QuestionActivity));
        }

        /// <summary>
        /// Testing Spec example 42
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample042()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example042.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Application");
            Assert.AreEqual(activity.Name, "Exampletron 3000");
            Assert.IsInstanceOfType(activity, typeof(ApplicationActor));
        }

        /// <summary>
        /// Testing Spec example 43
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample043()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example043.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Group");
            Assert.AreEqual(activity.Name, "Big Beards of Austin");
            Assert.IsInstanceOfType(activity, typeof(GroupActor));
        }

        /// <summary>
        /// Testing Spec example 44
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample044()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example044.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Organization");
            Assert.AreEqual(activity.Name, "Example Co.");
            Assert.IsInstanceOfType(activity, typeof(OrganizationActor));
        }

        /// <summary>
        /// Testing Spec example 45
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample045()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example045.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Person");
            Assert.AreEqual(activity.Name, "Sally Smith");
            Assert.IsInstanceOfType(activity, typeof(PersonActor));
        }

        /// <summary>
        /// Testing Spec example 46
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample046()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example046.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Service");
            Assert.AreEqual(activity.Name, "Acme Web Service");
            Assert.IsInstanceOfType(activity, typeof(ServiceActor));
        }

        /// <summary>
        /// Testing Spec example 47
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample047()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example047.json"))
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

        /// <summary>
        /// Testing Spec example 48
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample048()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example048.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Article");
            Assert.AreEqual(activity.Name, "What a Crazy Day I Had");
            Assert.IsInstanceOfType(activity, typeof(ArticleObject));
            Assert.AreEqual(activity.Content, "<div>... you will never believe ...</div>");
            Assert.AreEqual(activity.AttributedTo[0].Link.Type, ActivityLink.ActivityLinkType);
            Assert.AreEqual(activity.AttributedTo[0].Link.Href, "http://sally.example.org");
        }

        /// <summary>
        /// Testing Spec example 49
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample049()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example049.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Document");
            Assert.AreEqual(activity.Name, "4Q Sales Forecast");
            Assert.IsInstanceOfType(activity, typeof(DocumentObject));
            Assert.AreEqual(activity.Url[0].Href, "http://example.org/4q-sales-forecast.pdf");
        }

        /// <summary>
        /// Testing Spec example 50
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample050()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example050.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Audio");
            Assert.AreEqual(activity.Name, "Interview With A Famous Technologist");
            Assert.IsInstanceOfType(activity, typeof(AudioObject));
            Assert.AreEqual(activity.Url[0].Href, "http://example.org/podcast.mp3");
            Assert.AreEqual(activity.Url[0].Type, "Link");
            Assert.AreEqual(activity.Url[0].MediaType, "audio/mp3");
        }

        /// <summary>
        /// Testing Spec example 51
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample051()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example051.json"))
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

        /// <summary>
        /// Testing Spec example 52
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample052()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example052.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Video");
            Assert.IsInstanceOfType(activity, typeof(VideoObject));
            Assert.AreEqual(activity.Name, "Puppy Plays With Ball");
            Assert.AreEqual(activity.Url[0].Href, "http://example.org/video.mkv");
            Assert.AreEqual(activity.Duration, XmlConvert.ToTimeSpan("PT2H"));
        }

        /// <summary>
        /// Testing Spec example 53
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample053()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example053.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));
            Assert.AreEqual(activity.Name, "A Word of Warning");
            Assert.AreEqual(activity.Content, "Looks like it is going to rain today. Bring an umbrella!");
        }

        /// <summary>
        /// Testing Spec example 54
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample054()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example054.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Page");
            Assert.IsInstanceOfType(activity, typeof(PageObject));
            Assert.AreEqual(activity.Name, "Omaha Weather Report");
            Assert.AreEqual(activity.Url[0].Href, "http://example.org/weather-in-omaha.html");
        }

        /// <summary>
        /// Testing Spec example 55
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample055()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example055.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Event");
            Assert.IsInstanceOfType(activity, typeof(EventObject));
            Assert.AreEqual(activity.Name, "Going-Away Party for Jim");
            Assert.AreEqual(activity.StartTime, DateTime.Parse("2014-12-31T23:00:00-08:00"));
            Assert.AreEqual(activity.EndTime, DateTime.Parse("2015-01-01T06:00:00-08:00"));
        }

        /// <summary>
        /// Testing Spec example 56
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample056()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example056.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Place");
            Assert.IsInstanceOfType(activity, typeof(PlaceObject));
            Assert.AreEqual(activity.Name, "Work");
        }

        /// <summary>
        /// Testing Spec example 57
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample057()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example057.json"))
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

        /// <summary>
        /// Testing Spec example 58
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample058()
        {
            ActivityLinkBuilder builder = new ActivityLinkBuilder();
            var activityLink = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example058.json"))
                            .Build();

            Assert.IsNotNull(activityLink.Context, "the activity stream context was null");
            Assert.AreEqual(activityLink.Type, "Mention");
            Assert.IsInstanceOfType(activityLink, typeof(MentionLink));
            Assert.AreEqual(activityLink.Name, "Joe");
            Assert.AreEqual((activityLink as MentionLink).Href, "http://example.org/joe");
        }

        /// <summary>
        /// Testing Spec example 59
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample059()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example059.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Profile");
            Assert.IsInstanceOfType(activity, typeof(ProfileObject));
            Assert.AreEqual(activity.Summary, "Sally's Profile");
            Assert.AreEqual((activity as ProfileObject).Describes.Type, "Person");
            Assert.AreEqual((activity as ProfileObject).Describes.Name, "Sally Smith");
            Assert.IsInstanceOfType((activity as ProfileObject).Describes, typeof(PersonActor));
        }

        /// <summary>
        /// Testing Spec example 60
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample060()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example060.json"))
                            .Build();

            Assert.IsNull(activity.Context, "the activity stream context was null"); Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.AreEqual((uint)3, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).Items, "the Items were not empty");
            Assert.AreEqual(Collection.OrderedCollectionType, (activity as Collection).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as Collection).TotalItems, (activity as Collection).OrderedItems.Length, "the item count was incorrect");
            Assert.AreEqual("Image", (activity as Collection).OrderedItems[0].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual("Tombstone", (activity as Collection).OrderedItems[1].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual("Image", (activity as Collection).OrderedItems[2].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual(new Uri("http://image.example/1"), (activity as Collection).OrderedItems[0].Obj.Id, "the sub object id was incorrect");
            Assert.AreEqual(new Uri("http://image.example/2"), (activity as Collection).OrderedItems[1].Obj.Id, "the sub object id was incorrect");
            Assert.AreEqual(new Uri("http://image.example/3"), (activity as Collection).OrderedItems[2].Obj.Id, "the sub object id was incorrect");
            Assert.IsInstanceOfType((activity as Collection).OrderedItems[0].Obj, typeof(ImageObject));
            Assert.IsInstanceOfType((activity as Collection).OrderedItems[1].Obj, typeof(TombstoneObject));
            Assert.IsInstanceOfType((activity as Collection).OrderedItems[2].Obj, typeof(ImageObject));

            Assert.AreEqual(DateTimeKind.Utc, ((activity as Collection).OrderedItems[1].Obj as TombstoneObject).Deleted.Value.Kind, "the tombstone deleted was incorrect");
            Assert.AreEqual(DateTime.Parse("2016-03-17T00:00:00Z").ToUniversalTime(), ((activity as Collection).OrderedItems[1].Obj as TombstoneObject).Deleted.Value, "the tombstone deleted was incorrect");
            Assert.AreEqual("Image", ((activity as Collection).OrderedItems[1].Obj as TombstoneObject).FormerType, "the tombstone FormerType was incorrect");
        }

        /// <summary>
        /// Testing Spec example 61
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample061()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example061.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Name, "Foo");
            Assert.AreEqual(activity.Id, "http://example.org/foo");
        }

        /// <summary>
        /// Testing Spec example 62
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample062()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example062.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "A foo");
            Assert.AreEqual(activity.Type, "http://example.org/Foo");
        }

        /// <summary>
        /// Testing Spec example 63
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample063()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example063.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally offered the Foo object");
            Assert.AreEqual(activity.Type, "Offer");
            Assert.IsInstanceOfType(activity, typeof(OfferActivity));
            Assert.IsInstanceOfType(activity, typeof(OfferActivity));
            Assert.AreEqual((activity as OfferActivity).Object[0].Url[0].Href, "http://example.org/foo");
            Assert.AreEqual((activity as OfferActivity).Actor[0].Link.Href, "http://sally.example.org");
            Assert.AreEqual((activity as OfferActivity).Object[0].Type, ActivityLink.ActivityLinkType);
            Assert.AreEqual((activity as OfferActivity).Actor[0].Link.Type, ActivityLink.ActivityLinkType);
        }

        /// <summary>
        /// Testing Spec example 64
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample064()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example064.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally offered the Foo object");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Offer", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(OfferActivity));

            Assert.AreEqual(new Uri("http://sally.example.org/"), (activity as OfferActivity).Actor[0].Obj.Id, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as OfferActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.AreEqual("Sally", (activity as OfferActivity).Actor[0].Obj.Summary, "the actor object type was incorrect");
            Assert.AreEqual(new Uri("http://sally.example.org"), (activity as OfferActivity).Actor[0].Obj.Id, "the actor object id was incorrect");
            Assert.IsInstanceOfType((activity as OfferActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.IsNotNull((activity as OfferActivity).Object[0].Type, "the target object type was null");
            Assert.AreEqual("http://example.org/foo", (activity as OfferActivity).Object[0].Url[0].Href, "the target object url name was incorrect");
        }

        /// <summary>
        /// Testing Spec example 65
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample065()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example065.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally and Joe offered the Foo object");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Offer", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(OfferActivity));

            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as OfferActivity).Actor[0].Link.Type, "the actor object name was incorrect");
            Assert.AreEqual(new Uri("http://joe.example.org"), (activity as OfferActivity).Actor[0].Link.Href, "the actor object name was incorrect");

            Assert.AreEqual("Person", (activity as OfferActivity).Actor[1].Obj.Type, "the actor object type was incorrect");
            Assert.AreEqual("Sally", (activity as OfferActivity).Actor[1].Obj.Name, "the actor object type was incorrect");
            Assert.AreEqual(new Uri("http://sally.example.org"), (activity as OfferActivity).Actor[1].Obj.Id, "the actor object id was incorrect");
            Assert.IsInstanceOfType((activity as OfferActivity).Actor[1].Obj, typeof(PersonActor));

            Assert.IsNotNull((activity as OfferActivity).Object[0].Type, "the target object type was null");
            Assert.AreEqual("http://example.org/foo", (activity as OfferActivity).Object[0].Url[0].Href, "the target object url name was incorrect");
        }

        /// <summary>
        /// Testing Spec example 66
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample066()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example066.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Name, "Have you seen my cat?");

            Assert.IsNull((activity as Activity), "the sub object was null and should not have been");
            Assert.IsNotNull(activity.Type, "the sub object was null and should not have been");
            Assert.AreEqual("Note", activity.Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));

            Assert.IsNotNull((activity as NoteObject).Attachment);
            Assert.AreEqual("Image", (activity as NoteObject).Attachment[0].Obj.Type);
            Assert.IsInstanceOfType((activity as NoteObject).Attachment[0].Obj, typeof(ImageObject));
            Assert.AreEqual("This is what he looks like.", (activity as NoteObject).Attachment[0].Obj.Content);
            Assert.AreEqual(new Uri("http://example.org/cat.jpeg"), (activity as NoteObject).Attachment[0].Obj.Url[0].Href);
        }

        /// <summary>
        /// Testing Spec example 67
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample067()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example067.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Name, "My cat taking a nap");

            Assert.IsNull((activity as Activity));
            Assert.IsNotNull(activity.Type);
            Assert.AreEqual("Image", activity.Type);
            Assert.IsInstanceOfType(activity, typeof(ImageObject));
            Assert.AreEqual("http://example.org/cat.jpeg", (activity as ImageObject).Url[0].Href);
            Assert.AreEqual("Person", (activity as ImageObject).AttributedTo[0].Obj.Type);
            Assert.AreEqual("Sally", (activity as ImageObject).AttributedTo[0].Obj.Name);
        }

        /// <summary>
        /// Testing Spec example 68
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample068()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example068.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Name, "My cat taking a nap");

            Assert.IsNull((activity as Activity));
            Assert.IsNotNull(activity.Type);
            Assert.AreEqual("Image", activity.Type);
            Assert.IsInstanceOfType(activity, typeof(ImageObject));
            Assert.AreEqual("http://example.org/cat.jpeg", (activity as ImageObject).Url[0].Href);
            Assert.IsInstanceOfType((activity as ImageObject).AttributedTo[1].Obj, typeof(PersonActor));
            Assert.AreEqual("Person", (activity as ImageObject).AttributedTo[1].Obj.Type);
            Assert.AreEqual("Sally", (activity as ImageObject).AttributedTo[1].Obj.Name);
        }

        /// <summary>
        /// Testing Spec example 69
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample069()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example069.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Name, "Holiday announcement");

            Assert.IsNull(activity as Activity);
            Assert.IsNotNull(activity.Type);
            Assert.AreEqual("Note", activity.Type);
            Assert.IsInstanceOfType(activity, typeof(NoteObject));
            Assert.IsNotNull(activity.Content);
            Assert.AreEqual("Thursday will be a company-wide holiday. Enjoy your day off!", activity.Content);
            Assert.IsNotNull(activity.Audience);
            Assert.AreEqual("http://example.org/Organization", activity.Audience[0].Obj.Type);
            Assert.AreEqual("ExampleCo LLC", activity.Audience[0].Obj.Name);
        }

        /// <summary>
        /// Testing Spec example 70
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample070()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example070.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally offered a post to John");

            Assert.IsNotNull(activity.Type);
            Assert.AreEqual("Offer", activity.Type);
            Assert.IsInstanceOfType(activity, typeof(OfferActivity));
            Assert.AreEqual("http://sally.example.org", (activity as OfferActivity).Actor[0].Link.Href);
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as OfferActivity).Actor[0].Link.Type);
            Assert.AreEqual("http://example.org/posts/1", (activity as OfferActivity).Object[0].Url[0].Href);
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as OfferActivity).Object[0].Type);
            Assert.AreEqual("http://john.example.org", (activity as OfferActivity).Target[0].Link.Href);
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as OfferActivity).Target[0].Link.Type);
            Assert.AreEqual("http://joe.example.org", (activity as OfferActivity).bcc[0].Link.Href);
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as OfferActivity).bcc[0].Link.Type);
        }

        /// <summary>
        /// Testing Spec example 71
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample071()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example071.json"))
                            .Build();

            Assert.IsNotNull(activity.Type);
            Assert.AreEqual("Offer", activity.Type);
            Assert.AreEqual("Sally offered a post to John", activity.Summary);
            Assert.IsInstanceOfType(activity, typeof(OfferActivity));
            Assert.AreEqual("http://sally.example.org", (activity as OfferActivity).Actor[0].Link.Href);
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as OfferActivity).Actor[0].Link.Type);
            Assert.AreEqual("http://example.org/posts/1", (activity as OfferActivity).Object[0].Url[0].Href);
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as OfferActivity).Object[0].Type);
            Assert.AreEqual("http://john.example.org", (activity as OfferActivity).Target[0].Link.Href);
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as OfferActivity).Target[0].Link.Type);
            Assert.AreEqual("http://joe.example.org", (activity as OfferActivity).Bto[0].Link.Href);
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as OfferActivity).Bto[0].Link.Type);
        }

        /// <summary>
        /// Testing Spec example 72
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample072()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example072.json"))
                            .Build();

            Assert.IsNotNull(activity.Type);
            Assert.AreEqual("Offer", activity.Type);
            Assert.AreEqual("Sally offered a post to John", activity.Summary);
            Assert.IsInstanceOfType(activity, typeof(OfferActivity));
            Assert.AreEqual("http://sally.example.org", (activity as OfferActivity).Actor[0].Link.Href);
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as OfferActivity).Actor[0].Link.Type);
            Assert.AreEqual("http://example.org/posts/1", (activity as OfferActivity).Object[0].Url[0].Href);
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as OfferActivity).Object[0].Type);
            Assert.AreEqual("http://john.example.org", (activity as OfferActivity).Target[0].Link.Href);
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as OfferActivity).Target[0].Link.Type);
            Assert.AreEqual("http://joe.example.org", (activity as OfferActivity).CC[0].Link.Href);
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as OfferActivity).CC[0].Link.Type);
        }

        /// <summary>
        /// Testing Spec example 73
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample073()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example073.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null"); Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.IsNotNull(activity.Summary, "Activities in context 1");
            Assert.AreEqual((uint)2, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.AreEqual(Collection.CollectionType, (activity as Collection).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as Collection).TotalItems, (activity as Collection).Items.Length, "the item count was incorrect");
            Assert.AreEqual("Offer", (activity as Collection).Items[0].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual("Like", (activity as Collection).Items[1].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual("Link", ((activity as Collection).Items[0].Obj as OfferActivity).Actor[0].Link.Type, "the sub object id was incorrect");

            Assert.AreEqual("http://sally.example.org", ((activity as Collection).Items[0].Obj as OfferActivity).Actor[0].Link.Href, "the sub object id was incorrect");
            Assert.AreEqual("http://example.org/posts/1", ((activity as Collection).Items[0].Obj as OfferActivity).Object[0].Url[0].Href, "the sub object id was incorrect");
            Assert.AreEqual("http://john.example.org", ((activity as Collection).Items[0].Obj as OfferActivity).Target[0].Link.Href, "the sub object id was incorrect");
            Assert.AreEqual(new Uri("http://example.org/contexts/1"), ((activity as Collection).Items[0].Obj as OfferActivity).Context, "the sub object id was incorrect");

            Assert.AreEqual("http://joe.example.org", ((activity as Collection).Items[1].Obj as LikeActivity).Actor[0].Link.Href, "the sub object id was incorrect");
            Assert.AreEqual("http://example.org/posts/2", ((activity as Collection).Items[1].Obj as LikeActivity).Object[0].Url[0].Href, "the sub object id was incorrect");
            Assert.AreEqual(new Uri("http://example.org/contexts/1"), ((activity as Collection).Items[1].Obj as LikeActivity).Context, "the sub object id was incorrect");
        }

        /// <summary>
        /// Testing Spec example 74
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample074()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example074.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null"); Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.IsNotNull(activity.Summary, "Sally's blog posts");
            Assert.IsNotNull((activity as Collection).Current);
            Assert.IsNotNull((activity as Collection).Current.Link);
            Assert.AreEqual("Link", (activity as Collection).Current.Link.Type);
            Assert.AreEqual("http://example.org/collection", (activity as Collection).Current.Link.Href);
            Assert.IsInstanceOfType(activity, typeof(Collection));

            Assert.AreEqual((uint)3, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.AreEqual(Collection.CollectionType, (activity as Collection).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as Collection).TotalItems, (activity as Collection).Items.Length, "the item count was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[0].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[1].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[2].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/1", (activity as Collection).Items[0].Link.Href, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/2", (activity as Collection).Items[1].Link.Href, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/3", (activity as Collection).Items[2].Link.Href, "the sub object link was incorrect");
        }

        /// <summary>
        /// Testing Spec example 75
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample075()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example075.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null"); Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.IsNotNull(activity.Summary, "Sally's blog posts");
            Assert.IsNotNull((activity as Collection).Current);
            Assert.IsNotNull((activity as Collection).Current.Link);
            Assert.AreEqual("Link", (activity as Collection).Current.Link.Type);
            // ("Link", (activity as Collection).Current.Link.Summary); summary does not exist https://github.com/w3c/activitystreams/issues/496
            Assert.AreEqual("http://example.org/collection", (activity as Collection).Current.Link.Href);
            Assert.IsInstanceOfType(activity, typeof(Collection));

            Assert.AreEqual((uint)3, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.AreEqual(Collection.CollectionType, (activity as Collection).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as Collection).TotalItems, (activity as Collection).Items.Length, "the item count was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[0].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[1].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[2].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/1", (activity as Collection).Items[0].Link.Href, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/2", (activity as Collection).Items[1].Link.Href, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/3", (activity as Collection).Items[2].Link.Href, "the sub object link was incorrect");
        }

        /// <summary>
        /// Testing Spec example 76
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample076()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example076.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null"); Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.IsNotNull(activity.Summary, "Sally's blog posts");
            Assert.IsInstanceOfType(activity, typeof(Collection));

            Assert.AreEqual((uint)3, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.IsNull((activity as Collection).Items, "the Items were not empty");
            Assert.AreEqual(Collection.CollectionType, (activity as Collection).Type, "The type in the collection was wrong");

            Assert.AreEqual("http://example.org/collection?page=0", (activity as Collection).First.Link.Href, "the sub object link was incorrect");
        }

        /// <summary>
        /// Testing Spec example 77
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample077()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example077.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null"); Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.IsNotNull(activity.Summary, "Sally's blog posts");
            Assert.IsInstanceOfType(activity, typeof(Collection));

            Assert.AreEqual((uint)3, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.IsNull((activity as Collection).Items, "the Items were not empty");
            Assert.AreEqual(Collection.CollectionType, (activity as Collection).Type, "The type in the collection was wrong");

            Assert.AreEqual("http://example.org/collection?page=0", (activity as Collection).First.Link.Href, "the sub object link was incorrect");
        }

        /// <summary>
        /// Testing Spec example 78
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample078()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example078.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "A simple note");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));

            Assert.AreEqual(activity.Generator[0].Obj.Type, "Application");
            Assert.IsInstanceOfType(activity.Generator[0].Obj, typeof(ApplicationActor));
            Assert.AreEqual(activity.Generator[0].Obj.Name, "Exampletron 3000");
        }

        /// <summary>
        /// Testing Spec example 79
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample079()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example079.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "A simple note");
            Assert.AreEqual(activity.Content, "This is all there is.");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));

            Assert.AreEqual(activity.Icons[0].Obj.Type, "Image");
            Assert.IsInstanceOfType(activity.Icons[0].Obj, typeof(Icon));
            Assert.AreEqual(activity.Icons[0].Obj.Url[0].Href, "http://example.org/note.png");
            Assert.AreEqual(activity.Icons[0].Obj.Name, "Note icon");
            Assert.AreEqual((activity.Icons[0].Obj as Icon).Width, 16);
            Assert.AreEqual((activity.Icons[0].Obj as Icon).Height, 18);
        }

        /// <summary>
        /// Testing Spec example 80
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample080()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example080.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "A simple note");
            Assert.AreEqual(activity.Content, "A simple note");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));

            Assert.AreEqual(activity.Icons[0].Obj.Type, "Image");
            Assert.IsInstanceOfType(activity.Icons[0].Obj, typeof(Icon));
            Assert.AreEqual(activity.Icons[0].Obj.Url[0].Href, "http://example.org/note1.png");
            Assert.AreEqual(activity.Icons[0].Obj.Summary, "Note (16x16)");
            Assert.AreEqual((activity.Icons[0].Obj as Icon).Width, 16);
            Assert.AreEqual((activity.Icons[0].Obj as Icon).Height, 16);

            Assert.AreEqual(activity.Icons[1].Obj.Type, "Image");
            Assert.IsInstanceOfType(activity.Icons[1].Obj, typeof(Icon));
            Assert.AreEqual(activity.Icons[1].Obj.Url[0].Href, "http://example.org/note2.png");
            Assert.AreEqual(activity.Icons[1].Obj.Summary, "Note (32x32)");
            Assert.AreEqual((activity.Icons[1].Obj as Icon).Width, 32);
            Assert.AreEqual((activity.Icons[1].Obj as Icon).Height, 32);
        }

        /// <summary>
        /// Testing Spec example 81
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample081()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example081.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsNull(activity.Summary);
            Assert.AreEqual(activity.Content, "This is all there is.");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));

            Assert.AreEqual(activity.Images[0].Obj.Type, "Image");
            Assert.IsInstanceOfType(activity.Images[0].Obj, typeof(ImageObject));
            Assert.AreEqual(activity.Images[0].Obj.Url[0].Href, "http://example.org/cat.png");
            Assert.AreEqual(activity.Images[0].Obj.Name, "A Cat");
        }

        /// <summary>
        /// Testing Spec example 82
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample082()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example082.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsNull(activity.Summary);
            Assert.AreEqual(activity.Content, "This is all there is.");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));

            Assert.AreEqual(activity.Images[0].Obj.Type, "Image");
            Assert.IsInstanceOfType(activity.Images[0].Obj, typeof(ImageObject));
            Assert.AreEqual(activity.Images[0].Obj.Url[0].Href, "http://example.org/cat1.png");
            Assert.AreEqual(activity.Images[0].Obj.Name, "Cat 1");

            Assert.AreEqual(activity.Images[1].Obj.Type, "Image");
            Assert.IsInstanceOfType(activity.Images[1].Obj, typeof(ImageObject));
            Assert.AreEqual(activity.Images[1].Obj.Url[0].Href, "http://example.org/cat2.png");
            Assert.AreEqual(activity.Images[1].Obj.Name, "Cat 2");
        }

        /// <summary>
        /// Testing Spec example 83
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample083()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example083.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsNotNull(activity.Summary);
            Assert.AreEqual(activity.Summary, "A simple note");
            Assert.AreEqual(activity.Content, "This is all there is.");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));

            Assert.AreEqual(activity.InReplyTo[0].Obj.Summary, "Previous note");
            Assert.AreEqual(activity.InReplyTo[0].Obj.Type, "Note");
            Assert.AreEqual(activity.InReplyTo[0].Obj.Content, "What else is there?");
            Assert.IsInstanceOfType(activity.InReplyTo[0].Obj, typeof(NoteObject));
        }

        /// <summary>
        /// Testing Spec example 84
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample084()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example084.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsNotNull(activity.Summary);
            Assert.AreEqual(activity.Summary, "A simple note");
            Assert.AreEqual(activity.Content, "This is all there is.");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));

            Assert.AreEqual(activity.InReplyTo[0].Link.Href, "http://example.org/posts/1");
            Assert.AreEqual(activity.InReplyTo[0].Link.Type, ActivityLink.ActivityLinkType);
        }

        /// <summary>
        /// Testing Spec example 85
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample085()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example085.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsNotNull(activity.Summary);
            Assert.AreEqual(activity.Summary, "Sally listened to a piece of music on the Acme Music Service");
            Assert.IsNull(activity.Content);
            Assert.AreEqual(activity.Type, "Listen");
            Assert.IsInstanceOfType(activity, typeof(ListenActivity));

            Assert.AreEqual("Person", (activity as ListenActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.AreEqual("Sally", (activity as ListenActivity).Actor[0].Obj.Name, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as ListenActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.IsNotNull((activity as ListenActivity).Object[0].Type, "the target object type was null");
            Assert.AreEqual("http://example.org/foo.mp3", (activity as ListenActivity).Object[0].Url[0].Href, "the target object url name was incorrect");


            Assert.AreEqual("Service", (activity as ListenActivity).Instrument[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as ListenActivity).Instrument[0].Obj, typeof(ServiceActor));
            Assert.AreEqual("Acme Music Service", (activity as ListenActivity).Instrument[0].Obj.Name, "the actor object type was incorrect");
        }

        /// <summary>
        /// Testing Spec example 86
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample086()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example086.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.IsNotNull(activity.Summary, "A collection");

            Assert.AreEqual((uint)3, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.IsNull((activity as Collection).Items, "the Items were not empty");
            Assert.AreEqual(Collection.CollectionType, (activity as Collection).Type, "The type in the collection was wrong");

            Assert.AreEqual("http://example.org/collection?page=1", (activity as Collection).Last.Link.Href, "the sub object link was incorrect");
        }

        /// <summary>
        /// Testing Spec example 87
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample087()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example087.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.IsNotNull(activity.Summary, "A collection");

            Assert.AreEqual((uint)5, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.IsNull((activity as Collection).Items, "the Items were not empty");
            Assert.AreEqual(Collection.CollectionType, (activity as Collection).Type, "The type in the collection was wrong");

            Assert.AreEqual("http://example.org/collection?page=1", (activity as Collection).Last.Link.Href, "the sub object link was incorrect");
        }

        /// <summary>
        /// Testing Spec example 88
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample088()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example088.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Person");
            Assert.IsInstanceOfType(activity, typeof(PersonActor));
            Assert.AreEqual(activity.Name, "Sally");
            Assert.AreEqual((activity as PersonActor).Location.Latitude, 56.78);
            Assert.AreEqual((activity as PersonActor).Location.Longitude, 12.34);
            Assert.AreEqual((activity as PersonActor).Location.Altitude, 90);
            Assert.AreEqual((activity as PersonActor).Location.Units, "m");
        }

        /// <summary>
        /// Testing Spec example 89
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample089()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example089.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsNotNull(activity.Summary, "Sally's notes");
            Assert.IsInstanceOfType(activity, typeof(Collection));

            Assert.AreEqual((uint)2, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.AreEqual(Collection.CollectionType, (activity as Collection).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as Collection).TotalItems, (activity as Collection).Items.Length, "the item count was incorrect");

            Assert.AreEqual("Reminder for Going-Away Party", (activity as Collection).Items[0].Obj.Name, "the sub object link was incorrect");
            Assert.AreEqual("Note", (activity as Collection).Items[0].Obj.Type, "the sub object link was incorrect");
            Assert.AreEqual("Meeting 2016-11-17", (activity as Collection).Items[1].Obj.Name, "the sub object link was incorrect");
            Assert.AreEqual("Note", (activity as Collection).Items[1].Obj.Type, "the sub object link was incorrect");
        }

        /// <summary>
        /// Testing Spec example 90
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample090()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example090.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsNotNull(activity.Summary, "Sally's notes");
            Assert.IsInstanceOfType(activity, typeof(Collection));

            Assert.AreEqual((uint)2, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).Items, "the Items were not empty");
            Assert.AreEqual(Collection.OrderedCollectionType, (activity as Collection).Type, "The type in the collection was wrong");
            Assert.AreEqual((int)(activity as Collection).TotalItems, (activity as Collection).OrderedItems.Length, "the item count was incorrect");

            Assert.AreEqual("Reminder for Going-Away Party", (activity as Collection).OrderedItems[1].Obj.Name, "the sub object link was incorrect");
            Assert.AreEqual("Note", (activity as Collection).OrderedItems[1].Obj.Type, "the sub object link was incorrect");
            Assert.AreEqual("Meeting 2016-11-17", (activity as Collection).OrderedItems[0].Obj.Name, "the sub object link was incorrect");
            Assert.AreEqual("Note", (activity as Collection).OrderedItems[0].Obj.Type, "the sub object link was incorrect");
        }

        /// <summary>
        /// Testing Spec example 91
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample091()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example091.json"))
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

        /// <summary>
        /// Testing Spec example 92
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample092()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example092.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Name, "What is the answer?");

            Assert.IsNotNull((activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Question", (activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(QuestionActivity));

            Assert.AreEqual(2, (activity as QuestionActivity).AnyOf.Length, "one of count was set incorrectly.");
            Assert.IsInstanceOfType((activity as QuestionActivity).AnyOf[0].Obj, typeof(NoteObject));
            Assert.AreEqual("Note", (activity as QuestionActivity).AnyOf[0].Obj.Type, "one of type was set incorrectly.");
            Assert.AreEqual("Option A", (activity as QuestionActivity).AnyOf[0].Obj.Name, "one of name was set incorrectly.");

            Assert.IsInstanceOfType((activity as QuestionActivity).AnyOf[1].Obj, typeof(NoteObject));
            Assert.AreEqual("Note", (activity as QuestionActivity).AnyOf[1].Obj.Type, "one of type was set incorrectly.");
            Assert.AreEqual("Option B", (activity as QuestionActivity).AnyOf[1].Obj.Name, "one of name was set incorrectly.");
        }

        /// <summary>
        /// Testing Spec example 93
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample093()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example093.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Name, "What is the answer?");

            Assert.IsNotNull((activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Question", (activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.AreEqual(DateTime.Parse("2016-05-10T00:00:00Z").ToUniversalTime(), (activity as QuestionActivity).Closed, "the sub object was not set correctly and should not have been");
            Assert.IsInstanceOfType(activity, typeof(QuestionActivity));
        }

        /// <summary>
        /// Testing Spec example 94
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample094()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example094.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally moved a post from List A to List B");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Move", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(MoveActivity));

            Assert.AreEqual("http://sally.example.org", (activity as MoveActivity).Actor[0].Link.Href, "the actor object name was incorrect");
            Assert.AreEqual("Link", (activity as MoveActivity).Actor[0].Link.Type, "the actor object type was incorrect");

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

        /// <summary>
        /// Testing Spec example 95
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample095()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example095.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null"); Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.IsNotNull(activity.Summary, "Page 2 of Sally's blog posts");
            Assert.IsInstanceOfType(activity, typeof(CollectionPage));
            Assert.IsNotNull((activity as CollectionPage).Next);
            Assert.AreEqual("Link", (activity as CollectionPage).Next.Type);
            Assert.AreEqual("http://example.org/collection?page=2", (activity as CollectionPage).Next.Href);

            Assert.AreEqual((uint)3, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.AreEqual(CollectionPage.CollectionPageType, (activity as Collection).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as Collection).TotalItems, (activity as Collection).Items.Length, "the item count was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[0].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[1].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[2].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/1", (activity as Collection).Items[0].Link.Href, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/2", (activity as Collection).Items[1].Link.Href, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/3", (activity as Collection).Items[2].Link.Href, "the sub object link was incorrect");
        }

        /// <summary>
        /// Testing Spec example 96
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample096()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example096.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null"); Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.IsNotNull(activity.Summary, "Page 2 of Sally's blog posts");
            Assert.IsInstanceOfType(activity, typeof(CollectionPage));
            Assert.IsNotNull((activity as CollectionPage).Next);
            Assert.AreEqual("Link", (activity as CollectionPage).Next.Type);
            Assert.AreEqual("http://example.org/collection?page=2", (activity as CollectionPage).Next.Href);
            Assert.AreEqual("Next Page", (activity as CollectionPage).Next.Name);

            Assert.AreEqual((uint)3, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.AreEqual(CollectionPage.CollectionPageType, (activity as Collection).Type, "The type in the collectio was wrong");

            Assert.AreEqual("Link", (activity as Collection).Items[0].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[1].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[2].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/1", (activity as Collection).Items[0].Link.Href, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/2", (activity as Collection).Items[1].Link.Href, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/3", (activity as Collection).Items[2].Link.Href, "the sub object link was incorrect");
        }

        /// <summary>
        /// Testing Spec example 97
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample097()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example097.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally liked a post");

            Assert.IsNotNull((activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(LikeActivity));
            Assert.AreEqual("Like", (activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("http://sally.example.org", (activity as LikeActivity).Actor[0].Link.Href, "the sub object was not set correctly and should not have been");
            Assert.AreEqual("http://example.org/posts/1", (activity as LikeActivity).Object[0].Url[0].Href, "the sub object was not set correctly and should not have been");
        }

        /// <summary>
        /// Testing Spec example 98
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample098()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example098.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");

            Assert.IsNotNull((activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(LikeActivity));
            Assert.AreEqual("Like", (activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("http://sally.example.org", (activity as LikeActivity).Actor[0].Link.Href, "the sub object was not set correctly and should not have been");
            Assert.AreEqual("Note", (activity as LikeActivity).Object[0].Type, "the sub object was not set correctly and should not have been");
            Assert.AreEqual("A simple note", (activity as LikeActivity).Object[0].Content, "the sub object was not set correctly and should not have been");
            Assert.IsInstanceOfType((activity as LikeActivity).Object[0], typeof(NoteObject));
        }

        /// <summary>
        /// Testing Spec example 99
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample099()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example099.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");

            Assert.IsNotNull((activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(LikeActivity));
            Assert.AreEqual("Like", (activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("http://sally.example.org", (activity as LikeActivity).Actor[0].Link.Href, "the sub object was not set correctly and should not have been");
            
            Assert.AreEqual("Link", (activity as LikeActivity).Object[0].Type, "the sub object was not set correctly and should not have been");
            Assert.AreEqual("http://example.org/posts/1", (activity as LikeActivity).Object[0].Url[0].Href, "the sub object was not set correctly and should not have been");

            Assert.AreEqual("Note", (activity as LikeActivity).Object[1].Type, "the sub object was not set correctly and should not have been");
            Assert.AreEqual("A simple note", (activity as LikeActivity).Object[1].Summary, "the sub object was not set correctly and should not have been");
            Assert.AreEqual("That is a tree.", (activity as LikeActivity).Object[1].Content, "the sub object was not set correctly and should not have been");
            Assert.IsInstanceOfType((activity as LikeActivity).Object[1], typeof(NoteObject));
        }

        /// <summary>
        /// Testing Spec example 100
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample100()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example100.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null"); Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.IsNotNull(activity.Summary, "Page 1 of Sally's blog posts");
            Assert.IsInstanceOfType(activity, typeof(CollectionPage));
            Assert.IsNotNull((activity as CollectionPage).Prev);
            Assert.AreEqual("Link", (activity as CollectionPage).Prev.Type);
            Assert.AreEqual("http://example.org/collection?page=1", (activity as CollectionPage).Prev.Href);

            Assert.AreEqual((uint)3, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.AreEqual(CollectionPage.CollectionPageType, (activity as Collection).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as Collection).TotalItems, (activity as Collection).Items.Length, "the item count was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[0].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[1].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[2].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/1", (activity as Collection).Items[0].Link.Href, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/2", (activity as Collection).Items[1].Link.Href, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/3", (activity as Collection).Items[2].Link.Href, "the sub object link was incorrect");
        }

        /// <summary>
        /// Testing Spec example 101
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample101()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example101.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null"); Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.IsNotNull(activity.Summary, "Page 1 of Sally's blog posts");
            Assert.IsInstanceOfType(activity, typeof(CollectionPage));
            Assert.IsNotNull((activity as CollectionPage).Prev);
            Assert.AreEqual("Link", (activity as CollectionPage).Prev.Type);
            Assert.AreEqual("http://example.org/collection?page=1", (activity as CollectionPage).Prev.Href);
            Assert.AreEqual("Previous Page", (activity as CollectionPage).Prev.Name);

            Assert.AreEqual((uint)3, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.AreEqual(CollectionPage.CollectionPageType, (activity as Collection).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as Collection).TotalItems, (activity as Collection).Items.Length, "the item count was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[0].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[1].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("Link", (activity as Collection).Items[2].Link.Type, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/1", (activity as Collection).Items[0].Link.Href, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/2", (activity as Collection).Items[1].Link.Href, "the sub object link was incorrect");
            Assert.AreEqual("http://example.org/posts/3", (activity as Collection).Items[2].Link.Href, "the sub object link was incorrect");
        }

        /// <summary>
        /// Testing Spec example 102
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample102()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example102.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Video");
            Assert.IsInstanceOfType(activity, typeof(VideoObject));
            Assert.AreEqual(activity.Name, "Cool New Movie");
            Assert.AreEqual(activity.Duration, XmlConvert.ToTimeSpan("PT2H30M"));
            Assert.AreEqual(activity.Preview[0].Obj.Type, "Video");
            Assert.IsInstanceOfType(activity.Preview[0].Obj, typeof(VideoObject));
            Assert.AreEqual(activity.Preview[0].Obj.Name, "Trailer");
            Assert.AreEqual(activity.Preview[0].Obj.Duration, XmlConvert.ToTimeSpan("PT1M"));
            Assert.AreEqual(activity.Preview[0].Obj.Url[0].Href, "http://example.org/trailer.mkv");
            Assert.AreEqual(activity.Preview[0].Obj.Url[0].MediaType, "video/mkv");
        }

        /// <summary>
        /// Testing Spec example 103
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample103()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example103.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Activity");
            Assert.IsInstanceOfType(activity, typeof(Activity));
            Assert.AreEqual(activity.Summary, "Sally checked that her flight was on time");

            Assert.IsNotNull((activity as Activity).Actor, "the actor was null and should not have been");
            Assert.AreEqual("http://sally.example.org", (activity as Activity).Actor[0].Link.Href, "the actor name was not correct");

            Assert.IsNotNull((activity as Activity).Object[0], "the actor was null and should not have been");
            Assert.AreEqual("http://example.org/flights/1", (activity as Activity).Object[0].Url[0].Href, "the actor name was not correct");

            Assert.IsNotNull((activity as Activity).Result, "the actor was null and should not have been");
            Assert.AreEqual("http://www.types.example/flightstatus", (activity as Activity).Result[0].Obj.Type, "the actor name was not correct");
            Assert.AreEqual("On Time", (activity as Activity).Result[0].Obj.Name, "the actor name was not correct");

        }

        /// <summary>
        /// Testing Spec example 104
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample104()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example104.json"))
                            .Build(); 
            
            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));
            Assert.AreEqual(activity.Summary, "A simple note");
            Assert.AreEqual(activity.Content, "I am fine.");
            Assert.AreEqual(activity.Id, "http://www.test.example/notes/1");


            Assert.AreEqual(activity.Replies.Type, "Collection");
            Assert.AreEqual((activity.Replies as Collection).TotalItems, (uint)1);

            Assert.AreEqual((int)(activity.Replies as Collection).TotalItems, (activity.Replies as Collection).Items.Length, "the item count was incorrect");
            Assert.AreEqual("A response to the note", (activity.Replies as Collection).Items[0].Obj.Summary, "the sub object was incorrect");
            Assert.AreEqual("Note", (activity.Replies as Collection).Items[0].Obj.Type, "the sub object was incorrect");
            Assert.AreEqual("I am glad to hear it.", (activity.Replies as Collection).Items[0].Obj.Content, "the sub object was incorrect");
            Assert.AreEqual("http://www.test.example/notes/1", (activity.Replies as Collection).Items[0].Obj.InReplyTo[0].Link.Href, "the sub object was incorrect");

        }

        /// <summary>
        /// Testing Spec example 105
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample105()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example105.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Image");
            Assert.IsInstanceOfType(activity, typeof(ImageObject));
            Assert.AreEqual(activity.Summary, "Picture of Sally");
            Assert.AreEqual(activity.Url[0].Href, "http://example.org/sally.jpg");
            Assert.AreEqual((activity as ImageObject).Tag[0].Obj.Id, "http://sally.example.org");
            Assert.AreEqual((activity as ImageObject).Tag[0].Obj.Type, "Person");
            Assert.AreEqual((activity as ImageObject).Tag[0].Obj.Name, "Sally");
        }

        /// <summary>
        /// Testing Spec example 106
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample106()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example106.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Offer");
            Assert.IsInstanceOfType(activity, typeof(Activity));
            Assert.AreEqual(activity.Summary, "Sally offered the post to John");

            Assert.IsNotNull((activity as Activity).Actor, "the actor was null and should not have been");
            Assert.AreEqual("http://sally.example.org", (activity as Activity).Actor[0].Link.Href, "the actor name was not correct");

            Assert.IsNotNull((activity as Activity).Object[0], "the actor was null and should not have been");
            Assert.AreEqual("http://example.org/posts/1", (activity as Activity).Object[0].Url[0].Href, "the actor name was not correct");

            Assert.IsNotNull((activity as Activity).Target, "the actor was null and should not have been");
            Assert.AreEqual("http://john.example.org", (activity as Activity).Target[0].Link.Href, "the actor name was not correct");
        }

        /// <summary>
        /// Testing Spec example 107
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample107()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example107.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Offer");
            Assert.IsInstanceOfType(activity, typeof(Activity));
            Assert.AreEqual(activity.Summary, "Sally offered the post to John");

            Assert.IsNotNull((activity as Activity).Actor, "the actor was null and should not have been");
            Assert.AreEqual("http://sally.example.org", (activity as Activity).Actor[0].Link.Href, "the actor name was not correct");

            Assert.IsNotNull((activity as Activity).Object[0], "the actor was null and should not have been");
            Assert.AreEqual("http://example.org/posts/1", (activity as Activity).Object[0].Url[0].Href, "the actor name was not correct");

            Assert.IsNotNull((activity as Activity).Target, "the actor was null and should not have been");
            Assert.AreEqual("John", (activity as Activity).Target[0].Obj.Name, "the actor name was not correct");
            Assert.AreEqual("Person", (activity as Activity).Target[0].Obj.Type, "the actor name was not correct");
            Assert.IsInstanceOfType((activity as Activity).Target[0].Obj, typeof(PersonActor));
        }

        /// <summary>
        /// Testing Spec example 108
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample108()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example108.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Offer");
            Assert.IsInstanceOfType(activity, typeof(Activity));
            Assert.AreEqual(activity.Summary, "Sally offered the post to John");

            Assert.IsNotNull((activity as Activity).Actor, "the actor was null and should not have been");
            Assert.AreEqual("http://sally.example.org", (activity as Activity).Actor[0].Link.Href, "the actor name was not correct");

            Assert.IsNotNull((activity as Activity).Object[0], "the actor was null and should not have been");
            Assert.AreEqual("http://example.org/posts/1", (activity as Activity).Object[0].Url[0].Href, "the actor name was not correct");

            Assert.IsNotNull((activity as Activity).Target, "the actor was null and should not have been");
            Assert.AreEqual("http://john.example.org", (activity as Activity).Target[0].Link.Href, "the actor name was not correct");

            Assert.IsNotNull((activity as Activity).To, "the actor was null and should not have been");
            Assert.AreEqual("http://joe.example.org", (activity as Activity).To[0].Link.Href, "the actor name was not correct");
        }

        /// <summary>
        /// Testing Spec example 109
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample109()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example109.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Document");
            Assert.IsInstanceOfType(activity, typeof(DocumentObject));
            Assert.AreEqual(activity.Name, "4Q Sales Forecast");
            Assert.AreEqual(activity.Url[0].Href, "http://example.org/4q-sales-forecast.pdf");
        }

        /// <summary>
        /// Testing Spec example 110
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample110()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example110.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Document");
            Assert.IsInstanceOfType(activity, typeof(DocumentObject));
            Assert.AreEqual(activity.Name, "4Q Sales Forecast");
            Assert.AreEqual(activity.Url[0].Href, "http://example.org/4q-sales-forecast.pdf");
        }

        /// <summary>
        /// Testing Spec example 111
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample111()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example111.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Document");
            Assert.IsInstanceOfType(activity, typeof(DocumentObject));
            Assert.AreEqual(activity.Name, "4Q Sales Forecast");
            Assert.AreEqual(activity.Url[0].Href, "http://example.org/4q-sales-forecast.pdf");
            Assert.AreEqual(activity.Url[0].MediaType, "application/pdf");
            Assert.AreEqual(activity.Url[1].Href, "http://example.org/4q-sales-forecast.html");
            Assert.AreEqual(activity.Url[1].MediaType, "text/html");
        }

        /// <summary>
        /// Testing Spec example 112
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample112()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example112.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Place");
            Assert.IsInstanceOfType(activity, typeof(PlaceObject));
            Assert.AreEqual(activity.Name, "Liu Gu Lu Cun, Pingdu, Qingdao, Shandong, China");
            Assert.AreEqual((activity as PlaceObject).Latitude, 36.75);
            Assert.AreEqual((activity as PlaceObject).Longitude, 119.7667);
            Assert.AreEqual((activity as PlaceObject).Accuracy, 94.5);
            Assert.AreEqual((activity as PlaceObject).Units, "m");
        }

        /// <summary>
        /// Testing Spec example 113
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample113()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example113.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Place");
            Assert.IsInstanceOfType(activity, typeof(PlaceObject));
            Assert.AreEqual(activity.Name, "Fresno Area");
            Assert.AreEqual((activity as PlaceObject).Latitude, 36.75);
            Assert.AreEqual((activity as PlaceObject).Longitude, 119.7667);
            Assert.AreEqual((activity as PlaceObject).Altitude, 15.0);
            Assert.AreEqual((activity as PlaceObject).Units, "miles");
        }

        /// <summary>
        /// Testing Spec example 114
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample114()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example114.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));
            Assert.AreEqual(activity.Summary, "A simple note");
            Assert.AreEqual(activity.Content, "A <em>simple</em> note");
        }

        /// <summary>
        /// Testing Spec example 115
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample115()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example115.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));
            Assert.AreEqual(activity.Summary, "A simple note");
            Assert.AreEqual(activity.ContentMap.GetContent("en"), "A <em>simple</em> note");
            Assert.AreEqual(activity.ContentMap.GetContent("es"), "Una nota <em>sencilla</em>");
            Assert.AreEqual(activity.ContentMap.GetContent("zh-Hans"), "一段<em>简单的</em>笔记");
            Assert.AreEqual(activity.Content, "A <em>simple</em> note"); // NOTE this assumes you are running on an english box this could be better.
        }

        /// <summary>
        /// Testing Spec example 116
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample116()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example116.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));
            Assert.AreEqual(activity.Summary, "A simple note");
            Assert.AreEqual(activity.Content, "## A simple note\nA simple markdown `note`");
            Assert.AreEqual(activity.MediaType, "text/markdown");

        }

        /// <summary>
        /// Testing Spec example 117
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample117()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example117.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));
            Assert.AreEqual(activity.Name, "A simple note");
        }

        /// <summary>
        /// Testing Spec example 118
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample118()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example118.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));
            Assert.AreEqual(activity.NameMap.GetContent("en"), "A simple note");
            Assert.AreEqual(activity.NameMap.GetContent("es"), "Una nota sencilla");
            Assert.AreEqual(activity.NameMap.GetContent("zh-Hans"), "一段简单的笔记");
            Assert.AreEqual(activity.Name, "A simple note"); // NOTE this assumes you are running on an english box this could be better.
        }

        /// <summary>
        /// Testing Spec example 119
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample119()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example119.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Video");
            Assert.IsInstanceOfType(activity, typeof(VideoObject));
            Assert.AreEqual(activity.Name, "Birds Flying");
            Assert.AreEqual(activity.Url[0].Href, "http://example.org/video.mkv");
            Assert.AreEqual(activity.Duration, XmlConvert.ToTimeSpan("PT2H"));
        }

        /// <summary>
        /// Testing Spec example 120
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample120()
        {
            ActivityLinkBuilder builder = new ActivityLinkBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example120.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Link");
            Assert.IsInstanceOfType(activity, typeof(ActivityLink));
            Assert.AreEqual(activity.Href, "http://example.org/image.png");
            Assert.AreEqual(activity.Height, 101);
            Assert.AreEqual(activity.Width, 100);
        }

        /// <summary>
        /// Testing Spec example 121
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample121()
        {
            ActivityLinkBuilder builder = new ActivityLinkBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example121.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Link");
            Assert.IsInstanceOfType(activity, typeof(ActivityLink));
            Assert.AreEqual(activity.Name, "Previous");
            Assert.AreEqual(activity.Href, "http://example.org/abc");
            Assert.AreEqual(activity.MediaType, "text/html");
        }

        /// <summary>
        /// Testing Spec example 122
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample122()
        {
            ActivityLinkBuilder builder = new ActivityLinkBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example122.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Link");
            Assert.IsInstanceOfType(activity, typeof(ActivityLink));
            Assert.AreEqual(activity.Name, "Previous");
            Assert.AreEqual(activity.HrefLang, "en");
            Assert.AreEqual(activity.Href, "http://example.org/abc");
            Assert.AreEqual(activity.MediaType, "text/html");
        }

        /// <summary>
        /// Testing Spec example 123
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample123()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example123.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Page 1 of Sally's notes");
            Assert.IsInstanceOfType(activity, typeof(CollectionPage));

            Assert.AreEqual((uint)2, (activity as CollectionPage).TotalItems, "the total items were incorrect");
            Assert.AreEqual(CollectionPage.CollectionPageType, (activity as CollectionPage).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as CollectionPage).TotalItems, (activity as CollectionPage).Items.Length, "the item count was incorrect");
            Assert.IsNull((activity as CollectionPage).OrderedItems, "the items were not empty");
            Assert.AreEqual("Note", (activity as CollectionPage).Items[0].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual("Note", (activity as CollectionPage).Items[1].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual("Pizza Toppings to Try", (activity as CollectionPage).Items[0].Obj.Name, "the sub object type was incorrect");
            Assert.AreEqual("Thought about California", (activity as CollectionPage).Items[1].Obj.Name, "the sub object type was incorrect");
            Assert.IsInstanceOfType((activity as CollectionPage).Items[0].Obj, typeof(NoteObject));
            Assert.IsInstanceOfType((activity as CollectionPage).Items[1].Obj, typeof(NoteObject));
            Assert.IsNotNull((activity as CollectionPage).PartOf, "partof was null");
            Assert.IsNotNull((activity as CollectionPage).PartOf.Link, "partof link was null");
            Assert.IsNotNull((activity as CollectionPage).PartOf.Link.Href, "partof link href was null");
            Assert.IsNotNull(activity.Id, "the id was null");
            Assert.IsNull((activity as CollectionPage).Next, "the next was not null");
            Assert.IsNull((activity as CollectionPage).Prev, "the prev was not null");
            Assert.AreEqual("http://example.org/collection?page=1", activity.Id.AbsoluteUri, "the id was not correct");
            Assert.AreEqual("http://example.org/collection", (activity as CollectionPage).PartOf.Link.Href);
        }

        /// <summary>
        /// Testing Spec example 124
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample124()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example124.json"))
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

        /// <summary>
        /// Testing Spec example 125
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample125()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example125.json"))
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

        /// <summary>
        /// Testing Spec example 126
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample126()
        {
            ActivityLinkBuilder builder = new ActivityLinkBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example126.json"))
                            .Build();

            Assert.IsNotNull(activity.Type, "the activity link context was null");
            Assert.IsNotNull(activity.Href, "the activity link Href was null");
            Assert.IsNotNull(activity.HrefLang, "the activity link HrefLang was null");
            Assert.IsNotNull(activity.MediaType, "the activity link MediaType was null");
            Assert.IsNotNull(activity.Name, "the activity link Name was null");

            Assert.AreEqual(activity.Type, "Link", "the type was not set");
            Assert.AreEqual(activity.Href, "http://example.org/abc", "");
            Assert.AreEqual(activity.HrefLang, "en", "the hrref lang");
            Assert.AreEqual(activity.MediaType, "text/html", "the media type");
            Assert.AreEqual(activity.Name, "Next", "the name was wrong");
        }

        /// <summary>
        /// Testing Spec example 127
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample127()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example127.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Event");
            Assert.IsInstanceOfType(activity, typeof(EventObject));
            Assert.AreEqual(activity.Name, "Going-Away Party for Jim");
            Assert.AreEqual(activity.StartTime, DateTime.Parse("2014-12-31T23:00:00-08:00"));
            Assert.AreEqual(activity.EndTime, DateTime.Parse("2015-01-01T06:00:00-08:00"));
        }

        /// <summary>
        /// Testing Spec example 128
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample128()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example128.json"))
                            .Build();

            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));
            Assert.AreEqual(activity.Summary, "A simple note");
            Assert.AreEqual(activity.Content, "Fish swim.");
            Assert.AreEqual(activity.Published, DateTime.Parse("2014-12-12T12:12:12Z").ToUniversalTime());
        }

        /// <summary>
        /// Testing Spec example 129
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample129()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example129.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Event");
            Assert.IsInstanceOfType(activity, typeof(EventObject));
            Assert.AreEqual(activity.Name, "Going-Away Party for Jim");
            Assert.AreEqual(activity.StartTime, DateTime.Parse("2014-12-31T23:00:00-08:00"));
            Assert.AreEqual(activity.EndTime, DateTime.Parse("2015-01-01T06:00:00-08:00"));
        }

        /// <summary>
        /// Testing Spec example 130
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample130()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example130.json"))
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

        /// <summary>
        /// Testing Spec example 131
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample131()
        {
            ActivityLinkBuilder builder = new ActivityLinkBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example131.json"))
                            .Build();

            Assert.IsNotNull(activity.Type, "the activity link context was null");
            Assert.IsNotNull(activity.Href, "the activity link Href was null");
            Assert.IsNotNull(activity.HrefLang, "the activity link HrefLang was null");
            Assert.IsNotNull(activity.MediaType, "the activity link MediaType was null");
            Assert.IsNotNull(activity.Name, "the activity link Name was null");

            Assert.AreEqual(activity.Type, "Link", "the type was not set");
            Assert.AreEqual(activity.Href, "http://example.org/abc", "");
            Assert.AreEqual(activity.HrefLang, "en", "the hrref lang");
            Assert.AreEqual(activity.MediaType, "text/html", "the media type");
            Assert.AreEqual(activity.Rel[0], "canonical", "the name was wrong");
            Assert.AreEqual(activity.Rel[1], "preview", "the name was wrong");
        }

        /// <summary>
        /// Testing Spec example 132
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample132()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example132.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Page 1 of Sally's notes");
            Assert.IsInstanceOfType(activity, typeof(CollectionPage));
            Assert.AreEqual((uint)2, (activity as CollectionPage).TotalItems, "the total items were incorrect");
            Assert.AreEqual(CollectionPage.OrderedCollectionPageType, (activity as CollectionPage).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as CollectionPage).TotalItems, (activity as CollectionPage).OrderedItems.Length, "the item count was incorrect");
            Assert.IsNull((activity as CollectionPage).Items, "the items were not empty");
            Assert.AreEqual("Note", (activity as CollectionPage).OrderedItems[0].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual("Note", (activity as CollectionPage).OrderedItems[1].Obj.Type, "the sub object type was incorrect");
            Assert.AreEqual("Density of Water", (activity as CollectionPage).OrderedItems[0].Obj.Name, "the sub object type was incorrect");
            Assert.AreEqual("Air Mattress Idea", (activity as CollectionPage).OrderedItems[1].Obj.Name, "the sub object type was incorrect");
            Assert.IsInstanceOfType((activity as CollectionPage).OrderedItems[0].Obj, typeof(NoteObject));
            Assert.IsInstanceOfType((activity as CollectionPage).OrderedItems[1].Obj, typeof(NoteObject));
            Assert.IsNull(activity.Id, "the id was null");
            Assert.IsNull((activity as CollectionPage).Next, "the next was not null");
            Assert.IsNull((activity as CollectionPage).Prev, "the prev was not null");
        }

        /// <summary>
        /// Testing Spec example 133
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample133()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example133.json"))
                            .Build();
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));
            Assert.AreEqual(activity.Name, "Cane Sugar Processing");
            Assert.AreEqual(activity.Summary, "A simple <em>note</em>");
        }

        /// <summary>
        /// Testing Spec example 134
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample134()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example134.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));
            Assert.AreEqual(activity.Name, "Cane Sugar Processing");
            Assert.AreEqual(activity.SummaryMap.GetContent("en"), "A simple <em>note</em>");
            Assert.AreEqual(activity.SummaryMap.GetContent("es"), "Una <em>nota</em> sencilla");
            Assert.AreEqual(activity.SummaryMap.GetContent("zh-Hans"), "一段<em>简单的</em>笔记");
            Assert.AreEqual(activity.Summary, "A simple <em>note</em>"); // NOTE this assumes you are running on an english box this could be better.
        }

        /// <summary>
        /// Testing Spec example 135
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample135()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example135.json"))
                            .Build();

            Assert.IsNotNull(activity.Summary, "Sally's notes");
            Assert.IsInstanceOfType(activity, typeof(Collection));

            Assert.AreEqual((uint)2, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.AreEqual(Collection.CollectionType, (activity as Collection).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as Collection).TotalItems, (activity as Collection).Items.Length, "the item count was incorrect");

            Assert.AreEqual("Which Staircase Should I Use", (activity as Collection).Items[0].Obj.Name, "the sub object link was incorrect");
            Assert.AreEqual("Note", (activity as Collection).Items[0].Obj.Type, "the sub object link was incorrect");
            Assert.AreEqual("Something to Remember", (activity as Collection).Items[1].Obj.Name, "the sub object link was incorrect");
            Assert.AreEqual("Note", (activity as Collection).Items[1].Obj.Type, "the sub object link was incorrect");
        }

        /// <summary>
        /// Testing Spec example 136
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample136()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example136.json"))
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

        /// <summary>
        /// Testing Spec example 137
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample137()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example137.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsInstanceOfType(activity, typeof(NoteObject));
            Assert.AreEqual(activity.Content, "Mush it up so it does not have the same shape as the can.");
            Assert.AreEqual(activity.Updated, DateTime.Parse("2014-12-12T12:12:12Z").ToUniversalTime());
        }

        /// <summary>
        /// Testing Spec example 138
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample138()
        {
            ActivityLinkBuilder builder = new ActivityLinkBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example138.json"))
                            .Build();

            Assert.IsNotNull(activity.Type, "the activity link context was null");
            Assert.IsNotNull(activity.Href, "the activity link Href was null");

            Assert.AreEqual(activity.Type, "Link", "the type was not set");
            Assert.AreEqual(activity.Href, "http://example.org/image.png");
            Assert.AreEqual(activity.Height, 100);
            Assert.AreEqual(activity.Width, 100);
        }

        /// <summary>
        /// Testing Spec example 139
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample139()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example139.json"))
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

        /// <summary>
        /// Testing Spec example 140
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample140()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example140.json"))
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

        /// <summary>
        /// Testing Spec example 141
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample141()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example141.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Profile");
            Assert.IsInstanceOfType(activity, typeof(ProfileObject));
            Assert.AreEqual(activity.Summary, "Sally's profile");
            Assert.AreEqual((activity as ProfileObject).Describes.Type, "Person");
            Assert.AreEqual((activity as ProfileObject).Describes.Name, "Sally");
            Assert.IsInstanceOfType((activity as ProfileObject).Describes, typeof(PersonActor));
            Assert.AreEqual((activity as ProfileObject).Url[0].Href, "http://sally.example.org");
        }

        /// <summary>
        /// Testing Spec example 142
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample142()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example142.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Tombstone");
            Assert.IsInstanceOfType(activity, typeof(TombstoneObject));
            Assert.AreEqual((activity as TombstoneObject).FormerType, "Image");
            Assert.AreEqual(activity.Summary, "This image has been deleted");
            Assert.AreEqual(activity.Url[0].Href, "http://example.org/image/2");
        }

        /// <summary>
        /// Testing Spec example 143
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample143()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example143.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Tombstone");
            Assert.IsInstanceOfType(activity, typeof(TombstoneObject));
            Assert.AreEqual(activity.Summary, "This image has been deleted");
            Assert.AreEqual((activity as TombstoneObject).Deleted, DateTime.Parse("2016-05-03T00:00:00Z").ToUniversalTime());
        }

        /// <summary>
        /// Testing Spec example 144
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample144()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example144.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null"); Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.IsNotNull(activity.Summary, "Page 1 of Sally's blog posts");
            Assert.IsInstanceOfType(activity, typeof(Collection));

            Assert.AreEqual((uint)2, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.AreEqual(CollectionPage.CollectionType, (activity as Collection).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as Collection).TotalItems, (activity as Collection).Items.Length, "the item count was incorrect");

            Assert.AreEqual((activity as Collection).Items[0].Obj.Summary, "Sally created a note");
            Assert.AreEqual((activity as Collection).Items[0].Obj.Type, "Create");
            Assert.AreEqual((activity as Collection).Items[0].Obj.Id, "http://activities.example.com/1");
            Assert.IsInstanceOfType(((activity as Collection).Items[0].Obj as CreateActivity).Object[0], typeof(NoteObject));
            Assert.AreEqual(((activity as Collection).Items[0].Obj as CreateActivity).Actor[0].Link.Href, "http://sally.example.org");
            Assert.AreEqual(((activity as Collection).Items[0].Obj as CreateActivity).Object[0].Summary, "A note");
            Assert.AreEqual(((activity as Collection).Items[0].Obj as CreateActivity).Object[0].Type, "Note");
            Assert.AreEqual(((activity as Collection).Items[0].Obj as CreateActivity).Object[0].Id, "http://notes.example.com/1");
            Assert.AreEqual(((activity as Collection).Items[0].Obj as CreateActivity).Object[0].Content, "A note");
            
            // Assert.AreEqual((activity as Collection).Items[0].Obj.Context, "Sally created a note"); //??
            Assert.AreEqual((activity as Collection).Items[0].Obj.Audience[0].Obj.Name, "Project XYZ Working Group");
            Assert.AreEqual((activity as Collection).Items[0].Obj.Audience[0].Obj.Type, "Group");
            Assert.IsInstanceOfType((activity as Collection).Items[0].Obj.Audience[0].Obj, typeof(GroupActor));

            Assert.AreEqual(((activity as Collection).Items[0].Obj as CreateActivity).To[0].Link.Href, "http://john.example.org");

            Assert.AreEqual((activity as Collection).Items[1].Obj.Summary, "John liked Sally's note");
            Assert.AreEqual((activity as Collection).Items[1].Obj.Type, "Like");
            Assert.AreEqual((activity as Collection).Items[1].Obj.Id, "http://activities.example.com/1");
            Assert.IsInstanceOfType(((activity as Collection).Items[1].Obj as LikeActivity).Object[0], typeof(ActivityObject));
            Assert.AreEqual(((activity as Collection).Items[1].Obj as LikeActivity).Actor[0].Link.Href, "http://john.example.org");
            Assert.AreEqual(((activity as Collection).Items[1].Obj as LikeActivity).Object[0].Url[0].Href, "http://notes.example.com/1");

            //Assert.AreEqual((activity as Collection).Items[1].Obj.Context, "Sally created a note"); //??
            Assert.AreEqual((activity as Collection).Items[1].Obj.Audience[0].Obj.Name, "Project XYZ Working Group");
            Assert.AreEqual((activity as Collection).Items[1].Obj.Audience[0].Obj.Type, "Group");
            Assert.IsInstanceOfType((activity as Collection).Items[1].Obj.Audience[0].Obj, typeof(GroupActor));

            Assert.AreEqual((activity as Collection).Items[1].Obj.To[0].Link.Href, "http://sally.example.org");
        }

        /// <summary>
        /// Testing Spec example 145
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample145()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example145.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null"); Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.IsNotNull(activity.Summary, "Page 1 of Sally's blog posts");
            Assert.IsInstanceOfType(activity, typeof(Collection));

            Assert.AreEqual((uint)2, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.AreEqual(CollectionPage.CollectionType, (activity as Collection).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as Collection).TotalItems, (activity as Collection).Items.Length, "the item count was incorrect");

            Assert.AreEqual((activity as Collection).Items[0].Obj.Summary, "Sally is influenced by Joe");
            Assert.AreEqual((activity as Collection).Items[0].Obj.Type, "Relationship");
            Assert.AreEqual(((activity as Collection).Items[0].Obj as RelationshipObject).Subject[0].Obj.Type, "Person");
            Assert.IsInstanceOfType(((activity as Collection).Items[0].Obj as RelationshipObject).Subject[0].Obj, typeof(PersonActor));
            Assert.AreEqual(((activity as Collection).Items[0].Obj as RelationshipObject).Subject[0].Obj.Name, "Sally");
            Assert.AreEqual(((activity as Collection).Items[0].Obj as RelationshipObject).Object.Type, "Person");
            Assert.IsInstanceOfType(((activity as Collection).Items[0].Obj as RelationshipObject).Object, typeof(PersonActor));
            Assert.AreEqual(((activity as Collection).Items[0].Obj as RelationshipObject).Object.Name, "Joe");
            Assert.AreEqual(((activity as Collection).Items[0].Obj as RelationshipObject).Relationship, "http://purl.org/vocab/relationship/influencedBy");

            Assert.AreEqual((activity as Collection).Items[1].Obj.Summary, "Sally is a friend of Jane");
            Assert.AreEqual((activity as Collection).Items[1].Obj.Type, "Relationship");
            Assert.AreEqual(((activity as Collection).Items[1].Obj as RelationshipObject).Subject[0].Obj.Type, "Person");
            Assert.IsInstanceOfType(((activity as Collection).Items[1].Obj as RelationshipObject).Subject[0].Obj, typeof(PersonActor));
            Assert.AreEqual(((activity as Collection).Items[1].Obj as RelationshipObject).Subject[0].Obj.Name, "Sally");
            Assert.AreEqual(((activity as Collection).Items[1].Obj as RelationshipObject).Object.Type, "Person");
            Assert.IsInstanceOfType(((activity as Collection).Items[1].Obj as RelationshipObject).Object, typeof(PersonActor));
            Assert.AreEqual(((activity as Collection).Items[1].Obj as RelationshipObject).Object.Name, "Jane");
            Assert.AreEqual(((activity as Collection).Items[1].Obj as RelationshipObject).Relationship, "http://purl.org/vocab/relationship/friendOf");
        }

        /// <summary>
        /// Testing Spec example 146
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample146()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example146.json"))
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

        /// <summary>
        /// Testing Spec example 147
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample147()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example147.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Offer");
            Assert.IsInstanceOfType(activity, typeof(OfferActivity));

            Assert.AreEqual((activity as OfferActivity).Actor[0].Link.Href, "acct:sally@example.org");

            Assert.AreEqual((activity as OfferActivity).Object[0].Type, "Relationship");
            Assert.AreEqual(((activity as OfferActivity).Object[0] as RelationshipObject).Summary, "Sally and John's friendship");
            Assert.AreEqual(((activity as OfferActivity).Object[0] as RelationshipObject).Id, "http://example.org/connections/123");
            Assert.AreEqual(((activity as OfferActivity).Object[0] as RelationshipObject).Relationship, "http://purl.org/vocab/relationship/friendOf");
            Assert.AreEqual(((activity as OfferActivity).Object[0] as RelationshipObject).Subject[0].Link.Href, "acct:sally@example.org");
            Assert.AreEqual(((activity as OfferActivity).Object[0] as RelationshipObject).Relationship, "http://purl.org/vocab/relationship/friendOf");
            Assert.AreEqual(((activity as OfferActivity).Object[0] as RelationshipObject).Object.Url[0].Href, "acct:john@example.org");
            Assert.AreEqual(((activity as OfferActivity).Target[0]).Link.Href, "acct:john@example.org");
        }

        /// <summary>
        /// Testing Spec example 148
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample148()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example148.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null"); Assert.IsInstanceOfType(activity, typeof(Collection));
            Assert.IsNotNull(activity.Summary, "Page 1 of Sally's blog posts");
            Assert.IsInstanceOfType(activity, typeof(Collection));

            Assert.AreEqual((uint)5, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.AreEqual(CollectionPage.CollectionType, (activity as Collection).Type, "The type in the collectio was wrong");
            Assert.AreEqual((int)(activity as Collection).TotalItems, (activity as Collection).Items.Length, "the item count was incorrect");

            Assert.AreEqual((activity as Collection).Items[0].Obj.Summary, "John accepted Sally's friend request");
            Assert.AreEqual((activity as Collection).Items[0].Obj.Type, "Accept");
            Assert.AreEqual(((activity as Collection).Items[0].Obj as AcceptActivity).Id, "http://example.org/activities/122");
            Assert.AreEqual(((activity as Collection).Items[0].Obj as AcceptActivity).Actor[0].Link.Href, "acct:john@example.org");
            Assert.AreEqual(((activity as Collection).Items[0].Obj as AcceptActivity).Object[0].Url[0].Href, "http://example.org/connection-requests/123");
            Assert.AreEqual(((activity as Collection).Items[0].Obj as AcceptActivity).InReplyTo[0].Link.Href, "http://example.org/connection-requests/123");
            Assert.AreEqual(((activity as Collection).Items[0].Obj as AcceptActivity).Context, "http://example.org/connections/123");
            Assert.AreEqual(((activity as Collection).Items[0].Obj as AcceptActivity).Result[0].Link.Href, "http://example.org/activities/123");
            Assert.AreEqual(((activity as Collection).Items[0].Obj as AcceptActivity).Result[1].Link.Href, "http://example.org/activities/124");
            Assert.AreEqual(((activity as Collection).Items[0].Obj as AcceptActivity).Result[2].Link.Href, "http://example.org/activities/125");
            Assert.AreEqual(((activity as Collection).Items[0].Obj as AcceptActivity).Result[3].Link.Href, "http://example.org/activities/126");

            Assert.AreEqual((activity as Collection).Items[1].Obj.Summary, "John followed Sally");
            Assert.AreEqual((activity as Collection).Items[1].Obj.Type, "Follow");
            Assert.AreEqual(((activity as Collection).Items[1].Obj as FollowActivity).Id, "http://example.org/activities/123");
            Assert.AreEqual(((activity as Collection).Items[1].Obj as FollowActivity).Actor[0].Link.Href, "acct:john@example.org");
            Assert.AreEqual(((activity as Collection).Items[1].Obj as FollowActivity).Object[0].Url[0].Href, "acct:sally@example.org");
            Assert.AreEqual(((activity as Collection).Items[1].Obj as FollowActivity).Context, "http://example.org/connections/123");

            Assert.AreEqual((activity as Collection).Items[2].Obj.Summary, "Sally followed John");
            Assert.AreEqual((activity as Collection).Items[2].Obj.Type, "Follow");
            Assert.AreEqual(((activity as Collection).Items[2].Obj as FollowActivity).Id, "http://example.org/activities/124");
            Assert.AreEqual(((activity as Collection).Items[2].Obj as FollowActivity).Actor[0].Link.Href, "acct:sally@example.org");
            Assert.AreEqual(((activity as Collection).Items[2].Obj as FollowActivity).Object[0].Url[0].Href, "acct:john@example.org");
            Assert.AreEqual(((activity as Collection).Items[2].Obj as FollowActivity).Context, "http://example.org/connections/123");

            Assert.AreEqual((activity as Collection).Items[3].Obj.Summary, "John added Sally to his friends list");
            Assert.AreEqual((activity as Collection).Items[3].Obj.Type, "Add");
            Assert.AreEqual(((activity as Collection).Items[3].Obj as AddActivity).Id, "http://example.org/activities/125");
            Assert.AreEqual(((activity as Collection).Items[3].Obj as AddActivity).Actor[0].Link.Href, "acct:john@example.org");
            Assert.AreEqual(((activity as Collection).Items[3].Obj as AddActivity).Object[0].Url[0].Href, "http://example.org/connections/123");
            Assert.AreEqual(((activity as Collection).Items[3].Obj as AddActivity).Target[0].Obj.Type, "Collection");
            Assert.AreEqual(((activity as Collection).Items[3].Obj as AddActivity).Target[0].Obj.Summary, "John's Connections");
            Assert.AreEqual(((activity as Collection).Items[3].Obj as AddActivity).Context, "http://example.org/connections/123");

            Assert.AreEqual((activity as Collection).Items[4].Obj.Summary, "Sally added John to her friends list");
            Assert.AreEqual((activity as Collection).Items[4].Obj.Type, "Add");
            Assert.AreEqual(((activity as Collection).Items[4].Obj as AddActivity).Id, "http://example.org/activities/126");
            Assert.AreEqual(((activity as Collection).Items[4].Obj as AddActivity).Actor[0].Link.Href, "acct:sally@example.org");
            Assert.AreEqual(((activity as Collection).Items[4].Obj as AddActivity).Object[0].Url[0].Href, "http://example.org/connections/123");
            Assert.AreEqual(((activity as Collection).Items[4].Obj as AddActivity).Target[0].Obj.Type, "Collection");
            Assert.AreEqual(((activity as Collection).Items[4].Obj as AddActivity).Target[0].Obj.Summary, "Sally's Connections");
            Assert.AreEqual(((activity as Collection).Items[4].Obj as AddActivity).Context, "http://example.org/connections/123");
        }

        /// <summary>
        /// Testing Spec example 149
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample149()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example149.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsNotNull(activity.Name, "San Francisco, CA");
            Assert.IsInstanceOfType(activity, typeof(PlaceObject));
        }

        /// <summary>
        /// Testing Spec example 150
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample150()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example150.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsNotNull(activity.Name, "San Francisco, CA");
            Assert.IsInstanceOfType(activity, typeof(PlaceObject));
            Assert.AreEqual((activity as PlaceObject).Latitude, 37.7833);
            Assert.AreEqual((activity as PlaceObject).Longitude, 122.4167);
            Assert.AreEqual((activity as PlaceObject).Units, "m");
        }

        /// <summary>
        /// Testing Spec example 151
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample151()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example151.json"))
                            .Build();

            Assert.IsNotNull((activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Question", (activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(QuestionActivity));
            Assert.AreEqual("I'd like to build a robot to feed my cat. Should I use Arduino or Raspberry Pi?", (activity as IntransitiveActivity).Content);
            Assert.AreEqual(new Uri("http://help.example.org/question/1"), (activity as QuestionActivity).Id);
            Assert.AreEqual("A question about robots", (activity as QuestionActivity).Name);
        }

        /// <summary>
        /// Testing Spec example 152
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample152()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example152.json"))
                            .Build();

            Assert.AreEqual("A question about robots", (activity as QuestionActivity).Name);
            Assert.AreEqual("I'd like to build a robot to feed my cat. Which platform is best?", (activity as QuestionActivity).Content);
            Assert.AreEqual(2, (activity as QuestionActivity).OneOf.Length, "one of count was set incorrectly.");
            Assert.AreEqual("arduino", (activity as QuestionActivity).OneOf[0].Obj.Name, "one of name was set incorrectly.");
            Assert.AreEqual("raspberry pi", (activity as QuestionActivity).OneOf[1].Obj.Name, "one of name was set incorrectly.");
        }

        /// <summary>
        /// Testing Spec example 153
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample153()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example153.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual("http://sally.example.org", activity.AttributedTo[0].Link.Href);
            Assert.AreEqual("http://polls.example.org/question/1", activity.InReplyTo[0].Link.Href);
            Assert.AreEqual("arduino", activity.Name);
        }

        /// <summary>
        /// Testing Spec example 154
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample154()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example154.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual("A question about robots", (activity as QuestionActivity).Name);
            Assert.AreEqual(new Uri("http://polls.example.org/question/1"), (activity as QuestionActivity).Id);
            Assert.AreEqual("I'd like to build a robot to feed my cat. Which platform is best?", (activity as QuestionActivity).Content);

            Assert.AreEqual(2, (activity as QuestionActivity).OneOf.Length, "one of count was set incorrectly.");
            Assert.AreEqual("arduino", (activity as QuestionActivity).OneOf[0].Obj.Name, "one of name was set incorrectly.");
            Assert.AreEqual("raspberry pi", (activity as QuestionActivity).OneOf[1].Obj.Name, "one of name was set incorrectly.");


            Assert.AreEqual((uint)3, (activity as QuestionActivity).Replies.TotalItems, "one of count was set incorrectly.");
            
            Assert.AreEqual("arduino", (activity as QuestionActivity).Replies.Items[0].Obj.Name);
            Assert.AreEqual("http://polls.example.org/question/1", (activity as QuestionActivity).Replies.Items[0].Obj.InReplyTo[0].Link.Href);
            Assert.AreEqual("http://sally.example.org", (activity as QuestionActivity).Replies.Items[0].Obj.AttributedTo[0].Link.Href);

            Assert.AreEqual("arduino", (activity as QuestionActivity).Replies.Items[1].Obj.Name);
            Assert.AreEqual("http://polls.example.org/question/1", (activity as QuestionActivity).Replies.Items[1].Obj.InReplyTo[0].Link.Href);
            Assert.AreEqual("http://joe.example.org", (activity as QuestionActivity).Replies.Items[1].Obj.AttributedTo[0].Link.Href);


            Assert.AreEqual("raspberry pi", (activity as QuestionActivity).Replies.Items[2].Obj.Name);
            Assert.AreEqual("http://polls.example.org/question/1", (activity as QuestionActivity).Replies.Items[2].Obj.InReplyTo[0].Link.Href);
            Assert.AreEqual("http://john.example.org", (activity as QuestionActivity).Replies.Items[2].Obj.AttributedTo[0].Link.Href);

            Assert.AreEqual("Users are favoriting &quot;arduino&quot; by a 33% margin.", ((activity as QuestionActivity).Result[0].Obj as NoteObject).Content);
        }

        /// <summary>
        /// Testing Spec example 155
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample155()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example155.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(new Uri("http://activities.example.com/1"), ((activity as Collection).Items[0].Obj as LikeActivity).Id);
            Assert.AreEqual("http://sally.example.org", ((activity as Collection).Items[0].Obj as LikeActivity).Actor[0].Link.Href);
            Assert.AreEqual(DateTime.Parse("2015-11-12T12:34:56Z").ToUniversalTime(), ((activity as Collection).Items[0].Obj as LikeActivity).Published);
            Assert.AreEqual("John's note", ((activity as Collection).Items[0].Obj as LikeActivity).Object[0].Summary);
            Assert.AreEqual("http://john.example.org", (((activity as Collection).Items[0].Obj as LikeActivity).Object[0] as NoteObject).AttributedTo[0].Link.Href);
            Assert.AreEqual(new Uri("http://notes.example.com/1"), (((activity as Collection).Items[0].Obj as LikeActivity).Object[0] as NoteObject).Id);
            Assert.AreEqual("My note", (((activity as Collection).Items[0].Obj as LikeActivity).Object[0] as NoteObject).Content);


            Assert.AreEqual(new Uri("http://activities.example.com/2"), ((activity as Collection).Items[1].Obj as DislikeActivity).Id);
            Assert.AreEqual("http://sally.example.org", ((activity as Collection).Items[1].Obj as DislikeActivity).Actor[0].Link.Href);
            Assert.AreEqual(DateTime.Parse("2015-12-11T21:43:56Z").ToUniversalTime(), ((activity as Collection).Items[1].Obj as DislikeActivity).Published);
            Assert.AreEqual("John's note", ((activity as Collection).Items[1].Obj as DislikeActivity).Object[0].Summary);
            Assert.AreEqual("http://john.example.org", (((activity as Collection).Items[1].Obj as DislikeActivity).Object[0] as NoteObject).AttributedTo[0].Link.Href);
            Assert.AreEqual(new Uri("http://notes.example.com/1"), (((activity as Collection).Items[1].Obj as DislikeActivity).Object[0] as NoteObject).Id);
            Assert.AreEqual(new Uri("http://john.example.org"), (((activity as Collection).Items[1].Obj as DislikeActivity).Object[0] as NoteObject).AttributedTo[0].Link.Href);
            Assert.AreEqual("My note", (((activity as Collection).Items[1].Obj as DislikeActivity).Object[0] as NoteObject).Content);


        }

        /// <summary>
        /// Testing Spec example 156
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample156()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example156.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(new Uri("http://activities.example.com/1"), ((activity as Collection).Items[0].Obj as LikeActivity).Id);
            Assert.AreEqual("http://sally.example.org", ((activity as Collection).Items[0].Obj as LikeActivity).Actor[0].Link.Href);
            Assert.AreEqual(DateTime.Parse("2015-11-12T12:34:56Z").ToUniversalTime(), ((activity as Collection).Items[0].Obj as LikeActivity).Published);
            Assert.AreEqual("John's note", ((activity as Collection).Items[0].Obj as LikeActivity).Object[0].Summary);
            Assert.AreEqual("http://john.example.org", (((activity as Collection).Items[0].Obj as LikeActivity).Object[0] as NoteObject).AttributedTo[0].Link.Href);
            Assert.AreEqual(new Uri("http://notes.example.com/1"), (((activity as Collection).Items[0].Obj as LikeActivity).Object[0] as NoteObject).Id);
            Assert.AreEqual("My note", (((activity as Collection).Items[0].Obj as LikeActivity).Object[0] as NoteObject).Content);


            Assert.AreEqual(new Uri("http://activities.example.com/2"), ((activity as Collection).Items[1].Obj as UndoActivity).Id);
            Assert.AreEqual("http://sally.example.org", ((activity as Collection).Items[1].Obj as UndoActivity).Actor[0].Link.Href);
            Assert.AreEqual(DateTime.Parse("2015-12-11T21:43:56Z").ToUniversalTime(), ((activity as Collection).Items[1].Obj as UndoActivity).Published);
            Assert.AreEqual("Sally no longer likes John's note", ((activity as Collection).Items[1].Obj as UndoActivity).Summary);
            Assert.AreEqual(new Uri("http://activities.example.com/2"), (((activity as Collection).Items[1].Obj as UndoActivity).Id));
            Assert.AreEqual("http://sally.example.org", (((activity as Collection).Items[1].Obj as UndoActivity).Actor[0].Link.Href));
            Assert.AreEqual("http://activities.example.com/1", (((activity as Collection).Items[1].Obj as UndoActivity).Object[0].Url[0].Href));
        }

        /// <summary>
        /// Testing Spec example 157
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample157()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example157.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Name, "A thank-you note");
            Assert.AreEqual(activity.Type, "Note");
            Assert.IsNotNull(activity.Content, "the activity Content context was null");
            Assert.AreEqual(activity.To[0].Obj.Name, "Sally");
            Assert.AreEqual((activity.To[0].Obj as PersonActor).Id, "http://sally.example.org");
            Assert.AreEqual((activity as NoteObject).Tag[0].Obj.Id, new Uri("http://example.org/tags/givingthanks"));
            Assert.AreEqual((activity as NoteObject).Tag[0].Obj.Name, "#givingthanks");
        }

        /// <summary>
        /// Testing Spec example 158
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample158()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example158.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Name, "A thank-you note");
            Assert.AreEqual(activity.Type, "Note");
            Assert.AreEqual(activity.Content, "Thank you @sally for all your hard work! #givingthanks");
            Assert.AreEqual((activity as NoteObject).Tag[0].Link.Type, "Mention");
            Assert.AreEqual((activity as NoteObject).Tag[0].Link.Name, "@sally");
            Assert.AreEqual((activity as NoteObject).Tag[1].Obj.Id, new Uri("http://example.org/tags/givingthanks"));
            Assert.AreEqual((activity as NoteObject).Tag[1].Obj.Name, "#givingthanks");
            Assert.IsInstanceOfType((activity as NoteObject).Tag[0].Link, typeof(MentionLink));
        }

        /// <summary>
        /// Testing Spec example 159
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample159()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecVocabTestFiles\example159.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally moved the sales figures from Folder A to Folder B");
            Assert.AreEqual(activity.Type, "Move");
            Assert.AreEqual((activity as MoveActivity).Actor[0].Link.Href, "http://sally.example.org");

            Assert.AreEqual("http://sally.example.org", (activity as Activity).Actor[0].Link.Href, "the actor name was not correct");

            Assert.IsNotNull((activity as Activity).Object[0], "the sub object was null and should not have been");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(DocumentObject));
            Assert.AreEqual("sales figures", (activity as Activity).Object[0].Name, "the sub object name was incorrect");
            Assert.AreEqual("Document", (activity as Activity).Object[0].Type, "the sub object type was incorrect");

            Assert.IsNotNull((activity as Activity).Origin.Obj, "the sub object was null and should not have been");
            Assert.IsInstanceOfType((activity as Activity).Origin.Obj, typeof(Collection));
            Assert.AreEqual("Folder A", (activity as Activity).Origin.Obj.Name, "the sub object name was incorrect");
            Assert.AreEqual("Collection", (activity as Activity).Origin.Obj.Type, "the sub object type was incorrect");

            Assert.IsNotNull((activity as Activity).Target[0].Obj, "the sub object was null and should not have been");
            Assert.IsInstanceOfType((activity as Activity).Target[0].Obj, typeof(Collection));
            Assert.AreEqual("Folder B", (activity as Activity).Target[0].Obj.Name, "the sub object name was incorrect");
            Assert.AreEqual("Collection", (activity as Activity).Target[0].Obj.Type, "the sub object type was incorrect");
        }
    }
}
