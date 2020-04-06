using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Xml.Serialization;

namespace BMS.Common
{
    public static class CommonMethods
    {
        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public static string GenerateQRCode(string qrCodeString)
        {
            try
            {
                string pathToReturn = string.Empty;
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeString, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                pathToReturn = "/Content/Images/QRCodes/" + Guid.NewGuid() + ".png";
                string imagePath = HostingEnvironment.MapPath(pathToReturn);
                qrCodeImage.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);

                return pathToReturn;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string SaveFile(string pathToSave, HttpPostedFileBase file)
        {
            string pathToReturn = string.Empty;
            try
            {
                pathToReturn = pathToSave + "/" + Guid.NewGuid() + file.FileName;
                pathToSave = HostingEnvironment.MapPath(pathToSave);
                if (file == null)
                    return "";
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                pathToSave = HostingEnvironment.MapPath(pathToReturn);
                file.SaveAs(pathToSave);
            }
            catch (Exception ex)
            {

            }
            return pathToReturn;

        }
        public static string SaveFileFromBase64String(string pathToSave, string base64)
        {
            string pathToReturn = string.Empty;
            try
            {
                pathToReturn = pathToSave + "/" + Guid.NewGuid() + ".png";
                pathToSave = HostingEnvironment.MapPath(pathToSave);
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                pathToSave = HostingEnvironment.MapPath(pathToReturn);

                Byte[] bytes = Convert.FromBase64String(base64);
                File.WriteAllBytes(pathToSave, bytes);
            }
            catch (Exception ex)
            {

            }
            return pathToReturn;
        }

        public static string AddBaseURL(this string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                string baseUrl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority +
                    System.Web.HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
                return baseUrl + imagePath;
            }
            return "";
        }
        public static string SerializeXML<T>(T dataToSerialize)
        {
            try
            {
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringwriter, dataToSerialize);
                return stringwriter.ToString();
            }
            catch
            {
                throw;
            }
        }

    }
}
