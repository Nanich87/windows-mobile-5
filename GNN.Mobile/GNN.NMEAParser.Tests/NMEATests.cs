namespace GNN.NMEAParser.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using GNN.NMEAParser.Factories;
    using GNN.NMEAParser.Messages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class NMEATests
    {
        private MessageFactory factory;

        public NMEATests()
        {
            //
            // TODO: Add constructor logic here
            //

            factory = new MessageFactory();
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
        public void TestParseRMC()
        {
            var message = "$GPRMC,203522.00,A,5109.0262308,N,11401.8407342,W,0.004,133.4,130522,0.0,E,D*2B";
            var rmc = factory.CreateMessage(message);

            Assert.IsNotNull(rmc);
        }

        [TestMethod]
        public void TestParseLatitude()
        {
            var latitude = "5107.0017737";

            var delta = 0.0000000001;
            var expected = 51.1166962283;
            var parsed = RMC.ParseCoordinate(latitude, 2);

            Assert.AreEqual(expected, parsed, delta);
        }

        [TestMethod]
        public void TestParseLongitude()
        {
            var longitude = "11402.3291611";

            var delta = 0.0000000001;
            var expected = 114.0388193516;
            var parsed = RMC.ParseCoordinate(longitude, 3);

            Assert.AreEqual(expected, parsed, delta);
        }
    }
}
