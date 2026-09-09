using Avalonia.Controls;
using Avalonia.Data;
using AvaMark.Controls;
using AvaMark.ViewModels;
using Markdig.Syntax.Inlines;
using System.Text;

namespace AvaMark.Renderers.Inlines;

internal class LinkInlineRenderer(ImageResolverContext? imageResolverContext = null) : AvaloniaObjectRenderer<LinkInline>
{
    private readonly ImageResolverContext? _imageResolverContext = imageResolverContext;

    protected override void Write(AvaloniaMarkdownRenderer renderer, LinkInline link)
    {
        if (link.IsImage) {
            WriteImage(renderer, link);
            return;
        }

        var text = GetText(link);
        Uri.TryCreate(link.Url ?? string.Empty, UriKind.RelativeOrAbsolute, out var uri);
        renderer.WriteInline(new LinkRun(string.IsNullOrWhiteSpace(text) ? link.Url ?? "" : text, uri));
    }

    private static string GetText(ContainerInline root)
    {
        var sb = new StringBuilder();
        for (Inline? current = root.FirstChild; current is not null; current = current.NextSibling) {
            switch (current) {
                case LiteralInline literal:
                    sb.Append(literal.Content);
                    break;
                case CodeInline code:
                    sb.Append(code.Content);
                    break;
                case LineBreakInline:
                    sb.Append(' ');
                    break;
                case ContainerInline nested:
                    sb.Append(GetText(nested));
                    break;
            }
        }

        return sb.ToString();
    }

    private void WriteImage(AvaloniaMarkdownRenderer renderer, LinkInline link)
    {
        Image img = new() {
            DataContext = new ImageLoadContext(link.Url, _imageResolverContext),
            Classes = { "image" },
            [!Image.SourceProperty] = new Binding(nameof(ImageLoadContext.Source)),
        };

        ToolTip.SetTip(img, link.Label);

        renderer.WriteInline(img);
    }
}
