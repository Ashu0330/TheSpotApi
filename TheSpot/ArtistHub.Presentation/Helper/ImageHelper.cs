
using ArtistHub.Presentation.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;


public class ImageHelper
{

    private IWebHostEnvironment? environment;
    public ImageHelper(IWebHostEnvironment? environment)
    {
        this.environment = environment;
    }

    public void DeleteImage(string relativePath)
    {
        if (string.IsNullOrEmpty(relativePath))
            return;

        string fullPath = Path.Combine(Directory.GetCurrentDirectory(),
                                       environment?.WebRootPath,
                                       relativePath.Replace("/", "\\"));

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    public string GetPath(string folder, string file = "")
    {
        return Path.Combine(Directory.GetCurrentDirectory(), this.environment?.WebRootPath + Path.DirectorySeparatorChar +
                   folder + Path.DirectorySeparatorChar + file);
    }

    private string UploadImageWithQuality(string folderName, IFormFile file, int quality)
    {
        string directory = Path.Combine(
            Directory.GetCurrentDirectory(),
            environment.WebRootPath,
            folderName);

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        string fileName = $"{Guid.NewGuid()}.webp";
        string uploadPath = Path.Combine(directory, fileName);

        using (var image = Image.Load(file.OpenReadStream()))
        {
            var encoder = new SixLabors.ImageSharp.Formats.Webp.WebpEncoder
            {
                Quality = quality
            };

            image.Save(uploadPath, encoder);
        }

        return Path.Combine(folderName, fileName).Replace("\\", "/");
    }

    public string UploadHighQualityImage(string folderName, IFormFile file)
    {
        return UploadImageWithQuality(folderName, file, ImageQuality.High);
    }

    public string UploadMidQualityImage(string folderName, IFormFile file)
    {
        return UploadImageWithQuality(folderName, file, ImageQuality.Medium);
    }

    public string UploadLowQualityImage(string folderName, IFormFile? file)
    {
        return UploadImageWithQuality(folderName, file, ImageQuality.Low);
    }

    public string UploadProfileImage(string folderName, IFormFile file)
    {
        string directory = Path.Combine(
            Directory.GetCurrentDirectory(),
            environment.WebRootPath,
            folderName);

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        string fileName = $"{Guid.NewGuid()}.webp";
        string uploadPath = Path.Combine(directory, fileName);

        using (var image = Image.Load(file.OpenReadStream()))
        {
            image.Mutate(x => x.Resize(300, 300));

            var encoder = new SixLabors.ImageSharp.Formats.Webp.WebpEncoder
            {
                Quality = ImageQuality.Profile,
            };

            image.Save(uploadPath, encoder);
        }

        return Path.Combine(folderName, fileName).Replace("\\", "/");
    }


    //public string UploadImage(string folderName, IFormFile file)
    //{
    //    string directory = Path.Combine(
    //        Directory.GetCurrentDirectory(),
    //        environment.WebRootPath,
    //        folderName);

    //    if (!Directory.Exists(directory))
    //        Directory.CreateDirectory(directory);

    //    string fileName = $"{Guid.NewGuid()}.webp";
    //    string uploadPath = Path.Combine(directory, fileName);

    //    using (var image = Image.Load(file.OpenReadStream()))
    //    {
    //        var encoder = new SixLabors.ImageSharp.Formats.Webp.WebpEncoder
    //        {
    //            Quality = 80
    //        };

    //        image.Save(uploadPath, encoder);
    //    }

    //    return Path.Combine(folderName, fileName).Replace("\\", "/");
    //}




    public (string ImageUrl, string SubImageUrl) UploadImages(string folderName, HttpContext context, int index = 0)
    {
        string fileName = string.Empty;
        string ImageUrl = string.Empty;
        string SubImageUrl = string.Empty;
        var req = context.Request;
        if (req.Form.Files.Count > 0)
        {
            string ImageDirectory = Path.Combine(Directory.GetCurrentDirectory(), this.environment?.WebRootPath + Path.DirectorySeparatorChar + folderName);

            if (!Directory.Exists(ImageDirectory))
                Directory.CreateDirectory(ImageDirectory);

            for (int i = 0; i < req.Form.Files.Count; i++)
            {
                var file = req.Form.Files[i];
                fileName = $"{Guid.NewGuid()}.webp";

                string uploadPath = Path.Combine(ImageDirectory, fileName);

                using (var image = Image.Load(file.OpenReadStream()))
                {
                    var encoder = new JpegEncoder
                    {
                        Quality = 40
                    };
                    image.Save(uploadPath, encoder);
                }
                ImageUrl = Path.Combine(folderName, fileName);
            }
        }
        return (ImageUrl, SubImageUrl);
    }
}
