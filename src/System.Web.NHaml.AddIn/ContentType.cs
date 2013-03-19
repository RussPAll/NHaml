using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;

namespace NHamlSyntaxHighlighter
{
    static class ContentType
    {
        public const string NHaml = "NHaml";

        [Export]
        [Name(NHaml)]
        [DisplayName("NHaml")]
        public static ContentTypeDefinition NHamlContentType = null;

        [Export]
        [ContentType(NHaml)]
        [FileExtension(".haml")]
        public static FileExtensionToContentTypeDefinition HamlFileExtension = null;

        [Export]
        [ContentType(NHaml)]
        [FileExtension(".nhaml")]
        public static FileExtensionToContentTypeDefinition NHamlFileExtension = null;
    }
}
