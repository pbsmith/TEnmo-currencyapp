using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneTests
{
    [TestClass]
    public class StoreTest
    {
        [TestMethod]
        public void StoreTestObjectCreation()
        {
            //Arrange
            Store testObject = new Store();

            //Act (done in arrange above)

            //Assert
            Assert.IsNotNull(testObject);
        }
        [TestMethod]
        public void TakeMoney_Invalid_Test()
        {
            Store testObject = new Store();

            string test = testObject.TakeMoney("Garrett");

            Assert.AreEqual("Please enter a whole number.", test);
        }
        [TestMethod]
        public void TakeMoney_ValidString_Test()
        {
            Store testObject = new Store();

            string test = testObject.TakeMoney("10");

            Assert.AreEqual("Success!", test);
        }
        [TestMethod]
        public void TakeMoney_Valid100_Test()
        {
            Store testObject = new Store();

            string test = testObject.TakeMoney("100");

            Assert.AreEqual("Success!", test);
        }
        [TestMethod]
        public void TakeMoney_InValid101_Test()
        {
            Store testObject = new Store();

            string test = testObject.TakeMoney("101");

            Assert.AreEqual("Inputed amount is invalid. Please enter valid amount between $1 - $100", test);
        }
        [TestMethod]
        public void TakeMoney_InValidBalance_Test()
        {
            Store testObject = new Store();

            string test = testObject.TakeMoney("1001");

            Assert.AreEqual("Inputed total breaks Balance cap of $1000. Please enter valid amount.", test);
        }
        [TestMethod]
        public void ItemExistCheck_DoesExist_Test()
        {
            Store testObject = new Store();

            bool test = testObject.ItemExistCheck("C1");

            Assert.AreEqual(true, test);
        }
        [TestMethod]
        public void ItemExistCheck_DoesNotExist_Test()
        {
            Store testObject = new Store();

            bool test = testObject.ItemExistCheck("BOB2");

            Assert.AreEqual(false, test);
        }
        [TestMethod]
        public void ItemSoldOutCheck_SoldOut_Test()
        {
            Store testObject = new Store();
            Items testItem = new Items();

            testObject.SelectProducts("C1", "100", 200);
            string test = testObject.SelectProducts("C1", "2", 200);
            //How can we test for sold out?
            Assert.AreEqual("C1 is SOLD OUT", test);
            //How can we test for sold out?
        }
        [TestMethod]
        public void ItemSoldOutCheck_IsNotSoldOut_Test()
        {
            Store testObject = new Store();

            bool test = testObject.ItemSoldOutCheck("C1");
            //How can we test for sold out?
            Assert.AreEqual(false, test);
        }
        [TestMethod]
        public void ItemInsufficientStockCheck_Test()
        {
            Store testObject = new Store();

            testObject.SelectProducts("C1", "99", 200);
            string test = testObject.ItemInsufficientStockCheck("C1", "2");
            //How can we test for sold out?
            Assert.AreEqual("Your selected amount was invalid. Please enter a valid amount.", test);
        }
        [TestMethod]
        public void ItemSufficientStockCheck_Test()
        {
            Store testObject = new Store();

            string test = testObject.ItemInsufficientStockCheck("C1", "99");
            //How can we test for sold out?
            Assert.AreEqual("Success", test);
        }
        [TestMethod]
        public void GetChange_63dollars50CentsBalance_Test()
        {
            Store testObject = new Store();

            int[] test = testObject.GetChange(63.50m);

            CollectionAssert.AreEqual(new int[] { 3, 0, 0, 3, 2, 0, 0 }, test);
        }
        //[TestMethod]
        //public void ItemSufficientStockCheck_Test()
        //{
        //    Store testObject = new Store();

        //    string test = testObject.ItemInsufficientStockCheck("C1", "99");
        //    //How can we test for sold out?
        //    Assert.AreEqual("Success", test);
        //}
    }
}
