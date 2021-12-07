using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AssemblyBrowser;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        private static string Path = "C:\\Users\\anton\\Documents\\study\\OOP\\lab2\\Faker\\bin\\Debug\\net5.0\\FakerLib.dll";
        private List<Record> targetData = new List<Record>();

        public void init()
        {
            targetData.Add(AssemblyBrowsr.BrowseFile(Path));

        }

        [TestMethod]
        public void TestMethod()
        {
            init();

            var testMethod = targetData[0].records[0].records[0].records[2];
            Assert.AreEqual("Methods", testMethod.text);
        }

        [TestMethod]
        public void TestParam()
        {
            init();

            var testParam = targetData[0].records[0].records[4].records[2].records[4];
            Assert.AreEqual(0, testParam.records.Count);
            Assert.AreEqual("Finalize", testParam.text);
        }
        [TestMethod]
        public void TestProperty()
        {
            init();

            var testParam = targetData[0].records[0].records[3].records[1];

            Assert.AreEqual("Properties", testParam.text);
            Assert.AreEqual(0, testParam.records[0].records.Count);
            Assert.AreEqual("type : System.Type", testParam.records[0].text);
        }


    }
}
