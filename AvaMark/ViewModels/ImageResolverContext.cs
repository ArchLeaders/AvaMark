namespace AvaMark;

public sealed class ImageResolverContext(IImageResolver? imageResolver, object? state)
{
    public IImageResolver? ImageResolver { get; } = imageResolver;

    public object? State { get; } = state;
}