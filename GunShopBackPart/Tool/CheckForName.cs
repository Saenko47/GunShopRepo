using GunShopBackPart.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.Swift;

namespace GunShopBackPart.Tool
{
    public static class CheckForName
    {
        public static async Task<bool> IsAlreadyExist(this DbSet<BaseProduct> set, string name )
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;
            return await set.AnyAsync(e => e.Name == name.Trim());
        }
    }
}
