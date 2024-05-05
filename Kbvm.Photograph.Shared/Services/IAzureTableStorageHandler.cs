using Kbvm.Photograph.Shared.Models;
using System;
using System.Linq;

namespace Kbvm.Photograph.Shared.Services
{
	public interface IAzureTableStorageHandler
	{
		Task<IEnumerable<PhotoDto>> LoadCollectionAsync(string collectionName);
		Task<bool> AddPhotoAsync(PhotoDto photoInfo);
	}
}
