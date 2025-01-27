using Avalonia.Controls;
using Markdig.Syntax;

namespace AvaMark.Renderers;

internal class ThematicBreakRenderer : AvaloniaObjectRenderer<ThematicBreakBlock>
{
    private const string CLASS = "hr";
    
    protected override void Write(AvaloniaMarkdownRenderer renderer, ThematicBreakBlock obj)
    {
        renderer.Write(new Border {
            Classes = {
                CLASS
            }
        });
    }
}