namespace BEforREACT.Services
{
    public class FileStorageServices
    {
        private readonly string _userContentFolder;
        private const string USER_CONTENT_FOLDER_NAME = "assets";
        public FileStorageServices(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
        }

        public async Task<string> UploadImageFileAsync(IFormFile file, string subFolder)
        {
            ValidateFile(file);
            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName).ToLowerInvariant()}";
            var directoryPath = PrepareDirectory(subFolder);
            var filePath = Path.Combine(directoryPath, newFileName);

            await SaveFileAsync(file.OpenReadStream(), filePath);

            return $"/{subFolder}/{newFileName}";
        }

        private void ValidateFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is null or empty.", nameof(file));
            }
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new InvalidOperationException("Invalid file type. Only allowed image files are accepted.");
            }
            long maxFileSizeInBytes = 4 * 1024 * 1024;
            if (file.Length > maxFileSizeInBytes)
            {
                throw new InvalidOperationException("File size exceeds the maximum allowed limit of 4 MB.");
            }
        }

        private string PrepareDirectory(string subFolder)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", subFolder);
            Directory.CreateDirectory(directoryPath);
            return directoryPath;
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

        private async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);

            var directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}
