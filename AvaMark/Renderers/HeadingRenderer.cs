using Markdig.Syntax;

namespace AvaMark.Renderers;

internal class HeadingRenderer : AvaloniaObjectRenderer<HeadingBlock>
{
    protected override void Write(AvaloniaMarkdownRenderer renderer, HeadingBlock obj)
    {
        string cls = $"h{obj.Level}";
        renderer.PushStyle(cls);
        renderer.OpenBlock();
        renderer.WriteLeafInline(obj);
        renderer.CloseBlock();
        renderer.DropStyle();
    }
}