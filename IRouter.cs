using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsistentHash
{
	public interface IRouter
	{
		void AddNode(PhysicalNode pNode, int vNodeCount);
		void RemoveNode(PhysicalNode pNode);
		PhysicalNode GetNode(string key);
		int GetReplicas(string nodeName);
		int GetVirtualNodeCount();
	}
}
