using System;

namespace UpperMachine
{
    /// <summary>
    /// 用于逐个字节读取的 Reader，不推荐使用，你看看就好
    /// </summary>
    public class PREveryChar : IPackageReader
    {
        public event EventHandler<PackageEventArgs> PackReceived;
        public bool Enabled { get; set; }

        //构建函数，默认 enable
        public PREveryChar()
        {
            Enabled = true;
        }

        //当串口收到数据后，处理一个字节数据
        public void Feed(byte data)
        {
            //如果无效则不运行
            if (!Enabled) return;

            //Invoke PackReceived 事件，同时提供收到的数据（这里以收到的这个 data 为例子）
            PackReceived.Invoke(this, new PackageEventArgs(data));
        }
    }
}
