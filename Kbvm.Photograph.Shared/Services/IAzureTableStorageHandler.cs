using Kbvm.Photograph.Shared.Models;
using System;
using System.Linq;

namespace Kbvm.Photograph.Shared.Services
{
	public interface IAzureTableStorageHandler
	{
		Task<bool> AddPhotoAsync(PhotoDto photoInfo);
	}
}
