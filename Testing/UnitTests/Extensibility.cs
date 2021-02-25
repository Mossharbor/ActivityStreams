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
    public class Extensibility
    {
        /// <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void AddingCustomEmojiTypes()
        {
            ActivityObjectBuilder builder = new ActivityObjectBuilder();
            builder.TryRegisterType(EmojiExtension.EmojiType, () => new EmojiExtension());

            NoteObject activity = (NoteObject)builder.FromJson(System.IO.File.OpenRead(@".\Extensions\customemoji.json"))
                            .ExpandJsonLD()
                            .Build();

            Assert.IsNotNull(activity.Tag[0] is EmojiExtension);
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("toot"), "toot is missing");
            Assert.IsTrue(activity.ExtendedContexts.ContainsKey("Emoji"), "Emoji is missing");
            Assert.IsTrue(activity.Tag[0].Obj.Type=="Emoji", "Emoji expansion is missing");
            Assert.IsTrue(activity.Tag[0].Obj.Name == ":kappa:", "Emoji expansion is missing");
            Assert.IsNotNull(activity.Tag[0].Obj.Icons[0].Obj is ImageObject, "Emoji expansion is missing Images");
        }

        /// <summary>
        /// Testing Spec example 1
        /// </summary>
        [TestMethod]
        public void AddingCustomPublicKeyTypes()
        {
            // TODO figure out how to do this easily.
        }
    }
}
