using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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
            System.IO.File.OpenRead(@".\TestFiles\example000.json");
            ActivityBuilder builder = new ActivityBuilder();
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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example001.json"))
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
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example002.json"))
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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example003.json"))
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

            Assert.IsNotNull((activity as Activity).Object, "the sub object was null and should not have been");
            Assert.IsInstanceOfType((activity as Activity).Object, typeof(NoteObject));
            Assert.AreEqual("A Note", (activity as Activity).Object.Name, "the sub object name was incorrect");
            Assert.AreEqual("Note", (activity as Activity).Object.Type, "the sub object type was incorrect");

        }

        /// <summary>
        /// Testing Spec example 4
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample004()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example004.json")).Build();
            
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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example005.json"))
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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example006.json"))
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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example007.json"))
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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example008.json"))
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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example009.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally accepted an invitation to a party");
            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Accept", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(AcceptActivity));
            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));
            Assert.AreEqual("Invite", (activity as Activity).Object.Type, "the target object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object, typeof(InviteActivity));
            Assert.IsInstanceOfType(((activity as Activity).Object as InviteActivity).Object, typeof(EventObject));
            Assert.AreEqual("Event", (((activity as Activity).Object as InviteActivity).Object as EventObject).Type, "the name of the event was incorrect");
            Assert.AreEqual("Going-Away Party for Jim", (((activity as Activity).Object as InviteActivity).Object as EventObject).Name, "the name of the event was incorrect");
            Assert.AreEqual("http://john.example.org", ((activity as Activity).Object as InviteActivity).Actor[0].Link.Href, "The link for the href was incorrect");
        }

        /// <summary>
        /// Testing Spec example 10
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample010()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example010.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally accepted Joe into the club");
            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Accept", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(AcceptActivity));
            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));
            Assert.AreEqual("Person", (activity as Activity).Object.Type, "the target object type was incorrect");
            Assert.AreEqual("Joe", (activity as Activity).Object.Name, "the target object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object, typeof(PersonActor));
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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example011.json"))
                            .Build();


            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally tentatively accepted an invitation to a party");
            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("TentativeAccept", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(TentativeAcceptActivity));
            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));
            Assert.AreEqual("Invite", (activity as Activity).Object.Type, "the target object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object, typeof(InviteActivity));
            Assert.IsInstanceOfType(((activity as Activity).Object as InviteActivity).Object, typeof(EventObject));
            Assert.AreEqual("Event", (((activity as Activity).Object as InviteActivity).Object as EventObject).Type, "the name of the event was incorrect");
            Assert.AreEqual("Going-Away Party for Jim", (((activity as Activity).Object as InviteActivity).Object as EventObject).Name, "the name of the event was incorrect");
            Assert.AreEqual("http://john.example.org", ((activity as Activity).Object as InviteActivity).Actor[0].Link.Href, "The link for the href was incorrect");
        }

        /// <summary>
        /// Testing Spec example 12
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample012()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example012.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally added an object");
            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Add", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(AddActivity));
            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));
            Assert.IsInstanceOfType((activity as Activity).Object, typeof(Activity));
            Assert.AreEqual("http://example.org/abc", (activity as Activity).Object.Url[0].Href, "the object url was not correct");
        }

        /// <summary>
        /// Testing Spec example 13
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample013()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example013.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally added a picture of her cat to her cat picture collection");
            
            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Add", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(AddActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));
            
            Assert.AreEqual("Image", (activity as Activity).Object.Type, "the target object type was incorrect");
            Assert.AreEqual("A picture of my cat", (activity as Activity).Object.Name, "the target object name was incorrect");
            Assert.AreEqual("http://example.org/img/cat.png", (activity as Activity).Object.Url[0].Href, "the target object url name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object, typeof(ImageObject));

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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example014.json"))
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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example015.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally created a note");

            Assert.IsNotNull((activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Create", (activity as IntransitiveActivity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(CreateActivity));

            Assert.AreEqual("Sally", (activity as IntransitiveActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as IntransitiveActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as IntransitiveActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Note", (activity as Activity).Object.Type, "the target object type was incorrect");
            Assert.AreEqual("A Simple Note", (activity as Activity).Object.Name, "the target object name was incorrect");
            Assert.AreEqual("This is a simple note", (activity as Activity).Object.Content, "the target object url name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object, typeof(NoteObject));

        }

        /// <summary>
        /// Testing Spec example 16
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample016()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example016.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally deleted a note");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Delete", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(DeleteActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.IsNotNull((activity as Activity).Object.Type, "the target object type was null");
            Assert.AreEqual("http://example.org/notes/1", (activity as Activity).Object.Url[0].Href, "the target object url name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object, typeof(Activity));

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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example017.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally followed John");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Follow", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(FollowActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Person", (activity as Activity).Object.Type, "the target object type was not null");
            Assert.AreEqual("John", (activity as Activity).Object.Name, "the target object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object, typeof(PersonActor));
        }

        /// <summary>
        /// Testing Spec example 18
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample018()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example018.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally ignored a note");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Ignore", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(IgnoreActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Link", (activity as Activity).Object.Type, "the target object type was not null");
            Assert.AreEqual("http://example.org/notes/1", (activity as Activity).Object.Url[0].Href, "the object link was not correct.");
        }

        /// <summary>
        /// Testing Spec example 19
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample019()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example019.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally joined a group");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Join", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(JoinActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Group", (activity as Activity).Object.Type, "the target object type was not null");
            Assert.IsInstanceOfType((activity as Activity).Object, typeof(GroupActor));
            Assert.AreEqual("A Simple Group", (activity as Activity).Object.Name, "the object link was not correct.");
        }

        /// <summary>
        /// Testing Spec example 20
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample020()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example020.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally left work");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Leave", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(LeaveActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Place", (activity as Activity).Object.Type, "the target object type was not null");
            Assert.IsInstanceOfType((activity as Activity).Object, typeof(PlaceObject));
            Assert.AreEqual("Work", (activity as Activity).Object.Name, "the object link was not correct.");
        }

        /// <summary>
        /// Testing Spec example 21
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample021()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example021.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally left a group");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Leave", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(LeaveActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Group", (activity as Activity).Object.Type, "the target object type was not null");
            Assert.IsInstanceOfType((activity as Activity).Object, typeof(GroupActor));
            Assert.AreEqual("A Simple Group", (activity as Activity).Object.Name, "the object link was not correct.");
        }

        /// <summary>
        /// Testing Spec example 22
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample022()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example022.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally liked a note");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Like", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(LikeActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Link", (activity as Activity).Object.Type, "the target object type was not null");
            Assert.AreEqual("http://example.org/notes/1", (activity as Activity).Object.Url[0].Href, "the object link was not correct.");
        }

        /// <summary>
        /// Testing Spec example 23
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample023()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example023.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally offered 50% off to Lewis");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Offer", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(OfferActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("http://www.types.example/ProductOffer", (activity as Activity).Object.Type, "the target object type was incorrect");
            Assert.AreEqual("50% Off!", (activity as Activity).Object.Name, "the target object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object, typeof(ActivityObject));

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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example024.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally invited John and Lisa to a party");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Invite", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(InviteActivity));

            Assert.AreEqual("Sally", (activity as Activity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Event", (activity as Activity).Object.Type, "the target object type was incorrect");
            Assert.AreEqual("A Party", (activity as Activity).Object.Name, "the target object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object, typeof(EventObject));

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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example025.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally rejected an invitation to a party");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Reject", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(RejectActivity));

            Assert.AreEqual("Sally", (activity as RejectActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as RejectActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as RejectActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Invite", (activity as RejectActivity).Object.Type, "the target object type was incorrect");
            Assert.AreEqual("http://john.example.org", ((activity as RejectActivity).Object as InviteActivity).Actor[0].Link.Href, "thehref for the invte was incorrect");

            Assert.AreEqual("Event", ((activity as RejectActivity).Object as InviteActivity).Object.Type, "The sub object type was incorrect");
            Assert.AreEqual("Going-Away Party for Jim", ((activity as RejectActivity).Object as InviteActivity).Object.Name, "The sub object name was incorrect");
            Assert.IsInstanceOfType(((activity as RejectActivity).Object as InviteActivity).Object, typeof(EventObject));
        }

        /// <summary>
        /// Testing Spec example 26
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample026()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example026.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally tentatively rejected an invitation to a party");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("TentativeReject", (activity as Activity).Type, "the sub object type was not correct");
            Assert.IsInstanceOfType(activity, typeof(TentativeRejectActivity));

            Assert.AreEqual("Sally", (activity as TentativeRejectActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as TentativeRejectActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as TentativeRejectActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual("Invite", (activity as RejectActivity).Object.Type, "the target object type was incorrect");
            Assert.AreEqual("http://john.example.org", ((activity as RejectActivity).Object as InviteActivity).Actor[0].Link.Href, "thehref for the invte was incorrect");

            Assert.AreEqual("Event", ((activity as RejectActivity).Object as InviteActivity).Object.Type, "The sub object type was incorrect");
            Assert.AreEqual("Going-Away Party for Jim", ((activity as RejectActivity).Object as InviteActivity).Object.Name, "The sub object name was incorrect");
            Assert.IsInstanceOfType(((activity as RejectActivity).Object as InviteActivity).Object, typeof(EventObject));
        }

        /// <summary>
        /// Testing Spec example 27
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample027()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example027.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally removed a note from her notes folder");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Remove", (activity as Activity).Type, "the sub object type was not correct");
            Assert.IsInstanceOfType(activity, typeof(RemoveActivity));

            Assert.AreEqual("Sally", (activity as RemoveActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as RemoveActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as RemoveActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as RemoveActivity).Object.Type, "the target object type was incorrect");
            Assert.AreEqual("http://example.org/notes/1", (activity as RemoveActivity).Object.Url[0].Href, "thehref for the invte was incorrect");

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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example028.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "The moderator removed Sally from a group");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Remove", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(RemoveActivity));

            Assert.AreEqual("The Moderator", (activity as RemoveActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("http://example.org/Role", (activity as RemoveActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as RemoveActivity).Actor[0].Obj, typeof(ActivityObject));

            Assert.AreEqual("Person", (activity as Activity).Object.Type, "the target object type was incorrect");
            Assert.AreEqual("Sally", (activity as Activity).Object.Name, "the target object name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object, typeof(PersonActor));

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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example029.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally retracted her offer to John");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Undo", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(UndoActivity));

            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as UndoActivity).Actor[0].Link.Type, "the actor link name was incorrect");
            Assert.AreEqual("http://sally.example.org", (activity as UndoActivity).Actor[0].Link.Href, "the actor link href was incorrect");

            Assert.AreEqual("Offer", (activity as Activity).Object.Type, "the target object type was incorrect");
            Assert.AreEqual(ActivityLink.ActivityLinkType, ((activity as Activity).Object as Activity).Actor[0].Link.Type, "the target object actors href was incorrect");
            Assert.AreEqual("http://sally.example.org", ((activity as Activity).Object as Activity).Actor[0].Link.Href, "the target object actors href was incorrect");

            Assert.AreEqual(ActivityLink.ActivityLinkType, ((activity as Activity).Object as Activity).Object.Type, "the target object actors href was incorrect");
            Assert.AreEqual("http://example.org/posts/1", ((activity as Activity).Object as Activity).Object.Url[0].Href, "the target object actors href was incorrect");

            Assert.AreEqual(ActivityLink.ActivityLinkType, ((activity as Activity).Object as Activity).Target[0].Link.Type, "the origin object type was incorrect");
            Assert.AreEqual("http://john.example.org", ((activity as Activity).Object as Activity).Target[0].Link.Href, "the origin object name was incorrect");
        }

        /// <summary>
        /// Testing Spec example 30
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample030()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example030.json"))
                            .Build();
            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally updated her note");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Update", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(UpdateActivity));

            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as UpdateActivity).Object.Type, "the object link name was incorrect");
            Assert.AreEqual("http://example.org/notes/1", (activity as UpdateActivity).Object.Url[0].Href, "the object link href was incorrect");
        }

        /// <summary>
        /// Testing Spec example 31
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample031()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example031.json"))
                            .Build();
            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally read an article");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("View", (activity as Activity).Type, "activity type was correct");
            Assert.IsInstanceOfType(activity, typeof(ViewActivity));

            Assert.AreEqual("Article", (activity as ViewActivity).Object.Type, "the object link name was incorrect");
            Assert.IsInstanceOfType((activity as ViewActivity).Object, typeof(ArticleObject));
            Assert.AreEqual("What You Should Know About Activity Streams", (activity as ViewActivity).Object.Name, "the object name was incorrect");
        }

        /// <summary>
        /// Testing Spec example 32
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample032()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example032.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally listened to a piece of music");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Listen", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(ListenActivity));

            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as ListenActivity).Object.Type, "the object link name was incorrect");
            Assert.AreEqual("http://example.org/music.mp3", (activity as ListenActivity).Object.Url[0].Href, "the object link href was incorrect");
        }

        /// <summary>
        /// Testing Spec example 33
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample033()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example033.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally read a blog post");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Read", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(ReadActivity));

            Assert.AreEqual("Sally", (activity as ReadActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as ReadActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as ReadActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as Activity).Object.Type, "the target object actors href was incorrect");
            Assert.AreEqual("http://example.org/posts/1", (activity as Activity).Object.Url[0].Href, "the target object actors href was incorrect");
        }

        /// <summary>
        /// Testing Spec example 34
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample034()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example034.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally read a blog post");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Move", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(MoveActivity));

            Assert.AreEqual("Sally", (activity as MoveActivity).Actor[0].Obj.Name, "the actor object name was incorrect");
            Assert.AreEqual("Person", (activity as MoveActivity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as MoveActivity).Actor[0].Obj, typeof(PersonActor));

            Assert.IsNotNull((activity as Activity).Object.Type, "the target object type was null");
            Assert.AreEqual("http://example.org/posts/1", (activity as Activity).Object.Url[0].Href, "the target object url name was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object, typeof(Activity));

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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example035.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally read a blog post");

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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example036.json"))
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

            Assert.IsNotNull((activity as Activity).Object.Type, "the target object type was null");
            Assert.AreEqual("Arrive", (activity as Activity).Object.Type, "the target object url name was incorrect");

            Assert.AreEqual("http://sally.example.org", ((activity as Activity).Object as IntransitiveActivity).Actor[0].Link.Href, "the target object url name was incorrect");
            Assert.AreEqual(ActivityLink.ActivityLinkType, ((activity as Activity).Object as IntransitiveActivity).Actor[0].Link.Type, "the target object url name was incorrect");

            Assert.AreEqual("Place", ((activity as Activity).Object as IntransitiveActivity).Location.Type, " the local type was not correct");
            Assert.AreEqual("Work", ((activity as Activity).Object as IntransitiveActivity).Location.Name, " the local type was not correct");

        }

        /// <summary>
        /// Testing Spec example 37
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample037()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example037.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally blocked Joe");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Block", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(BlockActivity));

            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as BlockActivity).Actor[0].Link.Type, "the actor object type was incorrect");
            Assert.AreEqual("http://sally.example.org", (activity as BlockActivity).Actor[0].Link.Href, "the actor object link href was incorrect");

            Assert.IsNotNull((activity as Activity).Object.Type, "the target object type was null");
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as Activity).Object.Type, "the target object url name was incorrect");
            Assert.AreEqual("http://joe.example.org", (activity as Activity).Object.Url[0].Href, "the target object url name was incorrect");
        }

        /// <summary>
        /// Testing Spec example 38
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample038()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example038.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally flagged an inappropriate note");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Flag", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(FlagActivity));

            Assert.IsNotNull((activity as FlagActivity).Object.Type, "the target object type was null");
            Assert.AreEqual("Note", (activity as Activity).Object.Type, "the target object url name was incorrect");
            Assert.IsInstanceOfType((activity as FlagActivity).Object, typeof(NoteObject));
            Assert.AreEqual("An inappropriate note", (activity as Activity).Object.Content, "the target object url name was incorrect");
        }

        /// <summary>
        /// Testing Spec example 39
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample039()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example039.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Sally disliked a post");

            Assert.IsNotNull((activity as Activity).Type, "the sub object was null and should not have been");
            Assert.AreEqual("Dislike", (activity as Activity).Type, "the sub object was null and should not have been");
            Assert.IsInstanceOfType(activity, typeof(DislikeActivity));

            Assert.IsNotNull((activity as Activity).Object.Type, "the target object type was null");
            Assert.AreEqual(ActivityLink.ActivityLinkType, (activity as Activity).Object.Type, "the target object url name was incorrect");
            Assert.AreEqual("http://example.org/posts/1", (activity as Activity).Object.Url[0].Href, "the target object url name was incorrect");
        }

        /// <summary>
        /// Testing Spec example 40
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample040()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example040.json"))
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
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example041.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 42
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample042()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example042.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 43
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample043()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example043.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 44
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample044()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example044.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 45
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample045()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example045.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 46
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample046()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example046.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 47
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample047()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example047.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 48
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample048()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example048.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 49
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample049()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example049.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 50
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample050()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example050.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 51
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample051()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example051.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 52
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample052()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example052.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 53
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample053()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example053.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 54
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample054()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example054.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 55
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample055()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example055.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 56
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample056()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example056.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 57
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample057()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example057.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 58
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample058()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example058.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 59
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample059()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example059.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 60
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample060()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example060.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 61
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample061()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example061.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 62
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample062()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example062.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 63
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample063()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example063.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 64
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample064()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example064.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 65
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample065()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example065.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 66
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample066()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example066.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 67
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample067()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example067.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 68
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample068()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example068.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 69
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample069()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example069.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 70
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample070()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example070.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 71
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample071()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example071.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 72
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample072()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example072.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 73
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample073()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example073.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 74
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample074()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example074.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 75
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample075()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example075.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 76
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample076()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example076.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 77
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample077()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example077.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 78
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample078()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example078.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 79
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample079()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example079.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 80
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample080()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example080.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 81
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample081()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example081.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 82
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample082()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example082.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 83
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample083()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example083.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 84
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample084()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example084.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 85
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample085()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example085.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 86
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample086()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example086.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 87
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample087()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example087.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 88
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample088()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example088.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 89
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample089()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example089.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 90
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample090()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example090.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 91
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample091()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example091.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 92
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample092()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example092.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 93
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample093()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example093.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 94
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample094()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example094.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 95
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample095()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example095.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 96
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample096()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example096.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 97
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample097()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example097.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 98
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample098()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example098.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 99
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample099()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example099.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 100
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample100()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example100.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 101
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample101()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example101.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 102
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample102()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example102.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 103
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample103()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example103.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 104
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample104()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example104.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 105
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample105()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example105.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 106
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample106()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example106.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 107
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample107()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example107.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 108
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample108()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example108.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 109
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample109()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example109.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 110
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample110()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example110.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 111
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample111()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example111.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 112
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample112()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example112.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 113
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample113()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example113.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 114
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample114()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example114.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 115
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample115()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example115.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 116
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample116()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example116.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 117
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample117()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example117.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 118
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample118()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example118.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 119
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample119()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example119.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 120
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample120()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example120.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 121
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample121()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example121.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 122
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample122()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example122.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 123
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample123()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example123.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 124
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample124()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example124.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 125
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample125()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example125.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 126
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample126()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example126.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 127
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample127()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example127.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 128
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample128()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example128.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 129
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample129()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example129.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 130
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample130()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example130.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 131
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample131()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example131.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 132
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample132()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example132.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 133
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample133()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example133.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 134
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample134()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example134.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 135
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample135()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example135.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 136
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample136()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example136.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 137
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample137()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example137.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 138
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample138()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example138.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 139
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample139()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example139.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 140
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample140()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example140.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 141
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample141()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example141.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 142
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample142()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example142.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 143
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample143()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example143.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 144
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample144()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example144.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 145
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample145()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example145.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 146
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample146()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example146.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 147
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample147()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example147.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 148
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample148()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example148.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 149
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample149()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example149.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 150
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample150()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example150.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 151
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample151()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example151.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 152
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample152()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example152.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 153
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample153()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example153.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 154
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample154()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example154.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 155
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample155()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example155.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 156
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample156()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example156.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 157
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample157()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example157.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 158
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample158()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example158.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        /// <summary>
        /// Testing Spec example 159
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample159()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\TestFiles\example159.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }
    }
}
