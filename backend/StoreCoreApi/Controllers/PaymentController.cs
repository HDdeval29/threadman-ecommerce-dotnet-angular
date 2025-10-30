using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System;
using static StoreCoreApi.DAL.Model.Payment.Payment;
using Razorpay.Api;

namespace StoreCoreApi.Controllers
{
    [Route("Payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly string keyId = "rzp_test_RPjT5fDesHKouX";
        private readonly string keySecret = "cOKJFikNOonAwnt5tEfqZPWv";

        [HttpPost("create-order")]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest req)
        {
            var client = new RazorpayClient(keyId, keySecret);
            var options = new Dictionary<string, object>
        {
            { "amount", req.Amount * 100 }, // rupees -> paise
            { "currency", "INR" },
            { "receipt", "rcpt_" + Guid.NewGuid().ToString("N") },
            { "payment_capture", 1 }
        };

            var order = client.Order.Create(options);
            return Ok(new
            {
                orderId = order["id"].ToString(),
                amount = int.Parse(order["amount"].ToString()), // paise
                currency = order["currency"].ToString()
            });
        }

        [HttpPost("verify")]
        public IActionResult Verify([FromBody] PaymentVerifyRequest req)
        {
            // compute HMAC SHA256 of order_id|payment_id using keySecret
            var payload = req.razorpay_order_id + "|" + req.razorpay_payment_id;
            var secretBytes = Encoding.UTF8.GetBytes(keySecret);
            using var hmac = new HMACSHA256(secretBytes); 
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            var generatedSignature = BitConverter.ToString(hash).Replace("-", "").ToLower();

            if (generatedSignature == req.razorpay_signature)
                return Ok(new { status = "verified" });
            else
                return BadRequest(new { status = "invalid signature" });
        }
    }
}
