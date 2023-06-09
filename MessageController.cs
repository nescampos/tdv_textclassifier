using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace TextAIClassifierWeb;

[Route("[controller]")]
public class MessageController : TwilioController
{
    private readonly ClassifierService classifierService;

    public MessageController(ClassifierService classifierService)
    {
        this.classifierService = classifierService;
    }

    [HttpPost]
    public async Task<IActionResult> Index(SmsRequest incomingMessage)
    {
        var response = new MessagingResponse();
        var predictionList = await classifierService.ClassifyText(incomingMessage.Body);
        foreach(var prediction in predictionList)
        {
            response.Message($"Prediction: {prediction.Prediction} with confidence {prediction.Confidence:P2}");
        }
        return TwiML(response);
    }

}

