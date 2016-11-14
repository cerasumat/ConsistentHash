using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ConsistentHash;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsistentHashTest
{
	[TestClass]
	public class ConsistentHashTest
	{
		[TestMethod]
		public void TestMd5()
		{
			Md5Hash md5 = new Md5Hash();
			long hash = md5.Hash("1");
			long tmp = hash;
			hash = md5.Hash("2");
			Assert.IsTrue(hash != tmp);
			tmp = hash;
			hash = md5.Hash("3");
			Assert.IsTrue(hash != tmp);
			tmp = hash;
			hash = md5.Hash("4");
			Assert.IsTrue(hash != tmp);
			tmp = hash;
			hash = md5.Hash("5");
			Assert.IsTrue(hash != tmp);
			tmp = hash;
			hash = md5.Hash("6");
			Assert.IsTrue(hash != tmp);
			tmp = hash;
			hash = md5.Hash("7");
			Assert.IsTrue(hash != tmp);
			tmp = hash;
		}

		[TestMethod]
		public void TestConHash()
		{
			PhysicalNode node1 = new PhysicalNode("SituationAnalysis", "10.202.42.85", 80);
			PhysicalNode node2 = new PhysicalNode("SituationAnalysis", "10.202.42.86", 80);
			PhysicalNode node3 = new PhysicalNode("SituationAnalysis", "10.202.42.87", 80);
			PhysicalNode node4 = new PhysicalNode("SituationAnalysis", "10.202.42.88", 80);
			PhysicalNode node5 = new PhysicalNode("SituationAnalysis", "10.202.42.89", 80);
			var pNodes = new List<PhysicalNode>();
			pNodes.Add(node1);
			pNodes.Add(node2);
			pNodes.Add(node3);
			pNodes.Add(node4);
			pNodes.Add(node5);
			var router = new Router(pNodes, 3);

			int count85 = 0;
			int count86 = 0;
			int count87 = 0;
			int count88 = 0;
			int count89 = 0;

			Stopwatch sw = new Stopwatch();
			sw.Start();
			for (int i = 1; i < 100000; i++)
			{
				var dataKey = string.Format("SA-20161115-Issue232-{0}", i.ToString().PadLeft(5, '0'));
				var node = router.GetNode(dataKey);
				switch (node.Ip)
				{
					case "10.202.42.85":
						Interlocked.Increment(ref count85);
						break;
					case "10.202.42.86":
						Interlocked.Increment(ref count86);
						break;
					case "10.202.42.87":
						Interlocked.Increment(ref count87);
						break;
					case "10.202.42.88":
						Interlocked.Increment(ref count88);
						break;
					case "10.202.42.89":
						Interlocked.Increment(ref count89);
						break;
				}
			}
			sw.Stop();
			TimeSpan elapse = sw.Elapsed;
			int total = count85 + count86 + count87 + count88 + count89;
			Assert.IsTrue(total == 99999);
		}

		[TestMethod]
		public void TestAddNode()
		{
			var pNodes = new List<PhysicalNode>();
			PhysicalNode node1 = new PhysicalNode("SituationAnalysis", "10.202.42.85", 80);
			PhysicalNode node2 = new PhysicalNode("SituationAnalysis", "10.202.42.86", 80);
			pNodes.Add(node1);
			pNodes.Add(node2);
			var router = new Router(pNodes, 3);
			Assert.IsTrue(router.GetVirtualNodeCount()==6);
			router.AddNode(new PhysicalNode("SituationAnalysis", "10.202.42.87", 80),3);
			Assert.IsTrue(router.GetVirtualNodeCount() == 9);
		}

		[TestMethod]
		public void TestRemoveNode()
		{
			var pNodes = new List<PhysicalNode>();
			PhysicalNode node1 = new PhysicalNode("SituationAnalysis", "10.202.42.85", 80);
			PhysicalNode node2 = new PhysicalNode("SituationAnalysis", "10.202.42.86", 80);
			PhysicalNode node3 = new PhysicalNode("SituationAnalysis", "10.202.42.87", 80);
			pNodes.Add(node1);
			pNodes.Add(node2);
			pNodes.Add(node3);
			var router = new Router(pNodes, 3);
			Assert.IsTrue(router.GetVirtualNodeCount() == 9);
			router.RemoveNode(node3);
			Assert.IsTrue(router.GetVirtualNodeCount() == 6);
		}
	}
}
