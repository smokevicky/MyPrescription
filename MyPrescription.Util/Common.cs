using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MyPrescription.Util
{
    public class Common
    {
        /// <summary>
        /// Generates random Id depending on Field Type (User|Hospital|Doctor|Vault)
        /// </summary>
        /// <param name="fieldType">Accepts enum of type FieldType</param>
        /// <returns></returns>
        public static int generateRandomId(FieldType fieldType)
        {            
            var chars = "0123456789";
            var randomId = new int[9];
            var random = new Random();

            randomId[0] = 9;
            switch (fieldType)
            {
                case FieldType.Doctor:      randomId[1] = Id.doctor;
                                            break;
                case FieldType.User:        randomId[1] = Id.user;
                                            break;
                case FieldType.Hospital:    randomId[1] = Id.hospital;
                                            break;
                case FieldType.Vault:       randomId[1] = Id.vault;
                                            break;
                default:                    randomId[1] = Id.defaultId;
                                            break;
            }                                    

            for (int i = 3; i < randomId.Length; i++)
            {
                randomId[i] = chars[random.Next(chars.Length)];
            }

            var returnVar = 0;

            for (int i = 0; i < randomId.Length; i++)
            {
                returnVar += randomId[i] * Convert.ToInt32(Math.Pow(10, randomId.Length - i - 1));
            }

            return returnVar;
        }

        /// <summary>
        /// Show a snackbar notification toast to the user
        /// </summary>
        /// <param name="message">The Message to be shown</param>
        /// <param name="moodColor">The Color of the snackbar depending on the mood</param>
        public static void Notify(string message, string moodColor)
        {
            string script = @"$(document).ready(function(){
                    $('#snackbar').text( '" + message + @"');

                    //setting snackbar color
                    switch ('"+ moodColor.ToString() +@"')
                    {
                        case 'info': $('#snackbar').css('background-color', 'rgba(0, 39, 40, 0.9)');
                            break;
                        case 'warning': $('#snackbar').css('background-color', 'orangered');
                            break;
                        case 'danger': $('#snackbar').css('background-color', '#e93a54');
                            break;
                        case 'success': $('#snackbar').css('background-color', '#1b8745');
                            break;
                        default: $('#snackbar').css('background-color', 'rgba(0, 39, 40, 0.9)');
                    }

                $('#snackbar').addClass('show');
                    setTimeout(function() {
                    $('#snackbar').removeClass('show');
                    }, 2500);
            });";


            if (HttpContext.Current.CurrentHandler is Page)
            {
                Page p = (Page)HttpContext.Current.CurrentHandler;

                if (ScriptManager.GetCurrent(p) != null)
                {
                    ScriptManager.RegisterStartupScript(p, typeof(Page), "Message", script, true);
                }
                else
                {
                    p.ClientScript.RegisterStartupScript(typeof(Page), "Message", script, true);
                }
            }
        }

        /// <summary>
        /// Converts List to DataTable
        /// </summary>
        /// <typeparam name="T">List type T</typeparam>
        /// <param name="data">Accepts List object</param>
        /// <returns></returns>
        public static DataTable ConvertToDatatable<T>(List<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        /// <summary>
        /// Gets default upload directory from web.config
        /// </summary>
        /// <returns>string</returns>
        public static string GetUploadDirectory()
        {
            string uploadDirectory = WebConfigurationManager.AppSettings["uploadDirectory"];
            return uploadDirectory;
        }

        /// <summary>
        /// Algorithm to resize and crop the image
        /// </summary>
        /// <param name="image">Accepts Image object</param>
        /// <param name="Width">Accepts to be set Image Width</param>
        /// <param name="Height">Accepts to be set Image Height</param>
        /// <param name="needToFill">Accepts whether to fill the Image in the Crop area or not</param>
        /// <returns></returns>
        public static System.Drawing.Image FixedSize(System.Drawing.Image image, int Width, int Height, bool needToFill)
        {
            #region много арифметики
            int sourceWidth = Convert.ToInt32(image.Width);
            int sourceHeight = Convert.ToInt32(image.Height);
            int sourceX = 0;
            int sourceY = 0;
            double destX = 0;
            double destY = 0;

            double nScale = 0;
            double nScaleW = 0;
            double nScaleH = 0;

            nScaleW = ((double)Width / (double)sourceWidth);
            nScaleH = ((double)Height / (double)sourceHeight);
            if (!needToFill)
            {
                nScale = Math.Min(nScaleH, nScaleW);
            }
            else
            {
                nScale = Math.Max(nScaleH, nScaleW);
                destY = (Height - sourceHeight * nScale) / 2;
                destX = (Width - sourceWidth * nScale) / 2;
            }

            if (nScale > 1)
                nScale = 1;

            int destWidth = (int)Math.Round(sourceWidth * nScale);
            int destHeight = (int)Math.Round(sourceHeight * nScale);
            #endregion

            System.Drawing.Bitmap bmPhoto = null;
            try
            {
                bmPhoto = new System.Drawing.Bitmap(destWidth + (int)Math.Round(2 * destX), destHeight + (int)Math.Round(2 * destY));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("destWidth:{0}, destX:{1}, destHeight:{2}, desxtY:{3}, Width:{4}, Height:{5}",
                    destWidth, destX, destHeight, destY, Width, Height), ex);
            }
            using (System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto))
            {
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.CompositingQuality = CompositingQuality.HighQuality;
                grPhoto.SmoothingMode = SmoothingMode.HighQuality;

                Rectangle to = new System.Drawing.Rectangle((int)Math.Round(destX), (int)Math.Round(destY), destWidth, destHeight);
                Rectangle from = new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);
                //Console.WriteLine("From: " + from.ToString());
                //Console.WriteLine("To: " + to.ToString());
                grPhoto.DrawImage(image, to, from, System.Drawing.GraphicsUnit.Pixel);

                return bmPhoto;
            }
        }

    }
}
