using Avalonia.Controls.Documents;
using Markdig.Syntax.Inlines;

namespace AvaMark.Renderers.Inlines;

internal class LineBreakInlineRenderer : AvaloniaObjectRenderer<LineBreakInline>
{
    protected override void Write(AvaloniaMarkdownRenderer renderer, LineBreakInline obj)
    {
        renderer.WriteInline(new LineBreak());
    }
}