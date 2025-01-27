using System.Text;
using Avalonia;
using Avalonia.Styling;
using AvaloniaEdit;
using AvaloniaEdit.TextMate;
using Markdig.Helpers;
using Markdig.Syntax;
using TextMateSharp.Grammars;

namespace AvaMark.Renderers;

internal class CodeBlockRenderer : AvaloniaObjectRenderer<CodeBlock>
{
    private const string CLASS = "code";

    protected override void Write(AvaloniaMarkdownRenderer renderer, CodeBlock obj)
    {
        string? info = null;
        if (obj is FencedCodeBlock fencedCodeBlock) {
            info = fencedCodeBlock.Info;
        }
        
        StringBuilder sb = new();
        foreach (StringLine line in obj.Lines) {
            sb.Append(line.ToString());
            sb.Append(line.NewLine switch {
                NewLine.None => null,
                _ => '\n'
            });
        }

        while (sb[^1] is '\n') {
            sb.Remove(sb.Length - 1, 1);
        }

        TextEditor editor = new() {
            WordWrap = true,
            Text = sb.ToString(),
            IsReadOnly = true,
            Classes = {
                CLASS
            }
        };

        if (string.IsNullOrWhiteSpace(info)) {
            goto UpdatePanel;
        }

        ThemeName theme = Application.Current?.ActualThemeVariant == ThemeVariant.Light
            ? ThemeName.LightPlus
            : ThemeName.DarkPlus;

        RegistryOptions options = new(theme);
        TextMate.Installation textMateInstallation = editor.InstallTextMate(options);
        textMateInstallation.SetGrammar(
            options.GetScopeByLanguageId(options.GetLanguageByExtension($".{info}").Id)
        );
        
    UpdatePanel:
        renderer.Write(editor);
    }
}