using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;

namespace System.Web.NHaml.AddIn
{
    static class ContentType
    {
        public const string NHaml = "NHaml";

        [Export]
        [Name(NHaml)]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition NHamlContentTypeDefinition = null;

        [Export]
        [ContentType(NHaml)]
        [FileExtension(".haml")]
        internal static FileExtensionToContentTypeDefinition HamlFileExtensionDefinition = null;

        [Export]
        [ContentType(NHaml)]
        [FileExtension(".nhaml")]
        internal static FileExtensionToContentTypeDefinition NHamlFileExtensionDefinition = null;
    }
}
