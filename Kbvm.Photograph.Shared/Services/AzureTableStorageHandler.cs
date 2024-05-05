using Azure.Data.Tables;
using Kbvm.Photograph.Shared.Models;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;

namespace Kbvm.Photograph.Shared.Services
{
	public class AzureTableStorageHandler : IAzureTableStorageHandler
	{
		private readonly IAccessKeys _accessKeys;

		public AzureTableStorageHandler(IAccessKeys accessKeys)
		{
			_accessKeys = accessKeys ?? throw new ArgumentNullException(nameof(accessKeys));
		}

		public async Task<bool> AddPhotoAsync(PhotoDto photoInfo)
		{
			var tableServiceClient = new TableServiceClient(new Uri(_accessKeys.Table));
			var tableClient = tableServiceClient.GetTableClient("Photos");
			var item = new PhotoTableItem(photoInfo);

			await tableClient.UpsertEntityAsync(item);
			return true;
		}

		public async Task<IEnumerable<PhotoDto>> LoadCollectionAsync(string collectionName)
		{
			var tableServiceClient = new TableServiceClient(new Uri(_accessKeys.Table));
			var tableClient = tableServiceClient.GetTableClient("Photos");
			var items = tableClient.QueryAsync<PhotoTableItem>(p => p.PartitionKey == collectionName);

			var result = new List<PhotoDto>();
			await foreach (var item in items)
				result.Add(new PhotoDto(item));

			return result;
		}
	}
}
