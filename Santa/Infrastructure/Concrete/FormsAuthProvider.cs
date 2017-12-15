using Santa.Classes;
using Santa.Infrastructure.Abstract;
using Santa.Models;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Santa.Infrastructure.Concrete
{
    public class FormsAuthProvider:IAuthProvider
    {
        private IDataBase db;

        public FormsAuthProvider(IDataBase dbParam)
        {
            db = dbParam;
        }

        public User Authenticate(UserLogin user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return null;
            }
            user.Password = Encrypt(user.Password);


            User loggedUser = db.GetUser(user.Email, user.Password);
            if (loggedUser != null)
            {
                FormsAuthentication.SetAuthCookie(loggedUser.Email, false);
            }
            return loggedUser;
        }

        private string Encrypt(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] resultHash;
            SHA512 shaM = new SHA512Managed();
            resultHash = shaM.ComputeHash(data);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < resultHash.Length; i++)
            {
                result.Append(resultHash[i].ToString("X2").ToLower());
            }
            return result.ToString();
        }

    }
}