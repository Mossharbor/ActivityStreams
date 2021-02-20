using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mossharbor.ActivityStreams;
using System;
using System.Linq;

namespace Mossharbor.ActivityStreams.UnitTests
{
    /// <summary>
    /// this is the tsting for the subset of json LD we support
    /// </summary>
    [TestClass]
    public class ParsingJsonLDExamples
    {
        // <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void ParseJsonLDSpecExample032()
        {
            // Document providing context as an object using the @vocab keyword and a prefix for extension terms
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            var activity = builder.FromJson(System.IO.File.OpenRead(@".\JsonLDSpecExamples\example032.json"))
                            .Build();

            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("@version"), "version is missing");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("xsd"), "xsd is missing");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("foaf"), "foaf is missing");

            Assert.IsTrue(activity.Extensions.ContainsKey("foaf:name"), "foaf:name is missing");
            Assert.IsTrue(activity.Extensions.ContainsKey("foaf:homepage"), "foaf:homepage is missing");
            Assert.IsTrue(activity.Extensions.ContainsKey("picture"), "picture is missing");

            Assert.IsTrue(activity.ExtendedIds.Any(), "the extended ids are not available");

            Assert.AreEqual("foaf:homepage", activity.ExtendedIds.First().Key, "homepage is missing");
            Assert.AreEqual("picture", activity.ExtendedIds.ElementAt(1).Key, "picture is missing");
            Assert.IsNull(activity.ExtendedIds.First().Value.Id, "the extended idfor homepage was not null");
            Assert.AreEqual("foaf:depiction", activity.ExtendedIds.ElementAt(1).Value.Id, "the extended ids are not available");
        }
    }
}
