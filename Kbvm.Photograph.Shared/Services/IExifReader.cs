using Images.ExifData.Dto;

namespace Images.ExifData
{
	public interface IExifReader
	{
		ImageExifInfoDto ReadExifData(Stream stream);
	}
}
