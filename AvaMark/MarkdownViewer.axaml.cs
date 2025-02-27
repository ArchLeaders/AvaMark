using Avalonia;
using Avalonia.Controls;
using AvaMark.Renderers;

namespace AvaMark;

public class MarkdownViewer : ContentControl
{
    static MarkdownViewer()
    {
        MarkdownProperty.Changed.AddClassHandler<MarkdownViewer>(HandleMarkdownChanged);
    }
    
    public static readonly StyledProperty<string> MarkdownProperty = AvaloniaProperty.Register<MarkdownViewer, string>(nameof(Markdown), string.Empty);
    public static readonly StyledProperty<IImageResolver?> ImageResolverProperty = AvaloniaProperty.Register<MarkdownViewer, IImageResolver?>(nameof(ImageResolver));
    public static readonly StyledProperty<object?> ImageResolverStateProperty = AvaloniaProperty.Register<MarkdownViewer, object?>(nameof(ImageResolverState));

    public string Markdown {
        get => GetValue(MarkdownProperty);
        set => SetValue(MarkdownProperty, value);
    }
    
    public IImageResolver? ImageResolver {
        get => GetValue(ImageResolverProperty);
        set => SetValue(ImageResolverProperty, value);
    }
    
    public object? ImageResolverState {
        get => GetValue(ImageResolverStateProperty);
        set => SetValue(ImageResolverStateProperty, value);
    }
    
    public static void HandleMarkdownChanged(MarkdownViewer viewer, AvaloniaPropertyChangedEventArgs args)
    {
        if (args.NewValue is not string markdown) {
            viewer.Content = null;
            return;
        }

        ImageResolverContext imageResolverContext = new(viewer.ImageResolver, viewer.ImageResolverState);
        AvaloniaMarkdownRenderer renderer = new(imageResolverContext);
        
        viewer.Content = Markdig.Markdown.Convert(markdown, renderer);
    }
}