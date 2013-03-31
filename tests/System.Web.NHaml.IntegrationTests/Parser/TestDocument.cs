using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NHaml.IntegrationTests.Parser
{
    public class TestDocument
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public IList<TestToken> TokenList { get; set; }

        //public void AddToken(TestToken token)
        //{
        //    _tokenList.Add(token);
        //}

    }

    public class TestToken
    {

        public int LineIndex { get; set; }
        public int StartIndex { get; set; }
        public int Length { get; set; }

        //public TestToken(int lineIndex, int startIndex, int length)
        //{
        //    Length = length;
        //    StartIndex = startIndex;
        //    LineIndex = lineIndex;
        //}
    }
}