using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS1658 // Warning is overriding an error
namespace Mossharbor.ActivityStreams
{
    public class ActivityObject : IActivityObject, ICustomParser, IParsesChildObjectOrLinks, IParsesChildLinks, IParsesChildObject
    {
        internal ActivityObject() { }

        protected ActivityObject(string type) { this.Type = type; }

        /// <summary>
        /// Provides the globally unique identifier for an Object or Link.
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "name": "Foo",
        ///  "id": "http://example.org/foo"
        ///}
        /// </example>
        [JsonPropertyName("id")]
        public Uri Id { get; set; }

        /// <summary>
        /// Identifies the context within which the object exists or an activity was performed.
        /// The notion of "context" used is intentionally vague.The intended function is to serve as a 
        /// means of grouping objects and activities that share a common originating context or purpose.
        /// An example could be all activities relating to a common project or event.
        /// </summary>
        /// <see cref="	https://www.w3.org/ns/activitystreams#context"/>
        [JsonPropertyName("@context")]
        public Uri Context { get; set; }

        /// <summary>
        /// Identifies the Object or Link type. Multiple values may be specified.
        /// </summary>
        [JsonPropertyName("type")]
        public virtual string Type { get; set; }

        /// <summary>
        /// Identifies a resource attached or related to an object that potentially requires special handling.
        /// The intent is to provide a model that is at least semantically similar to attachments in email.
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "type": "Note",
        ///  "name": "Have you seen my cat?",
        ///  "attachment": [
        ///    {
        ///      "type": "Image",
        ///      "content": "This is what he looks like.",
        ///      "url": "http://example.org/cat.jpeg"
        ///    }
        ///  ]
        ///}
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#attachment"/>
        [JsonPropertyName("attachment")]
        public IActivityObjectOrLink[] Attachment { get; set; }

        /// <summary>
        /// Identifies one or more entities to which this object is attributed. 
        /// The attributed entities might not be Actors. For instance, an object might be attributed to the completion of another activity.
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "type": "Image",
        ///  "name": "My cat taking a nap",
        ///  "url": "http://example.org/cat.jpeg",
        ///  "attributedTo": [
        ///    {
        ///      "type": "Person",
        ///      "name": "Sally"
        ///    }
        ///  ]
        ///}
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#attributedTo"/>
        [JsonPropertyName("attributedTo")]
        public IActivityObjectOrLink[] AttributedTo { get; set; }

        /// <summary>
        /// the audience this activity is for
        /// </summary>
        /// <example>
        ///{
        /// "@context": "https://www.w3.org/ns/activitystreams",
        /// "summary": "Activities in Project XYZ",
        /// "type": "Collection",
        /// "items": [
        ///   {
        ///     "summary": "Sally created a note",
        ///     "type": "Create",
        ///     "id": "http://activities.example.com/1",
        ///     "actor": "http://sally.example.org",
        ///     "object": {
        ///      "summary": "A note",
        ///       "type": "Note",
        ///       "id": "http://notes.example.com/1",
        ///       "content": "A note"
        ///     },
        ///     "context": {
        ///       "type": "http://example.org/Project",
        ///       "name": "Project XYZ"
        ///     },
        ///     "audience": {
        ///    "type": "Group",
        ///       "name": "Project XYZ Working Group"
        ///     },
        ///     "to": "http://john.example.org"
        ///   },
        ///   {
        ///    "summary": "John liked Sally's note",
        ///     "type": "Like",
        ///     "id": "http://activities.example.com/1",
        ///     "actor": "http://john.example.org",
        ///     "object": "http://notes.example.com/1",
        ///     "context": {
        ///        "type": "http://example.org/Project",
        ///       "name": "Project XYZ"
        ///     },
        ///     "audience": {
        ///        "type": "Group",
        ///       "name": "Project XYZ Working Group"
        ///     },
        ///     "to": "http://sally.example.org"
        ///   }
        /// ]
        ///}
        /// </example>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "name": "Holiday announcement",
        ///  "type": "Note",
        ///  "content": "Thursday will be a company-wide holiday. Enjoy your day off!",
        ///  "audience": {
        ///    "type": "http://example.org/Organization",
        ///    "name": "ExampleCo LLC"
        ///  }
        ///}
        /// </example>
        [JsonPropertyName("audience")]
        public IActivityObjectOrLink[] Audience { get; set; }

