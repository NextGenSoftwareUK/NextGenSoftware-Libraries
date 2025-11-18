using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using NBitcoin;

namespace NextGenSoftware.Utilities
{
    public static class KeyHelper
    {
        public class KeyValuePairAndWallet
        {
            public string PublicKey { get; set; }
            public string PrivateKey { get; set; }
            public string WalletAddressLegacy { get; set; }
            public string WalletAddressSegwitP2SH { get; set; }
        }

        public static KeyValuePairAndWallet GenerateKeyValuePairAndWalletAddress()
        {
            // 1. Generate a new random private key
            // A private key is a 256-bit number (32 bytes)
            Key privateKey = new Key();

            // The secret number of the private key can be accessed as bytes or a BigInteger
            byte[] privateKeyBytes = privateKey.ToBytes();
            string privateKeyHex = NBitcoin.DataEncoders.Encoders.Hex.EncodeData(privateKeyBytes);

            //Console.WriteLine($"Private Key (Hex): {privateKeyHex}");
            //Console.WriteLine($"Private Key (WIF): {privateKey.ToString(Network.Main)}"); // Wallet Import Format

            // 2. Derive the public key from the private key
            PubKey publicKey = privateKey.PubKey;
            byte[] publicKeyBytes = publicKey.ToBytes();
            string publicKeyHex = NBitcoin.DataEncoders.Encoders.Hex.EncodeData(publicKeyBytes);

            //Console.WriteLine($"\nPublic Key (Hex): {publicKeyHex}");

            // 3. Generate wallet addresses from the public key
            // Addresses are derived from the public key using various script types and network parameters

            // P2PKH (Legacy) address (starts with '1')
            BitcoinAddress p2pkhAddress = publicKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main);
            //Console.WriteLine($"\nWallet Address (P2PKH): {p2pkhAddress}");

            string hex = p2pkhAddress.ScriptPubKey.ToHex();
            string str = p2pkhAddress.ScriptPubKey.ToString();

            // P2SH-Segwit address (starts with '3')
            BitcoinAddress p2shSegwitAddress = publicKey.GetAddress(ScriptPubKeyType.SegwitP2SH, Network.Main);
            //Console.WriteLine($"Wallet Address (P2SH-SegWit): {p2shSegwitAddress}");

            // Bech32 (Native Segwit) address (starts with 'bc1')
            //BitcoinAddress bech32Address = publicKey.GetAddress(ScriptPubKeyType.WitnessV0Only, Network.Main);
            //Console.WriteLine($"Wallet Address (Bech32): {bech32Address}");

            return new KeyValuePairAndWallet()
            {
                PrivateKey = privateKeyHex,
                PublicKey = publicKeyHex,
                WalletAddressLegacy = p2pkhAddress.ScriptPubKey.ToHex(),
                WalletAddressSegwitP2SH = p2shSegwitAddress.ScriptPubKey.ToHex()
            };
        }

        //public GenerateKeyValuePair()
        //{
        //    try
        //    {
        //        // Generate secp256k1 key pair
        //        var keyPair = GenerateSecp256k1KeyPair();

        //        // Convert keys to hex strings
        //        string privateKeyHex = BitConverter.ToString(((ECPrivateKeyParameters)keyPair.Private).D.ToByteArrayUnsigned()).Replace("-", "");
        //        string publicKeyHex = BitConverter.ToString(((ECPublicKeyParameters)keyPair.Public).Q.GetEncoded(false)).Replace("-", "");

        //        // Store in KeyValuePair (PublicKey, PrivateKey)
        //        var blockchainKeys = new KeyValuePair<string, string>(publicKeyHex, privateKeyHex);

        //        // Output
        //        Console.WriteLine("Public Key:  " + blockchainKeys.Key);
        //        Console.WriteLine("Private Key: " + blockchainKeys.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error generating keys: " + ex.Message);
        //    }
        //}

        public static bool IsValidRsaPublicKey(string publicKeyContent)
        {
            try
            {
                using var rsa = new RSACryptoServiceProvider();
                //rsa.ImportFromPem(File.ReadAllText("public_key.pem").AsSpan());
                rsa.ImportFromPem(publicKeyContent);
                return true;
            }
            catch
            {
                Console.WriteLine("Invalid RSA Public Key");
                return false;
            }
        }

        public static bool IsValidRsaPrivateKey(string keyContent)
        {
            if (string.IsNullOrWhiteSpace(keyContent))
                return false;

            try
            {
                // Detect PEM format
                if (keyContent.Contains("-----BEGIN"))
                {
                    // Extract base64 content between PEM headers
                    var match = Regex.Match(keyContent,
                        "-----BEGIN (.*?)-----([^-]*)-----END (.*?)-----",
                        RegexOptions.Singleline);

                    if (!match.Success)
                        return false;

                    string base64 = match.Groups[2].Value
                        .Replace("\r", "")
                        .Replace("\n", "")
                        .Trim();

                    byte[] keyBytes = Convert.FromBase64String(base64);
                    return TryImportPrivateKey(keyBytes);
                }
                else
                {
                    // Assume DER binary format
                    byte[] keyBytes = Convert.FromBase64String(keyContent);
                    return TryImportPrivateKey(keyBytes);
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts to import the key into RSA to verify it's a valid private key.
        /// </summary>
        private static bool TryImportPrivateKey(byte[] keyBytes)
        {
            try
            {
                using (RSA rsa = RSA.Create())
                {
                    // Try PKCS#8 first
                    try
                    {
                        rsa.ImportPkcs8PrivateKey(keyBytes, out _);
                        return true;
                    }
                    catch
                    {
                        // Try PKCS#1
                        try
                        {
                            rsa.ImportRSAPrivateKey(keyBytes, out _);
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
