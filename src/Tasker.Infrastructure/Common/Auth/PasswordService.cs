using System.Security.Cryptography;
using System.Text;
using Tasker.Application.Common.Interfaces.Auth;

namespace Tasker.Infrastructure.Common.Auth;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        Byte[] originalBytes;
        Byte[] encodedBytes;
        MD5 md5;
        //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)   
        md5 = new MD5CryptoServiceProvider();
        originalBytes = ASCIIEncoding.Default.GetBytes(password);
        encodedBytes = md5.ComputeHash(originalBytes);
        //Convert encoded bytes back to a 'readable' string   
        return BitConverter.ToString(encodedBytes);
    }
}