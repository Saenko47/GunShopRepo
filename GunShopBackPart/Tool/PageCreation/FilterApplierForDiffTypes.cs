using GunShopBackPart.Models;

namespace GunShopBackPart.Tool.PageCreation
{
    public static class FilterApplierForDiffTypes
    {
        public static IQueryable<Gun> GetGun(IQueryable<BaseProduct> baseQuery, FilterGun filter)
        {
            var query = baseQuery.OfType<Gun>();
            if (filter.Caliber.HasValue)
            {
                query = query.Where(e => e.Caliber == filter.Caliber);
            }
            if (filter.GunType.HasValue)
            {
                query = query.Where(e => e.GunType == filter.GunType);
            }
            return query;
        }

        public static IQueryable<Ammo> GetAmmo(IQueryable<BaseProduct> baseQuery, FilterAmmo filter)
        {
            var query = baseQuery.OfType<Ammo>();
            if (filter.Caliber.HasValue)
            {
                query = query.Where(e => e.Caliber == filter.Caliber);
            }
            return query;
        }

        public static IQueryable<Accessorie> GetAccessory(IQueryable<BaseProduct> baseQuery, FilterAccesorie filter)
        {
            var query = baseQuery.OfType<Accessorie>();
            if (filter.AccessoryType.HasValue)
            {
                query = query.Where(e => e.Type == filter.AccessoryType);
            }
            return query;

        }
    }
}
