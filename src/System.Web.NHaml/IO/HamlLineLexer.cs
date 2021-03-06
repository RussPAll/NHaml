﻿using System.Collections.Generic;
using System.Web.NHaml.Parser;

namespace System.Web.NHaml.IO
{
    public static class HamlLineLexer
    {
        public static IEnumerable<HamlLine> ParseHamlLine(string currentLine, int currentLineIndex)
        {
            int whiteSpaceIndex = 0;

            while (whiteSpaceIndex < currentLine.Length
                && (currentLine[whiteSpaceIndex] == ' ' || currentLine[whiteSpaceIndex] == '\t'))
            {
                whiteSpaceIndex++;
            }

            string indent = currentLine.Substring(0, whiteSpaceIndex);
            string content = (whiteSpaceIndex == currentLine.Length) ? "" : currentLine.Substring(whiteSpaceIndex);
            int tokenLength = 0;
            var hamlRule = HamlRuleFactory.ParseHamlRule(ref content, out tokenLength);

            var metrics = new HamlSourceFileMetrics(currentLineIndex, whiteSpaceIndex, currentLine.Length - whiteSpaceIndex, tokenLength);
            var result = new List<HamlLine>();
            var line = new HamlLine(content, hamlRule, metrics, indent, false);

            ProcessInlineTags(line, result);
            return result;
        }

        private static void ProcessInlineTags(HamlLine line, List<HamlLine> result)
        {
            if (IsRuleThatAllowsInlineContent(line.HamlRule))
            {
                int contentIndex = GetEndOfTagIndex('%' + line.Content)-1;
                if (contentIndex < line.Content.Length-1)
                {
                    string subTag = line.Content.Substring(contentIndex);
                    line.Content = line.Content.Substring(0, contentIndex);
                    int tokenLength;
                    var subTagRule = HamlRuleFactory.ParseHamlRule(ref subTag, out tokenLength);
                    var colNo = contentIndex + line.Metrics.TokenLength;
                    var metrics = new HamlSourceFileMetrics(line.Metrics.LineNo,
                        line.Metrics.ColNo + colNo, line.Metrics.Length - colNo, tokenLength);
                    var subLine = new HamlLine(subTag, subTagRule, metrics, line.Indent + "\t", isInline: true);
                    ProcessInlineTags(subLine, result);
                }
            }
            result.Insert(0, line);
        }

        private static bool IsRuleThatAllowsInlineContent(HamlRuleEnum hamlRule)
        {
            return hamlRule == HamlRuleEnum.Tag || hamlRule == HamlRuleEnum.DivId || hamlRule == HamlRuleEnum.DivClass;
        }

        internal static int GetEndOfTagIndex(string currentLine)
        {
            if (currentLine.Length < 1 ||
                "%.#".Contains(currentLine[0].ToString()) == false)
                return currentLine.Length;

            bool inAttributes = false;
            bool inSingleQuote = false;
            bool inDoubleQuote = false;
            bool acceptAlphas = true;

            int index;
            for (index = 1; index < currentLine.Length; index++)
            {
                char curChar = currentLine[index];
                if (inSingleQuote)
                {
                    if (curChar == '\'') inSingleQuote = false;
                }
                else if (inDoubleQuote)
                {
                    if (curChar == '\"') inDoubleQuote = false;
                }
                else if (inAttributes)
                {
                    if (curChar == '\'')
                        inSingleQuote = true;
                    else if (curChar == '\"')
                        inDoubleQuote = true;
                    else if (curChar == ')' || curChar == '}')
                    {
                        inAttributes = false;
                        acceptAlphas = false;
                    }
                }
                else
                {
                    if (curChar == '(' || curChar == '{')
                        inAttributes = true;
                    else if ("\\.#_-:>".Contains(curChar.ToString()))
                        continue;
                    else if (Char.IsLetterOrDigit(curChar) && acceptAlphas)
                        continue;
                    else
                        return index;
                }
            }
            return inAttributes ? -1 : index;
        }
    }
}
