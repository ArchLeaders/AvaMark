using Avalonia;
using Avalonia.Controls;
using AvaMark.Renderers;

namespace AvaMark;

public class MarkdownViewer : ContentControl
{
    public static readonly StyledProperty<string> MarkdownProperty = AvaloniaProperty.Register<MarkdownViewer, string>(nameof(Markdown), string.Empty, coerce: CoerceMarkdown);

    public string Markdown {
        get => GetValue(MarkdownProperty);
        set => SetValue(MarkdownProperty, value);
    }
    
    public static string CoerceMarkdown(AvaloniaObject source, string markdown)
    {
        if (source is not MarkdownViewer viewer) {
            return markdown;
        }
        
        viewer.Content = Markdig.Markdown.Convert(markdown, AvaloniaMarkdownRenderer.Instance);
        
        return markdown;
    }
}