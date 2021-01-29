using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    internal class ActivityObjectOrLink : IActivityObjectOrLink
    {
        public IActivityLink Link { get; set; }

        public IActivityObject Obj { get; set; }
    }
}
