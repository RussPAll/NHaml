﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHaml4.Parser;
using NHaml4.Walkers.CodeDom;
using NHaml4.IO;
using NHaml4.Compilers.Abstract;
using NHaml4.Compilers;
using NHaml4.Configuration;
using System.Configuration;

namespace NHaml4.Configuration
{
    public static class XmlConfigurator
    {
        public static TemplateEngine GetTemplateEngine()
        {
            return GetTemplateEngine(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);
        }

        public static TemplateEngine GetTemplateEngine(IList<string> imports, IList<string> referencedAssemblies)
        {
            return GetTemplateEngine(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath,
                imports, referencedAssemblies);
        }
        
        public static TemplateEngine GetTemplateEngine(string configFile)
        {
            return GetTemplateEngine(configFile, new List<string>(), new List<string>());
        }

        private static TemplateEngine GetTemplateEngine(string configFile, IList<string> imports, IList<string> referencedAssemblies)
        {
            var nhamlConfiguration = NHamlConfigurationSection.GetConfiguration(configFile);
            
            var templateCache = new SimpleTemplateCache();

            var templateFactoryFactory = new TemplateFactoryFactory(
                new HamlTreeParser(new HamlFileLexer()),
                new HamlDocumentWalker(new CodeDomClassBuilder()),
                new CodeDomTemplateCompiler(new CSharp2TemplateTypeBuilder()),
                nhamlConfiguration.ImportsList.Concat(imports),
                nhamlConfiguration.ReferencedAssembliesList.Concat(referencedAssemblies));

            return new TemplateEngine(templateCache, templateFactoryFactory);
        }
    }
}