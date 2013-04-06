﻿using System.Collections.Generic;
using System.Linq;
using System.Web.NHaml.IO;
using System.Web.NHaml.Parser;
using System.Web.NHaml.Parser.Rules;
using NUnit.Framework;
using NHaml.Tests.Builders;

namespace NHaml.Tests.Parser
{
    [TestFixture]
    public class HamlNode_Tests
    {
        private class HamlNodeDummy : HamlNode {
            public HamlNodeDummy() : base(new HamlSourceFileMetrics(0, 0, 0, 0), "") { }

            protected override bool IsContentGeneratingTag
            {
                get { return true; }
            }
        }

        [Test]
        public void AddChild_ValidNode_AddsNodeToChildren()
        {
            var node = new HamlNodeDummy();
            var childNode = new HamlNodeDummy();
            node.AddChild(childNode);
            Assert.AreSame(childNode, node.Children.First());
        }

        [Test]
        public void Previous_ValidPreviousSibling_ReturnsPreviousSibling()
        {
            var document = new HamlNodeDummy();
            document.AddChild(new HamlNodeDummy());
            document.AddChild(new HamlNodeDummy());

            var result = new List<HamlNode>(document.Children)[1].Previous;
            Assert.That(result, Is.SameAs(document.Children.First()));
        }

        [Test]
        public void Previous_FirstChild_ReturnsNull()
        {
            var document = new HamlNodeDummy();
            document.AddChild(new HamlNodeDummy());

            var result = document.Children.First().Previous;
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Next_ValidNextSibling_ReturnsNextSibling()
        {
            var document = new HamlNodeDummy();
            document.AddChild(new HamlNodeDummy());
            document.AddChild(new HamlNodeDummy());

            var result = document.Children.First().Next;
            Assert.That(result, Is.SameAs(document.Children.ToList()[1]));
        }

        [Test]
        public void Parent_ValidChildNode_ReturnsParent()
        {
            var document = new HamlNodeDummy();
            document.AddChild(new HamlNodeDummy());

            var result = document.Children.First().Parent;
            Assert.That(result, Is.SameAs(document));
        }

        [Test]
        public void Parent_RootNode_ReturnsNull()
        {
            var document = new HamlNodeDummy();

            var result = document.Parent;
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Next_LastChild_ReturnsNull()
        {
            var document = new HamlNodeDummy();
            document.AddChild(new HamlNodeDummy());

            var result = document.Children.First().Next;
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Next_ValidChildren_ReturnsNull()
        {
            var document = new HamlNodeDummy();
            document.AddChild(new HamlNodeDummy());
            document.AddChild(new HamlNodeDummy());

            var result = document.Next;
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetNextUnresolvedPartial_NoPartials_ReturnsNull()
        {
            var rootNode = new HamlNodeDummy();

            var result = rootNode.GetNextUnresolvedPartial();
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetNextUnresolvedPartial_Partials_ReturnsPartial()
        {
            var partial = new HamlNodePartial(new HamlLine("", HamlRuleEnum.Partial, new HamlSourceFileMetrics(-1, 0, 0, 0), ""));
            var rootNode = new HamlNodeDummy();
            rootNode.AddChild(partial);

            var result = rootNode.GetNextUnresolvedPartial();
            Assert.That(result, Is.EqualTo(partial));
        }

        [Test]
        public void GetNextUnresolvedPartial_OneResolvedAndOneUnresolvedPartial_ReturnsCorrectPartial()
        {
            var resolvedPartial = new HamlNodePartial(new HamlLine("", HamlRuleEnum.Partial, new HamlSourceFileMetrics(-1, 0, 0, 0), indent: ""));
            resolvedPartial.SetDocument(HamlDocumentBuilder.Create());

            var unresolvedPartial = new HamlNodePartial(new HamlLine("", HamlRuleEnum.Partial, new HamlSourceFileMetrics(-1, 0, 0, 0), indent: ""));

            var rootNode = new HamlNodeDummy();
            rootNode.AddChild(resolvedPartial);
            rootNode.AddChild(unresolvedPartial);

            var result = rootNode.GetNextUnresolvedPartial();
            Assert.That(result, Is.EqualTo(unresolvedPartial));
        }

        [Test]
        public void GetNextUnresolvedPartial_PartialIsAGrandchildNode_ReturnsPartial()
        {
            var partial = new HamlNodePartial(
                new HamlLine("", HamlRuleEnum.Partial, new HamlSourceFileMetrics(-1, 0, 0, 0), indent: ""));
            var textContainerNode = new HamlNodeTextContainer(new HamlSourceFileMetrics(-1, 0, 0, 0), "Test content");
            textContainerNode.AddChild(partial);

            var rootNode = new HamlNodeDummy();
            rootNode.AddChild(textContainerNode);

            var result = rootNode.GetNextUnresolvedPartial();
            Assert.That(result, Is.EqualTo(partial));
        }

    }
}
