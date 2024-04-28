using System;
using System.Linq;

namespace Kbvm.Photograph.Shared.Models
{
	public class AccessKeys : IAccessKeys
	{
		public string Blob { get; set; } = string.Empty;
		public string Table { get; set; } = string.Empty;
	}
}
