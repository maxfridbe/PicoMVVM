using PicoMVVM.PubSub;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PicoMVVM.Tests
{

	/// <summary>
	///This is a test class for MessageCenterTest and is intended
	///to contain all MessageCenterTest Unit Tests
	///</summary>
	[TestClass()]
	public class MessageCenterTest
	{

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
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion

		class message
		{
			public string txt { get; set; }
		}
		class message2
		{
			public string txt { get; set; }
		}

		/// <summary>
		///A test for MessageCenter Constructor
		///</summary>
		[TestMethod()]
		public void MessageCenterConstructorTest()
		{
			MessageCenter target = new MessageCenter();
		}

		/// <summary>
		///A test for ClearSubscriptions
		///</summary>
		[TestMethod()]
		public void ClearSubscriptionsTest()
		{
			MessageCenter target = new MessageCenter();
			target.ClearSubscriptions();
		}

		[TestMethod()]
		public void PublishTest()
		{
			MessageCenter target = new MessageCenter();
			var msg = new message();
			target.Publish(msg);
		}

		[TestMethod()]
		public void PublishTopicalTest()
		{
			MessageCenter target = new MessageCenter();
			var msg = new message();
			target.Publish(msg, "topic");
		}

		[TestMethod()]
		public void SubscribeTest()
		{
			MessageCenter target = new MessageCenter();
			target.Subscribe<message>(msg => { });
		}
		[TestMethod()]
		public void SubscribeTopicalTest()
		{
			MessageCenter target = new MessageCenter();
			target.Subscribe<message>(msg => { }, "topic");
		}

		[TestMethod]
		public void PublishToAltSubscribedTest()
		{
			//arrange
			MessageCenter target = new MessageCenter();
			string res = null;
			target.Subscribe<string>(a => { res = a; });

			var msg = new message() { txt = "hi" };

			//act
			target.Publish(msg);

			//assert
			Assert.AreNotEqual(res, "hi");
		}

		[TestMethod()]
		public void PublishToSubscribedTest()
		{
			//arrange
			MessageCenter target = new MessageCenter();
			string res = null;
			target.Subscribe<message>(a => { res = a.txt; });

			var msg = new message() { txt = "hi" };

			//act
			target.Publish(msg);

			//assert
			Assert.AreEqual(res, "hi");
		}

		[TestMethod()]
		public void PublishToSubscribedTopicalTest()
		{
			//arrange
			MessageCenter target = new MessageCenter();
			string res = null;
			target.Subscribe<message>(a => { res = a.txt; }, "topic");

			var msg = new message() { txt = "hi" };

			//act
			target.Publish(msg);

			//assert
			Assert.AreNotEqual(res, "hi");
		}

		[TestMethod()]
		public void PublishTopicalToSubscribedTest()
		{
			//arrange
			MessageCenter target = new MessageCenter();
			string res = null;
			target.Subscribe<message>(a => { res = a.txt; });

			var msg = new message() { txt = "hi" };

			//act
			target.Publish(msg, "topic");

			//assert
			Assert.AreEqual(res, "hi");
		}

		[TestMethod()]
		public void PublishTopicalToSubscribedTopicalTest()
		{
			//arrange
			MessageCenter target = new MessageCenter();
			string res = null;
			target.Subscribe<message>(a => { res = a.txt; }, "topic");

			var msg = new message { txt = "hi" };

			//act
			target.Publish(msg, "topic");

			//assert
			Assert.AreEqual(res, "hi");
		}

		[TestMethod()]
		public void PublishTopicalToSubscribedAltTopicalTest()
		{
			//arrange
			MessageCenter target = new MessageCenter();
			string res = null;
			target.Subscribe<message>(a => { res = a.txt; }, "topic");

			var msg = new message { txt = "hi" };

			//act
			target.Publish(msg, "topica");

			//assert
			Assert.AreNotEqual(res, "hi");
		}





	}
}
