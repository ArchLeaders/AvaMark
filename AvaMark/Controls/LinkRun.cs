using Avalonia;
using Avalonia.Controls.Documents;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;

namespace AvaMark.Controls;

public class LinkRun : Run
{
    public Uri? NavigateUri { get; set; }

    public LinkRun(string text, Uri? navigateUri) : base(text)
    {
        NavigateUri = navigateUri;
        Classes.Add("link");
        TextDecorations = Avalonia.Media.TextDecorations.Underline;
        SetState(false, false);
    }

    public void SetState(bool hovered, bool pressed)
    {
        ClearValue(ForegroundProperty);

        var key = pressed ? "SystemAccentColorLight1" : hovered ? "SystemAccentColorLight2" : null;

        if (key is not null && Application.Current is { } app &&
            app.TryGetResource(key, app.ActualThemeVariant, out var resource) &&
            resource is Color color) {
            Foreground = new SolidColorBrush(color);
            return;
        }

        this[!ForegroundProperty] = new DynamicResourceExtension("HyperlinkButtonForeground");
    }
}
