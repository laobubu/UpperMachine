using System;

namespace UpperMachine
{
    public class PackageEventArgs : EventArgs
    {
        //使用 byte[] 方式读取数据
        public int Length { get { return ByteData.Length; } }
        public byte[] ByteData { get { return (byte[])_Data; } }
        
        public object Data { get { return _Data; } }
        public DateTime CreatedAt { get { return _CreatedAt; } }

        private object _Data;
        private DateTime _CreatedAt;

        public PackageEventArgs(object data)
        {
            _Data = data;
            _CreatedAt = DateTime.Now;
        }

    }

    public interface IPackageReader
    {
        void Feed(byte data);
        bool Enabled { get; set; }
        event EventHandler<PackageEventArgs> PackReceived;
    }
}
