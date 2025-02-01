using Avalonia.Controls.Documents;
using Markdig.Syntax.Inlines;

namespace AvaMark.Renderers.Inlines;

internal class LiteralInlineRenderer : AvaloniaObjectRenderer<LiteralInline>
{
    private const string CLASS = "p";
    
    protected override void Write(AvaloniaMarkdownRenderer renderer, LiteralInline obj)
    {
        renderer.PushStyle(CLASS);
        
        renderer.WriteInline(new Run {
            Text = obj.ToString()
        });
        
        renderer.DropStyle();
    }
}