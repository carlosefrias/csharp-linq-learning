using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarsTests
{
    [TestClass]
    public class CarTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var a = 2;
            var b = 4;
            var sumExpected = 6;
            //Act
            var actualSum = a + b;
            //Assert
            Assert.AreEqual(sumExpected, actualSum);
        }
    }
}
