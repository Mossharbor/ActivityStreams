using System;
using System.Text.Json;

namespace Mossharbor.ActivityStreams
{
    interface IParsesChildObjects
    {
        void PerformCustomObjectParsing(JsonElement el, Func<JsonElement, IActivityObject, IActivityObject[]> activtyObjectsParser);
    }
}
