using System.Collections.ObjectModel;
using Generator.PlugIn.SqlGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SqlGenerator.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {

        private Collection<Table> _tables;
        private Collection<Column> _columns;
        private Collection<KeyPair> _keyPairs;

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
        
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            _tables = new Collection<Table>();
            _columns = new Collection<Column>();
            _keyPairs = new Collection<KeyPair>();
        }
        
        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            _tables = null;
            _columns = null;
            _keyPairs = null;
        }
        
        #endregion

        [TestMethod]
        public void Test1()
        {
            var t1 = new Table("table_1");
            var c1 = new Column("name", t1, null);
            var c2 = new Column("city", t1, null);

            var g = new Generator.PlugIn.BaseSqlGenerator.SqlGeneratorPlugin();
            _columns.Add(c1);
            _columns.Add(c2);
            _tables.Add(t1);

            Assert.AreEqual(
                "SELECT table_1.name, table_1.city FROM table_1",
                g.GetSql(_tables, _columns, _keyPairs)
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

            var g = new Generator.PlugIn.BaseSqlGenerator.SqlGeneratorPlugin();
            _columns.Add(c1);
            _columns.Add(c2);
            _columns.Add(c3);
            _tables.Add(t1);
            _tables.Add(t2);

            Assert.AreEqual(
                "SELECT table_1.name, table_1.city, table_2.family FROM table_1, table_2",
                g.GetSql(_tables, _columns, _keyPairs)
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

            var g = new Generator.PlugIn.BaseSqlGenerator.SqlGeneratorPlugin();
            _columns.Add(c1);
            _columns.Add(c2);
            _columns.Add(c3);
            _tables.Add(t1);
            _tables.Add(t2);
            _keyPairs.Add(k1);

            Assert.AreEqual(
                "SELECT table_1.name, table_1.city, table_2.family_name FROM table_2 JOIN table_1 ON table_2.family_id = table_1.id",
                g.GetSql(_tables, _columns, _keyPairs)
            );
        }

        [TestMethod]
        public void Test4()
        {
            var t1 = new Table("table_1");
            var c1 = new Column("name", t1, null);
            var c2 = new Column("price", t1, "SUM");

            var g = new Generator.PlugIn.BaseSqlGenerator.SqlGeneratorPlugin();
            _columns.Add(c1);
            _columns.Add(c2);
            _tables.Add(t1);

            Assert.AreEqual(
                "SELECT table_1.name, SUM(table_1.price) FROM table_1",
                g.GetSql(_tables, _columns, _keyPairs)
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

            var g = new Generator.PlugIn.BaseSqlGenerator.SqlGeneratorPlugin();
            _columns.Add(number);
            _columns.Add(cost);
            _tables.Add(orders);
            _tables.Add(products);
            _keyPairs.Add(k1);

            Assert.AreEqual(
                "SELECT orders.number, SUM(products.price) FROM products JOIN orders ON products.order_id = orders.id GROUP BY orders.number",
                g.GetSql(_tables, _columns, _keyPairs)
            );
        }
    }
}
