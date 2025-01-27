using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Markdig.Syntax.Inlines;

namespace AvaMark.Renderers.Inlines;

internal class AutolinkInlineRenderer : AvaloniaObjectRenderer<AutolinkInline>
{
    private const string CLASS = "autolink";
    
    protected override void Write(AvaloniaMarkdownRenderer renderer, AutolinkInline obj)
    {
        renderer.WriteInline(new InlineUIContainer {
            Child = new HyperlinkButton {
                Classes = {
                    CLASS
                },
                NavigateUri = new Uri(obj.Url),
                Content = obj.Url
            }
        });
    }
}