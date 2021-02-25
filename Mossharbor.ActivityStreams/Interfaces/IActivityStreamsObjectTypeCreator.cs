using System;
using System.Collections.Generic;
using System.Text;

namespace Mossharbor.ActivityStreams
{
    internal interface IActivityStreamsObjectTypeCreator
    {
        IActivityObject CreateType(string type);

        bool CanCreateType(string type);
    }
}
