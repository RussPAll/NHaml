using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace NHamlSyntaxHighlighter
{
    class NHamlClassificationFormat
    {
        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "nhaml.code")]
        [Name("nhaml.code")]
        [UserVisible(true)]
        [Order(Before = Priority.Default)]
        sealed class NHamlCode : ClassificationFormatDefinition
        {
            public NHamlCode()
            {
                DisplayName = "Code";
                ForegroundColor = Colors.BlueViolet;
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "nhaml.tag")]
        [Name("nhaml.tag")]
        [UserVisible(true)]
        [Order(Before = Priority.Default)]
        sealed class NHamlTag : ClassificationFormatDefinition
        {
            public NHamlTag()
            {
                DisplayName = "Tag";
                ForegroundColor = Colors.Blue;
            }
        }
    }
}
