using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace NHamlSyntaxHighlighter
{
    class NHamlClassifier : IClassifier
    {
        readonly IClassificationTypeRegistryService _classificationRegistry;
        readonly ITextBuffer _buffer;

        internal NHamlClassifier(ITextBuffer buffer, IClassificationTypeRegistryService classificationRegistry)
        {
            _classificationRegistry = classificationRegistry;
            _buffer = buffer;
            _buffer.Changed += BufferChanged;
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

        void BufferChanged(object sender, TextContentChangedEventArgs e)
        {
            foreach (var change in e.Changes)
            {
                var span = new SnapshotSpan(e.After, change.NewSpan);
                var wholeLinesSpan = GetWholeLinesSpan(span);

                if (ClassificationChanged != null)
                    ClassificationChanged(this, new ClassificationChangedEventArgs(wholeLinesSpan));
            }
        }

        private static SnapshotSpan GetWholeLinesSpan(SnapshotSpan span)
        {
            var startLine = span.Start.GetContainingLine();
            var endLine = (span.End <= startLine.EndIncludingLineBreak)
                              ? startLine
                              : span.Snapshot.GetLineFromPosition(span.End);

            return new SnapshotSpan(startLine.Start, endLine.End);
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            // And now parse the given paragraph and return classification spans for everything
            var spans = new List<ClassificationSpan>
                {
                    new ClassificationSpan(span, _classificationRegistry.GetClassificationType("nhaml"))
                };
            return spans;
        }
    }
}
