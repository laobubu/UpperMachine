using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UpperMachine
{
    /// <summary>
    /// 流式处理
    /// </summary>
    class PRQueueByte : IPackageReader
    {
        public event EventHandler<PackageEventArgs> PackReceived;
        public bool Enabled { get; set; }
        private Queue<byte> queue;

        public PRQueueByte(Queue<byte> queue)
        {
            Enabled = true;
            this.queue = queue;
        }

        public void Feed(byte data)
        {
            if (!Enabled) return;

            queue.Enqueue(data);
        }
    }
}
