using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace NHamlSyntaxHighlighter
{
    class NHamlClassificationFormat
    {
        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "nhaml")]
        [Name("nhaml")]
        [UserVisible(true)]
        [Order(Before = Priority.Default)]
        sealed class NHamlText : ClassificationFormatDefinition
        {
            public NHamlText()
            {
                DisplayName = "NHaml";
                ForegroundColor = Colors.BlueViolet;
            }
        }
    }
}
