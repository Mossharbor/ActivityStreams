using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Mossharbor.ActivityStreams
{
    interface IParsesChildObject
    {
        void PerformCustomObjectParsing(JsonElement el, Func<JsonElement, IActivityObject> activtyObjectParser);
    }
}
