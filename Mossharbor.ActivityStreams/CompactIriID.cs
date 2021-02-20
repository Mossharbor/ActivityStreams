using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Mossharbor.ActivityStreams
{
    /// <summary>
    /// a representation of the compact iri format For Json
    /// </summary>
    /// <see cref="https://www.w3.org/TR/json-ld/#compact-iris"/>
    public class CompactIriID
    {
        private readonly JsonElement je;

        public CompactIriID(JsonElement je)
        {
            this.je = je;
            if (je.ContainsElement("@id"))
                this.Id = je.GetProperty("@id").ToString();
        }

        public string Id { get; set; }
    }
}
