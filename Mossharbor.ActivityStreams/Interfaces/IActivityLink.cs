using System;

namespace Mossharbor.ActivityStreams
{
    public interface IActivityLink
    {
        long Height { get; set; }
        string Href { get; set; }
        string HrefLang { get; set; }
        string MediaType { get; set; }
        string Name { get; set; }
        string[] Rel { get; set; }
        string Type { get; set; }
        Uri Url { get; set; }
        long Width { get; set; }
    }
}