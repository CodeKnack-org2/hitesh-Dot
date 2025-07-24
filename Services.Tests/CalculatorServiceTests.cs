using System;
using NUnit.Framework;
using Moq;
using Dummy.Services;

namespace Services.Tests
{
    [TestFixture]
    public class CalculatorServiceTests
    {
        private Mock<ILoggingService> _mockLogger;
        private CalculatorService _calculatorService;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILoggingService>();
            _calculatorService = new CalculatorService(_mockLogger.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _calculatorService = null;
            _mockLogger = null;
        }

        #region Add Method Tests

        [Test]
        public void Add_WithPositiveNumbers_ReturnsCorrectSum()
        {
            // Arrange
            int a = 5;
            int b = 3;
            int expected = 8;

            // Act
            int result = _calculatorService.Add(a, b);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: Add({a}, {b})"), Times.Once);
        }

        [Test]
        public void Add_WithNegativeNumbers_ReturnsCorrectSum()
        {
            // Arrange
            int a = -5;
            int b = -3;
            int expected = -8;

            // Act
            int result = _calculatorService.Add(a, b);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: Add({a}, {b})"), Times.Once);
        }

        [Test]
        public void Add_WithZero_ReturnsCorrectSum()
        {
            // Arrange
            int a = 0;
            int b = 5;
            int expected = 5;

            // Act
            int result = _calculatorService.Add(a, b);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: Add({a}, {b})"), Times.Once);
        }

