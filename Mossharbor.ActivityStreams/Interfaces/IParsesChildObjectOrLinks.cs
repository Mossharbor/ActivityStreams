using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Mossharbor.ActivityStreams
{
    interface IParsesChildObjectOrLinks
    {
        void PerformCustomObjectOrLinkParsing(JsonElement el, Func<JsonElement, IActivityObject, IActivityObjectOrLink[]> activtyOrLinkObjectParser);
    }
}
