using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.Model.Payment
{
    public class Payment
    {
        public class CreateOrderRequest { public int Amount { get; set; } } // Amount in rupees
        public class PaymentVerifyRequest
        {
            public string razorpay_payment_id { get; set; }
            public string razorpay_order_id { get; set; }
            public string razorpay_signature { get; set; }
        }

    }
}
