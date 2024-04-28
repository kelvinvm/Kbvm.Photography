using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Images.ExifData.Dto
{
		public record ImageExifInfoDto(
			string FileType,
			Point CurrentSize,
			Point OriginalSize,
			string CameraMake,
			string CameraModel,
			string Exposure,
			int Iso,
			DateTime ShotDate,
			string ShutterSpeed,
			string Aperture,
			string ExposureBias);
}
