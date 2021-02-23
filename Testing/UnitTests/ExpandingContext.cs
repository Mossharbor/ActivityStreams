using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mossharbor.ActivityStreams;

namespace Mossharbor.ActivityStreams.UnitTests
{
    /// <summary>
    /// Validate that we can expand context into values and keys
    /// </summary>
    [TestClass]
    public class ExpandingContext
    {
        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ExpandCustomStreamValueConcat()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example002.json"))
                            .ExpandExtentionContexts()
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(new Uri("https://www.w3.org/ns/activitystreams"), activity.Context, "the activity stream context was not correct");

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("ext"), "the extended context did not exit");
            Assert.AreEqual("https://canine-extension.example/terms/", activity.ExtendedContexts["ext"], "the extended context was wrong");

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("@language"), "the extended language context did not exit");
            Assert.AreEqual("en", activity.ExtendedContexts["@language"], "the extended lanage was wrong");

            Assert.IsTrue(activity.Extensions.ContainsKey("https://canine-extension.example/terms/nose"), "the extension did not exit");
            Assert.AreEqual("0", activity.Extensions["https://canine-extension.example/terms/nose"], "the extension was wrong");

            Assert.IsTrue(activity.Extensions.ContainsKey("https://canine-extension.example/terms/smell"), "the extension did not exit");
            Assert.AreEqual("terrible", activity.Extensions["https://canine-extension.example/terms/smell"], "the extension was wrong");

            Assert.AreEqual("A note", activity.Summary, "the activity stream summary");
            Assert.IsNotNull(activity as NoteObject, "the activity was not a note");
            Assert.AreEqual("My dog has fleas.", activity.Content, "the activity stream content");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ExpandCustomStreamValueExact()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example003.json"))
                            .ExpandExtentionContexts()
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(new Uri("https://www.w3.org/ns/activitystreams"), activity.Context, "the activity stream context was not correct");

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("css"), "the extended context did not exit");
            Assert.AreEqual("http://www.w3.org/ns/oa#styledBy", activity.ExtendedContexts["css"], "the extended context was wrong");

            Assert.IsTrue(activity.Extensions.ContainsKey("http://www.w3.org/ns/oa#styledBy"), "the extension did not exit");
            Assert.AreEqual("http://www.csszengarden.com/217/217.css?v=8may2013", activity.Extensions["http://www.w3.org/ns/oa#styledBy"], "the extension was wrong");

            Assert.IsFalse(activity.Extensions.ContainsKey("summary"), "the extension did contain the summary and should not have");

            Assert.AreEqual("A note", activity.Summary, "the activity stream summary");
            Assert.IsNotNull(activity as NoteObject, "the activity was not a note");
            Assert.AreEqual("My dog has fleas.", activity.Content, "the activity stream content");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ExpandCustomStreamTypes()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example008.json"))
                            .ExpandExtentionContexts()
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.AreEqual(new Uri("https://www.w3.org/ns/activitystreams"), activity.Context, "the activity stream context was not correct");

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("gr"), "the extended context did not gr");
            Assert.AreEqual("http://purl.org/goodrelations/v1#", activity.ExtendedContexts["gr"], "the extended context was wrong");

            Assert.IsTrue(activity.Extensions.ContainsKey("http://purl.org/goodrelations/v1#category"), "the extension did not exit");
            Assert.AreEqual("restaurants/french_restaurants", activity.Extensions["http://purl.org/goodrelations/v1#category"], "the extension was wrong");

            Assert.IsFalse(activity.Extensions.ContainsKey("category"), "the extension did not exit");

            Assert.IsTrue(activity.ExtendedTypes.Contains("http://purl.org/goodrelations/v1#Location"), "the extended Type did not exit");

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
        public void ExpandCustomStreamSubActivities()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            CreateActivity activity = (CreateActivity)builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example015.json"))
                            .ExpandExtentionContexts()
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

            Assert.IsTrue(activity.Actor[0].Obj.Extensions.ContainsKey("http://www.w3.org/2006/vcard/ns#given-name"), "the extension did not exist");
            Assert.AreEqual("Sally", activity.Actor[0].Obj.Extensions["http://www.w3.org/2006/vcard/ns#given-name"], "the extension was wrong");

            Assert.IsTrue(activity.Actor[0].Obj.Extensions.ContainsKey("http://www.w3.org/2006/vcard/ns#family-name"), "the extension did not exist");
            Assert.AreEqual("Smith", activity.Actor[0].Obj.Extensions["http://www.w3.org/2006/vcard/ns#family-name"], "the extension was wrong");

            Assert.IsTrue(activity.Actor[0].Obj.ExtendedTypes.Contains("http://www.w3.org/2006/vcard/ns#Individual"), "the extended Typedid not exist");

            Assert.IsFalse(activity.Actor[0].Obj.Extensions.ContainsKey("name"), "the extension did contain the name and should not have");
        }


        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ExpandCustomStreamTypes2()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example028.json"))
                            .ExpandExtentionContexts()
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("gsp"), "the extended context did not exit");
            Assert.AreEqual("http://www.opengis.net/ont/geosparql", activity.ExtendedContexts["gsp"], "the extended context did not exit");
            Assert.AreEqual("Polygon((100.0, 0.0, 101.0, 0.0, 101.0, 1.0, 100.0, 1.0, 100.0, 0.0))", activity.Extensions["http://www.opengis.net/ont/geosparqlasWKT"], "the extention did not exit");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ExpandCustomStreamValue()
        {
            // support compact URI's
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example029.json"))
                            .ExpandExtentionContexts()
                            .Build();

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("ex"), "ex is missing");
            Assert.AreEqual("http://example.org/", activity.ExtendedContexts["ex"], "ex value is missing");
            Assert.IsFalse(activity.Extensions.ContainsKey("term"), "term is not missing");
            Assert.AreEqual("http://example.org/Foo", activity.Extensions["http://example.org/term"], "ex value is missing");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ExpandCustomStreamNameValue()
        {
            // support compact URI's
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\JsonLDSpecExamples\example032.json"))
                            .ExpandExtentionContexts()
                            .Build();

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("@version"), "version is missing");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("xsd"), "xsd is missing");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("foaf"), "foaf is missing");

            Assert.IsFalse(activity.Extensions.ContainsKey("foaf:name"), "foaf:name is not missing");
            Assert.IsFalse(activity.Extensions.ContainsKey("foaf:homepage"), "foaf:homepage is not missing");
            Assert.IsFalse(activity.Extensions.ContainsKey("picture"), "picture is not missing");

            Assert.IsTrue(activity.Extensions.ContainsKey("http://xmlns.com/foaf/0.1/name"), "foaf:name is missing");
            Assert.IsTrue(activity.Extensions.ContainsKey("http://xmlns.com/foaf/0.1/homepage"), "foaf:homepage is missing");
            Assert.IsTrue(activity.Extensions.ContainsKey("http://xmlns.com/foaf/0.1/depiction"), "picture is missing");

            Assert.IsTrue(activity.ExtendedIds.Any(), "the extended ids are not available");

            Assert.AreEqual("foaf:homepage", activity.ExtendedIds.First().Key, "homepage is missing");
            Assert.AreEqual("picture", activity.ExtendedIds.ElementAt(1).Key, "picture is missing");
            Assert.IsNull(activity.ExtendedIds.First().Value.Id, "the extended idfor homepage was not null");
            Assert.AreEqual("foaf:depiction", activity.ExtendedIds.ElementAt(1).Value.Id, "the extended ids are not available");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ExpandCustomStreamBlurHash()
        {
            // support compact URI's
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            NoteObject activity = (NoteObject)builder.FromJson(System.IO.File.OpenRead(@".\Extensions\blurhash.json"))
                            .ExpandExtentionContexts()
                            .Build();

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("toot"), "toot is missing");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("blurhash"), "blurhash is missing");
            Assert.IsTrue(activity.Attachment[0].Obj.Extensions.ContainsKey("http://joinmastodon.org/ns#blurhash"), "blurhash expansion is missing");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ExpandCustomStreamEmoji()
        {
            // support compact URI's
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            NoteObject activity = (NoteObject)builder.FromJson(System.IO.File.OpenRead(@".\Extensions\customemoji.json"))
                            .ExpandExtentionContexts()
                            .Build();

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("toot"), "toot is missing");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("Emoji"), "Emoji is missing");
            Assert.IsTrue(activity.Tag[0].Obj.ExtendedTypes.Contains("http://joinmastodon.org/ns#Emoji"), "Emoji expansion is missing");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ExpandCustomStreamDiscoverability()
        {
            // support compact URI's
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            PersonActor activity = (PersonActor)builder.FromJson(System.IO.File.OpenRead(@".\Extensions\discoverability.json"))
                            .ExpandExtentionContexts()
                            .Build();

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("toot"), "toot is missing");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("discoverable"), "discoverable is missing");
            Assert.IsTrue(activity.Extensions.ContainsKey("http://joinmastodon.org/ns#discoverable"), "discoverable expansion is missing");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ExpandCustomStreamFeatured()
        {
            // support compact URI's
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            PersonActor activity = (PersonActor)builder.FromJson(System.IO.File.OpenRead(@".\Extensions\featured.json"))
                            .ExpandExtentionContexts()
                            .Build();

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("toot"), "toot is missing");
            Assert.IsTrue(activity.ExtendedIds.ContainsKey("featured"), "featured is missing");
            Assert.AreEqual("http://joinmastodon.org/ns#featured", activity.ExtendedIds["featured"].ExpandedId, "featured is missing");
            Assert.IsTrue(activity.Extensions.ContainsKey("http://joinmastodon.org/ns#featured"), "featured expansion is missing");
            Assert.AreEqual(activity.Extensions["http://joinmastodon.org/ns#featured"], "https://example.com/@alice/collections/featured", "featured is missing");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ExpandCustomStreamFeaturedTags()
        {
            // support compact URI's
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            PersonActor activity = (PersonActor)builder.FromJson(System.IO.File.OpenRead(@".\Extensions\featuredtags.json"))
                            .ExpandExtentionContexts()
                            .Build();

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("toot"), "toot is missing");
            Assert.IsTrue(activity.ExtendedIds.ContainsKey("featuredTags"), "featured is missing");
            Assert.AreEqual("http://joinmastodon.org/ns#featuredTags", activity.ExtendedIds["featuredTags"].ExpandedId, "featuredTags is missing");
            Assert.IsTrue(activity.Extensions.ContainsKey("http://joinmastodon.org/ns#featuredTags"), "featuredTags expansion is missing");
            Assert.AreEqual(activity.Extensions["http://joinmastodon.org/ns#featuredTags"], "https://example.com/@alice/collections/tags", "featured is missing");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ExpandCustomStreamIdentityProofs()
        {
            // support compact URI's
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            PersonActor activity = (PersonActor)builder.FromJson(System.IO.File.OpenRead(@".\Extensions\identityproofs.json"))
                            .ExpandExtentionContexts()
                            .Build();

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("toot"), "toot is missing");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("IdentityProof"), "IdentityProof is missing");
            Assert.IsTrue(activity.Attachment[0].Obj.ExtendedTypes.Contains("http://joinmastodon.org/ns#IdentityProof"), "featuredTags expansion is missing");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ExpandCustomStreamPublicKey()
        {
            // support compact URI's
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            PersonActor activity = (PersonActor)builder.FromJson(System.IO.File.OpenRead(@".\Extensions\publickey.json"))
                            .ExpandExtentionContexts()
                            .Build();

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("https://w3id.org/security/v1"), "https://w3id.org/security/v1 is missing");
            Assert.IsTrue(activity.ExtensionsOutOfContext.ContainsKey("publicKey"), "featuredTags expansion is missing");
        }


        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ExpandCustomStreamIdentitySuspended()
        {
            // support compact URI's
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            PersonActor activity = (PersonActor)builder.FromJson(System.IO.File.OpenRead(@".\Extensions\suspended.json"))
                            .ExpandExtentionContexts()
                            .Build();

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("toot"), "toot is missing");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("suspended"), "suspended is missing");
            Assert.AreEqual("http://joinmastodon.org/ns#suspended", activity.ExtendedContexts["suspended"], "suspended is missing");
            Assert.IsTrue(activity.Extensions.ContainsKey("http://joinmastodon.org/ns#suspended"), "suspended expansion is missing");
            Assert.AreEqual(activity.Extensions["http://joinmastodon.org/ns#suspended"], "True", "suspended is missing");
        }
    }
}
