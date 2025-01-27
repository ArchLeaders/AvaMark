using Avalonia.Controls;
using AvaMark.ViewModels;
using Markdig.Syntax;

namespace AvaMark.Renderers;

internal class ListRenderer : AvaloniaObjectRenderer<ListBlock>
{
    private const string CLASS_UL = "ul";
    private const string CLASS_OL = "ol";
    
    protected override void Write(AvaloniaMarkdownRenderer renderer, ListBlock list)
    {
        char bullet = list.BulletType; 
        List<ListItem> items = [];

        foreach (Block item in list) {
            if (item is not ListItemBlock li) {
                continue;
            }

            StackPanel listPanel = new() {
                Classes = {
                    list.IsOrdered ? CLASS_OL : CLASS_UL
                }
            };
            
            renderer.OpenPanel(listPanel);
            renderer.WriteChildren(li);
            renderer.ClosePanel();
            
            items.Add(new ListItem(listPanel, list.IsOrdered ? $"{bullet}." : null));
            bullet += (char)0x1;
        }
        
        renderer.Write(new ItemsControl {
            ItemsSource = items,
            Classes = {
                list.IsOrdered ? CLASS_OL : CLASS_UL
            }
        });
    }
}