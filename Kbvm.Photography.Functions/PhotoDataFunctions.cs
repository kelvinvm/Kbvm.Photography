using System.Net;
using Kbvm.Photograph.Shared.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Drawing;
using System.Text.Json;
using Azure.Data.Tables;

namespace Kbvm.Photography.Functions
{
    public class PhotoDataFunctions
    {
        private readonly ILogger _logger;
        private readonly string _photoTableShareAccessSignature;

		public PhotoDataFunctions(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PhotoDataFunctions>();
            _photoTableShareAccessSignature = Environment.GetEnvironmentVariable("PhotoTableSharedAccessKey") ?? string.Empty;
		}

        [Function("AddPhoto")]
        public async Task<bool> AddPhoto([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req, PhotoDto photoInfo)
        {
            var tableServiceClient = new TableServiceClient(new Uri(_photoTableShareAccessSignature));
            var tableClient = tableServiceClient.GetTableClient("Photos");
            var item = new PhotoTableItem(photoInfo);
            
            await tableClient.UpsertEntityAsync(item);
            return true;
        }
    }
}
