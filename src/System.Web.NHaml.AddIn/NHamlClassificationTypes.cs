using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace NHamlSyntaxHighlighter
{
    class NHamlClassificationTypes
    {
        [Export]
        [Name("nhaml")]
        internal static ClassificationTypeDefinition NHamlClassificationDefinition = null;
    }
}
