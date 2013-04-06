using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Moq;
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
            classificationTypeRegistryServiceMock.Setup(x => x.GetClassificationType(It.IsAny<string>()))
                                                 .Returns(new Mock<IClassificationType>().Object);
            _classifier = new NHamlClassifier(bufferMock.Object, classificationTypeRegistryServiceMock.Object);
        }

        [Test]
        public void GetClassificationSpans_SingleTag_ReturnsSingleClassificationSpan()
        {
            // Arrange
            var snapshot = new SnapshotStub("%h1".Split('\n'));
            var snapshotSpan = new SnapshotSpan(snapshot, new Span(0, 3));

            // Act
            var spans = _classifier.GetClassificationSpans(snapshotSpan);

            // Assert
            Assert.That(spans.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetClassificationSpans_ComplexTag_ReturnsCorrectNumberOfClassificationSpans()
        {
            // Arrange
            var snapshot = new SnapshotStub("%h1#id.class(a=b c='d')".Split('\n'));
            var snapshotSpan = new SnapshotSpan(snapshot, new Span(0, snapshot.Length));

            // Act
            var spans = _classifier.GetClassificationSpans(snapshotSpan);

            // Assert
            Assert.That(spans.Count, Is.EqualTo(10));
        }

        [Test]
        public void GetClassificationSpans_MultiLineTag_ReturnsSecondLineCorrectly()
        {
            // Arrange
            var snapshot = new SnapshotStub("%h1\n%h2".Split('\n'));
            var snapshotSpan = new SnapshotSpan(snapshot, new Span(5, 3));

            // Act
            var spans = _classifier.GetClassificationSpans(snapshotSpan);

            // Assert
            Assert.That(spans.Count, Is.EqualTo(1));
            var span = spans[0];

            Assert.That(span.Span.Start.Position, Is.EqualTo(5));
            Assert.That(span.Span.Length, Is.EqualTo(3));
        }
    }
}
