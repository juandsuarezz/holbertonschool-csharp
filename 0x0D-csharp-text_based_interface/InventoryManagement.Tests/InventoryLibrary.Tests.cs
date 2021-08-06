using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Tests
{
    [TestClass]
    public class ClassTest
    {
        [TestMethod]
        public void TestBaseCreation()
        {
            BaseClass b = new BaseClass();

            Assert.IsNotNull(b.id);
        }
        [TestMethod]
        public void TestUserCreation()
        {
            User u = new User("Test User");

            Assert.IsNotNull(u.id);
            Assert.AreEqual(u.name, "Test User");
            Assert.IsInstanceOfType(u, typeof(BaseClass));
        }
        [TestMethod]
        public void TestItemCreation()
        {
            Item i = new Item("Test Item");

            Assert.IsNotNull(i.id);
            Assert.AreEqual(i.name, "Test Item");
            Assert.IsInstanceOfType(i, typeof(BaseClass));
        }
        [TestMethod]
        public void TestInvCreation()
        {
            Item i = new Item("Test Item");
            User u = new User("Test User");
            Inventory inv = new Inventory(u.id, i.id, 3);

            Assert.IsNotNull(inv.id);
            Assert.AreEqual(inv.item_id, i.id);
            Assert.AreEqual(inv.user_id, u.id);
            Assert.IsInstanceOfType(inv, typeof(BaseClass));
        }
        [TestMethod]
        public void TestInvCreationBad()
        {
            Item i = new Item("Test Item");
            User u = new User("Test User");
            Inventory inv = new Inventory(u.id, i.id, -1);

           Assert.IsTrue(inv.quantity == 1);
        }
        [TestMethod]
        public void TestItemCreationOptions()
        {
            List<string> tags = new List<string>();
            tags.Add("Dev");
            tags.Add("Test");
            Item i = new Item("Test Item", "Test Desc", 2.99f, tags);

            Assert.IsTrue(i.description == "Test Desc"); // same desc
            Assert.AreEqual(2.99f, i.price); // same price
            Assert.AreEqual(typeof(List<string>), i.tags.GetType()); // same type
            Assert.IsTrue(Enumerable.SequenceEqual(tags, i.tags)); // same contents
            Assert.IsFalse(Object.ReferenceEquals(tags, i.tags)); // different reference
        }
    }
}
