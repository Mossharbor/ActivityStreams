using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    public interface IActivityObject
    {


        /// <summary>
        /// this represents the extended context we can have for extending ativity streams
        /// </summary>
        /// <example>
        ///{
        ///  "@context": {
        ///     "@vocab": "https://www.w3.org/ns/activitystreams",
        ///     "ext": "https://canine-extension.example/terms/",
        ///     "@language": "en"
        ///  },
        ///  "summary": "A note",
        ///  "type": "Note",
        ///  "content": "My dog has fleas.",
        ///  "ext:nose": 0,
        ///  "ext:smell": "terrible"
        ///}
        /// </example>
        IDictionary<string, string> ExtendedContexts { get; set; }

        /// <summary>
        /// this represents the extentions that are outside of the activity streams spec but addd in the context
        /// </summary>
        /// <example>
        ///{
        ///  "@context": {
        ///     "@vocab": "https://www.w3.org/ns/activitystreams",
        ///     "ext": "https://canine-extension.example/terms/",
        ///     "@language": "en"
        ///  },
        ///  "summary": "A note",
        ///  "type": "Note",
        ///  "content": "My dog has fleas.",
        ///  "ext:nose": 0,
        ///  "ext:smell": "terrible"
        ///}
        /// </example>
        IDictionary<string, string> Extensions { get; set; }

        IActivityObjectOrLink[] Attachment { get; set; }
        IActivityObjectOrLink[] AttributedTo { get; set; }
        IActivityObjectOrLink[] Audience { get; set; }
        IActivityObjectOrLink[] bcc { get; set; }
        IActivityObjectOrLink[] Bto { get; set; }
        IActivityObjectOrLink[] CC { get; set; }
        /// <summary>
        /// One or more "tags" that have been associated with an objects.
        /// A tag can be any kind of Object. 
        /// The key difference between attachment and tag is that the former
        /// implies association by inclusion, while the latter implies associated by reference.
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "type": "Image",
        ///  "summary": "Picture of Sally",
        ///  "url": "http://example.org/sally.jpg",
        ///  "tag": [
        ///    {
        ///      "type": "Person",
        ///      "id": "http://sally.example.org",
        ///      "name": "Sally"
        ///    }
        ///  ]
        ///}
        ///  </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#tag"/>
        IActivityObjectOrLink[] Tag { get; set; }
        string Content { get; set; }
        ContentMap ContentMap { get; set; }
        Uri Context { get; set; }
        TimeSpan? Duration { get; set; }
        DateTime? EndTime { get; set; }
        IActivityObjectOrLink[] Generator { get; set; }
        IActivityObjectOrLink[] Icons { get; set; }
        Uri Id { get; set; }
        IActivityObjectOrLink[] Images { get; set; }
        IActivityObjectOrLink[] InReplyTo { get; set; }
        PlaceObject Location { get; set; }
        string MediaType { get; set; }
        string Name { get; set; }
        ContentMap NameMap { get; set; }
        IActivityObjectOrLink[] Preview { get; set; }
        DateTime? Published { get; set; }
        Collection Replies { get; set; }
        DateTime? StartTime { get; set; }
        string Summary { get; set; }
        ContentMap SummaryMap { get; set; }
        IActivityObjectOrLink[] To { get; set; }
        string Type { get; set; }
        DateTime? Updated { get; set; }
        IActivityLink[] Url { get; set; }
    }
}