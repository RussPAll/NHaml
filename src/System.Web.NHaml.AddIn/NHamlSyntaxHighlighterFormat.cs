using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace NHamlSyntaxHighlighter
{
    #region Format definition
    /// <summary>
    /// Defines an editor format for the NHamlSyntaxHighlighter type that has a purple background
    /// and is underlined.
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "NHamlSyntaxHighlighter")]
    [Name("NHamlSyntaxHighlighter")]
    [UserVisible(true)] //this should be visible to the end user
    [Order(Before = Priority.Default)] //set the priority to be after the default classifiers
    internal sealed class NHamlSyntaxHighlighterFormat : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "NHamlSyntaxHighlighter" classification type
        /// </summary>
        public NHamlSyntaxHighlighterFormat()
        {
            this.DisplayName = "NHamlSyntaxHighlighter"; //human readable version of the name
            this.BackgroundColor = Colors.BlueViolet;
            this.TextDecorations = System.Windows.TextDecorations.Underline;
        }
    }
    #endregion //Format definition
}
