using System;

namespace VCIDepthFirstScheduler
{
    public interface IScheduler : IDisposable
    {
        void Enqueue(TaskChain item);
    }
}
