using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace NHaml.IntegrationTests.Parser
{
    public static class TestDocumentLoader
    {
        private const string FileName = "Parser/ParseExamples.json";

        public static IEnumerable<TestDocument> LoadDocumentList()
        {
            var json = File.ReadAllText(FileName);
            return JsonConvert.DeserializeObject<IList<TestDocument>>(json);
        }
    }
}