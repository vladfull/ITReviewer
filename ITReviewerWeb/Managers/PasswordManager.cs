using System.Security.Cryptography;
using System.Text;

namespace ITReviewerWeb.Managers
{
	public static class PasswordManager
	{
		public static string HashPassword(string password)
		{
			using(var sha256 = SHA256.Create()) 
			{
				var hashbytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
				var newPass = BitConverter.ToString(hashbytes).ToLower().Replace("-", "");
				return newPass;
			}
	
		}
	}
}
