using GunShopBackPart.Models;
using System.Runtime.InteropServices.Swift;

namespace GunShopBackPart.Tool.PageCreation
{
    public static class SortProductBySomething
    {
        public static IQueryable<BaseProduct> Sort(IQueryable<BaseProduct> query, SortBy? sortBy)
        {
           switch(sortBy)
           {
               case SortBy.MinPrice:
                   return query.OrderBy(e => e.Price);
               case SortBy.MaxPrice:
                   return query.OrderByDescending(e => e.Price);
               default:
                   return query;
           }
        }

    }
}
