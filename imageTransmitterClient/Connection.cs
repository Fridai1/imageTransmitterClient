using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;

namespace imageTransmitterClient
{
    public class Connection
    {
        
        UdpClient client = new UdpClient();

        public void SendScreenShot()
        {
            Bitmap image = TakeScreenshot();
            byte[] bytearray = imageToByteArray(image);

            client.Connect("localhost",6789);
            client.Send(bytearray,bytearray.Length);
        }

        private Bitmap TakeScreenshot()
        {
            Rectangle rect = new Rectangle(0, 0, 100, 100);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            return bmp;

        }

        private byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
    }
}