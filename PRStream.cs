using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UpperMachine
{
    /// <summary>
    /// 流式处理
    /// </summary>
    class PRStream : IPackageReader
    {
        public event EventHandler<PackageEventArgs> PackReceived;
        public bool Enabled { get; set; }
        private Stream stream;

        public PRStream(Stream stream)
        {
            Enabled = true;
            this.stream = stream;
        }

        public void Feed(byte data)
        {
            if (!Enabled) return;

            stream.WriteByte(data);
        }
    }
}
