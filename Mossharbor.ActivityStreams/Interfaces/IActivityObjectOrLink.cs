using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    public interface IActivityObjectOrLink
    {
        IActivityLink Link { get;set; }

        IActivityObject Obj { get; set; }
    }
}
