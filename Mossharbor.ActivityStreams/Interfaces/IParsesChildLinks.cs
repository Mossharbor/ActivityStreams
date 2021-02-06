using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Mossharbor.ActivityStreams
{
    interface IParsesChildLinks
    {
        void PerformCustomLinkParsing(JsonElement el, Func<JsonElement, IActivityLink[]> activtyLinkParser);
    }
}
