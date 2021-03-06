﻿using System.Web.NHaml.IO;
using System.Web.NHaml.Parser.Exceptions;
using System.Web.NHaml.Parser.Rules;

namespace System.Web.NHaml.Parser
{
    public static class HamlNodeFactory
    {
        public static HamlNode GetHamlNode(HamlLine nodeLine)
        {
            switch (nodeLine.HamlRule)
            {
                case HamlRuleEnum.PlainText:
                    return new HamlNodeTextContainer(nodeLine);
                case HamlRuleEnum.Tag:
                    return new HamlNodeTag(nodeLine);
                case HamlRuleEnum.HamlComment:
                    return new HamlNodeHamlComment(nodeLine);
                case HamlRuleEnum.HtmlComment:
                    return new HamlNodeHtmlComment(nodeLine);
                case HamlRuleEnum.Evaluation:
                    return new HamlNodeEval(nodeLine);
                case HamlRuleEnum.Code:
                    return new HamlNodeCode(nodeLine);
                case HamlRuleEnum.DocType:
                    return new HamlNodeDocType(nodeLine);
                case HamlRuleEnum.Partial:
                    return new HamlNodePartial(nodeLine);
                default:
                    throw new HamlParserUnknownRuleException(nodeLine.Content, nodeLine.Metrics);
            }
        }
    }
}
