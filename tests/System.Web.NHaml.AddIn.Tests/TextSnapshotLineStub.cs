using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text;

namespace System.Web.NHaml.AddIn
{
    public class TextSnapshotLineStub : ITextSnapshotLine
    {
        private readonly ITextSnapshot snapshot;
        private readonly IList<string> lines;
        private readonly int lineNumber;
        private readonly int startLinePosition;

        public TextSnapshotLineStub(ITextSnapshot snapshot, IList<string> lines, int lineNumber)
        {
            this.snapshot = snapshot;
            this.lines = lines;
            this.lineNumber = lineNumber;
            startLinePosition = string.Join(Environment.NewLine, lines.Where((l, i) => i < lineNumber).ToArray()).Length;
            if (startLinePosition > 0) startLinePosition += 2;

        }

        public string GetText()
        {
            return lines[lineNumber];
        }

        public string GetTextIncludingLineBreak()
        {
            throw new NotImplementedException();
        }

        public string GetLineBreakText()
        {
            throw new NotImplementedException();
        }

        public ITextSnapshot Snapshot
        {
            get { return snapshot; }
        }

        public SnapshotSpan Extent
        {
            get { throw new NotImplementedException(); }
        }

        public SnapshotSpan ExtentIncludingLineBreak
        {
            get { throw new NotImplementedException(); }
        }

        public int LineNumber
        {
            get { return lineNumber; }
        }

        public SnapshotPoint Start
        {
            get
            {

                return new SnapshotPoint(snapshot, startLinePosition);
            }
        }

        public int Length
        {
            get { return GetText().Length; }
        }

        public int LengthIncludingLineBreak
        {
            get { throw new NotImplementedException(); }
        }

        public SnapshotPoint End
        {
            get
            {
                return new SnapshotPoint(snapshot, startLinePosition + GetText().Length);
            }
        }

        public SnapshotPoint EndIncludingLineBreak
        {
            get { throw new NotImplementedException(); }
        }

        public int LineBreakLength
        {
            get { throw new NotImplementedException(); }
        }
    }
}
