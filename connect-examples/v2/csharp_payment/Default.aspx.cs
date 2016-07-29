using System;

using Square.Connect.Api;
using Square.Connect.Model;

namespace SquareEcommerce
{
   
    public partial class Default : System.Web.UI.Page
    {
        private static TransactionApi _transactionApi;
        private static string _accessToken = "sq0atb-44gzjrtZTseuJK5BQqCfbg";
        private static string _locationId = "CBASEFU1QAw5oZ261gN6WWyCXfA";

        protected void Page_Load(object sender, EventArgs e)
        {
            _transactionApi = new TransactionApi();
        }

        [System.Web.Services.WebMethod]
        public static string GetCardNonce(string nonce)
        {
            return "Hello " + nonce;
        }

        [System.Web.Services.WebMethod]
        public static string Charge(string nonce)
        {
            string uuid = NewIdempotencyKey();
            Money amount = NewMoney(100, "USD");
            ChargeRequest body = new ChargeRequest(AmountMoney: amount, IdempotencyKey: uuid, CardNonce: nonce);
            var response = _transactionApi.Charge(_accessToken, _locationId, body);
            if (response.Errors == null)
            {
                return "Transaction complete\n" + response.ToJson();
            } else
            {
                return response.Errors.ToString();
            }
        }

        public static Money NewMoney(int amount, string currency)
        {
            return new Money(amount, Money.ToCurrencyEnum(currency));
        }

        public static string NewIdempotencyKey()
        {
            return Guid.NewGuid().ToString();
        }
    }

}