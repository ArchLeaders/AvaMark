using Avalonia.Controls;
using Markdig.Syntax;

namespace AvaMark.Renderers;

internal class QuoteBlockRenderer : AvaloniaObjectRenderer<QuoteBlock>
{
    private const string CLASS = "quote";
    
    protected override void Write(AvaloniaMarkdownRenderer renderer, QuoteBlock obj)
    {
        StackPanel panel = new() {
            Spacing = 10
        };
        
        renderer.OpenPanel(panel);
        renderer.WriteChildren(obj);
        renderer.ClosePanel();
        
        renderer.Write(new Border {
            Classes = {
                CLASS
            },
            Child = panel
        });
    }
}