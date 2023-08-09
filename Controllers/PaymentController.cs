using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    // Replace with your actual Stripe API secret key
    private const string StripeSecretKey = "sk_test_51Nd9auSBwBH1cAophJBO4KveO5fRJgW9rkc19b2fDRCW7Nr7MFNUzxEdcDizqgzrb6RET8K2oSQOSrwuvhwO9OeE00bDrEkZnY";

    [HttpPost("charge")]
    public async Task<ActionResult<string>> Charge([FromBody] PaymentRequest paymentRequest)
    {
        StripeConfiguration.ApiKey = StripeSecretKey;

        var options = new PaymentIntentCreateOptions
        {
            Amount = paymentRequest.Amount,
            Currency = "usd",
            Description = paymentRequest.Description,
            PaymentMethodTypes = new List<string>
            {
                "card"
            }
        };

        var service = new PaymentIntentService();
        var paymentIntent = await service.CreateAsync(options);

        return paymentIntent.ClientSecret;
    }
}

public class PaymentRequest
{
    public int Amount { get; set; }
    public string Description { get; set; }
}
