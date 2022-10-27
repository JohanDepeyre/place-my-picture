
using System.Drawing;
using System.Drawing.Imaging;

namespace ApplicationPhoto.Web.UI
{
    public class GestionPropertyItem
    {
        public static double? GetLatitude(Image targetImg)
        {
            try
            {
               

                //Property Item 0x0001 - PropertyTagGpsLatitudeRef
                PropertyItem? propItemRef = targetImg.GetPropertyItem(1);
                    //Property Item 0x0002 - PropertyTagGpsLatitude
                    PropertyItem? propItemLat = targetImg.GetPropertyItem(2);
                    return ExifGpsToFloat(propItemRef, propItemLat);
                
               
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
        public static double? GetLongitude(Image targetImg)
        {
            try
            {
                ///Property Item 0x0003 - PropertyTagGpsLongitudeRef
                PropertyItem propItemRef = targetImg.GetPropertyItem(3);
                //Property Item 0x0004 - PropertyTagGpsLongitude
                PropertyItem propItemLong = targetImg.GetPropertyItem(4);
                return ExifGpsToFloat(propItemRef, propItemLong);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
        private static double ExifGpsToFloat(PropertyItem propItemRef, PropertyItem propItem)
        {
            uint degreesNumerator = BitConverter.ToUInt32(propItem.Value, 0);
            uint degreesDenominator = BitConverter.ToUInt32(propItem.Value, 4);
            float degrees = degreesNumerator / (float)degreesDenominator;

            uint minutesNumerator = BitConverter.ToUInt32(propItem.Value, 8);
            uint minutesDenominator = BitConverter.ToUInt32(propItem.Value, 12);
            float minutes = minutesNumerator / (float)minutesDenominator;

            uint secondsNumerator = BitConverter.ToUInt32(propItem.Value, 16);
            uint secondsDenominator = BitConverter.ToUInt32(propItem.Value, 20);
            float seconds = secondsNumerator / (float)secondsDenominator;

            float coorditate = degrees + (minutes / 60f) + (seconds / 3600f);
            string gpsRef = System.Text.Encoding.ASCII.GetString(new byte[1] { propItemRef.Value[0] }); //N, S, E, or W
            if (gpsRef == "S" || gpsRef == "W")
                coorditate = 0 - coorditate;
            return coorditate;
        }
        /// <summary>
        /// Converti l'item property de l'image (byte) en dateTime
        /// </summary>
        /// <param name="image"> Type Image</param>
        /// <returns>retourne DateTime</returns>
        public static DateTime? ReturnDate(Image image)
        {

#pragma warning disable CS8602 // Déréférencement d'une éventuelle référence null.
            byte[]? byteDate = image.GetPropertyItem(0x0132).Value ?? null;
#pragma warning restore CS8602 // Déréférencement d'une éventuelle référence null.
            if (byteDate == null)
            {
                return null;
            }
            string dateDataBrut = System.Text.Encoding.UTF8.GetString(byteDate);
            string ddmmaaaa = String.Concat(dateDataBrut.Substring(0, 10).Replace(":", "/"), " ");
            string hhmmss = dateDataBrut.Substring(11, 8);
            DateTime dateTimeVar = Convert.ToDateTime(String.Concat(ddmmaaaa, hhmmss));


            return dateTimeVar;
        }
    }

}
