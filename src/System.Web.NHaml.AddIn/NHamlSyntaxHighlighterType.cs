using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace NHamlSyntaxHighlighter
{
    internal static class NHamlSyntaxHighlighterClassificationDefinition
    {
        /// <summary>
        /// Defines the "NHamlSyntaxHighlighter" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("NHamlSyntaxHighlighter")]
        internal static ClassificationTypeDefinition NHamlSyntaxHighlighterType = null;
    }
}
