using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Tests
{
    [TestClass]
    public class StorageTest
    {
        [TestMethod]
        public void TestStorageCreation()
        {
            JSONStorage storage = new JSONStorage();
            User u = new User("Test User 2");

            storage.New(u);
            storage.Save();
            Assert.IsTrue(File.Exists("storage/inventory_manager.json"));
        }
        [TestMethod]
        public void TestStorageLoad()
        {
            JSONStorage storage = new JSONStorage();
            User u = new User("Test User 2");

            storage.New(u);
            storage.Save();
            storage.Load();
            Assert.IsTrue(storage.All().ContainsKey(String.Format("User.{0}", u.id)));
        }
        [TestMethod]
        public void TestStorageAll()
        {
            JSONStorage storage = new JSONStorage();
            User u1 = new User("Test User 1");
            User u2 = new User("Test User 2");

            storage.New(u1);
            storage.New(u2);
            storage.Save();
            Assert.IsTrue(storage.All().ContainsKey(String.Format("User.{0}", u1.id)));
            Assert.IsTrue(storage.All().ContainsKey(String.Format("User.{0}", u2.id)));
        }
    }
}
