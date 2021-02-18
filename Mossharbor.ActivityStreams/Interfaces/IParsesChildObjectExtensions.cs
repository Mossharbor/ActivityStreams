using System;
using System.Text.Json;

namespace Mossharbor.ActivityStreams
{
    interface IParsesChildObjectExtensions
    {
        void PerformCustomExtendedObjectParsing(JsonElement el, Action<JsonElement, IActivityObject> activtyExtensionParser);
    }
}
