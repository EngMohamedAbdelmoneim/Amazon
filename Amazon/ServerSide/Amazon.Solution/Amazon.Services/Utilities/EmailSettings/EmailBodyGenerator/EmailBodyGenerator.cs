using Amazon.Core.Entities.Identity;

namespace Amazon.Services.Utilities.EmailSettings.EmailBodyGenerator
{
	public static class EmailBodyGenerator
	{
		public static string ConfirmationEmailBodyGenerator(AppUser user, string confirmationLink)
		{
			return $@"
            <html>
                <body style=""font-family: Arial, sans-serif; background-color: #f2f2f2; margin: 0; padding: 0;"">
                    <table align=""center"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px; background-color: #ffffff; margin: 20px auto; border: 1px solid #e0e0e0;"">
                        <tr>
                        <td style=""background-color: #384b64; padding: 20px 0; text-align: center;"">
                            <img src=""https://i.postimg.cc/cL4cFd1x/Amazon-2024-svg.png"" alt=""Amazon Logo"" style=""width: 150px;   padding-top: 20px;"">

                        </td>
                        </tr>
                        <tr>
                            <td style=""padding: 30px 40px; color: #333333;"">
                                <h1 style=""font-size: 24px; margin-bottom: 20px;"">Confirm your email address</h1>
                                <p style=""font-size: 16px; line-height: 1.5; margin-bottom: 30px;"">
                                    Hello {user.DisplayName},
                                    <br><br>
                                    Thank you for creating an account with us. Please confirm your email address by clicking the button below:
                                </p>
                                <div style=""text-align: center; margin-bottom: 40px;"">
                                    <a href=""{confirmationLink}"" style=""background-color: #ff9900; color: #ffffff; padding: 10px 20px; text-decoration: none; border-radius: 4px; font-size: 16px;"">Confirm Your Email</a>
                                </div>
                                <p style=""font-size: 14px; line-height: 1.5; color: #666666;"">
                                    If the button above doesn't work, copy and paste the following link into your browser:
                                    <br>
                                    <a href=""{confirmationLink}"" style=""color: #0066c0;"">{confirmationLink}</a>
                                </p>
                                <p style=""font-size: 14px; line-height: 1.5; color: #666666;"">
                                    This link is Active for Only One Hour.
                                </p>
                                <p style=""font-size: 14px; line-height: 1.5; color: #666666;"">
                                    If you did not create this account, you can ignore this email.
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td style=""background-color: #f3f3f3; padding: 20px; text-align: center; font-size: 12px; color: #999999;"">
                                <p>
                                    &copy; 2024 Amazon. All rights reserved. 
                                    <br>
                                    Amazon, 410 Terry Avenue North, Seattle, WA 98109-5210, USA.
                                </p>
                            </td>
                        </tr>
                    </table>
                 </body>
            </html>";
		}

		public static string ResetPasswordEmailBodyGenerator(AppUser user, string resetLink)
		{
			return $@"
            <html>
            <body style=""font-family: Arial, sans-serif; background-color: #f2f2f2; margin: 0; padding: 0;"">
                <table align=""center"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px; background-color: #ffffff; margin: 20px auto; border: 1px solid #e0e0e0;"">
                    <tr>
                        <td style=""background-color: #384b64; padding: 20px 0; text-align: center;"">
                            <img src=""https://i.ibb.co/Pry1Hnz/Amazon-2024-svg.png"" alt=""Amazon Logo"" style=""width: 150px;"">
                        </td>
                    </tr>
                    <tr>
                        <td style=""padding: 30px 40px; color: #333333;"">
                            <h1 style=""font-size: 24px; margin-bottom: 20px;"">Reset your password</h1>
                            <p style=""font-size: 16px; line-height: 1.5; margin-bottom: 30px;"">
                                Hello {user.DisplayName},
                                <br><br>
                                We received a request to reset the password for your Amazon account. You can reset your password by clicking the button below:
                            </p>
                            <div style=""text-align: center; margin-bottom: 40px;"">
                                <a href=""{resetLink}"" style=""background-color: #ff9900; color: #ffffff; padding: 10px 20px; text-decoration: none; border-radius: 4px; font-size: 16px;"">Reset Password</a>
                            </div>
                            <p style=""font-size: 14px; line-height: 1.5; color: #666666;"">
                                If the button above doesn't work, copy and paste the following link into your browser:
                                <br>
                                <a href=""{resetLink}"" style=""color: #0066c0;"">{resetLink}</a>
                            </p>
                            <p style=""font-size: 14px; line-height: 1.5; color: #666666;"">
                                If you did not request this password reset, you can ignore this email. Your password will remain unchanged.
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style=""background-color: #f3f3f3; padding: 20px; text-align: center; font-size: 12px; color: #999999;"">
                            <p>
                                &copy; 2024 Amazon. All rights reserved.
                                <br>
                                Amazon, 410 Terry Avenue North, Seattle, WA 98109-5210, USA.
                            </p>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";
		}

	}
}
