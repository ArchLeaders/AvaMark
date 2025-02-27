using AvaMark.ViewModels;

namespace AvaMark;

public interface IImageResolver
{
    ValueTask FetchImage(ImageLoadContext context, object? state, string? url);
}