using System.Net;
using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Kbvm.Photography.Functions
{
    public class BlobHandlingFunctions
    {
        private readonly ILogger _logger;

        private const string PhotoContainerName = "photos";
        private readonly string _photoBlobSharedAccessKey;


		public BlobHandlingFunctions(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<BlobHandlingFunctions>();
            _photoBlobSharedAccessKey = Environment.GetEnvironmentVariable("PhotoBlobSharedAccessKey") ?? string.Empty;
		}

        [Function("UploadBlob")]
        public async Task<string> UploadBlobAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var fileName = Guid.NewGuid().ToString();
            var containerClient = new BlobContainerClient(new Uri(_photoBlobSharedAccessKey));
            BlobClient client = containerClient.GetBlobClient(fileName);

            await client.UploadAsync(req.Body);

            return fileName;
        }
    }
}
