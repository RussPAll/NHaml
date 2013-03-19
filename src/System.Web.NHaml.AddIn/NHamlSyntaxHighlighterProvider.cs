using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace NHamlSyntaxHighlighter
{
    [Export(typeof(IClassifierProvider))]
    [ContentType(ContentType.NHaml)]
    internal class NHamlSyntaxHighlighterProvider : IClassifierProvider
    {
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry = null;

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            return buffer.Properties.GetOrCreateSingletonProperty(
                () => new NHamlSyntaxHighlighter(ClassificationRegistry));
        }
    }
}