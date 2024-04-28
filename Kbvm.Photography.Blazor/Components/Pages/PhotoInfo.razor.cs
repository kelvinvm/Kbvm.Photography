using Images.ExifData;
using Kbvm.Photograph.Shared.Models;
using Kbvm.Photograph.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Kbvm.Photography.Blazor.Components.Pages
{
	public partial class PhotoInfo
	{
		[Inject]
		private IAzureBlobHandler _blobHandler { get; set; } = default!;

		[Inject]
		private IExifReader _exifReader { get; set; } = default!;

		[Inject]
		private IAzureTableStorageHandler _tableHandler { get; set; } = default!;	


		private List<PhotoDto> _photos = [];
		private PhotoDto _newPhoto = new();

		private List<string> _collections =
		[
			"All Time Favorites",
			"Astoria, OR",
			"Bainbridge Island, 2022",
			"Cherry Blossoms",
			"Darcell XV, July, 2022",
			"Europe, 2018",
			"Keukenhof, Netherlands, 2018",
			"Sachsenhausen",
			"San Francisco, 2023",
			"Seattle, 2022",
		];

		private async Task LoadPhotoFile(InputFileChangeEventArgs e)
		{
			_newPhoto.Location.File = await _blobHandler.UploadBlobAsync(e.File.OpenReadStream(1024 * 4000));

			// I want to read this stream twice, once to save to blob storage, and once for EXIF data.
			// So, I'm saving it to Blob storage, and then reading it back for the EXIF data
			var blobStream = await _blobHandler.ReadBlobAsync(_newPhoto.Location.File);
			var exifInfo = _exifReader.ReadExifData(blobStream);

			_newPhoto.FileName = e.File.Name;
			_newPhoto.CaptureDate = DateOnly.FromDateTime(exifInfo.ShotDate);
			_newPhoto.PixelDimensions = exifInfo.CurrentSize;
			_newPhoto.PublishDate = DateOnly.FromDateTime(exifInfo.ShotDate);
		}

		private async Task LoadThumbnailFile(InputFileChangeEventArgs e)
			=> _newPhoto.Location.Thumbnail = await _blobHandler.UploadBlobAsync(e.File.OpenReadStream());

		private void ResetForm()
			=> _newPhoto = new();

		private async Task Submit()
		{
			await _tableHandler.AddPhotoAsync(_newPhoto);
			ResetForm();
		}

	}
}
