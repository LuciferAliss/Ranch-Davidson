using System;
using System.Net;
using System.Net.Mail;

class EmailManager
{
    public static void SendConfirmationMessages(string toEmail, string SubjectMessage, string Message)
	{
		try
		{
			SmtpClient smtpClient = new SmtpClient("smtp.mail.ru")
			{
				Port = 587,
				Credentials = new NetworkCredential("alissentertainment@mail.ru", "ate5yzgzyP4bMB7KfJ0K"),
				EnableSsl = true,
			};

			MailMessage mail = new MailMessage
			{
				From = new MailAddress("alissentertainment@mail.ru", "Admin"),
				Subject = SubjectMessage,
				Body = Message,
				IsBodyHtml = false 
			};

			mail.To.Add(toEmail);
			smtpClient.Send(mail);
			
		}
		catch (Exception)
		{
			return;
		}
	}

    public static bool IsDomainValid(string email)
	{
		try
		{
			var domain = email.Split('@')[1];
			var hostEntry = Dns.GetHostEntry(domain);
			return hostEntry.AddressList.Length > 0;
		}
		catch
		{
			return false;
		}
	}
}