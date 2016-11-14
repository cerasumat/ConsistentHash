using System;

namespace ConsistentHash
{
	public class VirtualNode
	{
		public int ReplicaNumber { get; set; }
		public PhysicalNode Parent { get; private set; }

		public VirtualNode(PhysicalNode parent, int replicaNumber)
		{
			Parent = parent;
			ReplicaNumber = replicaNumber;
		}

		public bool Matches(string host)
		{
			return Parent.ToString().Equals(host, StringComparison.OrdinalIgnoreCase);
		}

		public override string ToString()
		{
			return string.Format("{0}:{1}", Parent.ToString().ToLower(), ReplicaNumber);
		}
	}
}