        [JsonPropertyName("mediaType")]
        public string MediaType { get; set; } // TODO assume text/html if nothing is here.

        private string content = null;

        /// <summary>
        /// The content or textual representation of the Object encoded as a JSON string. 
        /// By default, the value of content is HTML. The mediaType property can be used in the object to indicate a different content type.
        /// The content may be expressed using multiple language-tagged values.
        /// </summary>
        /// <see cref="https://www.w3.org/ns/activitystreams#content"/>
        [JsonPropertyName("content")]
        public string Content
        {
            get
            {
                if (content != null)
                    return content;

                if (this.ContentMap != null)
                    return this.ContentMap.GetContent();

                return null;
            }
            set
            {
                content = value;
            }
        }

        /// <summary>
        /// The content or textual representation of the Object encoded as a JSON string. 
        /// By default, the value of content is HTML. The mediaType property can be used in the object to indicate a different content type.
        /// The content may be expressed using multiple language-tagged values.
        /// </summary>
        /// <see cref="https://www.w3.org/ns/activitystreams#content"/>
        [JsonPropertyName("contentMap")]
        public ContentMap ContentMap { get; set; }

        private string name = null;

        /// <summary>
        /// A simple, human-readable, plain-text name for the object. HTML markup must not be included. The name may be expressed using multiple language-tagged values.
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "type": "Note",
        ///  "name": "A simple note"
        ///}
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#name"/>
        [JsonPropertyName("name")]
        public string Name
        {
            get
            {
                if (name != null)
                    return name;

                if (this.NameMap != null)
                    return this.NameMap.GetContent();

                return null;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// A simple, human-readable, plain-text name for the object. HTML markup must not be included. The name may be expressed using multiple language-tagged values.
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "type": "Note",
        ///  "nameMap": {
        ///    "en": "A simple note",
        ///    "es": "Una nota sencilla",
        ///    "zh-Hans": "一段简单的笔记"
        ///  }
        ///}
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#name"/>
        [JsonPropertyName("namemMap")]
        public ContentMap NameMap { get; set; }

        /// <summary>
        /// The date and time describing the actual or expected ending time of the object. 
        /// When used with an Activity object, for instance, the endTime property specifies the moment the activity concluded or is expected to conclude.
        /// xsd:dateTime
        /// </summary>
        /// <example>2014-12-12T12:12:12Z</example>
        /// <see cref="https://www.w3.org/ns/activitystreams#endTime"/>
        [JsonPropertyName("endTime")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// The date and time describing the actual or expected starting time of the object.
        /// When used with an Activity object, for instance, the startTime property specifies the moment the activity began or is scheduled to begin
        /// xsd:dateTime
        /// </summary>
        /// <example>2014-12-12T12:12:12Z</example>
        /// <see cref="https://www.w3.org/ns/activitystreams#startTime"/>
        [JsonPropertyName("startime")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// The date and time at which the object was updated
        /// xsd:dateTime
        /// </summary>
        /// <example>2014-12-12T12:12:12Z</example>
        /// <see cref="https://www.w3.org/ns/activitystreams#updated"/>
        [JsonPropertyName("updated")]
        public DateTime? Updated { get; set; }

        /// <summary>
        /// The date and time at which the object was published
        /// xsd:dateTime
        /// </summary>
        /// <example>2014-12-12T12:12:12Z</example>
        /// <see cref="https://www.w3.org/ns/activitystreams#published"/>
        [JsonPropertyName("published")]
        public DateTime? Published { get; set; }

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
        [JsonPropertyName("tag")]
        public IActivityObjectOrLink[] Tag { get; set; }

        /// <summary>
        /// Identifies one or more links to representations of the object
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "type": "Document",
        ///  "name": "4Q Sales Forecast",
        ///  "url": "http://example.org/4q-sales-forecast.pdf"
        ///}
        /// </example>
        ///  <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "type": "Document",
        ///  "name": "4Q Sales Forecast",
        ///  "url": [
        ///    {
        ///      "type": "Link",
        ///      "href": "http://example.org/4q-sales-forecast.pdf",
        ///      "mediaType": "application/pdf"
        ///    },
        ///    {
        ///      "type": "Link",
        ///      "href": "http://example.org/4q-sales-forecast.html",
        ///      "mediaType": "text/html"
        ///    }
        ///  ]
        ///}
        ///  </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#url"/>
        [JsonPropertyName("url")]
        public IActivityLink[] Url { get; set; }

        /// <summary>
        /// Identifies an entity considered to be part of the public primary audience of an Object
        /// </summary>
        /// <see cref="https://www.w3.org/ns/activitystreams#to"/>
        [JsonPropertyName("to")]
        public IActivityObjectOrLink[] To { get; set; }

        /// <summary>
        /// Identifies an Object that is part of the private primary audience of this Object.
        /// </summary>
        /// <example>
        /// {
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "summary": "Sally offered a post to John",
        ///  "type": "Offer",
        ///  "actor": "http://sally.example.org",
        ///  "object": "http://example.org/posts/1",
        ///  "target": "http://john.example.org",
        ///  "bto": [ "http://joe.example.org" ]
        /// }
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#bto"/>
        [JsonPropertyName("bto")]
        public IActivityObjectOrLink[] Bto { get; set; }

        /// <summary>
        /// Identifies an Object that is part of the public secondary audience of this Object.
        /// </summary>
        /// <see cref="https://www.w3.org/ns/activitystreams#cc"/>
        [JsonPropertyName("cc")]
        public IActivityObjectOrLink[] CC { get; set; }

        /// <summary>
        /// Identifies one or more Objects that are part of the private secondary audience of this Object.
        /// </summary>
        /// <see cref="https://www.w3.org/ns/activitystreams#bcc"/>
        [JsonPropertyName("bcc")]
        public IActivityObjectOrLink[] bcc { get; set; }

        /// <summary>
        /// Identifies the entity (e.g. an application) that generated the object.
        /// </summary>
        /// <see cref="https://www.w3.org/ns/activitystreams#generator"/>
        [JsonPropertyName("generator")]
        public IActivityObjectOrLink[] Generator { get; set; }

        /// <summary>
        /// Indicates an entity that describes an icon for this object. 
        /// The image should have an aspect ratio of one (horizontal) to one (vertical) and should be suitable for presentation at a small size.
        /// </summary>
        /// <example>
        ///{
        ///  "@context": "https://www.w3.org/ns/activitystreams",
        ///  "summary": "A simple note",
        ///  "type": "Note",
        ///  "content": "A simple note",
        ///  "icon": [
        ///    {
        ///      "type": "Image",
        ///      "summary": "Note (16x16)",
        ///      "url": "http://example.org/note1.png",
        ///      "width": 16,
        ///      "height": 16
        ///    },
        ///    {
        ///      "type": "Image",
        ///      "summary": "Note (32x32)",
        ///      "url": "http://example.org/note2.png",
        ///      "width": 32,
        ///      "height": 32
        ///    }
        ///  ]
        ///}
        /// </example>
        /// <see cref="https://www.w3.org/ns/activitystreams#icon"/>
        [JsonPropertyName("icon")]
        public IActivityObjectOrLink[] Icons { get; set; }

        /// <summary>
        /// Indicates an entity that describes an image for this object. Unlike the icon property, there are no aspect ratio or display size limitations assumed.
        /// </summary>
        /// <see cref="https://www.w3.org/ns/activitystreams#image"/>
        [JsonPropertyName("image")]
        public IActivityObjectOrLink[] Images { get; set; }

        /// <summary>
        /// Indicates one or more entities for which this object is considered a response.
        /// </summary>
        /// <see cref="https://www.w3.org/ns/activitystreams#inReplyTo"/>
        [JsonPropertyName("inReplyTo")]
        public IActivityObjectOrLink[] InReplyTo { get; set; }

        /// <summary>
        /// Identifies a Collection containing objects considered to be responses to this object.
        /// </summary>
        /// <see cref="	https://www.w3.org/ns/activitystreams#replies"/>
        [JsonPropertyName("replies")]
        public Collection Replies { get; set; }

        /// <summary>
        /// Indicates one or more physical or logical locations associated with the object.
        /// </summary>
        [JsonPropertyName("location")]
        public PlaceObject Location { get; set; }

        /// <summary>
        /// A natural language summarization of the object encoded as HTML. Multiple language tagged summaries may be provided.
        /// </summary>
        /// <see cref="https://www.w3.org/ns/activitystreams#summary"/>
        [JsonPropertyName("summary")]
        public string Summary
        {
            get
            {
                if (summary != null)
                    return summary;

                if (this.SummaryMap != null)
                    return this.SummaryMap.GetContent();

                return null;
            }
            set
            {
                summary = value;
            }
        }

        private string summary;

        /// <summary>
        /// A natural language summarization of the object encoded as HTML. Multiple language tagged summaries may be provided.
        /// </summary>
        /// <see cref="https://www.w3.org/ns/activitystreams#summary"/>
        [JsonPropertyName("summaryMap")]
        public ContentMap SummaryMap { get; set; }

        /// <summary>
        /// When the object describes a time-bound resource, such as an audio or video, a meeting, etc, the duration property indicates the object's approximate duration. The value must be expressed as an xsd:duration as defined by [ xmlschema11-2], section 3.3.6 (e.g. a period of 5 seconds is represented as "PT5S").
        /// </summary>
        /// <see cref="https://www.w3.org/ns/activitystreams#duration"/>
        [JsonPropertyName("duration")]
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Identifies an entity that provides a preview of this object.
        /// </summary>
        [JsonPropertyName("preview")]
        public IActivityObjectOrLink[] Preview { get; set; }

        /// <inheritdoc/>
        public virtual void PerformCustomParsing(JsonElement el)
        {
            string typeString = el.TryGetProperty("type", out JsonElement typeProperty) ? ActivityStreamsParser.GetActivityType(typeProperty) : null;
            var idElement = el.GetUriOrDefault("id");
            var summary = el.GetStringOrDefault("summary");
            var name = el.GetStringOrDefault("name");
            var content = el.GetStringOrDefault("content");
            var context = el.GetUriOrDefault("@context");
            var context2 = el.GetUriOrDefault("context");
            var mediaType = el.GetStringOrDefault("mediaType");
            DateTime? start = el.GetDateTimeOrDefault("startTime");
            DateTime? end = el.GetDateTimeOrDefault("endTime");
            DateTime? updated = el.GetDateTimeOrDefault("updated");
            DateTime? published = el.GetDateTimeOrDefault("published");
            TimeSpan? duration = el.GetTimeSpanOrDefault("duration");  // NOTE to write  var duration = (TimeSpan) value; writer.WriteValue(XmlConvert.ToString(duration));

            this.Id = idElement;
            this.Summary = summary;
            this.Context = context ?? context2;
            this.Type = typeString;
            this.Name = name;
            this.Content = content;
            this.Duration = duration;
            this.StartTime = start;
            this.EndTime = end;
            this.Published = published;
            this.Updated = updated;
            this.MediaType = mediaType;

        }

        /// <inheritdoc/>
        public virtual void PerformCustomObjectOrLinkParsing(JsonElement el, Func<JsonElement, IActivityObjectOrLink[]> activtyOrLinkObjectParser)
        {
            if (el.TryGetProperty("attributedTo", out JsonElement attributeTo))
            {
                this.AttributedTo = activtyOrLinkObjectParser(attributeTo);
            }

            if (el.TryGetProperty("attachment", out JsonElement attachmentEl))
            {
                this.Attachment = activtyOrLinkObjectParser(attachmentEl);
            }

            if (el.TryGetProperty("audience", out JsonElement audienceEl))
            {
                this.Audience = activtyOrLinkObjectParser(audienceEl);
            }

            if (el.TryGetProperty("inReplyTo", out JsonElement inReplyToEl))
            {
                this.InReplyTo = activtyOrLinkObjectParser(inReplyToEl);
            }

            if (el.ContainsElement("bcc"))
            {
                this.bcc = activtyOrLinkObjectParser(el.GetProperty("bcc"));
            }

            if (el.ContainsElement("bto"))
            {
                this.Bto = activtyOrLinkObjectParser(el.GetProperty("bto"));
            }

            if (el.ContainsElement("cc"))
            {
                this.CC = activtyOrLinkObjectParser(el.GetProperty("cc"));
            }

            if (el.ContainsElement("to"))
            {
                this.To = activtyOrLinkObjectParser(el.GetProperty("to"));
            }

            if (el.ContainsElement("generator"))
            {
                this.Generator = activtyOrLinkObjectParser(el.GetProperty("generator"));
            }

            if (el.ContainsElement("preview"))
            {
                this.Preview = activtyOrLinkObjectParser(el.GetProperty("preview"));
            }

            if (el.ContainsElement("tag"))
            {
                this.Tag = activtyOrLinkObjectParser(el.GetProperty("tag"));
            }

            if (el.TryGetProperty("image", out JsonElement imageEl))
            {
                this.Images = activtyOrLinkObjectParser(imageEl);
            }

            if (el.TryGetProperty("icon", out JsonElement iconEl))
            {
                this.Icons = activtyOrLinkObjectParser(iconEl);
            }

            if (el.ContainsElement("contentMap"))
            {
                this.ContentMap = ParseOutMap(el.GetProperty("contentMap"));
            }

            if (el.ContainsElement("nameMap"))
            {
                this.NameMap = ParseOutMap(el.GetProperty("nameMap"));
            }

            if (el.ContainsElement("summaryMap"))
            {
                this.SummaryMap = ParseOutMap(el.GetProperty("summaryMap"));
            }
        }

        /// <summary>
        /// Parses ouot the maps
        /// </summary>
        /// <param name="el">the json element to parse</param>
        /// <returns>thecontent map</returns>
        private static ContentMap ParseOutMap(JsonElement el)
        {
            if (el.ValueKind == JsonValueKind.Undefined || el.ValueKind == JsonValueKind.Null)
                return null;

            JsonProperty[] elementArray = el.ValueKind == JsonValueKind.Object ? el.EnumerateObject().ToArray() : throw new InvalidContentMapException(el.ToString());
            Dictionary<string, string> mapping = new Dictionary<string, string>();


            for (int i = 0; i < elementArray.Length; ++i)
            {
                var toParse = elementArray[i];

                mapping.Add(toParse.Name, toParse.Value.ToString());
            }

            return new ContentMap(mapping);
        }

        /// <inheritdoc/>
        public virtual void PerformCustomLinkParsing(JsonElement el, Func<JsonElement, IActivityLink[]> activtyLinkParser)
        {
            if (el.TryGetProperty("url", out JsonElement urlEl))
            {
                this.Url = activtyLinkParser(urlEl);
            }
        }

        /// <inheritdoc/>
        public virtual void PerformCustomObjectParsing(JsonElement el, Func<JsonElement, IActivityObject> activtyObjectParser)
        {
            if (el.ContainsElement("replies"))
            {
                this.Replies = (activtyObjectParser(el.GetProperty("replies")) as Collection);
            }

            if (el.TryGetProperty("location", out JsonElement localEl))
            {
                this.Location = activtyObjectParser(localEl) as PlaceObject;
            }
        }
    }
}
