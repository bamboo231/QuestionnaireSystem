using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tryD
{
	public partial class WebForm1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Bitmap chart2 = GetChart(100);
        MemoryStream ms = new MemoryStream();
        chart2.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

        img.Save(Response.OutputStream, ImageFormat.Gif);


    }
    public Bitmap GetChart(int numerator)
    {
        Bitmap B = new Bitmap(numerator, 20);
        Graphics G = Graphics.FromImage(B);
        //Color BrushColor = ColorTranslator.FromHtml("#00cccb");

        int pPixel = 2;
        Pen frame = new Pen(Color.Black, pPixel);
        G.DrawRectangle(frame, 0, 0, numerator, 20);//矩形

        //SolidBrush MyBrush = new SolidBrush(BrushColor);
        //G.FillRectangle(MyBrush, 0, numerator, 5, numerator);
        return B;
    }
}
}