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

        [Export]
        [Name("nhaml.code")]
        [BaseDefinition("nhaml")]
        internal static ClassificationTypeDefinition NHamlCodeDefinition = null;

        [Export]
        [Name("nhaml.tag")]
        [BaseDefinition("nhaml")]
        internal static ClassificationTypeDefinition NHamlTagDefinition = null;

        [Export]
        [Name("nhaml.tagId")]
        [BaseDefinition("nhaml")]
        internal static ClassificationTypeDefinition NHamlIdDefinition = null;

        [Export]
        [Name("nhaml.tagClass")]
        [BaseDefinition("nhaml")]
        internal static ClassificationTypeDefinition NHamlClassDefinition = null;

        [Export]
        [Name("nhaml.docType")]
        [BaseDefinition("nhaml")]
        internal static ClassificationTypeDefinition NHamlDocTypeDefinition = null;

        [Export]
        [Name("nhaml.htmlComment")]
        [BaseDefinition("nhaml")]
        internal static ClassificationTypeDefinition NHamlHtmlCommentDefinition = null;

        [Export]
        [Name("nhaml.hamlComment")]
        [BaseDefinition("nhaml")]
        internal static ClassificationTypeDefinition NHamlHamlCommentDefinition = null;

        [Export]
        [Name("nhaml.eval")]
        [BaseDefinition("nhaml")]
        internal static ClassificationTypeDefinition NHamlEvalDefinition = null;

        [Export]
        [Name("nhaml.partial")]
        [BaseDefinition("nhaml")]
        internal static ClassificationTypeDefinition NHamlPartialDefinition = null;

        [Export]
        [Name("nhaml.textLiteral")]
        [BaseDefinition("nhaml")]
        internal static ClassificationTypeDefinition NHamlTextLiteralDefinition = null;

        [Export]
        [Name("nhaml.textVariable")]
        [BaseDefinition("nhaml")]
        internal static ClassificationTypeDefinition NHamlTextVariableDefinition = null;

        [Export]
        [Name("nhaml.htmlAttributeCollection")]
        [BaseDefinition("nhaml")]
        internal static ClassificationTypeDefinition NHamlHtmlAttributeCollectionDefinition = null;

    }
}
