using System.Text;

namespace NextGenSoftware.Utilities
{
    public static class DataHelper
    {
        public static string ConvertBinaryDataToString(byte[] data, bool ignoreEmptyByte = true)
        {
            // StringBuilder: per-byte string.Concat was O(n^2) and effectively hung on multi-MB payloads
            // (e.g. a Holochain full-state dump). Empty input used to throw on Substring(1).
            if (data == null || data.Length == 0)
                return "";

            StringBuilder sb = new StringBuilder(data.Length * 4);

            foreach (byte b in data)
            {
                if ((ignoreEmptyByte && b > 0) || !ignoreEmptyByte)
                {
                    if (sb.Length > 0)
                        sb.Append(',');

                    sb.Append(b);
                }
            }

            return sb.ToString();
        }

        public static string DecodeBinaryDataAsUTF8(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Encoding.UTF8.GetString(data, 0, data.Length));
            return sb.ToString().Replace("\0", "").Trim();
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