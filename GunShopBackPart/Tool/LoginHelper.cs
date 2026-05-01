using GunShopBackPart.Interfaces;
using GunShopBackPart.RequestsObjects.LoginRequest;
using GunShopBackPart.Tool.JVT;
using Microsoft.EntityFrameworkCore;

namespace GunShopBackPart.Tool
{
    public class LoginHelper: ILogin
    {
        private readonly IJVTProvider jvtProvider;
        private readonly ICrypto crypto;
        public LoginHelper(IJVTProvider jvtProvider, ICrypto crypto)
        {
            this.jvtProvider = jvtProvider;
            this.crypto = crypto;
        }
        public async Task<string> LoginAsync<T>(IQueryable<T> set, CustomerLoginRequest req, Role role) 
            where T : class,IAuthEntity
        {
            var customer = await set
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Login == req.Login);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            var decryptedPassword = crypto.Decrypt(customer.Password);
            if (decryptedPassword != req.Password)
            {
                throw new Exception("Invalid password");
            }


            return jvtProvider.GenJVT(customer.Id, customer.Name, role);


        }
    }
}
