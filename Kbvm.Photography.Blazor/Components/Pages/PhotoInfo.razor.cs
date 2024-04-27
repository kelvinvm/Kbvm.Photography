using Kbvm.Photograph.Shared.Models;

namespace Kbvm.Photography.Blazor.Components.Pages
{
	public partial class PhotoInfo
	{
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

		private void Submit()
		{

		}
	}
}
