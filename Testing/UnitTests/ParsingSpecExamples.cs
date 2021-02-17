using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mossharbor.ActivityStreams;
using System;

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
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample015()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example025.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample020()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example0020.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
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
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample024()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example0024.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
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
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample029()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example029.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
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
        }

        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseActivityStreamSpecExample032()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\SpecTestFiles\example032.json"))
                            .Build();

            Assert.IsNotNull(activity.Context, "the activity stream context was null");
        }
    }
}
