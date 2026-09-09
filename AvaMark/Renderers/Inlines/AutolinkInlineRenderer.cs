using AvaMark.Controls;
using Markdig.Syntax.Inlines;

namespace AvaMark.Renderers.Inlines;

internal class AutolinkInlineRenderer : AvaloniaObjectRenderer<AutolinkInline>
{
    protected override void Write(AvaloniaMarkdownRenderer renderer, AutolinkInline obj)
    {
        renderer.WriteInline(new LinkRun(obj.Url, new Uri(obj.Url)));
    }
}
