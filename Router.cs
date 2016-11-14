using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace ConsistentHash
{
	public class Router
	{
		private SortedList<long, VirtualNode> _ring = new SortedList<long, VirtualNode>();
		private Md5Hash _hashFunc = new Md5Hash();

		public Router(IEnumerable<PhysicalNode> pNodes, int vNodeCount)
		{
			foreach (var pNode in pNodes)
			{
				AddNode(pNode, vNodeCount);
			}
		}

		public void AddNode(PhysicalNode pNode, int vNodeCount)
		{
			int existingReplicas = GetReplicas(pNode.ToString());
			for (int i = 0; i < vNodeCount; i++)
			{
				var vNode = new VirtualNode(pNode, existingReplicas + i);
				_ring.Add(_hashFunc.Hash(vNode.ToString()), vNode);
			}
		}

		public void RemoveNode(PhysicalNode pNode)
		{
			var deleteKeys = _ring.Keys.Where(vNodeKey => _ring[vNodeKey].Matches(pNode.ToString())).ToList();
			foreach (var key in deleteKeys)
			{
				_ring.Remove(key);
			}
		}

		public PhysicalNode GetNode(string key)
		{
			if (!_ring.Any())
			{
				return null;
			}
			long hashKey = _hashFunc.Hash(key);

			for (int i = 0; i < _ring.Count; i++)
			{
				if (hashKey < _ring.ElementAt(i).Key)
				{
					return _ring.ElementAt(i).Value.Parent;
				}
			}
			return _ring.First().Value.Parent;
		}

		public int GetReplicas(string nodeName)
		{
			return _ring.Values.Count(node => node.Matches(nodeName));
		}

		public int GetVirtualNodeCount()
		{
			return _ring.Count;
		}
	}
}
