using Amazon.SQS.Model;
using System.Collections.Generic;

namespace MainAPI.Services
{
    public interface IQueueService
    {
        string SendMessage(string msgBody, Dictionary<string, MessageAttributeValue> msgAttrs, string queueUrls);
    }
}
