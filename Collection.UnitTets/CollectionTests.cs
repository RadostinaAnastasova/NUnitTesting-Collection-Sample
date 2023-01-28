using Collections;
using NUnit.Framework.Constraints;
using System.Security.Cryptography.X509Certificates;

namespace Collection.UnitTets
{
    public class CollectionTests
    {
        [Test]
        public void Test_Collection_Initialisation()
        {
            // Arrange and Act
            var coll = new Collection<int>();

            // Assert
            Assert.AreEqual(coll.Count, 0);
            Assert.AreEqual(coll.Capacity, 16);
        }

        [Test]
        public void Test_Collection_emptyConstructor()
        {
            // Arrange and Act
            var coll = new Collection<int>(); 

            // Assert
            Assert.AreEqual(coll.ToString(), "[]");
        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            // Arrange and Act
            var coll = new Collection<int>(5);

            var actual = coll.ToString();
            var expected = "[5]";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            // Arrange and Act
            var coll = new Collection<int>(5, 6);

            var actual = coll.ToString();
            var expected = "[5, 6]";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Collection_CountAndCapacity()
        {
            // Arrange and Act
            var coll = new Collection<int>(5, 6);

            // Assert
            Assert.AreEqual(coll.Count, 2, "Check for count");
            Assert.That(coll.Capacity, Is.GreaterThanOrEqualTo(coll.Count));
        }

        [Test]
        public void Test_Collection_Add()
        {
            // Arrange
            var coll = new Collection<string>("Ivan", "Peter");

            // Act
            coll.Add("Gosho");

            // Assert
            Assert.AreEqual("[Ivan, Peter, Gosho]", coll.ToString());            
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            // Arrange
            var collection = new Collection<int>(5, 6, 7);

            // Act
            var item = collection[1];

            // Assert
            Assert.AreEqual(6, item);
            Assert.That(item.ToString(), Is.EqualTo("6"));
        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            // Arrange
            var collection = new Collection<int>(5, 6, 7);

            // Act
            collection[1] = 60;

            // Assert
            Assert.AreEqual(60, collection[1]);
            Assert.That(collection.ToString(), Is.EqualTo("[5, 60, 7]"));
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            // Arrange
            var coll = new Collection<string>("Ivan", "Peter");

            // Assert
            Assert.That(()=> { var item = coll[2]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => coll[2], Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_AddRangeWithGrow()
        {
            Collection<int> coll = new Collection<int>(1, 2);

            Assert.That(coll.Count, Is.EqualTo(2), "Count");
            Assert.That(coll.Capacity, Is.GreaterThanOrEqualTo(1), "Capacity");

            for (int i = 0; i < 50; i++) 
            {
                coll.Add(i);
            }

            Assert.That(coll.Count, Is.EqualTo(52), "Count");
            Assert.That(coll.Capacity, Is.GreaterThanOrEqualTo(52), "Capacity");

            var expected = Enumerable.Range(0, 50).ToArray(); ;
            string expectedStr = "[1, 2, " + string.Join(", ", expected) + "]";
            //var expected = "[1, 2, " + string.Join(", ", Enumerable.Range(0,50).ToArray() + "]");

            Assert.AreEqual(expectedStr, coll.ToString());
        }

        [TestCase("Peter,Maria,Ivan", 0, "Peter")]
        [TestCase("Peter,Maria,Ivan", 1, "Maria")]
        [TestCase("Peter,Maria,Ivan", 2, "Ivan")]
        [TestCase("Peter", 0, "Peter")]
        public void Test_Collection_GetByValidIndex(string data, int index, string expected) 
        {
            var coll = new Collection<string>(data.Split(","));

            var actual = coll[index];

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("Peter", 1)]
        [TestCase("Peter,Maria, Ivan", 5)]
        [TestCase("Peter,Maria, Ivan", -1)]
        [TestCase("", 1)]
        [TestCase("", 0)] // ?
        public void Test_Collection_GetByInvalidIndex(string data, int index)
        {
            var coll = new Collection<string>(data.Split(",", StringSplitOptions.RemoveEmptyEntries));

            Assert.That(() => coll[index], Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

    }
}