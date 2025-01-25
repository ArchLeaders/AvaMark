using System;
using System.IO;
using Avalonia;
using Markdig;
using Markdig.Renderers;

namespace AvaMark.Demo;

internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        // using MemoryStream ms = new();
        // using TextWriter writer = new StreamWriter(ms);
        // HtmlRenderer renderer = new(writer);
        // Markdown.Convert("**Test**", renderer);
        
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}