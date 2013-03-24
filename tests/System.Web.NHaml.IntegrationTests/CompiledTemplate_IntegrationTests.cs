﻿using System.Web.NHaml;
using System.Web.NHaml.Compilers;
using System.Web.NHaml.IO;
using System.Web.NHaml.Parser;
using System.Web.NHaml.TemplateBase;
using System.Web.NHaml.TemplateResolution;
using System.Web.NHaml.Walkers.CodeDom;
using NUnit.Framework;
using NHaml.Tests.Builders;
using System.IO;
using System.Collections.Generic;

namespace NHaml.IntegrationTests
{
    [TestFixture]
    [Category("Integration")]
    public class CompiledTemplate_IntegrationTests
    {
        private TemplateFactoryFactory _templateFactoryFactory;

        [SetUp]
        public void SetUp()
        {
            _templateFactoryFactory = new TemplateFactoryFactory(
                new FileTemplateContentProvider(),
                new ViewSourceParser(new HamlFileLexer(), new HamlTreeParser()), 
                new HamlDocumentWalker(new CodeDomClassBuilder()),
                new CodeDomTemplateCompiler(new CSharp2TemplateTypeBuilder()),
                new List<string>(),
                new List<string>());            
        }

        [Test]
        public void SimpleIntegrationTest()
        {
            // Arrange
            const string templateContent = @"This is a test";
            var viewSource = ViewSourceBuilder.Create(templateContent);

            // Act
            var templateFactory = _templateFactoryFactory.CompileTemplateFactory(viewSource.GetClassName(), viewSource);
            Template template = templateFactory.CreateTemplate();
            var textWriter = new StringWriter();
            template.Render(textWriter);

            // Assert
            Assert.AreEqual("This is a test", textWriter.ToString());
        }

        [Test]
        public void PartialsIntegrationTest()
        {
            // Arrange
            const string template1Content = "This is a test\n_File2";
            const string template2Content = "Of a partial";

            var viewSourceList = new ViewSourceCollection {
                ViewSourceBuilder.Create(template1Content),
                ViewSourceBuilder.Create(template2Content, "File2")
            };

            // Act
            var templateFactory = _templateFactoryFactory.CompileTemplateFactory(viewSourceList.GetClassName(), viewSourceList);
            Template template = templateFactory.CreateTemplate();
            var textWriter = new StringWriter();
            template.Render(textWriter);

            // Assert
            Assert.AreEqual("This is a test\nOf a partial", textWriter.ToString());
        }

        //[Test]
        //public void MasterPageIntegrationTest()
        //{
        //    // Arrange
        //    const string masterPageContent = "_\n" +
        //                                     "_ Partial";
        //    const string templateContent = "Template goes here";
        //    const string partialContent = "Partial goes here";

        //    var viewSourceList = new ViewSourceCollection {
        //        ViewSourceBuilder.Create(masterPageContent),
        //        ViewSourceBuilder.Create(templateContent, "Template"),
        //        ViewSourceBuilder.Create(partialContent, "Partial")
        //    };

        //    // Act
        //    var templateFactory = _templateFactoryFactory.CompileTemplateFactory(viewSourceList.GetClassName(), viewSourceList);
        //    Template template = templateFactory.CreateTemplate();
        //    var textWriter = new StringWriter();
        //    template.Render(textWriter);

        //    // Assert
        //    Assert.AreEqual("This is a test\nOf a partial", textWriter.ToString());
        //}
    }
}
