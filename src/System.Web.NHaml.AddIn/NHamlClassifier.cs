using System;
using System.Collections.Generic;
using System.Web.NHaml.IO;
using System.Web.NHaml.Parser;
using System.Web.NHaml.Parser.Rules;
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

            return new SnapshotSpan(
                startLine.Start,
                endLine.End);
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            ITextSnapshot snapshot = span.Snapshot;
            string text = snapshot.GetText(span);

            // And now parse the given paragraph and return classification spans for everything
            var spans = new List<ClassificationSpan>();

            var document = new HamlTreeParser().ParseHamlFile(new HamlFileLexer().Read(text));
            foreach (var token in document.Children)
            {
                IClassificationType type = GetClassificationTypeForMarkdownToken(token.GetType());

                var snapshotSpan = new SnapshotSpan(span.Start + token.Span.Start, token.Span.Length);
                spans.Add(new ClassificationSpan(snapshotSpan, type));
            }

            return spans;
        }

        //SnapshotSpan GetEnclosingParagraph(SnapshotSpan span)
        //{
        //    ITextSnapshot snapshot = span.Snapshot;

        //    ITextSnapshotLine startLine = span.Start.GetContainingLine();
        //    int startLineNumber = startLine.LineNumber;
        //    int endLineNumber = (span.End <= startLine.EndIncludingLineBreak) ? startLineNumber : snapshot.GetLineNumberFromPosition(span.End);

        //    // Find the first/last empty line
        //    bool foundEmpty = false;
        //    while (startLineNumber > 0)
        //    {
        //        bool lineEmpty = snapshot.GetLineFromLineNumber(startLineNumber).GetText().Trim().Length == 0;

        //        if (lineEmpty)
        //        {
        //            foundEmpty = true;
        //        }
        //        else if (foundEmpty)
        //        {
        //            startLineNumber++;
        //            break;
        //        }

        //        startLineNumber--;
        //    }

        //    foundEmpty = false;
        //    while (endLineNumber < snapshot.LineCount - 1)
        //    {
        //        bool lineEmpty = snapshot.GetLineFromLineNumber(endLineNumber).GetText().Trim().Length == 0;

        //        if (lineEmpty)
        //        {
        //            foundEmpty = true;
        //        }
        //        else if (foundEmpty)
        //        {
        //            endLineNumber--;
        //            break;
        //        }

        //        endLineNumber++;
        //    }

        //    // Generate a string for this paragraph chunk
        //    SnapshotPoint startPoint = snapshot.GetLineFromLineNumber(startLineNumber).Start;
        //    SnapshotPoint endPoint = snapshot.GetLineFromLineNumber(endLineNumber).End;

        //    return new SnapshotSpan(startPoint, endPoint);
        //}

        static readonly Dictionary<Type, string> TokenToClassificationType = new Dictionary<Type, string>
        {
            { typeof(HamlNodeCode), "nhaml.code" },
            { typeof(HamlNodeTag), "nhaml.tag" },
            { typeof(HamlNodeTagId), "nhaml.tagId" },
            { typeof(HamlNodeTagClass), "nhaml.tagClass" },
            { typeof(HamlNodeDocType), "nhaml.docType" },
            { typeof(HamlNodeHtmlComment), "nhaml.htmlComment" },
            { typeof(HamlNodeHamlComment), "nhaml.hamlComment" },
            { typeof(HamlNodeEval), "nhaml.eval" },
            { typeof(HamlNodePartial), "nhaml.partial" },
            { typeof(HamlNodeTextLiteral), "nhaml.textLiteral" },
            { typeof(HamlNodeTextVariable), "nhaml.textVariable" },
            { typeof(HamlNodeHtmlAttributeCollection), "nhaml.htmlAttributeCollection" },
        };

        IClassificationType GetClassificationTypeForMarkdownToken(Type tokenType)
        {
            string classificationType;
            if (!TokenToClassificationType.TryGetValue(tokenType, out classificationType))
                throw new ArgumentException("Unable to find classification type for " + tokenType.ToString(), "tokenType");

            return _classificationRegistry.GetClassificationType(classificationType);
        }

    }
}
