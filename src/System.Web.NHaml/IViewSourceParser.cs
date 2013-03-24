using System.Web.NHaml.Parser;
using System.Web.NHaml.TemplateResolution;

namespace System.Web.NHaml
{
    public interface IViewSourceParser
    {
        HamlDocument Parse(ViewSource viewSource);
    }
}