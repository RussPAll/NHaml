using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Moq;
using NHamlSyntaxHighlighter;
using NUnit.Framework;

namespace System.Web.NHaml.AddIn.Tests
{
    public class NHamlClassifier_IntegrationTests
    {
        private NHamlClassifier _classifier;

        [SetUp]
        public void SetUp()
        {
            var bufferMock = new Mock<ITextBuffer>();
            var classificationTypeRegistryServiceMock = new Mock<IClassificationTypeRegistryService>();
            _classifier = new NHamlClassifier(bufferMock.Object, classificationTypeRegistryServiceMock.Object);
        }

        [Test]
        public void TBC()
        {
            // Arrange
            var snapshotSpan = new SnapshotSpan();

            // Act
            var spans = _classifier.GetClassificationSpans(snapshotSpan);

            // Assert
            Assert.Pass();
        }
    }
}
