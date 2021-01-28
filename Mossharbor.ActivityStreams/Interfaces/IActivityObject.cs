using System;
using System.Text.Json.Serialization;

namespace Mossharbor.ActivityStreams
{
    public interface IActivityObject
    {
        IActivityObjectOrLink[] Attachment { get; set; }
        IActivityObjectOrLink[] AttributedTo { get; set; }
        IActivityObjectOrLink[] Audience { get; set; }
        IActivityObjectOrLink[] bcc { get; set; }
        IActivityObjectOrLink[] Bto { get; set; }
        IActivityObjectOrLink[] CC { get; set; }
        string Content { get; set; }
        ContentMap ContentMap { get; set; }
        Uri Context { get; set; }
        string Duration { get; set; }
        DateTime EndTime { get; set; }
        IActivityObjectOrLink Generator { get; set; }
        Icon[] Icons { get; set; }
        Uri Id { get; set; }
        ImageObject[] Images { get; set; }
        IActivityObjectOrLink InReplyTo { get; set; }
        Location Location { get; set; }
        string MediaType { get; set; }
        string Name { get; set; }
        ContentMap NameMap { get; set; }
        IActivityObjectOrLink Preview { get; set; }
        DateTime Published { get; set; }
        Collection Replies { get; set; }
        DateTime StartTime { get; set; }
        string Summary { get; set; }
        ContentMap SummaryMap { get; set; }
        IActivityObjectOrLink[] To { get; set; }
        string Type { get; set; }
        DateTime Updated { get; set; }
        IActivityLink[] Url { get; set; }
    }
}