using Lab01;

namespace Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            List<Item> items = new List<Item>();
            for (int i = 0; i < 10; i++)
            {
                items.Add(new Item());
            }
            Assert.AreEqual(10, items.Count());
        }
        [Test]
        public void Test2()
        {
            Backpack backpack = new Backpack();

            Assert.AreEqual(30, backpack.Capacity());
        }
        [Test]
        public void Test3()
        {
            List<Item> items = new List<Item>();
            Backpack backpack = new Backpack();

            for (int i = 0; i < 10; i++)
            {
                items.Add(new Item());
            }
            
        }
    }
}