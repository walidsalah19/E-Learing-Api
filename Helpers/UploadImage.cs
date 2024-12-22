namespace E_Learning.Helpers
{
    public class UploadImage
    {
        public static async Task<string> ProcessUploadedFile(IFormFile ImageURL, IWebHostEnvironment webHostEnvironment)
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
    }
}
