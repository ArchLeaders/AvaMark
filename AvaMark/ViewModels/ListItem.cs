using Avalonia.Controls;

namespace AvaMark.ViewModels;

public sealed class ListItem(Panel content, string? bullet = null)
{
    public Panel Content { get; } = content;

    public string? Bullet { get; } = bullet;
}