using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace TextAIClassifierWeb;

[Route("[controller]")]
public class MessageController : TwilioController
{
    private ClassifierService _classifierService;

    public MessageController(IConfiguration configuration)
    {
        _classifierService = new ClassifierService(configuration);
    }

    [HttpPost]
    public async Task<IActionResult> Index(SmsRequest incomingMessage)
    {
        var response = new MessagingResponse();
        var predictionList = await _classifierService.ClassifyText(incomingMessage.Body);
        foreach(Classification prediction in predictionList)
        {
            response.Message($"Prediction: {prediction.prediction} with confidence {prediction.confidence}");
        }
        return TwiML(response);
    }

}

