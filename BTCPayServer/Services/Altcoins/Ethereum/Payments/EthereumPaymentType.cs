#if ALTCOINS
using System.Globalization;
using BTCPayServer.Payments;
using BTCPayServer.Services.Invoices;
using NBitcoin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BTCPayServer.Services.Altcoins.Ethereum.Payments
{
    public class EthereumPaymentType: PaymentType
    {
        public static EthereumPaymentType Instance { get; } = new EthereumPaymentType();
        public override string ToPrettyString() => "On-Chain";

        public override string GetId()=> "EthereumLike";


        public override CryptoPaymentData DeserializePaymentData(BTCPayNetworkBase network, string str)
        {
            return JsonConvert.DeserializeObject<EthereumLikePaymentData>(str);
        }

        public override string SerializePaymentData(BTCPayNetworkBase network, CryptoPaymentData paymentData)
        {
            return JsonConvert.SerializeObject(paymentData);
        }

        public override IPaymentMethodDetails DeserializePaymentMethodDetails(BTCPayNetworkBase network, string str)
        {
            return JsonConvert.DeserializeObject<EthereumLikeOnChainPaymentMethodDetails>(str);
        }

        public override string SerializePaymentMethodDetails(BTCPayNetworkBase network, IPaymentMethodDetails details)
        {
            return JsonConvert.SerializeObject(details);
        }

        public override ISupportedPaymentMethod DeserializeSupportedPaymentMethod(BTCPayNetworkBase network, JToken value)
        {
            return JsonConvert.DeserializeObject<EthereumSupportedPaymentMethod>(value.ToString());
        }

        public override string GetTransactionLink(BTCPayNetworkBase network, string txId)
        {
            return string.Format(CultureInfo.InvariantCulture, network.BlockExplorerLink, txId);
        }

        public override string GetPaymentLink(BTCPayNetworkBase network, IPaymentMethodDetails paymentMethodDetails, Money cryptoInfoDue,
            string serverUri)
        {
            return "";
        }

        public override string InvoiceViewPaymentPartialName { get; }= "Ethereum/ViewEthereumLikePaymentData";
        public override object GetGreenfieldData(ISupportedPaymentMethod supportedPaymentMethod, bool canModifyStore)
        {
            if (supportedPaymentMethod is EthereumSupportedPaymentMethod ethereumSupportedPaymentMethod)
            {
                return new
                {
                    ethereumSupportedPaymentMethod.XPub
                    //no clue what all those properties saved are and don't care.
                };
            }

            return null;
        }

        public override void PopulateCryptoInfo(PaymentMethod details, InvoiceCryptoInfo invoiceCryptoInfo, string serverUrl)
        {
        }
    }
}
#endif
