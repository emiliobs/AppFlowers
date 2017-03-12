using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FlowersBack.Classes
{
    public class Fileshelper
    {

        public static string UploadPhoto(HttpPostedFileBase file, string folder)
        {
            var path = string.Empty;
            var pic = string.Empty;

            if (file != null)
            {
                pic = Path.GetFileName(file.FileName);
                path = Path.Combine(HttpContext.Current.Server.MapPath(folder), pic);
                file.SaveAs(path);
                
                //Esto no hace nada:
                //using (MemoryStream ms = new MemoryStream())
                //{
                //    file.InputStream.CopyTo(ms);
                //    byte[] array = ms.GetBuffer();
                //}
            }


            return pic;
        }

        public static bool UploadPhoto(MemoryStream stream, string folder, string name)
        {

            try
            {
                stream.Position = 0;

                //Aqui lo convierto en string los datos que vienen en array cuando se toma la  foto:
                var path = Path.Combine(HttpContext.Current.Server.MapPath(folder), name);

                //Aqui lo convierto en un archivo
                File.WriteAllBytes(path, stream.ToArray());
            }
            catch 
            {
                return false;
            }


            return true;

        }

    }
}