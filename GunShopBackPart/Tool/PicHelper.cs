namespace GunShopBackPart.Tool
{
    public static class PicHelper
    {
        public static readonly string IMG_DEF_FOLDER_URL = "img/def/";
        public static readonly string IMG_UNI_FOLDER_URL = "img/uni/";

        public static async Task SavePhotoToFolder(string path, IFormFile image) 
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
