using Avalonia;
using Avalonia.Controls;
using AvaMark.Renderers;

namespace AvaMark;

public class MarkdownViewer : ContentControl
{
    public static readonly StyledProperty<string> MarkdownProperty = AvaloniaProperty.Register<MarkdownViewer, string>(nameof(Markdown), string.Empty, coerce: CoerceMarkdown);
    public static readonly StyledProperty<IImageResolver?> ImageResolverProperty = AvaloniaProperty.Register<MarkdownViewer, IImageResolver?>(nameof(ImageResolver));
    public static readonly StyledProperty<object?> ImageResolverStateProperty = AvaloniaProperty.Register<MarkdownViewer, object?>(nameof(ImageResolverState));

    public string Markdown {
        get => GetValue(MarkdownProperty);
        set => SetValue(MarkdownProperty, value);
    }
    
    public IImageResolver? ImageResolver {
        get => GetValue(ImageResolverProperty);
        set {
            SetValue(ImageResolverProperty, value);
            CoerceMarkdown(this, Markdown);
        }
    }
    
    public object? ImageResolverState {
        get => GetValue(ImageResolverStateProperty);
        set => SetValue(ImageResolverStateProperty, value);
    }
    
    public static string CoerceMarkdown(AvaloniaObject source, string markdown)
    {
        if (source is not MarkdownViewer viewer) {
            return markdown;
        }
        
        IImageResolver? imageResolver = source.GetValue(ImageResolverProperty);
        object? imageResolverState = source.GetValue(ImageResolverStateProperty);

        ImageResolverContext imageResolverContext = new(imageResolver, imageResolverState);
        AvaloniaMarkdownRenderer renderer = new(imageResolverContext);
        
        viewer.Content = Markdig.Markdown.Convert(markdown, renderer);
        
        return markdown;
    }
}