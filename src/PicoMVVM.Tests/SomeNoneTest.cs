using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PicoMVVM.PubSub;

namespace PicoMVVM.Tests
{
	/// <summary>
	/// Summary description for SomeNoneTest
	/// </summary>
	[TestClass]
	public class SomeNoneTest
	{
		public SomeNoneTest()
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

		class Uneq
		{

		}
		class Equ : IEquatable<Equ>
		{
			#region Implementation of IEquatable<Equ>

			public bool Equals(Equ other)
			{
				return false;
			}

			#endregion
		}

		[TestMethod]
		public void TestCreation()
		{
			var some = new Some<string>("hi");
			var some2 = new Some<Uneq>(new Uneq());
			var some3 = new Some<Equ>(new Equ());

			var none = new None<string>();
			var none2 = new None<Uneq>();
			var none3 = new None<Equ>();
		}

		[TestMethod]
		public void TestSome()
		{
			Option<string> some = new Some<string>("hi");
			Assert.IsTrue(some.IsSome());
			Assert.IsFalse(some.IsNone());
			Assert.IsNotNull((some as Some<string>).Value);
			Assert.IsTrue((some as Some<string>).Value == "hi");

			Option<Uneq> some2 = new Some<Uneq>(new Uneq());
			Assert.IsTrue(some2.IsSome());
			Assert.IsFalse(some2.IsNone());
			Assert.IsNotNull((some2 as Some<Uneq>).Value);
			Assert.IsFalse((some2 as Some<Uneq>).Value == new Uneq());

			Option<Equ> some3 = new Some<Equ>(new Equ());
			Assert.IsTrue(some3.IsSome());
			Assert.IsFalse(some3.IsNone());
			Assert.IsNotNull((some3 as Some<Equ>).Value);
			Assert.IsFalse((some3 as Some<Equ>).Value == new Equ());

			Option<string> a = new Some<string>("hi1");
			Option<string> aa = new Some<string>("hi1");
			Option<string> na = new Some<string>("hi2");
			Assert.AreNotEqual(a, na);
			Assert.AreEqual(a, a);
			Assert.AreEqual(a, aa);

			var ueq = new Uneq();
			Assert.AreEqual(ueq, ueq);
			Option<Uneq> b = new Some<Uneq>(ueq);
			Option<Uneq> bb = new Some<Uneq>(ueq);
			Option<Uneq> nb = new Some<Uneq>(new Uneq());
			Assert.AreNotEqual(b, nb);
			Assert.AreEqual(b, b);
			Assert.AreEqual(b, bb);


			var eq = new Equ();
			//Assert.AreNotEqual(eq,eq);
			Assert.IsFalse(eq.Equals(eq));
			Option<Equ> c = new Some<Equ>(eq);
			Option<Equ> cc = new Some<Equ>(eq);
			Option<Equ> nc = new Some<Equ>(new Equ());
			Assert.AreNotEqual(c, nc);
			Assert.AreEqual(c, c);
			Assert.AreEqual(c, cc);
		}

		[TestMethod]
		public void TestNone()
		{
			Option<string> none = new None<string>();
			Assert.IsTrue(none.IsNone());
			Assert.IsFalse(none.IsSome());
			Option<Uneq> none2 = new None<Uneq>();
			Assert.IsTrue(none2.IsNone());
			Assert.IsFalse(none2.IsSome());
			Option<Equ> none3 = new None<Equ>();
			Assert.IsTrue(none3.IsNone());
			Assert.IsFalse(none3.IsSome());
		}

		[TestMethod]
		public void TestNoneSome()
		{
			Option<string> some = new Some<string>("hi");
			Option<Uneq> some2 = new Some<Uneq>(new Uneq());
			Option<Equ> some3 = new Some<Equ>(new Equ());

			Option<string> none = new None<string>();
			Option<Uneq> none2 = new None<Uneq>();
			Option<Equ> none3 = new None<Equ>();

			Assert.AreNotEqual(some, none);
			Assert.AreNotEqual(some2, none2);
			Assert.AreNotEqual(some3, none3);


		}
	}
}
