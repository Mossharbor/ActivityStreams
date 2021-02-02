using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Mossharbor.ActivityStreams
{
    interface ICustomParser
    {
        /// <summary>
        /// Parses out the details specific to the Place Object
        /// </summary>
        /// <param name="el"></param>
        void PerformCustomParsing(JsonElement el);
    }
}
