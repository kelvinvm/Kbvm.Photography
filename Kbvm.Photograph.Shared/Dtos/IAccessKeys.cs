using System;
using System.Linq;

namespace Kbvm.Photograph.Shared.Models
{
	public interface IAccessKeys
	{
		string Blob { get; set; }
		string Table { get; set; }
	}
}
