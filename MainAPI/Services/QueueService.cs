using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Services
{
    public class QueueService : IQueueService
    {
        public IAmazonSQS sqs;

        public QueueService()
        {
            this.sqs = new AmazonSQSClient(RegionEndpoint.APSouth1);
        }

        public string SendMessage(string msgBody, Dictionary<string, MessageAttributeValue> msgAttrs, string queueUrl)
        {
            var sqsMessageRequest = new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageAttributes = msgAttrs,
                MessageBody = msgBody
            };

            SendMessageResponse res = sqs.SendMessageAsync(sqsMessageRequest).Result;
            return res.MessageId;
        }
    }
}
