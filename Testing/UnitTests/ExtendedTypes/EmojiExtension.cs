using System;
using Mossharbor.ActivityStreams;
using System;
using System.Linq;

namespace Mossharbor.ActivityStreams.UnitTests
{
    /// <summary>
    /// this is a test class to test adding parsing of an emoji
    /// </summary>
    public class EmojiExtension : ActivityObject
    {
        /// <summary>
        /// the emoji type we are parsing
        /// </summary>
        public static string EmojiType = "Emoji";

        /// <summary>
        /// constructor
        /// </summary>
        public EmojiExtension() : base(EmojiType) { }
    }
}
