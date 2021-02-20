using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mossharbor.ActivityStreams;
using System;
using System.Linq;

namespace Mossharbor.ActivityStreams.UnitTests
{
    [TestClass]
    public class ParsingSpecExamples
    {
        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample002()
        {
            // Document providing context as an object using the @vocab keyword and a prefix for extension terms
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example002.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(new Uri("https://www.w3.org/ns/activitystreams"), activity.Context, "the activity stream context was not correct");

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("ext"), "the extended context did not exit");
            Assert.AreEqual("https://canine-extension.example/terms/", activity.ExtendedContexts["ext"], "the extended context was wrong");

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("@language"), "the extended language context did not exit");
            Assert.AreEqual("en", activity.ExtendedContexts["@language"], "the extended lanage was wrong");

            Assert.IsTrue(activity.Extensions.ContainsKey("ext:nose"), "the extension did not exit");
            Assert.AreEqual("0", activity.Extensions["ext:nose"], "the extension was wrong");

            Assert.IsTrue(activity.Extensions.ContainsKey("nose"), "the extension did not exit");
            Assert.AreEqual("0", activity.Extensions["nose"], "the extension was wrong");

            Assert.IsTrue(activity.Extensions.ContainsKey("ext:smell"), "the extension did not exit");
            Assert.AreEqual("terrible", activity.Extensions["ext:smell"], "the extension was wrong");

            Assert.IsTrue(activity.Extensions.ContainsKey("smell"), "the extension did not exit");
            Assert.AreEqual("terrible", activity.Extensions["smell"], "the extension was wrong");

