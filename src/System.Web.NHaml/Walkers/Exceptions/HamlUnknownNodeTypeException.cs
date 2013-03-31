using System.Web.NHaml.IO;

namespace System.Web.NHaml.Walkers.Exceptions
{
    [Serializable]
    public class HamlUnknownNodeTypeException : Exception
    {
        public HamlUnknownNodeTypeException(Type nodeType, HamlSourceFileMetrics metrics)
            : this(nodeType, metrics, null)
        { }

        private HamlUnknownNodeTypeException(Type nodeType, HamlSourceFileMetrics metrics, Exception ex)
            : base(string.Format("Unknown node type '{0}' at {1}", nodeType.FullName, metrics), ex)
        { }
    }
}
