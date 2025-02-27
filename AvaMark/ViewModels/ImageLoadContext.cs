using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaMark.ViewModels;

public partial class ImageLoadContext : ObservableObject
{
    private readonly string? _url;

    [ObservableProperty]
    private Bitmap? _source;

    public ImageLoadContext(string? url, ImageResolverContext? imageResolverContext = null)
    {
        _url = url;

        if (imageResolverContext?.ImageResolver is not null) {
            _ = Task.Run(
                async () => await imageResolverContext.ImageResolver.FetchImage(this, imageResolverContext.State, url)
            );
            return;
        }

        _ = Task.Run(FillImage);
    }

    private async ValueTask FillImage()
    {
        try {
            using HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(_url);

            if (!response.IsSuccessStatusCode) {
                return;
            }

            using MemoryStream ms = new();
            await using Stream src = await response.Content.ReadAsStreamAsync();
            await src.CopyToAsync(ms);

            ms.Seek(0, SeekOrigin.Begin);
            Source = new Bitmap(ms);
        }
        catch {
            // ignored
        }
    }
}