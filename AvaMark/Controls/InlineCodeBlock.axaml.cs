using Avalonia;
using Avalonia.Controls.Primitives;

namespace AvaMark.Controls;

public class InlineCodeBlock : TemplatedControl
{
    public static readonly StyledProperty<string> TextProperty = AvaloniaProperty.Register<InlineCodeBlock, string>(nameof(Text));

    public string Text {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}