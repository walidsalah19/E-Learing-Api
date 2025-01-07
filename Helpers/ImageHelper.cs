namespace E_Learning.Helpers
{
    public class ImageHelper
    {
        public static async Task<string> ProcessUploadedImage(IFormFile ImageURL, IWebHostEnvironment webHostEnvironment)
        {
            string uniqueFileName = null;
            if (ImageURL != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageURL.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageURL.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        public static async Task<string> ProcessDeleteImage(string ImageURL, IWebHostEnvironment webHostEnvironment)
        {
            if (ImageURL != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath,"Images");
                string filePath = Path.Combine(uploadsFolder, ImageURL);
                System.IO.File.Delete(filePath);
                return "Success";
            }

            return "Failed";
        }
    }
}
