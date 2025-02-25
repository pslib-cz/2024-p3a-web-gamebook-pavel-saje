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


        public ICollection<ViewInteractiblesOption>? InteractOptions { get; set; } = new List<ViewInteractiblesOption>();

        public static implicit operator ViewInteractible?(DataInteractible? v)
        {
            throw new NotImplementedException();
        }
    }
}
