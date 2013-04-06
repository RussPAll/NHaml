using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.NHaml.IO;
using System.Web.NHaml.Parser;
using NUnit.Framework;

namespace NHaml.IntegrationTests.Parser
{
    // ReSharper disable InconsistentNaming
    public class HamlDocumentParser_GeneratesCorrectContentIndexes
    {
        [TestCase("simple tag")]
        [TestCase("tag with property")]
        [TestCase("tag with multiple classes")]
        [TestCase("tag followed by whitespace")]
        [TestCase("indented tag followed by whitespace")]
        public void HamlDocumentParser_JsonFiles_GeneratesCorrectContentIndexes(string testToExecute)
        {
            var documentList = TestDocumentLoader.LoadDocumentList();
            var document = documentList.Single(x => x.Name == testToExecute);
            
            var hamlFile = new HamlFileLexer().Read(document.Content);
            var hamlTree = new HamlTreeParser().ParseHamlFile(hamlFile);

            int tokenIndex = 0;
            VerifyTokens(hamlTree, ref tokenIndex, document.TokenList);
        }

        private static void VerifyTokens(HamlNode node, ref int tokenIndex, IList<TestToken> tokenList)
        {
            foreach (var child in node.Children)
            {
                var token = tokenList[tokenIndex];
                Console.WriteLine("Verifying token " + token.Type + " [" + tokenIndex + "]");
                Assert.That(child.GetType().ToString(), Is.StringContaining(token.Type));
                Assert.That(child.Metrics.LineNo, Is.EqualTo(token.LineIndex));
                Assert.That(child.Metrics.ColNo, Is.EqualTo(token.StartIndex));
                Assert.That(child.Metrics.Length, Is.EqualTo(token.Length));
                Console.WriteLine("Pass token " + tokenIndex);
                tokenIndex++;

                VerifyTokens(child, ref tokenIndex, tokenList);
            }
        }
    }
}
