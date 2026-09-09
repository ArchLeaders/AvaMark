using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Input;

namespace AvaMark.Controls;

public class InteractiveTextBlock : TextBlock
{
    private LinkRun? _active;
    private bool _pressed;

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);
        Track(e);
    }

    protected override void OnPointerExited(PointerEventArgs e)
    {
        base.OnPointerExited(e);
        Sync(null, false);
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        Track(e);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
        var link = FindLinkAt(e.GetPosition(this));
        Sync(link, false);

        if (e.InitialPressMouseButton is MouseButton.Left && link?.NavigateUri is { } uri) {
            _ = TopLevel.GetTopLevel(this)?.Launcher?.LaunchUriAsync(uri);
            e.Handled = true;
        }
    }

    private void Track(PointerEventArgs e) =>
        Sync(FindLinkAt(e.GetPosition(this)), e.GetCurrentPoint(this).Properties.IsLeftButtonPressed);

    private void Sync(LinkRun? link, bool pressed)
    {
        Cursor = link is null ? Cursor.Default : new Cursor(StandardCursorType.Hand);
        ToolTip.SetTip(this, link?.NavigateUri?.ToString());

        if (ReferenceEquals(_active, link) && _pressed == pressed) {
            return;
        }

        _active?.SetState(false, false);
        (_active, _pressed) = (link, pressed);
        _active?.SetState(true, pressed);
    }

    private LinkRun? FindLinkAt(Point point)
    {
        if (Inlines is not { Count: > 0 }) {
            return null;
        }

        point -= new Point(Padding.Left, Padding.Top);
        var index = 0;
        
        foreach (var inline in Inlines) {
            var length = inline is Run run ? run.Text?.Length ?? 0 : 1;
            if (inline is LinkRun link && length > 0 &&
                TextLayout.HitTestTextRange(index, length).Any(r => r.Contains(point))) {
                return link;
            }

            index += length;
        }

        return null;
    }
}
