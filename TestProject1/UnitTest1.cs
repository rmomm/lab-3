using NUnit.Framework;
using RefactoringGuru.DesignPatterns.Builder.Conceptual;

namespace TestProject2
{
    [TestFixture]
    public class BuilderTests
    {
        private ConcreteBuilder builder;
        private Director director;

        [SetUp]
        public void Setup()
        {
            builder = new ConcreteBuilder();
            director = new Director();
            director.Builder = builder;
        }

        [Test]
        public void TestMinimalViableProduct()
        {
            director.BuildMinimalViableProduct();
            var product = builder.GetProduct();

            var expectedParts = "Product parts: PartA1";
            var actualParts = product.ListParts().Trim();

            Assert.That(actualParts, Is.EqualTo(expectedParts));
        }

        [Test]
        public void TestFullFeaturedProduct()
        {
            director.BuildFullFeaturedProduct();
            var product = builder.GetProduct();

            var expectedParts = "Product parts: PartA1, PartB1, PartC1";
            var actualParts = product.ListParts().Trim();

            Assert.That(actualParts, Is.EqualTo(expectedParts));
        }

        [Test]
        public void TestCustomProduct()
        {
            builder.BuildPartA();
            builder.BuildPartC();

            var product = builder.GetProduct();

            var expectedParts = "Product parts: PartA1, PartC1";
            var actualParts = product.ListParts().Trim();

            Assert.That(actualParts, Is.EqualTo(expectedParts));
        }

        [Test]
        public void TestProductResetAfterBuilding()
        {
            director.BuildFullFeaturedProduct();
            var firstProduct = builder.GetProduct();

            builder.BuildPartA();
            var secondProduct = builder.GetProduct();

            Assert.That(firstProduct.ListParts(), Is.Not.EqualTo(secondProduct.ListParts()));
        }

        [Test]
        public void TestProductAddPartsShouldReturnCorrectList()
        {
            var product = new Product();
            product.Add("CustomPart1");
            product.Add("CustomPart2");

            var expected = "Product parts: CustomPart1, CustomPart2";
            var actual = product.ListParts().Trim();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TestGetProductShouldResetBuilderState()
        {
            var builder = new ConcreteBuilder();

            builder.BuildPartA();
            var product1 = builder.GetProduct();
            var result1 = product1.ListParts().Trim();

            builder.BuildPartB();
            var product2 = builder.GetProduct();
            var result2 = product2.ListParts().Trim();

            Assert.That(result1, Is.EqualTo("Product parts: PartA1"));
            Assert.That(result2, Is.EqualTo("Product parts: PartB1"));
        }
    }
}