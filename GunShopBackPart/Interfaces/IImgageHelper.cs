using GunShopBackPart.DTOs;

namespace GunShopBackPart.Interfaces
{
    public interface IImgageHelper
    {
        public string CreateImageUrl(IFormFile imageFile, ProductType type);
        public Task SaveImageAsync(IFormFile? imageFile, string imageUrl);
    }
}