            Assert.AreEqual("A note", activity.Summary, "the activity stream summary");
            Assert.IsNotNull(activity as NoteObject, "the activity was not a note");
            Assert.AreEqual("My dog has fleas.", activity.Content, "the activity stream content");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample003()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example003.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(new Uri("https://www.w3.org/ns/activitystreams"), activity.Context, "the activity stream context was not correct");

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("css"), "the extended context did not exit");
            Assert.AreEqual("http://www.w3.org/ns/oa#styledBy", activity.ExtendedContexts["css"], "the extended context was wrong");

            Assert.IsTrue(activity.Extensions.ContainsKey("css"), "the extension did not exit");
            Assert.AreEqual("http://www.csszengarden.com/217/217.css?v=8may2013", activity.Extensions["css"], "the extension was wrong");

            Assert.IsFalse(activity.Extensions.ContainsKey("summary"), "the extension did contain the summary and should not have");

            Assert.AreEqual("A note", activity.Summary, "the activity stream summary");
            Assert.IsNotNull(activity as NoteObject, "the activity was not a note");
            Assert.AreEqual("My dog has fleas.", activity.Content, "the activity stream content");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample005()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example005.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsNotNull(activity.Summary, "Martin added an article to his blog");

            Assert.AreEqual(new Uri("https://www.w3.org/ns/activitystreams"), activity.Context, "the activity stream context was not correct");

            Assert.IsInstanceOfType(activity, typeof(AddActivity));
            Assert.AreEqual("Person", (activity as Activity).Actor[0].Obj.Type, "the actor object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Actor[0].Obj, typeof(PersonActor));
            Assert.AreEqual(new Uri("http://www.test.example/martin"), (activity as Activity).Actor[0].Obj.Id, "the actor object name was incorrect");
            Assert.AreEqual("http://example.org/martin", (activity as Activity).Actor[0].Obj.Url[0].Href, "the actor object name was incorrect");
            Assert.AreEqual("http://example.org/martin/image.jpg", (activity as Activity).Actor[0].Obj.Images[0].Link.Href, "the actor object name was incorrect");
            Assert.AreEqual("image/jpeg", (activity as Activity).Actor[0].Obj.Images[0].Link.MediaType, "the actor object name was incorrect");

            Assert.AreEqual("Article", (activity as Activity).Object[0].Type, "the target object type was incorrect");
            Assert.IsInstanceOfType((activity as Activity).Object[0], typeof(ArticleObject));
            Assert.AreEqual("Why I love Activity Streams", (activity as Activity).Object[0].Name, "the target object name was incorrect");
            Assert.AreEqual(new Uri("http://www.test.example/blog/abc123/xyz"), (activity as Activity).Object[0].Id, "the target object id was incorrect");
            Assert.AreEqual("http://example.org/blog/2011/02/entry", (activity as Activity).Object[0].Url[0].Href, "the target object href was incorrect");

            Assert.AreEqual("Martin's Blog", (activity as Activity).Target[0].Obj.Name, "the target object name was incorrect");
            Assert.AreEqual("OrderedCollection", (activity as Activity).Target[0].Obj.Type, "the target object type was incorrect");
            Assert.AreEqual(new Uri("http://example.org/blog/"), (activity as Activity).Target[0].Obj.Id, "the target object id was incorrect");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample006()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example006.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Summary, "Martin's recent activities");

            Assert.AreEqual((uint)1, (activity as Collection).TotalItems, "the total items were incorrect");
            Assert.IsNull((activity as Collection).OrderedItems, "the Items were not empty");
            Assert.AreEqual(Collection.CollectionType, (activity as Collection).Type, "The type in the collection was wrong");
            Assert.AreEqual((int)(activity as Collection).TotalItems, (activity as Collection).Items.Length, "the item count was incorrect");
            Assert.IsInstanceOfType((activity as Collection).Items[0].Obj, typeof(AddActivity));

            Assert.AreEqual("http://example.org/activities-app", (activity as Collection).Items[0].Obj.Generator[0].Link.Href, "the sub object id was incorrect");
            Assert.AreEqual("Martin added a new image to his album.", (activity as Collection).Items[0].Obj.NameMap["en"], "the sub object id was incorrect");
            Assert.AreEqual("Martin phost le fisean nua a albam.", (activity as Collection).Items[0].Obj.NameMap["ga"], "the sub object id was incorrect");

            Assert.IsInstanceOfType(((activity as Collection).Items[0].Obj as AddActivity).Actor[0].Obj, typeof(PersonActor));
            Assert.AreEqual("Person", ((activity as Collection).Items[0].Obj as AddActivity).Actor[0].Obj.Type, "the sub object id was incorrect");
            Assert.AreEqual(new Uri("http://www.test.example/martin"), ((activity as Collection).Items[0].Obj as AddActivity).Actor[0].Obj.Id, "the sub object id was incorrect");
            Assert.AreEqual("Martin Smith", ((activity as Collection).Items[0].Obj as AddActivity).Actor[0].Obj.Name, "the sub object id was incorrect");
            Assert.AreEqual("http://example.org/martin", ((activity as Collection).Items[0].Obj as AddActivity).Actor[0].Obj.Url[0].Href, "the sub object id was incorrect");
            Assert.AreEqual("http://example.org/martin/image", ((activity as Collection).Items[0].Obj as AddActivity).Actor[0].Obj.Images[0].Link.Href, "the sub object id was incorrect");
            Assert.AreEqual("image/jpeg", ((activity as Collection).Items[0].Obj as AddActivity).Actor[0].Obj.Images[0].Link.MediaType, "the sub object id was incorrect");
            Assert.AreEqual(250, ((activity as Collection).Items[0].Obj as AddActivity).Actor[0].Obj.Images[0].Link.Width, "the sub object id was incorrect");
            Assert.AreEqual(250, ((activity as Collection).Items[0].Obj as AddActivity).Actor[0].Obj.Images[0].Link.Height, "the sub object id was incorrect");

            Assert.IsInstanceOfType(((activity as Collection).Items[0].Obj as AddActivity).Object[0], typeof(ImageObject));
            Assert.AreEqual("My fluffy cat", ((activity as Collection).Items[0].Obj as AddActivity).Object[0].Name, "the sub object id was incorrect");
            Assert.AreEqual(new Uri("http://example.org/album/máiréad.jpg"), ((activity as Collection).Items[0].Obj as AddActivity).Object[0].Id, "the sub object id was incorrect");
            Assert.AreEqual("http://example.org/album/máiréad.jpg", ((activity as Collection).Items[0].Obj as AddActivity).Object[0].Preview[0].Link.Href, "the sub object id was incorrect");
            Assert.AreEqual("image/jpeg", ((activity as Collection).Items[0].Obj as AddActivity).Object[0].Preview[0].Link.MediaType, "the sub object id was incorrect");
            Assert.AreEqual("http://example.org/album/máiréad.jpg", ((activity as Collection).Items[0].Obj as AddActivity).Object[0].Url[0].Href, "the sub object id was incorrect");
            Assert.AreEqual("image/jpeg", ((activity as Collection).Items[0].Obj as AddActivity).Object[0].Url[0].MediaType, "the sub object id was incorrect");
            Assert.AreEqual("http://example.org/album/máiréad.png", ((activity as Collection).Items[0].Obj as AddActivity).Object[0].Url[1].Href, "the sub object id was incorrect");
            Assert.AreEqual("image/png", ((activity as Collection).Items[0].Obj as AddActivity).Object[0].Url[1].MediaType, "the sub object id was incorrect");

            Assert.IsInstanceOfType(((activity as Collection).Items[0].Obj as AddActivity).Target[0].Obj, typeof(Collection));
            Assert.AreEqual(new Uri("http://example.org/album/"), ((activity as Collection).Items[0].Obj as AddActivity).Target[0].Obj.Id, "the sub object id was incorrect");
            Assert.AreEqual("Martin's Photo Album", (((activity as Collection).Items[0].Obj as AddActivity).Target[0].Obj as Collection).NameMap["en"], "the sub object id was incorrect");
            Assert.AreEqual("Grianghraif Mairtin", (((activity as Collection).Items[0].Obj as AddActivity).Target[0].Obj as Collection).NameMap["ga"], "the sub object id was incorrect");

            Assert.AreEqual((uint)0, (((activity as Collection).Items[0].Obj as AddActivity).Target[0].Obj as Collection).TotalItems, "the total items were incorrect");
            Assert.AreEqual("http://example.org/album/thumbnail.jpg", (((activity as Collection).Items[0].Obj as AddActivity).Target[0].Obj as Collection).Images[0].Link.Href, "the sub object id was incorrect");
            Assert.AreEqual("image/jpeg", (((activity as Collection).Items[0].Obj as AddActivity).Target[0].Obj as Collection).Images[0].Link.MediaType, "the sub object id was incorrect");

        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample008()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example008.json"))
                            .Build();
            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(new Uri("https://www.w3.org/ns/activitystreams"), activity.Context, "the activity stream context was not correct");

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("gr"), "the extended context did not gr");
            Assert.AreEqual("http://purl.org/goodrelations/v1#", activity.ExtendedContexts["gr"], "the extended context was wrong");

            Assert.IsTrue(activity.Extensions.ContainsKey("gr:category"), "the extension did not exit");
            Assert.AreEqual("restaurants/french_restaurants", activity.Extensions["gr:category"], "the extension was wrong");

            Assert.IsTrue(activity.Extensions.ContainsKey("category"), "the extension did not exit");
            Assert.AreEqual("restaurants/french_restaurants", activity.Extensions["category"], "the extension was wrong");

            Assert.IsTrue(activity.ExtendedTypes.Contains("gr:Location"), "the extended Typedid not exit");

            Assert.IsFalse(activity.Extensions.ContainsKey("name"), "the extension did contain the name and should not have");

            Assert.IsNotNull(activity as PlaceObject, "the activity was not a place");
            Assert.AreEqual(12.34, (activity as PlaceObject).Longitude);
            Assert.AreEqual(56.78, (activity as PlaceObject).Latitude);
            Assert.AreEqual("Sally's Restaurant", activity.Name, "the activity stream content");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample012()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example012.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Application");
            Assert.AreEqual(activity.Name, "Exampletron 3000");
            Assert.AreEqual(activity.Id, "http://example.org/application/123");
            Assert.IsInstanceOfType(activity, typeof(ApplicationActor));

            Assert.AreEqual(activity.Images[0].Link.Href, "http://example.org/application/123.png");
            Assert.AreEqual(activity.Images[0].Link.MediaType, "image/png");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample013()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example013.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Type, "Application");
            Assert.AreEqual(activity.Name, "Exampletron 3000");
            Assert.AreEqual(activity.Id, "http://example.org/application/123");
            Assert.IsInstanceOfType(activity, typeof(ApplicationActor));
            Assert.AreEqual(activity.Images[0].Link.Href, "http://example.org/application/abc.gif");
            Assert.AreEqual(activity.Images[1].Link.Href, "http://example.org/application/123.png");
            Assert.AreEqual(activity.Images[1].Link.MediaType, "image/png");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample015()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            CreateActivity activity = (CreateActivity)builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example015.json"))
                            .Build();
            
            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsNotNull(activity.Summary, "Sally created a note");

            Assert.AreEqual(new Uri("https://www.w3.org/ns/activitystreams"), activity.Context, "the activity stream context was not correct");

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("vcard"), "the extended context did not gr");
            Assert.AreEqual("http://www.w3.org/2006/vcard/ns#", activity.ExtendedContexts["vcard"], "the extended context was wrong");

            Assert.IsTrue(!activity.Extensions.ContainsKey("vcard:given-name"), "the extension did not exist");
            Assert.IsTrue(!activity.ExtendedTypes.Contains("vcard:Individual"), "the extension did not exist");

            Assert.AreEqual(new Uri("http://sally.example.org"), activity.Actor[0].Obj.Id);
            Assert.AreEqual("Sally Smith", activity.Actor[0].Obj.Name, "the activity stream content");

            Assert.IsTrue(activity.Actor[0].Obj.ExtendedContexts.ContainsKey("vcard"), "the extension did not exist");

            Assert.IsTrue(activity.Actor[0].Obj.Extensions.ContainsKey("vcard:given-name"), "the extension did not exist");
            Assert.AreEqual("Sally", activity.Actor[0].Obj.Extensions["vcard:given-name"], "the extension was wrong");

            Assert.IsTrue(activity.Actor[0].Obj.Extensions.ContainsKey("given-name"), "the extension did not exit");
            Assert.AreEqual("Sally", activity.Actor[0].Obj.Extensions["given-name"], "the extension was wrong");

            Assert.IsTrue(activity.Actor[0].Obj.Extensions.ContainsKey("vcard:family-name"), "the extension did not exist");
            Assert.AreEqual("Smith", activity.Actor[0].Obj.Extensions["vcard:family-name"], "the extension was wrong");

            Assert.IsTrue(activity.Actor[0].Obj.Extensions.ContainsKey("family-name"), "the extension did not exit");
            Assert.AreEqual("Smith", activity.Actor[0].Obj.Extensions["family-name"], "the extension was wrong");

            Assert.IsTrue(activity.Actor[0].Obj.ExtendedTypes.Contains("vcard:Individual"), "the extended Typedid not exist");

            Assert.IsFalse(activity.Actor[0].Obj.Extensions.ContainsKey("name"), "the extension did contain the name and should not have");

            Assert.IsNotNull(activity.Object[0] as NoteObject, "the object was null");
            Assert.AreEqual("This is a simple note", activity.Object[0].Content, "the object was content was wrong");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample020()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            Collection activity = (Collection)builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example020.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.Id, new Uri("http://example.org/foo"));

            Assert.AreEqual((uint)10, activity.TotalItems, "the total items were incorrect");
            Assert.IsNull(activity.OrderedItems, "the Items were not empty");
            Assert.AreEqual(Collection.CollectionType, activity.Type, "The type in the collection was wrong");

            Assert.IsInstanceOfType(activity.First.Obj, typeof(CollectionPage));
            CollectionPage page = activity.First.Obj as CollectionPage;

            Assert.AreEqual(page.Id, new Uri("http://example.org/foo?page=1"));
            Assert.AreEqual(page.PartOf.Link.Href, "http://example.org/foo");
            Assert.AreEqual(page.PartOf.Link.Href, "http://example.org/foo");
            Assert.AreEqual(page.Next.Href, "http://example.org/foo?page=2");
            Assert.AreEqual((page.Items[0].Obj as CreateActivity).Actor[0].Link.Href, "http://www.test.example/sally");
            Assert.AreEqual((page.Items[0].Obj as CreateActivity).Object[0].Url[0].Href, "http://example.org/foo");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample023()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example023.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(activity.NameMap["und"], "This is the title");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample024()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example024.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("@language"), "the extended language context did not exit");
            Assert.AreEqual("en", activity.ExtendedContexts["@language"], "the extended lanage was wrong");
            Assert.AreEqual("This is the title", activity.Name, "the extended lanage was wrong");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample028()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example028.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("gsp"), "the extended context did not exit");
            Assert.AreEqual("http://www.opengis.net/ont/geosparql", activity.ExtendedContexts["gsp"], "the extended context did not exit");
            Assert.AreEqual("Polygon((100.0, 0.0, 101.0, 0.0, 101.0, 1.0, 100.0, 1.0, 100.0, 0.0))", activity.Extensions["gsp:asWKT"], "the extention did not exit");
            Assert.AreEqual("Polygon((100.0, 0.0, 101.0, 0.0, 101.0, 1.0, 100.0, 1.0, 100.0, 0.0))", activity.Extensions["asWKT"], "the extention did not exit");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample029()
        {
            // support compact URI's
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example029.json"))
                            .Build();

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("ex"), "ex is missing");
            Assert.AreEqual("http://example.org/", activity.ExtendedContexts["ex"], "ex value is missing");
            Assert.IsTrue(activity.Extensions.ContainsKey("term"), "term is missing");

            Assert.IsTrue(activity.ExtendedIds.Any(), "the extended ids are not available");

            Assert.AreEqual("term", activity.ExtendedIds.First().Key, "term is missing");
            Assert.AreEqual("ex:term", activity.ExtendedIds["term"].Id, "ex:term value is missing");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample030()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example030.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("foo"), "the extended context did not exit");
            Assert.AreEqual("http://example.org/foo", activity.ExtendedContexts["foo"], "the extended context did not exit");
            Assert.AreEqual("123", activity.Extensions["foo"], "the extention did not exit");
            Assert.IsTrue(!activity.Extensions.ContainsKey("bar"), "the extention did exit");
            Assert.IsTrue(activity.ExtensionsOutOfContext.ContainsKey("bar"), "the extention did exit");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample032()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            Collection activity = (Collection)builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example032.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("oa"), "the extended context did not exit");
            Assert.AreEqual("http://www.w3.org/ns/oa#", activity.ExtendedContexts["oa"], "the extended context did not exit");

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("prov"), "the extended context did not exit");
            Assert.AreEqual("http://www.w3.org/ns/prov#", activity.ExtendedContexts["prov"], "the extended context did not exit");

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("dcterms"), "the extended context did not exit");
            Assert.AreEqual("http://purl.org/dc/terms/", activity.ExtendedContexts["dcterms"], "the extended context did not exit");

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("dcterms:created"), "the extended context did not exit");
            Assert.IsTrue(!activity.ExtendedIds.ContainsKey("dcterms:created"), "the extended context did not exit");
            Assert.IsTrue(!activity.Extensions.Any(), "We have extensions at the root and should not have.");

            Assert.IsTrue(activity.Items[0].Obj.ExtendedContexts.ContainsKey("dcterms:created"));
            Assert.IsTrue(activity.Items[0].Obj.ExtendedContexts.ContainsKey("dcterms"));
            Assert.IsTrue(activity.Items[0].Obj.ExtendedContexts.ContainsKey("prov"));
            Assert.IsTrue(activity.Items[0].Obj.ExtendedContexts.ContainsKey("oa"));
        }
    }
}
