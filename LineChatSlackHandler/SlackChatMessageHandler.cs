using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using LineChatSlackHandler.Models;
using LineChatSlackHandler.Services;
using LineChatSlackHandler.Factory;

namespace LineChatSlackHandler
{
    public class SlackMessageHandler
    {
        private readonly ILineChatService _lineChatService;
        private readonly ILineMessageFactory _messageFactory;

        public SlackMessageHandler(
            ILineChatService lineChatService,
            ILineMessageFactory messageFactory)
        {
            _lineChatService = lineChatService;
            _messageFactory = messageFactory;
        }

        [FunctionName("SlackMessageHandler")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<MessageChannelsEvent>(requestBody);

            if (!string.IsNullOrEmpty(data.Challenge))
            {
                return new OkObjectResult(data.Challenge);
            }

            var message =_messageFactory.Create(data.Event);
            await _lineChatService.SendMessageAsync(message);
            var user = data.Event.User;

            return new OkObjectResult(data.Token);
        }
    }
}
