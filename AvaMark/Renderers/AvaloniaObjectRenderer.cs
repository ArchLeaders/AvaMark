using Markdig.Renderers;
using Markdig.Syntax;

namespace AvaMark.Renderers;

internal abstract class AvaloniaObjectRenderer<TObject> : MarkdownObjectRenderer<AvaloniaMarkdownRenderer, TObject> where TObject : MarkdownObject
{
}