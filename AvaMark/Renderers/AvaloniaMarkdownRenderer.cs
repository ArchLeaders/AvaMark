using System.Runtime.CompilerServices;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Media;
using AvaMark.Renderers.Inlines;
using Markdig.Renderers;
using Markdig.Syntax;
using MdInline = Markdig.Syntax.Inlines.Inline;

namespace AvaMark.Renderers;

internal sealed class AvaloniaMarkdownRenderer : RendererBase
{
    public StackPanel Root { get; } = new() {
        Spacing = 10
    };
    
    public Stack<StackPanel> Panels { get; }

    public StackPanel Panel => Panels.Peek();

    public Stack<TextBlock> Blocks { get; } = [];
    
    public TextBlock? CurrentBlock => Blocks.TryPeek(out TextBlock? block) ? block : null;

    /// <summary>
    /// The current applied styles. (e.g. strong, em, ins)
    /// </summary>
    public Stack<string> InlineStyles { get; } = [];

    public AvaloniaMarkdownRenderer(ImageResolverContext? imageResolverContext = null)
    {
        Panels = new Stack<StackPanel>(
            [Root]
        );
        
        ObjectRenderers.Add(new CodeBlockRenderer());
        ObjectRenderers.Add(new HeadingRenderer());
        ObjectRenderers.Add(new ListRenderer());
        ObjectRenderers.Add(new ParagraphRenderer());
        ObjectRenderers.Add(new QuoteBlockRenderer());
        ObjectRenderers.Add(new ThematicBreakRenderer());

        // Inline
        ObjectRenderers.Add(new AutolinkInlineRenderer());
        ObjectRenderers.Add(new CodeInlineRenderer());
        ObjectRenderers.Add(new EmphasisInlineRenderer());
        ObjectRenderers.Add(new LiteralInlineRenderer());
        ObjectRenderers.Add(new LineBreakInlineRenderer());
        ObjectRenderers.Add(new LinkInlineRenderer(imageResolverContext));
    }

    public override object Render(MarkdownObject markdownObject)
    {
        Write(markdownObject);
        return Root;
    }

    public void OpenPanel(StackPanel panel)
    {
        Panels.Push(panel);
    }

    public void ClosePanel()
    {
        Panels.Pop();
    }

    public void OpenBlock(params IEnumerable<string> classes)
    {
        TextBlock block = new() {
            TextWrapping = TextWrapping.WrapWithOverflow
        };
        
        block.Classes.AddRange(classes);
        
        OpenBlock(block);
    }

    public void OpenBlock(TextBlock block)
    {
        Blocks.Push(block);
    }

    public void CloseBlock(bool insertIntoPanel = true)
    {
        if (CurrentBlock is null) {
            throw new InvalidOperationException("The current block cannot be closed because it is empty.");
        }

        if (insertIntoPanel) {
            Panel.Children.Add(CurrentBlock);
        }
        
        Blocks.Pop();
    }

    public void PushStyle(string cls)
    {
        InlineStyles.Push(cls);
    }

    public void DropStyle()
    {
        InlineStyles.Pop();
    }

    public void Write(Control element)
    {
        Panel.Children.Add(element);
    }

    public void WriteInline(Control element)
    {
        element.Classes.AddRange(InlineStyles);
        WriteInline(new InlineUIContainer(element));
    }

    public void WriteInline(Inline inline)
    {
        if (CurrentBlock is null) {
            throw new InvalidOperationException("Writing an inline element outside a block is not possible.");
        }
        
        inline.Classes.AddRange(InlineStyles);

        CurrentBlock.Inlines ??= [];
        CurrentBlock.Inlines.Add(inline);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteLeafInline(LeafBlock leafBlock)
    {
        MdInline? inline = leafBlock.Inline;

        while (inline != null) {
            Write(inline);
            inline = inline.NextSibling;
        }
    }
}