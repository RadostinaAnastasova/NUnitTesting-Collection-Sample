using Collections;
using NUnit.Framework.Constraints;

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
    }
}