using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Unit tests can be done using 3 frameworks in .NET: MSTest which is integrated in Visual Studio itself. NUnit is an independent Testing framework based on JUnit. XUnit is widely used for testing API Framework and is one of the popular Testing Frameworks for .NET.
//Our example uses NUnit as the Testing framework. 
namespace ComponentLib.Tests
{
    [TestFixture]
    public class MathClassTest
    {

        static MathClass cls = null;

        [SetUp]
        public void CreateSetUp()
        {
            cls = new MathClass();
            TestContext.WriteLine("The object is instantiated only once");
        }

        [TestCase(12,13, ExpectedResult =25)]
        [TestCase(12,-13, ExpectedResult =-1)]
        [TestCase(12,0, ExpectedResult =12)]
        [TestCase(-12,-13, ExpectedResult =-25)]
        public double When_Adding_AllKindsOfNumbers(double v1, double v2)
        {
//            var com = new MathClass();
            var actual = cls.AddFunc(v1, v2);
            return actual;
        }
        [Test]
        public void When_Adding_2PositiveNumbers()
        {
            //Any Testing will have AAA in it. 
            //Arrange:
            var component = new MathClass();
            var expected = 357;
            var val1 = 234;
            var val2 = 123;
            //Act
            var actual = component.AddFunc(val1, val2);
            //Assert
            Assert.That(actual == expected);
        }

        [Test]
        public void When_Subtracting_2PositiveNumbers()
        {
            //Arrange:
            var component = new MathClass();
            var expected = 111;
            var val1 = 234;
            var val2 = 123;
            //Act
            var actual = component.SubFunc(val1, val2);
            //Assert
            Assert.That(actual == expected);
        }

        [Test]
        public void When_ArrayHasEvenNumbersOnly()
        {
            //Arrange
            var com = new MathClass();
            var array = new int[] { 2, 4, 6 };
            //Act
            var result = com.ContainsOnlyEvenNumbers(array);
            //Assert
            Assert.That(result == true);
        }

        [TestCase(new int[] {2,3,4}, ExpectedResult = 9)]
        [TestCase(new int[] {2,-3, 4}, ExpectedResult = 3)]
        public double When_TestForGetSumOfIntegers(int [] arg)
        {
            //Arrange
            var com = new MathClass();
            //Act
            return com.GetSum(arg);
        }
    }
}
