using Azure;
using Azure.Data.Tables;
using Kbvm.Photograph.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbvm.Photography.Functions.Models
{
	internal class PhotoTableItem : ITableEntity
	{
		public PhotoTableItem(Photo photo)
		{
			PartitionKey = photo.CollectionName;
			RowKey = photo.FileName;
			Caption = photo.Caption;
			BlobImageName = photo.Location.File;
			BlobThumbnailName = photo.Location.Thumbnail;
			CaptureDate = DateTime.SpecifyKind(photo.CaptureDate.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
			PublishDate = DateTime.SpecifyKind(photo.PublishDate.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
			PixelWidth = photo.PixelDimensions.X;
			PixelHeight = photo.PixelDimensions.Y;
		}

		public PhotoTableItem()
		{
			PartitionKey = "default";
			RowKey = Guid.NewGuid().ToString();
			Caption = string.Empty;
		}

		public string PartitionKey { get; set; }
		public string RowKey { get; set; }
		public DateTimeOffset? Timestamp { get; set; }
		public ETag ETag { get; set; }

		public string Caption { get; set; }
		public Guid BlobImageName { get; set; }
		public Guid BlobThumbnailName { get; set; }
		public DateTime CaptureDate { get; set; }
		public DateTime PublishDate { get; set; }
		public int PixelWidth { get; set; }
		public int PixelHeight { get; set; }

		public bool IsCopyrightRegistered { get; set; }
	}
}
