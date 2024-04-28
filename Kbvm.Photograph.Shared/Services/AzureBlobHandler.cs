using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Kbvm.Photograph.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbvm.Photograph.Shared.Services
{
	public class AzureBlobHandler : IAzureBlobHandler
	{
		private const string PhotoContainerName = "photos";

		private readonly IAccessKeys _accessKeys;

		public AzureBlobHandler(IAccessKeys accessKeys)
		{
			_accessKeys = accessKeys ?? throw new ArgumentNullException(nameof(accessKeys));
		}

		public async Task<Guid> UploadBlobAsync(Stream stream)
		{
			var fileGuid = Guid.NewGuid();
			var containerClient = new BlobContainerClient(new Uri(_accessKeys.Blob));
			BlobClient client = containerClient.GetBlobClient(fileGuid.ToString());

			await client.UploadAsync(stream);

			return fileGuid;
		}

		public async Task<Stream> ReadBlobAsync(Guid fileGuid)
		{
			var containerClient = new BlobContainerClient(new Uri(_accessKeys.Blob));
			var client = containerClient.GetBlobClient(fileGuid.ToString());
			return await client.OpenReadAsync();
		}
	}
}
