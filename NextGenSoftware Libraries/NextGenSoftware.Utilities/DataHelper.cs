
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace NextGenSoftware.Utilities
{
    public static class DataHelper
    {
        public static string ConvertBinaryDataToString(byte[] data, bool ignoreEmptyByte = true)
        {
            string result = "";

            foreach (byte b in data)
            {
                if (ignoreEmptyByte && b > 0 || !ignoreEmptyByte)
                    result = string.Concat(result, ", ", b.ToString());
            }

            result = result.Substring(1, result.Length - 1);
            return result;
        }

        public static string DecodeBinaryDataAsUTF8(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Encoding.UTF8.GetString(data, 0, data.Length));
            return sb.ToString();
        }


        // Convert an object to a byte array
        //public static byte[] ObjectToByteArray(object obj)
        //{
        //    BinaryFormatter bf = new BinaryFormatter();
        //    using (var ms = new MemoryStream())
        //    {
        //        bf.Serialize(ms, obj);
        //        return ms.ToArray();
        //    }
        //}
    }
}