namespace System.Web.NHaml.IO
{
    public class HamlSourceFileMetrics
    {
        public int LineNo { get; private set; }
        public int ColNo { get; private set; }
        public int Length { get; private set; }
    
        public HamlSourceFileMetrics(int lineNo, int colNo, int length)
        {
            Length = length;
            ColNo = colNo;
            LineNo = lineNo;
        }

        public override string ToString()
        {
            return string.Concat("Line ", LineNo, ", Col", ColNo);
        }

        public HamlSourceFileMetrics SubSpan(int startIndex, int length)
        {
            return new HamlSourceFileMetrics(LineNo, ColNo + startIndex, length);
        }
    }
}