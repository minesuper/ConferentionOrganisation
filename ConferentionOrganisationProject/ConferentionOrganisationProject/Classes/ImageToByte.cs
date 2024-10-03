using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferentionOrganisationProject.Classes
{
    public static class ImageToByte
    {
        public static void ImageConverter() {
            foreach (var item in Model.ConferentionOrganisationDBEntities.GetContext().Event.ToList())
            {
                if (File.Exists($"/Resources/EventsImg/{item.Event_Id}.jpg"))
                {
                    item.Event_Image = File.ReadAllBytes($"/Resources/EventsImg/{item.Event_Id}.jpg");
                }
            }
            Model.ConferentionOrganisationDBEntities.GetContext().SaveChanges();
        }
    }
}
