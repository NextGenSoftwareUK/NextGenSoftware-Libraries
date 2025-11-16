using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using NBitcoin;
using SshNet.Security.Cryptography;

namespace NextGenSoftware.Utilities
{
    public static class WalletAddressHelper
    {
        // Base58 alphabet used in Bitcoin addresses
        private const string Base58Chars = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

        public static string PrivateKeyToAddress(string privateKey)
        {
            var bitcoinPrivateKey = new BitcoinSecret(privateKey, Network.Main);

            string hex = bitcoinPrivateKey.GetAddress(ScriptPubKeyType.Legacy).ScriptPubKey.ToHex();
            string str = bitcoinPrivateKey.GetAddress(ScriptPubKeyType.Legacy).ScriptPubKey.ToString();
            string hash = bitcoinPrivateKey.GetAddress(ScriptPubKeyType.Legacy).ScriptPubKey.Hash.ToString();
            var address = bitcoinPrivateKey.GetAddress(ScriptPubKeyType.Legacy);

            return bitcoinPrivateKey.GetAddress(ScriptPubKeyType.Legacy).ScriptPubKey.ToHex();
        }

        public static string PublicKeyToAddress(string pubKeyHex)
        {
            try
            {
                // Validate hex string
                if (string.IsNullOrEmpty(pubKeyHex) || pubKeyHex.Length % 2 != 0)
                {
                    Console.WriteLine("Invalid public key format.");
                    return null;
                }

                // Create PubKey object from hex
                PubKey pubKey = new PubKey(pubKeyHex);

                // Convert to Bitcoin address (P2PKH)
                BitcoinAddress address = pubKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main);

                string hex = address.ScriptPubKey.ToHex();
                string str = address.ScriptPubKey.ToString();
                string hash = address.ScriptPubKey.Hash.ToString();

                return address.ScriptPubKey.ToHex();



                Console.WriteLine($"Bitcoin Address: {address}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;

            //if (string.IsNullOrWhiteSpace(publicKeyHex))
            //    throw new ArgumentException("Public key cannot be null or empty.");

            //byte[] publicKeyBytes = HexToBytes(publicKeyHex);

            //// Step 1: SHA-256
            //byte[] sha256 = System.Security.Cryptography.SHA256.Create().ComputeHash(publicKeyBytes);

            //// Step 2: RIPEMD-160
            //byte[] ripemd160 = RIPEMD160.Create().ComputeHash(sha256);

            //// Step 3: Add version byte (0x00 for Bitcoin mainnet)
            //byte[] versionedPayload = new byte[ripemd160.Length + 1];
            //versionedPayload[0] = 0x00;
            //Buffer.BlockCopy(ripemd160, 0, versionedPayload, 1, ripemd160.Length);

            //// Step 4: Double SHA-256 for checksum
            //byte[] checksum = System.Security.Cryptography.SHA256.Create().ComputeHash(System.Security.Cryptography.SHA256.Create().ComputeHash(versionedPayload))
            //    .Take(4).ToArray();

            //// Step 5: Append checksum
            //byte[] finalPayload = new byte[versionedPayload.Length + 4];
            //Buffer.BlockCopy(versionedPayload, 0, finalPayload, 0, versionedPayload.Length);
            //Buffer.BlockCopy(checksum, 0, finalPayload, versionedPayload.Length, 4);

            //// Step 6: Base58Check encode
            //return Base58Encode(finalPayload);
        }

        private static byte[] HexToBytes(string hex)
        {
            return Convert.FromHexString(hex);

            //if (hex.Length % 2 != 0)
            //    throw new ArgumentException("Invalid hex string length.");

            //byte[] bytes = new byte[hex.Length / 2];
            //for (int i = 0; i < bytes.Length; i++)
            //    bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);

            //return bytes;
        }

        private static string Base58Encode(byte[] data)
        {
            // Convert byte array to BigInteger
            var intData = data.Aggregate<byte, System.Numerics.BigInteger>(0, (current, t) => current * 256 + t);

            // Encode to Base58
            string result = "";
            while (intData > 0)
            {
                int remainder = (int)(intData % 58);
                intData /= 58;
                result = Base58Chars[remainder] + result;
            }

            // Add '1' for each leading 0 byte
            foreach (byte b in data)
            {
                if (b == 0x00)
                    result = '1' + result;
                else
                    break;
            }

            return result;
        }
    }
}