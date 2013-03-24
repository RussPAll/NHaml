using System.Web.NHaml.IO;
using System.Web.NHaml.Parser;
using System.Web.NHaml.TemplateResolution;

namespace System.Web.NHaml
{
    public class ViewSourceParser : IViewSourceParser
    {
        private readonly HamlFileLexer _fileLexer;
        private readonly HamlTreeParser _treeParser;

        public ViewSourceParser(HamlFileLexer fileLexer, HamlTreeParser treeParser)
        {
            _fileLexer = fileLexer;
            _treeParser = treeParser;
        }

        public HamlDocument Parse(ViewSource viewSource)
        {
            using (var streamReader = viewSource.GetTextReader())
            {
                var hamlFile = _fileLexer.Read(streamReader, viewSource.FileName);
                return _treeParser.ParseHamlFile(hamlFile);
            }
        }
    }
}
