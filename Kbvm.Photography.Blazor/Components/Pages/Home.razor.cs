using Kbvm.Photograph.Shared.Models;
using Kbvm.Photograph.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace Kbvm.Photography.Blazor.Components.Pages
{
	public partial class Home
	{
		private IEnumerable<PhotoDto> _photos = Enumerable.Empty<PhotoDto>();	

		[Inject]
		private IAzureTableStorageHandler _tableHandler { get; set; } = default!;

		protected override async Task OnInitializedAsync()
		{
			if (_photos.Any())
				return;

			_photos = await _tableHandler.LoadCollectionAsync("All Time Favorites");
			await base.OnInitializedAsync();
		}

	}
}


// https://savspphotoswus3dev.blob.core.windows.net/photos/3ae1edd3-a710-41fc-a515-44ef428a9534