using Images.ExifData;
using Kbvm.Photograph.Shared.Models;
using Kbvm.Photograph.Shared.Services;
using Kbvm.Photography.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBlazorBootstrap();

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services
	.AddScoped<IAzureBlobHandler, AzureBlobHandler>()
	.AddScoped<IAzureTableStorageHandler, AzureTableStorageHandler>()
	.AddScoped<IExifReader, ExifReader>()
	.AddSingleton<IAccessKeys>(new AccessKeys()
	{
		Blob = builder.Configuration["PhotoBlobSharedAccessKey"] ?? string.Empty,
		Table = builder.Configuration["PhotoTableSharedAccessKey"] ?? string.Empty
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