        [Test]
        public void Add_WithMaxValues_HandlesOverflow()
        {
            // Arrange
            int a = int.MaxValue;
            int b = 1;

            // Act
            int result = _calculatorService.Add(a, b);

            // Assert - In C#, integer overflow wraps around to MinValue
            Assert.AreEqual(int.MinValue, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: Add({a}, {b})"), Times.Once);
        }

        [Test]
        public void Add_WithMinValues_HandlesUnderflow()
        {
            // Arrange
            int a = int.MinValue;
            int b = -1;

            // Act
            int result = _calculatorService.Add(a, b);

            // Assert - In C#, integer underflow wraps around to MaxValue
            Assert.AreEqual(int.MaxValue, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: Add({a}, {b})"), Times.Once);
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 1, 2)]
        [TestCase(-1, -1, -2)]
        [TestCase(100, -50, 50)]
        [TestCase(-100, 200, 100)]
        public void Add_WithVariousInputs_ReturnsExpectedResults(int a, int b, int expected)
        {
            // Act
            int result = _calculatorService.Add(a, b);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: Add({a}, {b})"), Times.Once);
        }

        #endregion

        #region Concat Method Tests

        [Test]
        public void Concat_WithValidStrings_ReturnsConcatenatedString()
        {
            // Arrange
            string a = "Hello";
            string b = "World";
            string expected = "HelloWorld";

            // Act
            string result = _calculatorService.Concat(a, b);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: Concat({a}, {b})"), Times.Once);
        }

        [Test]
        public void Concat_WithEmptyStrings_ReturnsEmptyString()
        {
            // Arrange
            string a = "";
            string b = "";
            string expected = "";

            // Act
            string result = _calculatorService.Concat(a, b);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: Concat({a}, {b})"), Times.Once);
        }

        [Test]
        public void Concat_WithNullStrings_ConcatenatesNulls()
        {
            // Arrange
            string a = null;
            string b = null;

            // Act
            string result = _calculatorService.Concat(a, b);

            // Assert
            Assert.IsNull(result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: Concat({a}, {b})"), Times.Once);
        }

        [Test]
        public void Concat_WithOneNullString_ConcatenatesWithNull()
        {
            // Arrange
            string a = "Hello";
            string b = null;
            string expected = "Hello";

            // Act
            string result = _calculatorService.Concat(a, b);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: Concat({a}, {b})"), Times.Once);
        }

        [Test]
        public void Concat_WithSpecialCharacters_ReturnsConcatenatedString()
        {
            // Arrange
            string a = "!@#$%";
            string b = "^&*()";
            string expected = "!@#$%^&*()";

            // Act
            string result = _calculatorService.Concat(a, b);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: Concat({a}, {b})"), Times.Once);
        }

        [Test]
        public void Concat_WithUnicodeCharacters_ReturnsConcatenatedString()
        {
            // Arrange
            string a = "こんにちは";
            string b = "世界";
            string expected = "こんにちは世界";

            // Act
            string result = _calculatorService.Concat(a, b);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: Concat({a}, {b})"), Times.Once);
        }

        [Test]
        public void Concat_WithWhitespaceStrings_PreservesWhitespace()
        {
            // Arrange
            string a = "  ";
            string b = "\t\n";
            string expected = "  \t\n";

            // Act
            string result = _calculatorService.Concat(a, b);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: Concat({a}, {b})"), Times.Once);
        }

        [TestCase("", "", "")]
        [TestCase("a", "b", "ab")]
        [TestCase("Hello", " World", "Hello World")]
        [TestCase("123", "456", "123456")]
        public void Concat_WithVariousInputs_ReturnsExpectedResults(string a, string b, string expected)
        {
            // Act
            string result = _calculatorService.Concat(a, b);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: Concat({a}, {b})"), Times.Once);
        }

        #endregion

        #region MultiplyAndAdd Method Tests

        [Test]
        public void MultiplyAndAdd_WithPositiveNumbers_ReturnsCorrectResult()
        {
            // Arrange
            double a = 5.5;
            int b = 3;
            double expected = (5.5 * 2) + 3; // 14.0

            // Act
            double result = _calculatorService.MultiplyAndAdd(a, b);

            // Assert
            Assert.AreEqual(expected, result, 0.001);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: MultiplyAndAdd({a}, {b})"), Times.Once);
        }

        [Test]
        public void MultiplyAndAdd_WithNegativeNumbers_ReturnsCorrectResult()
        {
            // Arrange
            double a = -2.5;
            int b = -1;
            double expected = (-2.5 * 2) + (-1); // -6.0

            // Act
            double result = _calculatorService.MultiplyAndAdd(a, b);

            // Assert
            Assert.AreEqual(expected, result, 0.001);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: MultiplyAndAdd({a}, {b})"), Times.Once);
        }

        [Test]
        public void MultiplyAndAdd_WithZero_ReturnsCorrectResult()
        {
            // Arrange
            double a = 0.0;
            int b = 5;
            double expected = (0.0 * 2) + 5; // 5.0

            // Act
            double result = _calculatorService.MultiplyAndAdd(a, b);

            // Assert
            Assert.AreEqual(expected, result, 0.001);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: MultiplyAndAdd({a}, {b})"), Times.Once);
        }

        [Test]
        public void MultiplyAndAdd_WithVeryLargeNumbers_HandlesCorrectly()
        {
            // Arrange
            double a = 1e100;
            int b = 1;
            double expected = (a * 2) + b;

            // Act
            double result = _calculatorService.MultiplyAndAdd(a, b);

            // Assert
            Assert.AreEqual(expected, result, expected * 0.001);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: MultiplyAndAdd({a}, {b})"), Times.Once);
        }

        [Test]
        public void MultiplyAndAdd_WithInfinity_ReturnsInfinity()
        {
            // Arrange
            double a = double.PositiveInfinity;
            int b = 1;

            // Act
            double result = _calculatorService.MultiplyAndAdd(a, b);

            // Assert
            Assert.AreEqual(double.PositiveInfinity, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: MultiplyAndAdd({a}, {b})"), Times.Once);
        }

        [Test]
        public void MultiplyAndAdd_WithNegativeInfinity_ReturnsNegativeInfinity()
        {
            // Arrange
            double a = double.NegativeInfinity;
            int b = 1;

            // Act
            double result = _calculatorService.MultiplyAndAdd(a, b);

            // Assert
            Assert.AreEqual(double.NegativeInfinity, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: MultiplyAndAdd({a}, {b})"), Times.Once);
        }

        [Test]
        public void MultiplyAndAdd_WithNaN_ReturnsNaN()
        {
            // Arrange
            double a = double.NaN;
            int b = 1;

            // Act
            double result = _calculatorService.MultiplyAndAdd(a, b);

            // Assert
            Assert.IsTrue(double.IsNaN(result));
            _mockLogger.Verify(x => x.LogInfo($"Service method called: MultiplyAndAdd({a}, {b})"), Times.Once);
        }

        [TestCase(1.0, 0, 2.0)]
        [TestCase(2.5, 1, 6.0)]
        [TestCase(-1.5, 2, -1.0)]
        [TestCase(0.0, -5, -5.0)]
        public void MultiplyAndAdd_WithVariousInputs_ReturnsExpectedResults(double a, int b, double expected)
        {
            // Act
            double result = _calculatorService.MultiplyAndAdd(a, b);

            // Assert
            Assert.AreEqual(expected, result, 0.001);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: MultiplyAndAdd({a}, {b})"), Times.Once);
        }

        #endregion

        #region RepeatString Method Tests

        [Test]
        public void RepeatString_WithPositiveTimes_ReturnsRepeatedString()
        {
            // Arrange
            string s = "abc";
            int times = 3;
            string expected = "abcabcabc";

            // Act
            string result = _calculatorService.RepeatString(s, times);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: RepeatString({s}, {times})"), Times.Once);
        }

        [Test]
        public void RepeatString_WithZeroTimes_ReturnsEmptyString()
        {
            // Arrange
            string s = "abc";
            int times = 0;
            string expected = "";

            // Act
            string result = _calculatorService.RepeatString(s, times);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: RepeatString({s}, {times})"), Times.Once);
        }

        [Test]
        public void RepeatString_WithNegativeTimes_ReturnsEmptyString()
        {
            // Arrange
            string s = "abc";
            int times = -5;
            string expected = "";

            // Act
            string result = _calculatorService.RepeatString(s, times);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: RepeatString({s}, {times})"), Times.Once);
        }

        [Test]
        public void RepeatString_WithEmptyString_ReturnsEmptyString()
        {
            // Arrange
            string s = "";
            int times = 5;
            string expected = "";

            // Act
            string result = _calculatorService.RepeatString(s, times);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: RepeatString({s}, {times})"), Times.Once);
        }

        [Test]
        public void RepeatString_WithNullString_ThrowsArgumentNullException()
        {
            // Arrange
            string s = null;
            int times = 3;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _calculatorService.RepeatString(s, times));
            _mockLogger.Verify(x => x.LogInfo($"Service method called: RepeatString({s}, {times})"), Times.Once);
        }

        [Test]
        public void RepeatString_WithSingleCharacter_ReturnsRepeatedCharacter()
        {
            // Arrange
            string s = "a";
            int times = 10;
            string expected = "aaaaaaaaaa";

            // Act
            string result = _calculatorService.RepeatString(s, times);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: RepeatString({s}, {times})"), Times.Once);
        }

        [Test]
        public void RepeatString_WithSpecialCharacters_ReturnsRepeatedString()
        {
            // Arrange
            string s = "!@#";
            int times = 2;
            string expected = "!@#!@#";

            // Act
            string result = _calculatorService.RepeatString(s, times);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: RepeatString({s}, {times})"), Times.Once);
        }

        [Test]
        public void RepeatString_WithUnicodeCharacters_ReturnsRepeatedString()
        {
            // Arrange
            string s = "🚀";
            int times = 3;
            string expected = "🚀🚀🚀";

            // Act
            string result = _calculatorService.RepeatString(s, times);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: RepeatString({s}, {times})"), Times.Once);
        }

        [Test]
        public void RepeatString_WithLargeTimes_HandlesCorrectly()
        {
            // Arrange
            string s = "x";
            int times = 1000;
            string expected = new string('x', 1000);

            // Act
            string result = _calculatorService.RepeatString(s, times);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: RepeatString({s}, {times})"), Times.Once);
        }

        [Test]
        public void RepeatString_WithWhitespace_ReturnsRepeatedWhitespace()
        {
            // Arrange
            string s = " \t";
            int times = 2;
            string expected = " \t \t";

            // Act
            string result = _calculatorService.RepeatString(s, times);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: RepeatString({s}, {times})"), Times.Once);
        }

        [TestCase("a", 1, "a")]
        [TestCase("ab", 2, "abab")]
        [TestCase("test", 3, "testtesttest")]
        [TestCase("x", 0, "")]
        [TestCase("y", -1, "")]
        public void RepeatString_WithVariousInputs_ReturnsExpectedResults(string s, int times, string expected)
        {
            // Act
            string result = _calculatorService.RepeatString(s, times);

            // Assert
            Assert.AreEqual(expected, result);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: RepeatString({s}, {times})"), Times.Once);
        }

        #endregion

        #region Constructor and Inheritance Tests

        [Test]
        public void Constructor_WithValidLogger_InitializesCorrectly()
        {
            // Arrange & Act
            var service = new CalculatorService(_mockLogger.Object);

            // Assert
            Assert.IsNotNull(service);
            Assert.IsInstanceOf<ICalculatorService>(service);
            Assert.IsInstanceOf<BaseService>(service);
        }

        [Test]
        public void Constructor_WithNullLogger_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CalculatorService(null));
        }

        #endregion

        #region Integration and Logging Tests

        [Test]
        public void MultipleOperations_LoggingServiceCalledForEach()
        {
            // Arrange
            int addA = 1, addB = 2;
            string concatA = "Hello", concatB = "World";
            double multiplyA = 3.5;
            int multiplyB = 2;
            string repeatS = "test";
            int repeatTimes = 3;

            // Act
            _calculatorService.Add(addA, addB);
            _calculatorService.Concat(concatA, concatB);
            _calculatorService.MultiplyAndAdd(multiplyA, multiplyB);
            _calculatorService.RepeatString(repeatS, repeatTimes);

            // Assert
            _mockLogger.Verify(x => x.LogInfo($"Service method called: Add({addA}, {addB})"), Times.Once);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: Concat({concatA}, {concatB})"), Times.Once);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: MultiplyAndAdd({multiplyA}, {multiplyB})"), Times.Once);
            _mockLogger.Verify(x => x.LogInfo($"Service method called: RepeatString({repeatS}, {repeatTimes})"), Times.Once);
            _mockLogger.Verify(x => x.LogInfo(It.IsAny<string>()), Times.Exactly(4));
        }

