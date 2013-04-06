using System;
using System.Web.NHaml.IO;
using System.Web.NHaml.Parser;
using System.Web.NHaml.Parser.Rules;
using System.Web.NHaml.Walkers.CodeDom;
using NUnit.Framework;
using NHaml.Tests.Mocks;

namespace NHaml.Tests.Walkers.CodeDom
{
    [TestFixture]
    public class HamlNodeHtmlCommentWalker_Tests
    {
        private class BogusHamlNode : HamlNode
        {
            public BogusHamlNode() : base(new HamlSourceFileMetrics(0, 0, -1, 0), (string) "") { }

            protected override bool IsContentGeneratingTag
            {
                get { return true; }
            }
        }

        ClassBuilderMock _classBuilderMock;
        private HamlNodeHtmlCommentWalker _walker;
        private HamlHtmlOptions _hamlOptions;

        [SetUp]
        public void SetUp()
        {
            _classBuilderMock = new ClassBuilderMock();
            _hamlOptions = new HamlHtmlOptions();
            _walker = new HamlNodeHtmlCommentWalker(_classBuilderMock, _hamlOptions);
        }

        [Test]
        public void Walk_NodeIsWrongType_ThrowsException()
        {
            var node = new BogusHamlNode();
            Assert.Throws<InvalidCastException>(() => _walker.Walk(node));
        }

        [Test]
        public void Walk_ValidNode_AppendsCorrectOutput()
        {
            // Arrange
            string comment = "Comment";
            var node = new HamlNodeHtmlComment(new HamlLine(comment, HamlRuleEnum.HtmlComment, indent: ""));

            // Act
            _walker.Walk(node);

            // Assert
            Assert.That(_classBuilderMock.Build(""), Is.EqualTo("<!--" + comment + " -->"));
        }

        [Test]
        public void Walk_NestedTags_AppendsCorrectTags()
        {
            // Arrange
            HamlLine nestedText = new HamlLine("Hello world", HamlRuleEnum.PlainText, indent: "  ", isInline: true);
            var tagNode = new HamlNodeHtmlComment(new HamlLine("", HamlRuleEnum.HtmlComment, indent: ""));
            tagNode.AddChild(new HamlNodeTextContainer(nestedText));

            // Act
            _walker.Walk(tagNode);

            // Assert
            string expectedComment = "<!--" + nestedText.Indent + nestedText.Content + " -->";
            Assert.That(_classBuilderMock.Build(""), Is.EqualTo(expectedComment));
        }
    }
}
