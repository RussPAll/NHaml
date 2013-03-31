using System.Web.NHaml.IO;

namespace System.Web.NHaml.Parser
{
    public class HamlTreeParser : ITreeParser
    {
        public HamlDocument ParseHamlFile(HamlFile hamlFile)
        {
            var result = new HamlDocument(hamlFile.FileName);
            ParseNode(result, hamlFile);
            return result;
        }

        private void ParseNode(HamlNode node, HamlFile hamlFile)
        {
            //node.IsMultiLine = true;
            while ((!hamlFile.EndOfFile) && (hamlFile.CurrentLine.IndentCount > node.IndentCount))
            {
                var nodeLine = hamlFile.CurrentLine;
                var childNode = HamlNodeFactory.GetHamlNode(nodeLine);
                node.AddChild(childNode);

                hamlFile.MoveNext();
                if (hamlFile.EndOfFile == false
                    && hamlFile.CurrentLine.IndentCount > nodeLine.IndentCount)
                {
                    if (hamlFile.CurrentLine.IsInline == false) childNode.AppendInnerTagNewLine();
                    ParseNode(childNode, hamlFile);
                }

                if (hamlFile.EndOfFile == false
                    && hamlFile.CurrentLine.IndentCount >= nodeLine.IndentCount)
                {
                    node.AppendPostTagNewLine(childNode, hamlFile.CurrentLine.Metrics.LineNo);
                }
            }
        }
    }
}
