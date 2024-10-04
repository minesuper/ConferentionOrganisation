using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConferentionOrganisationProject.Classes
{
    public static class ImageToByte
    {
        public static void ImageConverter() {
            foreach (var item in Model.ConferentionOrganisationDBEntities.GetContext().Event.ToList())
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"/EventsImg/{item.Event_Id}.jpg"))
                {
                    item.Event_Image = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + $@"/EventsImg/{item.Event_Id}.jpg");
                    continue;
                }
                else if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"/EventsImg/{item.Event_Id}.png"))
                {
                    item.Event_Image = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + $@"/EventsImg/{item.Event_Id}.png");
                    continue;
                }
                else if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"/EventsImg/{item.Event_Id}.jpeg"))
                {
                    item.Event_Image = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + $@"/EventsImg/{item.Event_Id}.jpeg");
                }
            }
            Model.ConferentionOrganisationDBEntities.GetContext().SaveChanges();
        }
    }
}
