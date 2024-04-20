using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbvm.Photograph.Shared.Models
{
	public class Photo
	{
		public string CollectionName { get; set; } = "Default";
		public string Name { get; set; } = string.Empty;
		public string FileName { get; set; } = string.Empty;
		public string Caption { get; set; } = string.Empty;
		public Location Location { get; set; } = new Location();
		public DateOnly CaptureDate { get; set; } = DateOnly.MinValue;
		public DateOnly PublishDate { get; set; } = DateOnly.MinValue;
		public Point PixelDimensions { get; set; } = Point.Empty;
	}
}
