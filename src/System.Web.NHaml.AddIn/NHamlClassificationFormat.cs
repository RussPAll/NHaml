using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace System.Web.NHaml.AddIn
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
                DisplayName = "NHaml Tag";
                ForegroundColor = Color.FromRgb(128, 0, 0);
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "nhaml.tagId")]
        [Name("nhaml.tagId")]
        [UserVisible(true)]
        [Order(Before = Priority.Default)]
        sealed class NHamlTagId : ClassificationFormatDefinition
        {
            public NHamlTagId()
            {
                DisplayName = "NHaml Tag Id";
                ForegroundColor = Color.FromRgb(0, 0, 255);
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "nhaml.tagClass")]
        [Name("nhaml.tagClass")]
        [UserVisible(true)]
        [Order(Before = Priority.Default)]
        sealed class NHamlTagClass : ClassificationFormatDefinition
        {
            public NHamlTagClass()
            {
                DisplayName = "NHaml Tag Id";
                ForegroundColor = Color.FromRgb(0, 0, 255);
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "nhaml.htmlAttributeCollection")]
        [Name("nhaml.htmlAttributeCollection")]
        [UserVisible(true)]
        [Order(Before = Priority.Default)]
        sealed class NHamlHtmlAttributeCollection : ClassificationFormatDefinition
        {
            public NHamlHtmlAttributeCollection()
            {
                DisplayName = "NHaml HTML Attribute Collection";
                ForegroundColor = Color.FromRgb(0, 0, 255);
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "nhaml.htmlAttribute")]
        [Name("nhaml.htmlAttribute")]
        [UserVisible(true)]
        [Order(Before = Priority.Default)]
        sealed class NHamlHtmlAttribute : ClassificationFormatDefinition
        {
            public NHamlHtmlAttribute()
            {
                DisplayName = "NHaml HTML Attribute";
                ForegroundColor = Color.FromRgb(255, 0, 0);
            }
        }

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "nhaml.textContainer")]
        [Name("nhaml.textContainer")]
        [UserVisible(true)]
        [Order(Before = Priority.Default)]
        sealed class NHamlTextContainer : ClassificationFormatDefinition
        {
            public NHamlTextContainer()
            {
                DisplayName = "NHaml Text Container";
                ForegroundColor = Color.FromRgb(0, 0, 255);
            }
        }


    
    }
}
