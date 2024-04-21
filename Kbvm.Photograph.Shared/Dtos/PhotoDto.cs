using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbvm.Photograph.Shared.Models
{
	public class PhotoDto
	{
		public PhotoDto(PhotoTableItem pti)
		{
			CollectionName = pti.PartitionKey;
			FileName = pti.RowKey;
			Caption = pti.Caption;
			Location = new LocationDto
			{
				File = pti.BlobImageName,
				Thumbnail = pti.BlobThumbnailName
			};
			CaptureDate = DateOnly.FromDateTime(pti.CaptureDate);
			PublishDate = DateOnly.FromDateTime(pti.PublishDate);
			PixelDimensions = new Point(pti.PixelWidth, pti.PixelHeight);
		}

		public PhotoDto() {	}

		public string CollectionName { get; set; } = "Default";
		public string FileName { get; set; } = string.Empty;
		public string Caption { get; set; } = string.Empty;
		public LocationDto Location { get; set; } = new LocationDto();
		public DateOnly CaptureDate { get; set; } = DateOnly.MinValue;
		public DateOnly PublishDate { get; set; } = DateOnly.MinValue;
		public Point PixelDimensions { get; set; } = Point.Empty;
	}
}
