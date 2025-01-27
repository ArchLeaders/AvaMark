using Avalonia.Controls.Documents;
using Markdig.Syntax;

namespace AvaMark.Renderers;

internal class ParagraphRenderer : AvaloniaObjectRenderer<ParagraphBlock>
{
    private const string CLASS = "paragraph";
    
    protected override void Write(AvaloniaMarkdownRenderer renderer, ParagraphBlock obj)
    {
        renderer.OpenBlock(CLASS);
        renderer.WriteLeafInline(obj);
        renderer.CloseBlock();
    }
}