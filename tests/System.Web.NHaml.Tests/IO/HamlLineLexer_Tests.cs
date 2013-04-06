using System.Linq;
using System.Web.NHaml.IO;
using NUnit.Framework;

namespace NHaml.Tests.IO
{
    [TestFixture]
    public class HamlLineLexer_Tests
    {
        [Test]
        [TestCase("", "", Description = "Empty string")]
        [TestCase(" ", " ", Description = "Single space")]
        [TestCase("Test", "", Description = "No space followed by plain text")]
        [TestCase(" Test", " ", Description = "Space followed by plain text")]
        [TestCase("\tTest", "\t", Description = "Tab followed by plain text")]
        [TestCase(" \tTest", " \t", Description = "Space + Tab followed by plain text")]
        [TestCase("\t Test", "\t ", Description = "Tab + Space followed by plain text")]
        public void Constructor_CalculatesIndentCorrectly(string testString, string expectedIndent)
        {
            var line = HamlLineLexer.ParseHamlLine(testString, 0);
            Assert.AreEqual(expectedIndent, line.First().Indent);
        }

        [Test]
        [TestCase("%p", "p", null)]
        [TestCase("%meta", "meta", null)]
        [TestCase("%p Content", "p", "Content")]
        [TestCase("%p(a='b')Content", "p(a='b')", "Content")]
        [TestCase("%p.className Content", "p.className", "Content")]
        [TestCase("%a:b Content", "a:b", "Content")]
        [TestCase("%a_b Content", "a_b", "Content")]
        [TestCase("%a-b Content", "a-b", "Content")]
        [TestCase("%a-b> Content", "a-b>", "Content")]
        [TestCase("%p #{one}", "p", "#{one}")]
        [TestCase("%p \\#{one}", "p", "\\#{one}")]
        public void Constructor_InlineContent_GeneratesCorrectLines(
            string templateLine, string expectedLine1, string expectedLine2)
        {
            var lines = HamlLineLexer.ParseHamlLine(templateLine, 0).ToList();

            int expectedLineCount = expectedLine2 == null ? 1 : 2;

            Assert.That(lines.Count, Is.EqualTo(expectedLineCount));
            Assert.That(lines[0].Content, Is.EqualTo(expectedLine1));
            if (expectedLineCount > 1)
                Assert.That(lines[1].Content, Is.EqualTo(expectedLine2));
        }

        [Test]
        [TestCase("%p Content", 0, 10, 2, 8)]
        [TestCase("%p(a='b')Content", 0, 16, 9, 7)]
        [TestCase("%p.className Content", 0, 20, 12, 8)]
        //[TestCase("%a:b Content", "a:b", "Content")]
        //[TestCase("%a_b Content", "a_b", "Content")]
        //[TestCase("%a-b Content", "a-b", "Content")]
        //[TestCase("%a-b> Content", "a-b>", "Content")]
        //[TestCase("%p #{one}", "p", "#{one}")]
        [TestCase(@"%p \#{one}", 0, 10, 2, 8)]
        public void Constructor_InlineContent_GeneratesCorrectMetrics(
            string templateLine, int expectedLine1Col, int expectedLine1Length,
            int expectedLine2Col, int expectedLine2Length)
        {
            var lines = HamlLineLexer.ParseHamlLine(templateLine, 0).ToList();

            Assert.That(lines[0].Metrics.ColNo, Is.EqualTo(expectedLine1Col));
            Assert.That(lines[0].Metrics.Length, Is.EqualTo(expectedLine1Length));
            Assert.That(lines[1].Metrics.ColNo, Is.EqualTo(expectedLine2Col));
            Assert.That(lines[1].Metrics.Length, Is.EqualTo(expectedLine2Length));
        }
        
        [Test]
        public void Constructor_SimpleTag_GeneratesCorrectIndexes()
        {
            var lines = HamlLineLexer.ParseHamlLine("%p", 2).ToList();

            Assert.That(lines.Count, Is.EqualTo(1));
            Assert.That(lines[0].Metrics.LineNo, Is.EqualTo(2));
            Assert.That(lines[0].Metrics.Length, Is.EqualTo(2));
        }
    }
}
