using System;
using System.Collections.Generic;
using System.Text;

namespace RaftIt.Workers
{
    public interface ISingletonWorker
    {
        void Run(string id);
    }
}
