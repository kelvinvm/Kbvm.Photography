using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.FileType;
using MetadataExtractor.Formats.Jpeg;
using System.Drawing;
using System.Globalization;

namespace Images.ExifData
{
	public static class ExifDirectoriesExtensions
	{
		public static string FileType(this IReadOnlyList<MetadataExtractor.Directory> directories)
			=> directories.Get<FileTypeDirectory>(FileTypeDirectory.TagDetectedFileTypeName) ?? string.Empty;

		public static Point CurrentSize(this IReadOnlyList<MetadataExtractor.Directory> directories)
		{
			string width = directories.Get<JpegDirectory>(JpegDirectory.TagImageWidth);
			string height = directories.Get<JpegDirectory>(JpegDirectory.TagImageHeight);

			return new Point(width.ParseInt(), height.ParseInt());
		}

		public static Point OriginalSize(this IReadOnlyList<MetadataExtractor.Directory> directories)
		{
			string width = directories.Get<ExifIfd0Directory>(ExifDirectoryBase.TagImageWidth);
			string height = directories.Get<ExifIfd0Directory>(ExifDirectoryBase.TagImageHeight);

			return new Point(width.ParseInt(), height.ParseInt());
		}

		public static string CameraMake(this IReadOnlyList<MetadataExtractor.Directory> directories)
			=> directories.Get<ExifIfd0Directory>(ExifDirectoryBase.TagMake);

		public static string CameraModel(this IReadOnlyList<MetadataExtractor.Directory> directories)
			=> directories.Get<ExifIfd0Directory>(ExifDirectoryBase.TagModel);

		public static string Exposure(this IReadOnlyList<MetadataExtractor.Directory> directories)
			=> directories.Get<ExifSubIfdDirectory>(ExifDirectoryBase.TagExposureTime);

		public static string ShutterSpeed(this IReadOnlyList<MetadataExtractor.Directory> directories)
			=> directories.Get<ExifSubIfdDirectory>(ExifDirectoryBase.TagShutterSpeed);

		public static string Aperture(this IReadOnlyList<MetadataExtractor.Directory> directories)
			=> directories.Get<ExifSubIfdDirectory>(ExifDirectoryBase.TagAperture);

		public static string ExposureBias(this IReadOnlyList<MetadataExtractor.Directory> directories)
			=> directories.Get<ExifSubIfdDirectory>(ExifDirectoryBase.TagExposureBias);

		public static int Iso(this IReadOnlyList<MetadataExtractor.Directory> directories)
			=> directories.Get<ExifSubIfdDirectory>(ExifDirectoryBase.TagIsoEquivalent).ParseInt();

		public static DateTime ShotDate(this IReadOnlyList<MetadataExtractor.Directory> directories)
			=> directories.Get<ExifSubIfdDirectory>(ExifDirectoryBase.TagDateTimeOriginal).ParseDateTime();

		public static T? Get<T>(this IReadOnlyList<MetadataExtractor.Directory> directories) where T : MetadataExtractor.Directory
			=> directories.OfType<T>().FirstOrDefault();

		public static string Get<T>(this IReadOnlyList<MetadataExtractor.Directory> directories, int tagNumber) where T : MetadataExtractor.Directory
			=> directories.Get<T>()?.GetDescription(tagNumber) ?? string.Empty;

		private static int ParseInt(this string val)
		{
			var digitsOnly = string.Concat(val.Where(char.IsDigit));

			if (int.TryParse(digitsOnly, out var result))
				return result;
			return 0;
		}

		private const string ExifDateTimeFormat = "yyyy:MM:dd HH:mm:ss";
		private static DateTime ParseDateTime(this string val)
		{
			if (DateTime.TryParseExact(val, ExifDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
				return result;
			return DateTime.MinValue;
		}
	}
}
