﻿using System;
using System.Web.NHaml.IO;
using System.Web.NHaml.Parser;
using System.Web.NHaml.Parser.Rules;
using System.Web.NHaml.Walkers.CodeDom;
using NUnit.Framework;
using NHaml.Tests.Mocks;
using NHaml.Tests.Builders;

namespace NHaml.Tests.Walkers.CodeDom
{
    [TestFixture]
    public class HamlNodeTextContainerWalker_Tests
    {
        private ClassBuilderMock _mockClassBuilder;
        private HamlNodeTextContainerWalker _walker;

        private class BogusHamlNode : HamlNode
        {
            public BogusHamlNode() : base(0, 0, 0, "") { }

            protected override bool IsContentGeneratingTag
            {
                get { return true; }
            }
        }

        [SetUp]
        public void SetUp()
        {
            _mockClassBuilder = new ClassBuilderMock();
            _walker = new HamlNodeTextContainerWalker(_mockClassBuilder, new HamlHtmlOptions());
        }

        [Test]
        public void Walk_NodeIsWrongType_ThrowsException()
        {
            var node = new BogusHamlNode();
            Assert.Throws<InvalidCastException>(() => _walker.Walk(node));
        }

        [Test]
        public void Walk_IndentedNode_WritesIndent()
        {
            const string indent = "  ";
            var node = new HamlNodeTextContainer(new HamlLine("Content", HamlRuleEnum.PlainText, indent: indent, sourceFileLineNum: 0));

            _walker.Walk(node);

            Assert.That(_mockClassBuilder.Build(""), Is.StringStarting(indent));
        }

        [Test]
        [TestCase("   ")]
        [TestCase("\n\t   ")]
        public void Walk_PreviousTagHasSurroundingWhitespaceRemoved_RendersTag(string whiteSpace)
        {
            var node = HamlDocumentBuilder.Create("",
                new HamlNodeTag(new HamlLine("p>", HamlRuleEnum.Tag, indent: "", sourceFileLineNum: -1)),
                new HamlNodeTextContainer(new HamlLine("", HamlRuleEnum.PlainText, indent: whiteSpace, sourceFileLineNum: -1)));

            new HamlDocumentWalker(_mockClassBuilder).Walk(node);

            Assert.That(_mockClassBuilder.Build(""), Is.EqualTo("<p></p>"));
        }

        [Test]
        public void Walk_MultipleWhitespaceWithPreviousTagSurroundingWhitespaceRemoved_RendersTag()
        {
            var node = HamlDocumentBuilder.Create("",
                new HamlNodeTag(new HamlLine("p>", HamlRuleEnum.Tag, indent: "", sourceFileLineNum: -1)),
                new HamlNodeTextContainer(new HamlLine("", HamlRuleEnum.PlainText, indent: "   ", sourceFileLineNum: -1)),
                new HamlNodeTextContainer(new HamlLine("", HamlRuleEnum.PlainText, indent: "   ", sourceFileLineNum: -1)),
                new HamlNodeTextContainer(new HamlLine("", HamlRuleEnum.PlainText, indent: "   ", sourceFileLineNum: -1)));

            new HamlDocumentWalker(_mockClassBuilder).Walk(node);

            Assert.That(_mockClassBuilder.Build(""), Is.EqualTo("<p></p>"));
        }

        [Test]
        [TestCase("   ")]
        [TestCase("\n\t   ")]
        public void Walk_NextTagHasSurroundingWhitespaceRemoved_RendersTag(string whiteSpace)
        {
            var node = HamlDocumentBuilder.Create("",
                new HamlNodeTextContainer(new HamlLine("", HamlRuleEnum.PlainText, indent: whiteSpace, sourceFileLineNum: -1)),
                new HamlNodeTag(new HamlLine("p>", HamlRuleEnum.Tag, indent: "", sourceFileLineNum: -1)));

            new HamlDocumentWalker(_mockClassBuilder).Walk(node);

            Assert.That(_mockClassBuilder.Build(""), Is.EqualTo("<p></p>"));
        }

        [Test]
        public void Walk_MultipleWhitespaceWithNextTagSurroundingWhitespaceRemoved_RendersTag()
        {
            var node = HamlDocumentBuilder.Create("",
                new HamlNodeTextContainer(new HamlLine("", HamlRuleEnum.PlainText, indent: "   ", sourceFileLineNum: -1)),
                new HamlNodeTextContainer(new HamlLine("", HamlRuleEnum.PlainText, indent: "   ", sourceFileLineNum: -1)),
                new HamlNodeTextContainer(new HamlLine("", HamlRuleEnum.PlainText, indent: "   ", sourceFileLineNum: -1)),
                new HamlNodeTag(new HamlLine("p>", HamlRuleEnum.Tag, indent: "", sourceFileLineNum: -1)));

            new HamlDocumentWalker(_mockClassBuilder).Walk(node);

            Assert.That(_mockClassBuilder.Build(""), Is.EqualTo("<p></p>"));
        }

    }
}
