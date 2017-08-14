using GameStore.Common.Abstract;
using System.Text;

namespace GameStore.Common.Concrete
{
	public class Md5Hasher : IHasher<string>
	{
		public string GenerateHash(string input)
		{
			using (var md5 = System.Security.Cryptography.MD5.Create())
			{
				var inputBytes = Encoding.ASCII.GetBytes(input);
				var hashBytes = md5.ComputeHash(inputBytes);

				var sb = new StringBuilder();

				foreach (var b in hashBytes)
				{
					sb.Append(b.ToString("X2"));
				}

				return sb.ToString();
			}
		}
	}
}
