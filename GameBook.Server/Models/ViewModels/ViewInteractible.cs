using System;
using System.Collections.Generic;
using System.IO;

namespace GameBook.Server.Models
{
    public class ViewInteractible
    {
        public int InteractibleID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public string ImageBase64
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                    return string.Empty;

                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", ImagePath);
                return File.Exists(fullPath) ? Convert.ToBase64String(File.ReadAllBytes(fullPath)) : string.Empty;
            }
        }

        public ICollection<ViewInteractiblesOption>? InteractOptions { get; set; } = new List<ViewInteractiblesOption>();

        public static implicit operator ViewInteractible?(DataInteractible? v)
        {
            throw new NotImplementedException();
        }
    }
}
