using System;
using System.Web.NHaml.IO;
using System.Web.NHaml.Parser;
using System.Web.NHaml.Parser.Exceptions;
using System.Web.NHaml.Parser.Rules;
using NUnit.Framework;
using NHaml.Tests.Builders;
using System.Linq;

namespace NHaml.Tests.Parser
{
    [TestFixture]
    public class HamlTreeParser_Tests
    {
        private HamlTreeParser _parser;

        [SetUp]
        public void Setup()
        {
            _parser = new HamlTreeParser();
        }

        [Test]
        public void ParseHamlFile_SingleLineTemplate_ReturnsHamlTree()
        {
            var file = new HamlFileLexer().Read("Test");
            var result = _parser.ParseHamlFile(file);
            Assert.IsInstanceOf(typeof(HamlDocument), result);
        }

        [Test]
        [TestCase("Test content", typeof(HamlNodeTextContainer))]
        [TestCase("%p", typeof(HamlNodeTag))]
        [TestCase("-#comment", typeof(HamlNodeHamlComment))]
        [TestCase("/comment", typeof(HamlNodeHtmlComment))]
        [TestCase("= Test", typeof(HamlNodeEval))]
        public void ParseHamlFile_DifferentLineTypes_CreatesCorrectTreeNodeTypes(string template, Type nodeType)
        {
            var file = new HamlFileLexer().Read(template);
            var result = _parser.ParseHamlFile(file);
            Assert.IsInstanceOf(nodeType, result.Children.First());
        }

        [Test]
        [TestCase("", 0)]
        [TestCase("Test", 1)]
        [TestCase("Test\nTest", 3)]
        [TestCase("Test\n  Test", 1)]
        public void ParseHamlFile_SingleLevelTemplates_TreeContainsCorrectNoOfChildren(string template, int expectedChildrenCount)
        {
            var file = new HamlFileLexer().Read(template);
            var result = _parser.ParseHamlFile(file);
            Assert.That(result.Children.Count(), Is.EqualTo(expectedChildrenCount));
        }

        [Test]
        public void ParseHamlFile_MultiLineTemplates_AddsLineBreakNode()
        {
            const string template = "Line1\nLine2";
            var file = new HamlFileLexer().Read(template);
            var result = _parser.ParseHamlFile(file);
            Assert.That(result.Children.ToList()[1].Content, Is.EqualTo("\n"));
        }

        [Test]
        [TestCase("Test\n  Test", 1)]
        [TestCase("Test\n  Test\n  Test", 1)]
        [TestCase("Test\n  Test\n    Test", 1)]
        [TestCase("Test\n  Test\nTest", 3)]
        public void ParseHamlFile_MultiLevelTemplates_TreeContainsCorrectNoChildren(string template, int expectedChildren)
        {
            var file = new HamlFileLexer().Read(template);
            var result = _parser.ParseHamlFile(file);
            Assert.AreEqual(expectedChildren, result.Children.Count());
        }

        [Test]
        public void ParseHamlFile_NestedContent_PlacesLineBreaksCorrectly()
        {
            const string template = "%p Line 1\n%p\n  Line 2\n%p Line 3";
            var file = new HamlFileLexer().Read(template);
            var result = _parser.ParseHamlFile(file);

            var children = result.Children.ToList();

            Assert.That(children[1].Content, Is.EqualTo("\n"));
            Assert.That(children[2].Children.First().Content, Is.EqualTo("\n"));
            Assert.That(children[2].Children.Count(), Is.EqualTo(2));
            Assert.That(children[3].Content, Is.EqualTo("\n"));
        }

        [Test]
        public void ParseHamlFile_InlineContent_PlacesLineBreaksCorrectly()
        {
            const string template = "%p =DateTime.Now()";
            var file = new HamlFileLexer().Read(template);
            var result = _parser.ParseHamlFile(file);

            var children = result.Children.ToList();
            Assert.That(children[0].Children.Count(), Is.EqualTo(1));
            Assert.That(children[0].Children.First().Content, Is.EqualTo("DateTime.Now()"));
        }

        [Test]
        public void ParseHamlFile_UnknownRuleType_ThrowsUnknownRuleException()
        {
            var line = new HamlLine("", HamlRuleEnum.Unknown, "", 0);

            var file = new HamlFile("");
            file.AddLine(line);
            Assert.Throws<HamlUnknownRuleException>(() => _parser.ParseHamlFile(file));           
        }
    }
}
