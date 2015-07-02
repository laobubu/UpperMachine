using System;

namespace UpperMachine
{
    /// <summary>
    /// 用于读取带有某种开头的、固定长度的数据包
    /// </summary>
    public class PRFixLength : UpperMachine.IPackageReader
    {
        private byte[] PackHeader;
        private byte[] PackData;

        public event EventHandler<PackageEventArgs> PackReceived;
        public bool Enabled { get; set; }

        //当前状态，正数或0表示已接收到的Header的字节数，会递增；负数表示剩余要接受的字节数，也会递增
        private int currX;

        public PRFixLength(byte[] header, int bufferLength)
        {
            Enabled = true;
            currX = 0;
            PackHeader = header;
            PackData = new byte[bufferLength];
        }

        public void Feed(byte data)
        {
            if (!Enabled) return;

            if (currX >= 0)
            {
                if (data == PackHeader[currX])
                {
                    currX++;
                    if (currX == PackHeader.Length)
                    {
                        //开始读取
                        currX = -PackData.Length;
                    }
                }
                else
                {
                    currX = 0;
                }
            }
            else //正在读取数据
            {
                PackData[PackData.Length + currX] = data;
                currX++;
                if (currX == 0) //读取结束
                {
                    PackReceived.BeginInvoke(this, new PackageEventArgs(PackData), null, null);
                }
            }
        }
    }
}
