using System;
using System.Linq;

namespace Kbvm.Photograph.Shared.Services
{
	public interface IAzureBlobHandler
	{
		Task<Stream> ReadBlobAsync(Guid fileGuid);
		Task<Guid> UploadBlobAsync(Stream stream);
	}
}
