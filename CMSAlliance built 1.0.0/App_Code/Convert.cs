using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

/// <summary>
/// Summary description for Convert
/// </summary>
public class Conversion
{
    public static byte[] bytesFromImage(Bitmap bmp)
    {
        MemoryStream ms = new MemoryStream();
        // Save to memory using the Jpeg format
        bmp.Save(ms, ImageFormat.Jpeg);

        // read to end
        byte[] bmpBytes = ms.GetBuffer();
        bmp.Dispose();
        ms.Close();

        return bmpBytes;
    }

    public static Image imageFromBytes(byte[] bmpBytes)
    {
        MemoryStream ms = new MemoryStream(bmpBytes);
        Image img = Image.FromStream(ms);
        // Do NOT close the stream!

        return img;
    }

    public static String asset_avatar(int userid)
    {
        return "http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/asset.aspx?userid=" + userid;
    }
}