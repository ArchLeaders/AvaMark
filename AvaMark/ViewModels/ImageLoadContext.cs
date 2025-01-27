using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaMark.ViewModels;

internal partial class ImageLoadContext : ObservableObject
{
    private readonly string? _url;

    [ObservableProperty]
    private Bitmap? _source;

    public ImageLoadContext(string? url)
    {
        _url = url;
        
        _ = Task.Run(FillImage);
    }
    
    private async ValueTask FillImage()
    {
        using HttpClient client = new();
        await using Stream src = await client.GetStreamAsync(_url);
        using MemoryStream ms = new();
        await src.CopyToAsync(ms);
        
        ms.Seek(0, SeekOrigin.Begin);
        Source = new Bitmap(ms);
    }
}