        [Test]
        public void AllMethods_ReturnExpectedTypes()
        {
            // Act & Assert
            var addResult = _calculatorService.Add(1, 2);
            Assert.IsInstanceOf<int>(addResult);

            var concatResult = _calculatorService.Concat("a", "b");
            Assert.IsInstanceOf<string>(concatResult);

            var multiplyResult = _calculatorService.MultiplyAndAdd(1.5, 2);
            Assert.IsInstanceOf<double>(multiplyResult);

            var repeatResult = _calculatorService.RepeatString("test", 2);
            Assert.IsInstanceOf<string>(repeatResult);
        }

        [Test]
        public void LogServiceCall_IsCalledForEveryPublicMethod()
        {
            // Arrange & Act
            _calculatorService.Add(1, 2);
            _calculatorService.Concat("a", "b");
            _calculatorService.MultiplyAndAdd(1.0, 1);
            _calculatorService.RepeatString("test", 1);

            // Assert - Verify that LogInfo was called for each method with correct service call format
            _mockLogger.Verify(x => x.LogInfo(It.Is<string>(s => s.Contains("Service method called: Add("))), Times.Once);
            _mockLogger.Verify(x => x.LogInfo(It.Is<string>(s => s.Contains("Service method called: Concat("))), Times.Once);
            _mockLogger.Verify(x => x.LogInfo(It.Is<string>(s => s.Contains("Service method called: MultiplyAndAdd("))), Times.Once);
            _mockLogger.Verify(x => x.LogInfo(It.Is<string>(s => s.Contains("Service method called: RepeatString("))), Times.Once);
        }

