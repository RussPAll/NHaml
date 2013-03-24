﻿using System.Collections.Generic;
using System.Linq;
using System.Web.NHaml.IO;
using System.Web.NHaml.Parser.Rules;

namespace System.Web.NHaml.Parser
{
    public abstract class HamlNode
    {
        public readonly int SourceFileCharIndex;
        public readonly int SourceFileLineNum;
        public readonly int SourceFileCharCount;
        public readonly string Content;
        private readonly IList<HamlNode> _children = new List<HamlNode>();
        private readonly HamlLine _line;

        protected HamlNode(HamlLine nodeLine, int sourceFileCharIndex)
        {
            _line = nodeLine;
            Content = nodeLine.Content;
            SourceFileLineNum = _line.SourceFileLineNo;
            SourceFileCharIndex = sourceFileCharIndex;
            SourceFileCharCount = nodeLine.Content.Length;
        }

        protected HamlNode(int sourceFileLineNum, int sourceFileCharIndex, string content)
            : this(new HamlLine(content, HamlRuleEnum.Unknown, "", sourceFileLineNum, true), sourceFileCharIndex)
        { }

        public string Indent
        {
            get { return _line.HamlRule == HamlRuleEnum.Document ? "" : _line.Indent; }
        }

        public bool IsMultiLine
        {
            get { return Children.Any(x => x.IsInline == false); }
        }

        public int IndentCount
        {
            get { return _line.HamlRule == HamlRuleEnum.Document ? -1 : _line.IndentCount; }
        }

        public IEnumerable<HamlNode> Children
        {
            get { return _children; }
        }

        public void AddChild(HamlNode hamlNode)
        {
            hamlNode.Parent = this;
            hamlNode.Previous = _children.LastOrDefault();
            if (hamlNode.Previous != null)
                hamlNode.Previous.Next = hamlNode;

            _children.Add(hamlNode);
        }

        public HamlNode Previous { get; private set; }
        public HamlNode Next { get; private set; }
        public HamlNode Parent { get; private set; }

        public bool IsWhitespaceNode()
        {
            return (this is HamlNodeTextContainer)
                && ((HamlNodeTextContainer)this).IsWhitespace();
        }

        public bool IsLeadingWhitespaceTrimmed
        {
            get
            {
                if (IsSurroundingWhitespaceRemoved())
                    return true;

                var previousNonWhitespaceNode = PreviousNonWhitespaceNode();
                if (previousNonWhitespaceNode != null && previousNonWhitespaceNode.IsSurroundingWhitespaceRemoved())
                    return true;
                if (previousNonWhitespaceNode == null)
                {
                    var parentNode = ParentNonWhitespaceNode();
                    if (parentNode != null && parentNode.IsInternalWhitespaceTrimmed())
                        return true;
                }
                return false;
            }
        }

        public bool IsTrailingWhitespaceTrimmed
        {
            get
            {
                var nextNonWhitespaceNode = NextNonWhitespaceNode();
                if (nextNonWhitespaceNode != null && nextNonWhitespaceNode.IsSurroundingWhitespaceRemoved())
                    return true;

                if (nextNonWhitespaceNode == null)
                {
                    var parentNode = ParentNonWhitespaceNode();
                    if (parentNode != null && parentNode.IsInternalWhitespaceTrimmed())
                        return true;
                }
                return false;
            }
        }

        public void AppendInnerTagNewLine()
        {
            if (IsContentGeneratingTag)
                AddChild(new HamlNodeTextContainer(new HamlLine("\n", HamlRuleEnum.PlainText, "", SourceFileLineNum)));
        }

        public void AppendPostTagNewLine(HamlNode childNode, int lineNo)
        {
            if (childNode.IsContentGeneratingTag)
                AddChild(new HamlNodeTextContainer(new HamlLine("\n", HamlRuleEnum.PlainText, "", lineNo)));
        }

        public HamlNodePartial GetNextUnresolvedPartial()
        {
            foreach (var childNode in Children)
            {
                var partialNode = childNode as HamlNodePartial;
                if (partialNode != null && partialNode.IsResolved == false)
                    return (HamlNodePartial)childNode;

                var childPartialNode = childNode.GetNextUnresolvedPartial();
                if (childPartialNode != null)
                    return childPartialNode;
            }

            return null;
        }

        protected abstract bool IsContentGeneratingTag { get; }

        protected bool IsInline
        {
            get { return _line.IsInline; }
        }

        private HamlNode PreviousNonWhitespaceNode()
        {
            var node = Previous;
            while (node != null && node.IsWhitespaceNode())
                node = node.Previous;
            return node;
        }

        private HamlNode NextNonWhitespaceNode()
        {
            var node = Next;
            while (node != null && node.IsWhitespaceNode())
                node = node.Next;
            return node;
        }

        private bool IsInternalWhitespaceTrimmed()
        {
            return this is HamlNodeTag
                && ((HamlNodeTag)this).WhitespaceRemoval == WhitespaceRemoval.Internal;
        }

        private bool IsSurroundingWhitespaceRemoved()
        {
            return this is HamlNodeTag
                && ((HamlNodeTag)this).WhitespaceRemoval == WhitespaceRemoval.Surrounding;
        }

        private HamlNode ParentNonWhitespaceNode()
        {
            var parentNode = Parent;
            while (parentNode != null && parentNode.IsWhitespaceNode())
                parentNode = parentNode.Parent;
            return parentNode;
        }
    }
}
