using System;
using MetadataExtractor;
using System.Diagnostics;
using Images.ExifData.Dto;

namespace Images.ExifData
{
	public class ExifReader : IExifReader
	{
		public ImageExifInfoDto ReadExifData(Stream stream)
		{
			IReadOnlyList<MetadataExtractor.Directory> metaDataReader = ImageMetadataReader.ReadMetadata(stream);
			return GetExifInfo("", metaDataReader);
		}

		private ImageExifInfoDto GetExifInfo(string fileName, IReadOnlyList<MetadataExtractor.Directory> metaDataReader)
		{
			return new ImageExifInfoDto
			(
				FileType: metaDataReader.FileType(),
				CurrentSize: metaDataReader.CurrentSize(),
				OriginalSize: metaDataReader.OriginalSize(),
				CameraMake: metaDataReader.CameraMake(),
				CameraModel: metaDataReader.CameraModel(),
				Exposure: metaDataReader.Exposure(),
				Iso: metaDataReader.Iso(),
				ShotDate: metaDataReader.ShotDate(),
				ShutterSpeed: metaDataReader.ShutterSpeed(),
				Aperture: metaDataReader.Aperture(),
				ExposureBias: metaDataReader.ExposureBias()
			);
		}
	}
}
