﻿using System.Linq;
using System.Web.NHaml.Crosscutting;
using System.Web.NHaml.IO;
using System.Web.NHaml.Parser.Exceptions;

namespace System.Web.NHaml.Parser.Rules
{
    public class HamlNodeHtmlAttributeCollection : HamlNode
    {
        public HamlNodeHtmlAttributeCollection(HamlSourceFileMetrics metrics, string attributeCollection)
            : base(metrics, attributeCollection)
            
        {
            if (Content[0] != '(' && Content[0] != '{')
                throw new HamlParserMalformedTagException("AttributeCollection tag must start with an opening bracket or curly bracket.", Metrics);

            ParseChildren(attributeCollection);
        }

        protected override bool IsContentGeneratingTag
        {
            get { return true; }
        }

        private void ParseChildren(string attributeCollection)
        {
            int index = 1;
            char closingBracketChar = attributeCollection[0] == '{' ? '}' : ')';
            while (index < attributeCollection.Length)
            {
                int startIndex = index;
                string nameValuePair = GetNextAttributeToken(attributeCollection, closingBracketChar, ref index);
                if (!string.IsNullOrEmpty(nameValuePair))
                    AddChild(new HamlNodeHtmlAttribute(Metrics.SubSpan(startIndex, nameValuePair.Length), nameValuePair));
                index++;
            }
        }

        private static string GetNextAttributeToken(string attributeCollection, char closingBracketChar, ref int index)
        {
            var terminatingChars = new[] { ',', ' ', '\t', closingBracketChar };
            string nameValuePair = HtmlStringHelper.ExtractTokenFromTagString(attributeCollection, ref index,
                terminatingChars);
            if (terminatingChars.Contains(nameValuePair[nameValuePair.Length - 1]))
                nameValuePair = nameValuePair.Substring(0, nameValuePair.Length - 1);
            return nameValuePair;
        }
    }
}
