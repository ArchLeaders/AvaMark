using Markdig.Syntax.Inlines;

namespace AvaMark.Renderers.Inlines;

internal class EmphasisInlineRenderer : AvaloniaObjectRenderer<EmphasisInline>
{
    private const string CLASS_EM = "em";
    private const string CLASS_STRONG = "strong";

    protected override void Write(AvaloniaMarkdownRenderer renderer, EmphasisInline obj)
    {
        renderer.PushStyle(obj.DelimiterCount switch {
            2 => CLASS_STRONG,
            _ => CLASS_EM
        });

        renderer.WriteChildren(obj);
        renderer.DropStyle();
    }
}