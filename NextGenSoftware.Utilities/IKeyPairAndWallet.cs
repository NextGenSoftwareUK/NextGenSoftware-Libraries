namespace NextGenSoftware.Utilities
{
    public interface IKeyPairAndWallet
    {
        string PrivateKey { get; set; }
        string PublicKey { get; set; }
        string WalletAddressLegacy { get; set; }
        string WalletAddressSegwitP2SH { get; set; }
    }
}