using Avalonia.Controls;
using Markdig.Renderers;
using Markdig.Syntax;

namespace AvaMark.Renderers;

internal class AvaloniaMarkdownRenderer : RendererBase
{
    public static readonly AvaloniaMarkdownRenderer Instance = new();

    public StackPanel Panel { get; } = new();

    public AvaloniaMarkdownRenderer()
    {
        
    }

    public override object Render(MarkdownObject markdownObject)
    {
        Write(markdownObject);
        return Panel;
    }
}