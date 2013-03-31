using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.NHaml.IO;
using System.Web.NHaml.Parser;
using NUnit.Framework;

namespace NHaml.IntegrationTests.Parser
{
    // ReSharper disable InconsistentNaming
    public class HamlDocumentParser_GeneratesCorrectContentIndexes
    {
        [Test]
        public void HamlDocumentParser_JsonFiles_GeneratesCorrectContentIndexes()
        {
            var documentList = TestDocumentLoader.LoadDocumentList();
            foreach (var document in documentList)
            {
                string testName = document.Name;
                int failCount = 0;

                try
                {
                    ExecuteSingleTest(document);
                    Console.WriteLine("PASS : " + testName + "\n");
                }
                catch (Exception ex)
                {
                    failCount++;
                    Console.WriteLine("FAIL - " + testName + "\n");
                    Console.WriteLine(ex.Message);
                }

                Assert.That(failCount, Is.EqualTo(0));
            }
        }

        private void ExecuteSingleTest(TestDocument document)
        {
            var hamlFile = new HamlFileLexer().Read(document.Content);
            var hamlTree = new HamlTreeParser().ParseHamlFile(hamlFile);

            int tokenIndex = 0;
            VerifyTokens(hamlTree, ref tokenIndex, document.TokenList);
        }

        private static void VerifyTokens(HamlNode node, ref int tokenIndex, IList<TestToken> tokenList)
        {
            foreach (var child in node.Children)
            {
                Console.WriteLine("Verifying token " + tokenIndex);
                var token = tokenList[tokenIndex];
                Assert.That(child.SourceFileLineNum, Is.EqualTo(token.LineIndex));
                Assert.That(child.SourceFileCharIndex, Is.EqualTo(token.StartIndex));
                Assert.That(child.SourceFileCharCount, Is.EqualTo(token.Length));
                tokenIndex++;

                VerifyTokens(child, ref tokenIndex, tokenList);
            }
        }
    }
}
