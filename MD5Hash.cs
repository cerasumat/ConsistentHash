using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsistentHash
{
	public class Md5Hash
	{
		private readonly System.Security.Cryptography.MD5 _md5;

		 public Md5Hash() 
		{
			_md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		}

		public long Hash(string key)
		{
			byte[] results = _md5.ComputeHash(System.Text.Encoding.Default.GetBytes(key));
			long hash = 0;
			for (int i = 0; i < 4; i++)
			{
				hash <<= 8;
				hash |= ((int) results[i]) & 0xFF;
			}
			return hash;
		}
	}
}
