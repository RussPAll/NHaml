using System.Web.NHaml.IO;

namespace System.Web.NHaml.Walkers.Exceptions
{
    [Serializable]
    public class HamlInvalidChildNodeException : Exception
    {
        public HamlInvalidChildNodeException(Type nodeType, Type childType, HamlSourceFileMetrics lineNo)
            : this(nodeType, childType, lineNo, null)
        { }

        private HamlInvalidChildNodeException(Type nodeType, Type childType, HamlSourceFileMetrics lineNo, Exception ex)
            : base(string.Format("Node '{0}' has invalid child node {1} at {2}", nodeType.FullName, childType.FullName, lineNo), ex)
        { }
    }
}
