using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Globalization;

namespace UpperMachine
{
    public class Utils
    {
        public static Regex regexHex = new Regex(@"[0-9a-zA-Z]{2}");

        /// <summary>
        /// 字符串转byte[]
        /// </summary>
        /// <param name="str">字符串，格式为十六进制，例如 FA 0C，中间的连接符无限制甚至可以省略</param>
        public static byte[] HexStr2Byte(string str)
        {
            MatchCollection mc = regexHex.Matches(str);
            byte[] ret = new byte[mc.Count];
            for (int i = mc.Count - 1; i >= 0; i--)
            {
                ret[i] = Byte.Parse(mc[i].Value, NumberStyles.HexNumber);
            }
            return ret;
        }
        
        /// <summary>
        /// byte[]转Hex字符串
        /// </summary>
        /// <param name="join">连接符，默认为一个空格</param>
        /// <returns>转换结果，例如 FA 0C </returns>
        public static string Byte2HexStr(byte[] data, string join = " ")
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                if (i != 0)
                    sb.Append(join);
                sb.AppendFormat("{0:X2}", data[i]);
            }
            return sb.ToString();
        }

        //https://msdn.microsoft.com/zh-cn/library/4ca6d5z7

        /// <summary>
        /// 由结构体转换为byte数组
        /// </summary>
        public static byte[] StructureToByte<T>(T structure)
        {
            int size = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[size];
            IntPtr bufferIntPtr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structure, bufferIntPtr, true);
                Marshal.Copy(bufferIntPtr, buffer, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(bufferIntPtr);
            }
            return buffer;
        }

        /// <summary>
        /// 由byte数组转换为结构体
        /// </summary>
        public static T ByteToStructure<T>(byte[] dataBuffer)
        {
            object structure = null;
            int size = Marshal.SizeOf(typeof(T));
            IntPtr allocIntPtr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(dataBuffer, 0, allocIntPtr, size);
                structure = Marshal.PtrToStructure(allocIntPtr, typeof(T));
            }
            finally
            {
                Marshal.FreeHGlobal(allocIntPtr);
            }
            return (T)structure;
        }

        /// <summary>
        /// 获取相对路径
        /// </summary>
        public static string MakeRelative(string filePath, string referencePath)
        {
            try
            {
                var referenceUri = new Uri(referencePath);
                var fileUri = new Uri(filePath);
                return referenceUri.MakeRelativeUri(fileUri).ToString();
            }
            catch (Exception)
            {
                return filePath;
            }
        }

    }
}
