using Avalonia.Controls.Documents;
using AvaMark.Controls;
using Markdig.Syntax.Inlines;

namespace AvaMark.Renderers.Inlines;

internal class CodeInlineRenderer : AvaloniaObjectRenderer<CodeInline>
{
    private const string CLASS = "code";
    
    protected override void Write(AvaloniaMarkdownRenderer renderer, CodeInline obj)
    {
        renderer.WriteInline(new InlineUIContainer {
            Child = new InlineCodeBlock {
                Classes = {
                    CLASS
                },
                Text = obj.ContentSpan.ToString()
            }
        });
    }
}