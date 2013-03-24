using System;
using System.Collections.Generic;
using System.Web.NHaml.IO;
using System.Web.NHaml.Parser;
using System.Web.NHaml.Parser.Rules;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace NHamlSyntaxHighlighter
{
    public class NHamlClassifier : IClassifier
    {
        readonly IClassificationTypeRegistryService _classificationRegistry;
        readonly ITextBuffer _buffer;

        public NHamlClassifier(ITextBuffer buffer, IClassificationTypeRegistryService classificationRegistry)
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

            var document = new HamlTreeParser()
                .ParseHamlFile(new HamlFileLexer().Read(text));

            var spans = new List<ClassificationSpan>();
            AddClassificationSpans(span, spans, document);
            return spans;
        }

        private void AddClassificationSpans(SnapshotSpan snapshotSpan, List<ClassificationSpan> spans, HamlNode node)
        {
            if (node.SourceFileCharIndex + node.SourceFileCharCount > 0)
            {
                var span = new SnapshotSpan(snapshotSpan.Snapshot, node.SourceFileCharIndex, node.SourceFileCharCount);
                var type = GetClassificationTypeForMarkdownToken(node.GetType());
                spans.Add(new ClassificationSpan(span, type));
            }
            foreach (var childNode in node.Children)
                AddClassificationSpans(snapshotSpan, spans, childNode);
        }

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
                throw new ArgumentException("Unable to find classification type for " + tokenType, "tokenType");

            return _classificationRegistry.GetClassificationType(classificationType);
        }

    }
}
