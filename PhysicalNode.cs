using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsistentHash
{
	public class PhysicalNode
	{
		public string Domain { get; set; }
		public string Ip { get; set; }
		public int Port { get; set; }

		public PhysicalNode(string domain, string ip, int port)
		{
			Domain = domain;
			Ip = ip;
			Port = port;
		}

		public override string ToString()
		{
			return string.Format("{0}:{1}:{2}", Domain, Ip, Port);
		}
	}
}
