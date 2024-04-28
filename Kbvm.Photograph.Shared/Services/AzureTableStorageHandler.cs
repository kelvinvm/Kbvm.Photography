using Azure.Data.Tables;
using Kbvm.Photograph.Shared.Models;
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
	}
}
