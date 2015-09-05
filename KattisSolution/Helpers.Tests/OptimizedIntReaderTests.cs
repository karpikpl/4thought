﻿using System.IO;
using System.Text;
using KattisSolution.IO;
using NUnit.Framework;

namespace KattisSolution.Helpers.Tests
{
    [TestFixture]
    public class OptimizedIntReaderTests
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
        }

        [Test]
        [ExpectedException(typeof(NoMoreTokensException))]
        public void Should_ThrowException_When_StreamEmpty()
        {
            // Arrange
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("")))
            {
                var target = new OptimizedIntReader(ms);

                // Act
                target.NextInt();
            }
        }

        [Test]
        public void Should_Return0_When_0()
        {
            // Arrange
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("0")))
            {
                var target = new OptimizedIntReader(ms);

                // Act
                var result = target.NextInt();

                // Assert
                Assert.That(result, Is.EqualTo(0));
            }
        }

        [Test]
        public void Should_Return9_When_9()
        {
            // Arrange
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("9")))
            {
                var target = new OptimizedIntReader(ms);

                // Act
                var result = target.NextInt();

                // Assert
                Assert.That(result, Is.EqualTo(9));
            }
        }

        [Test]
        public void Should_Return12345_When_12345()
        {
            // Arrange
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("12345")))
            {
                var target = new OptimizedIntReader(ms);

                // Act
                var result = target.NextInt();

                // Assert
                Assert.That(result, Is.EqualTo(12345));
            }
        }

        [Test]
        public void Should_Return12_When_12AndSomeOtherText()
        {
            // Arrange
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(" \n 12 ")))
            {
                var target = new OptimizedIntReader(ms);

                // Act
                var result = target.NextInt();

                // Assert
                Assert.That(result, Is.EqualTo(12));
            }
        }
    }
}
