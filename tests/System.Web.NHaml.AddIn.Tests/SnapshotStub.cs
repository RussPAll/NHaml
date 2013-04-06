using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using Moq;

namespace System.Web.NHaml.AddIn
{
    public class SnapshotStub : ITextSnapshot
    {
        private readonly string[] lines;
        private string fullText;

        public SnapshotStub(string[] lines)
        {
            this.lines = lines;
            fullText = string.Join(Environment.NewLine, lines);
        }

        public string GetText(Span span)
        {
            return fullText.Substring(span.Start, span.Length);
        }

        public string GetText(int startIndex, int length)
        {
            return fullText.Substring(startIndex, length);
        }

        public string GetText()
        {
            return fullText;
        }

        public char[] ToCharArray(int startIndex, int length)
        {
            return fullText.Substring(startIndex, length).ToArray();
        }

        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        {
            throw new NotImplementedException();
        }

        public ITrackingPoint CreateTrackingPoint(int position, PointTrackingMode trackingMode)
        {
            throw new NotImplementedException();
        }

        public ITrackingPoint CreateTrackingPoint(int position, PointTrackingMode trackingMode, TrackingFidelityMode trackingFidelity)
        {
            throw new NotImplementedException();
        }

        public ITrackingSpan CreateTrackingSpan(Span span, SpanTrackingMode trackingMode)
        {
            throw new NotImplementedException();
        }

        public ITrackingSpan CreateTrackingSpan(Span span, SpanTrackingMode trackingMode, TrackingFidelityMode trackingFidelity)
        {
            throw new NotImplementedException();
        }

        public ITrackingSpan CreateTrackingSpan(int start, int length, SpanTrackingMode trackingMode)
        {
            var mock = new Mock<ITrackingSpan>();
            mock.Setup(m => m.GetSpan(It.IsAny<ITextSnapshot>()))
                .Returns<ITextSnapshot>(sn => new SnapshotSpan(sn, start, length));
            return mock.Object;
        }

        public ITrackingSpan CreateTrackingSpan(int start, int length, SpanTrackingMode trackingMode, TrackingFidelityMode trackingFidelity)
        {
            throw new NotImplementedException();
        }

        public ITextSnapshotLine GetLineFromLineNumber(int lineNumber)
        {
            return Lines.ToArray()[lineNumber];
        }

        public ITextSnapshotLine GetLineFromPosition(int position)
        {
            return GetLineFromLineNumber(GetLineNumberFromPosition(position));
        }

        public int GetLineNumberFromPosition(int position)
        {
            var linesToPosition = fullText.Substring(0, position)
                            .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            return linesToPosition.Count() - 1;
        }

        public void Write(TextWriter writer, Span span)
        {
            throw new NotImplementedException();
        }

        public void Write(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        public ITextBuffer TextBuffer
        {
            get { return new Mock<ITextBuffer>().Object; }
        }

        public IContentType ContentType
        {
            get { throw new NotImplementedException(); }
        }

        public ITextVersion Version
        {
            get { throw new NotImplementedException(); }
        }

        public int Length
        {
            get { return fullText.Length; }
        }

        public int LineCount
        {
            get { return lines.Count(); }
        }

        public char this[int position]
        {
            get
            {
                return fullText.Substring(position, 1).ToArray()[0];
            }
        }

        public IEnumerable<ITextSnapshotLine> Lines
        {
            get
            {
                for (int index = 0; index < lines.Length; index++)
                {
                    var textSnapshotLine = new TextSnapshotLineStub(this, lines, index);
                    yield return textSnapshotLine;
                }
            }
        }
    }
}
