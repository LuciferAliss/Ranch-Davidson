using System.Security.Cryptography;
using System.Text;

class SHA512Hash
{
    public static string ToSHA512(string inputMessage)
	{
		string outputMessage = "";
		using (SHA512 sha512 = SHA512.Create())
		{
			byte[] toBytes = Encoding.ASCII.GetBytes(inputMessage);
			byte[] hash = sha512.ComputeHash(toBytes);

			foreach (byte b in hash)
			{
				outputMessage += b.ToString("x2");
			}

			return outputMessage;
		}
	}
}