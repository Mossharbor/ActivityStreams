using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mossharbor.ActivityStreams;

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
