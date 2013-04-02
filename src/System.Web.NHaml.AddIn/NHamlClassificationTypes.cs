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
        [Name("nhaml.tag")]
        internal static ClassificationTypeDefinition NHamlTagClassificationDefinition = null;

        [Export]
        [Name("nhaml.tagId")]
        internal static ClassificationTypeDefinition NHamlTagIdClassificationDefinition = null; 
    
        [Export]
        [Name("nhaml.tagClass")]
        internal static ClassificationTypeDefinition NHamlTagClassClassificationDefinition = null;

        [Export]
        [Name("nhaml.htmlAttributeCollection")]
        internal static ClassificationTypeDefinition NHamlHtmlAttributeCollectionClassificationDefinition = null;

        [Export]
        [Name("nhaml.htmlAttribute")]
        internal static ClassificationTypeDefinition NHamlHtmlAttributeClassificationDefinition = null;
            
        [Export]
        [Name("nhaml.textContainer")]
        internal static ClassificationTypeDefinition NHamlTextContainerClassificationDefinition = null;

        [Export]
        [Name("nhaml.textLiteral")]
        internal static ClassificationTypeDefinition NHamlTextLiteralClassificationDefinition = null;

        [Export]
        [Name("nhaml.textVariable")]
        internal static ClassificationTypeDefinition NHamlTextVariableClassificationDefinition = null;

        [Export]
        [Name("nhaml.code")]
        internal static ClassificationTypeDefinition NHamlCodeClassificationDefinition = null;

        [Export]
        [Name("nhaml.eval")]
        internal static ClassificationTypeDefinition NHamlEvalClassificationDefinition = null;

    }
}
