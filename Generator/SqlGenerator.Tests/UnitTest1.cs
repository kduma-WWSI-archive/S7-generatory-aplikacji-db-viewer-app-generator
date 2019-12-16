using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SqlGenerator.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic	here
            //
        }

        [TestMethod]
        public void Test1()
        {
            var t1 = new Table("table_1");
            var c1 = new Column("name", t1, null);
            var c2 = new Column("city", t1, null);

            var g = new Generator();
            g.Columns.Add(c1);
            g.Columns.Add(c2);
            g.Tables.Add(t1);

            Assert.AreEqual(
                "SELECT table_1.name, table_1.city FROM table_1",
                g.ToString()
            );
        }

        [TestMethod]
        public void Test2()
        {
            var t1 = new Table("table_1");
            var t2 = new Table("table_2");
            var c1 = new Column("name", t1, null);
            var c2 = new Column("city", t1, null);
            var c3 = new Column("family", t2, null);

            var g = new Generator();
            g.Columns.Add(c1);
            g.Columns.Add(c2);
            g.Columns.Add(c3);
            g.Tables.Add(t1);
            g.Tables.Add(t2);

            Assert.AreEqual(
                "SELECT table_1.name, table_1.city, table_2.family FROM table_1, table_2",
                g.ToString()
                );
        }

        [TestMethod]
        public void Test3()
        {
            var t1 = new Table("table_1");
            var t2 = new Table("table_2");
            var c1 = new Column("name", t1, null);
            var c2 = new Column("city", t1, null);
            var c3 = new Column("family_name", t2, null);
            var k1 = new KeyPair(t2, "id", t1, "family_id");

            var g = new Generator();
            g.Columns.Add(c1);
            g.Columns.Add(c2);
            g.Columns.Add(c3);
            g.Tables.Add(t1);
            g.Tables.Add(t2);
            g.KeyPairs.Add(k1);

            Assert.AreEqual(
                "SELECT table_1.name, table_1.city, table_2.family_name FROM table_2 JOIN table_1 ON table_2.family_id = table_1.id",
                g.ToString()
            );
        }

        [TestMethod]
        public void Test4()
        {
            var t1 = new Table("table_1");
            var c1 = new Column("name", t1, null);
            var c2 = new Column("price", t1, "SUM");

            var g = new Generator();
            g.Columns.Add(c1);
            g.Columns.Add(c2);
            g.Tables.Add(t1);

            Assert.AreEqual(
                "SELECT table_1.name, SUM(table_1.price) FROM table_1",
                g.ToString()
            );
        }

        [TestMethod]
        public void Test5()
        {
            var orders = new Table("orders");
            var products = new Table("products");
            var number = new Column("number", orders, null);
            var cost = new Column("price", products, "SUM");
            var k1 = new KeyPair(products, "id", orders, "order_id");

            var g = new Generator();
            g.Columns.Add(number);
            g.Columns.Add(cost);
            g.Tables.Add(orders);
            g.Tables.Add(products);
            g.KeyPairs.Add(k1);

            Assert.AreEqual(
                "SELECT orders.number, SUM(products.price) FROM products JOIN orders ON products.order_id = orders.id GROUP BY orders.number",
                g.ToString()
            );
        }
    }
}
