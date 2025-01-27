using Avalonia.Controls;
using Avalonia.Data;
using AvaMark.ViewModels;
using Markdig.Syntax.Inlines;

namespace AvaMark.Renderers.Inlines;

internal class LinkInlineRenderer : AvaloniaObjectRenderer<LinkInline>
{
    private const string CLASS_LINK = "link";
    private const string CLASS_IMG = "image";

    protected override void Write(AvaloniaMarkdownRenderer renderer, LinkInline link)
    {
        if (link.IsImage) {
            WriteImage(renderer, link);
            return;
        }

        TextBlock content = new() {
            Classes = {
                CLASS_LINK
            }
        };
        
        renderer.OpenBlock(content);
        renderer.WriteChildren(link);
        renderer.CloseBlock(insertIntoPanel: false);
        
        renderer.WriteInline(new HyperlinkButton {
            Classes = {
                CLASS_LINK
            },
            NavigateUri = Uri.TryCreate(link.Url ?? string.Empty, UriKind.RelativeOrAbsolute, out Uri? uri)
                ? uri
                : null,
            Content = content
        });
    }

    private static void WriteImage(AvaloniaMarkdownRenderer renderer, LinkInline link)
    {
        Image img = new() {
            DataContext = new ImageLoadContext(link.Url),
            Classes = {
                CLASS_IMG
            },
            [!Image.SourceProperty] = new Binding(nameof(ImageLoadContext.Source)),
        };

        ToolTip.SetTip(img, link.Label);

        renderer.WriteInline(img);
    }
}