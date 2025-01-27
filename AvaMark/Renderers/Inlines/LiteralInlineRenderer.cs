using Avalonia.Controls.Documents;
using Markdig.Syntax.Inlines;

namespace AvaMark.Renderers.Inlines;

internal class LiteralInlineRenderer : AvaloniaObjectRenderer<LiteralInline>
{
    protected override void Write(AvaloniaMarkdownRenderer renderer, LiteralInline obj)
    {
        renderer.WriteInline(new Run {
            Text = obj.ToString()
        });
    }
}