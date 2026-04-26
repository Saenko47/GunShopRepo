using GunShopBackPart.Interfaces;
using System.Security.Cryptography;
using System.Text;
namespace GunShopBackPart.Tool.Crypto
{
    public class Crypto: ICrypto
    {
        private const int KeySize = 32; // 256 bit
        private const int IvSize = 16;  // 128 bit
        private const int Iterations = 100000;
        private const string password = "itsasecret";

        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();

            byte[] salt = RandomNumberGenerator.GetBytes(16);
            var key = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);

            aes.Key = key.GetBytes(KeySize);
            aes.IV = key.GetBytes(IvSize);

            using var ms = new MemoryStream();
            ms.Write(salt, 0, salt.Length); // сохраняем salt

            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                byte[] data = Encoding.UTF8.GetBytes(plainText);
                cs.Write(data, 0, data.Length);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            byte[] fullData = Convert.FromBase64String(cipherText);

            byte[] salt = new byte[16];
            Array.Copy(fullData, 0, salt, 0, salt.Length);

            using var aes = Aes.Create();
            var key = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);

            aes.Key = key.GetBytes(KeySize);
            aes.IV = key.GetBytes(IvSize);

            using var ms = new MemoryStream(fullData, salt.Length, fullData.Length - salt.Length);
            using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using var reader = new StreamReader(cs);

            return reader.ReadToEnd();
        }
    }
}
