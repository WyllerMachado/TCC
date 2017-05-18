using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ClassLogger.Helpers
{
    public class Photo
    {
        public static byte[] ToByteArray(HttpPostedFileBase photo)
        {
            byte[] imageBinaryData = null;

            if (photo != null)
                using (var binaryReader = new BinaryReader(photo.InputStream))
                    imageBinaryData = binaryReader.ReadBytes(photo.ContentLength);

            return imageBinaryData;
        }
    }
}