using System.IO;
using Avalonia.Controls;
using Markdig;

namespace AvaMark.Demo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        string md = File.ReadAllText("Assets\\Sample.md");
        
        MdView.Markdown = md;
        HtmlView.Text = Markdown.ToHtml(md);
    }
    
}