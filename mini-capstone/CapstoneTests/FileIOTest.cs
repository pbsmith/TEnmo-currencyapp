using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTests
{
    [TestClass]
    public class FileIOTest
    {
        [TestMethod]
        public void ReadInventoryTest()
        {
            FileIO test = new FileIO();

            //Act (done in arrange above)

            //Assert
            Assert.IsNotNull(test);
        }
        [TestMethod]
        public void ReadInventory_ListItem1_Test()
        {
            FileIO test = new FileIO();

            List<Items> testInventory = test.ReadInventory();


            Assert.AreEqual("Snuckers Bar", testInventory[0].ProductName);
        }
        [TestMethod]
        public void ReadInventory_ListItem2_Test()
        {
            FileIO test = new FileIO();

            List<Items> testInventory = test.ReadInventory();


            Assert.AreEqual(.80m, testInventory[1].Price);
        }
    }
}
