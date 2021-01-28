using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Mossharbor.ActivityStreams.UnitTests
{
    [TestClass]
    public class ParsingTests
    {
        [TestMethod]
        public void ParseBasicObjectWithType62 ()
        {
            ActivityBuilder builder = new ActivityBuilder();
            var activity = builder.FromJson(@"
                {
                  ""@context"": ""https://www.w3.org/ns/activitystreams"",
                  ""summary"": ""A foo"",
                  ""type"": ""http://example.org/Foo""
                }")
                .Build();

            Assert.AreEqual(activity.Summary, "A foo", "The summary Did not match");
            Assert.AreEqual(activity.Context.AbsoluteUri, "https://www.w3.org/ns/activitystreams", "The Uri Did not match");
            Assert.AreEqual(activity.Type, "http://example.org/Foo", "The type did not match");

        }
    }
}
