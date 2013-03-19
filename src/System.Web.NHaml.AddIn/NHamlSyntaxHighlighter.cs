using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace NHamlSyntaxHighlighter
{
    /// <summary>
    /// Classifier that classifies all text as an instance of the OrinaryClassifierType
    /// </summary>
    class NHamlSyntaxHighlighter : IClassifier
    {
        readonly IClassificationType _classificationType;

        internal NHamlSyntaxHighlighter(IClassificationTypeRegistryService registry)
        {
            _classificationType = registry.GetClassificationType("NHamlSyntaxHighlighter");
        }

        /// <summary>
        /// This method scans the given SnapshotSpan for potential matches for this classification.
        /// In this instance, it classifies everything and returns each span as a new ClassificationSpan.
        /// </summary>
        /// <param name="span">The span currently being classified</param>
        /// <returns>A list of ClassificationSpans that represent spans identified to be of this classification</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            //create a list to hold the results
            var classifications = new List<ClassificationSpan>
                {
                    new ClassificationSpan(new SnapshotSpan(span.Snapshot, new Span(span.Start, span.Length)),
                                           _classificationType)
                };
            return classifications;
        }

        #pragma warning disable 67
        // This event gets raised if a non-text change would affect the classification in some way,
        // for example typing /* would cause the classification to change in C# without directly
        // affecting the span.
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
        #pragma warning restore 67
    }
}
