using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// https://www.w3.org/ns/activitystreams#Article
    /// </summary>
    /// <example>
    ///{
    ///  "@context": "https://www.w3.org/ns/activitystreams",
    ///  "type": "Article",
    ///  "name": "What a Crazy Day I Had",
    ///  "content": "<div>... you will never believe ...</div>",
    ///  "attributedTo": "http://sally.example.org"
    ///}
    /// </example>
    /// <see cref="https://www.w3.org/ns/activitystreams#Article"/>
    public class ArticleObject : ActivityObject
    {
        /// <summary>
        /// the type constant for this Object
        /// </summary>
        public const string ArticleType = "Article";

        public ArticleObject() : base(type: ArticleType) { }
    }
}
