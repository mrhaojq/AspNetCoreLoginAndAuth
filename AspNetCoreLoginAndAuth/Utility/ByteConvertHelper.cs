using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreLoginAndAuth.Utility
{
    public class ByteConvertHelper
    {
        /// <summary>
        /// 将对象转换成Byte数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToBytes(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            byte[] serializedResult = System.Text.Encoding.UTF8.GetBytes(json);
            return serializedResult;
        }

        /// <summary>
        /// 将Byte数字转换成对象
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        public static object ByteToObject(byte[] buff)
        {
            string json = System.Text.Encoding.UTF8.GetString(buff);
            return JsonConvert.DeserializeObject<object>(json);
        }

        /// <summary>
        /// 将Byte数字转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buff"></param>
        /// <returns></returns>
        public static T ByteToObject<T>(byte[] buff)
        {
            string json = System.Text.Encoding.UTF8.GetString(buff);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