        #endregion

        #region Performance and Edge Case Tests

        [Test]
        public void RepeatString_WithVeryLargeTimes_DoesNotCauseMemoryIssues()
        {
            // Arrange
            string s = "";
            int times = int.MaxValue;

            // Act & Assert - Should not throw OutOfMemoryException for empty string
            Assert.DoesNotThrow(() => _calculatorService.RepeatString(s, times));
        }

        [Test]
        public void Add_BoundaryValues_HandledCorrectly()
        {
            // Test with boundary values
            Assert.AreEqual(int.MaxValue, _calculatorService.Add(int.MaxValue, 0));
            Assert.AreEqual(int.MinValue, _calculatorService.Add(int.MinValue, 0));
            Assert.AreEqual(-1, _calculatorService.Add(int.MaxValue, int.MinValue));
        }

        [Test]
        public void MultiplyAndAdd_EdgeCaseValues_HandledCorrectly()
        {
            // Test with edge case floating point values
            Assert.AreEqual(double.PositiveInfinity, _calculatorService.MultiplyAndAdd(double.MaxValue, 0));
            Assert.AreEqual(double.NegativeInfinity, _calculatorService.MultiplyAndAdd(double.MinValue, 0));
            Assert.IsTrue(double.IsNaN(_calculatorService.MultiplyAndAdd(double.NaN, 5)));
        }

        #endregion
    }
}