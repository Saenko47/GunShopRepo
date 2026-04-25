using Azure.Core;
using GunShopBackPart.DTOs;
using GunShopBackPart.Interfaces;

namespace GunShopBackPart.Tool.CreateProduct
{
    public class PicHelper: IImgageHelper
    {
        public static readonly string IMG_DEF_FOLDER_URL = "img/def/";
        public static readonly string IMG_UNI_FOLDER_URL = "img/uni/";
        
        public string CreateImageUrl(IFormFile? imageFile, ProductType type) 
        { 
        
            return imageFile != null ? $"{PicHelper.IMG_UNI_FOLDER_URL}{Guid.NewGuid()}.jpg"
           : $"{PicHelper.IMG_DEF_FOLDER_URL}{type}.jpg";
        }
        public async Task SaveImageAsync(IFormFile? image, string path) 
        {
            if (image == null || image.Length == 0)
                throw new Exception("Файл пустой");

            var folder = Path.GetDirectoryName(path);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            using var stream = new FileStream(path, FileMode.Create);
            await image.CopyToAsync(stream);
        }

    }

}
