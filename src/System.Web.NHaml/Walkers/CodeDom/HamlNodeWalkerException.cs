using System.Web.NHaml.IO;

namespace System.Web.NHaml.Walkers.CodeDom
{
    class HamlNodeWalkerException : Exception
    {
        public HamlNodeWalkerException(string hamlNodeType, HamlSourceFileMetrics metrics, Exception e)
            : base(
                string.Format("Exception occurred walking {0} HamlNode at {1}.", hamlNodeType, metrics),
                e)
        { }
    }
}
