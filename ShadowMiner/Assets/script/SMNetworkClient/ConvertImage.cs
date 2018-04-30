using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SMNetwork
{
    /// <summary>
    ///   Class to convert Image to byte array and the reverse.
    /// </summary>
    public static class ConvertImage
	{
	    /// <summary>
	    ///   Converts an Image object to an array of bytes.
	    /// </summary>
	    /// <param name="imageIn">The image to convert.</param>
	    /// <returns></returns>
	    public static byte[] ImageToByteArray(Image imageIn)
		{
			var ms = new MemoryStream();
			imageIn.Save(ms, ImageFormat.Gif);
			return ms.ToArray();
		}

	    /// <summary>
	    ///   Converts an array of bytes to an Image object.
	    /// </summary>
	    /// <param name="byteArrayIn">The byte array to convert.</param>
	    /// <returns></returns>
	    public static Image ByteArrayToImage(byte[] byteArrayIn)
		{
			var ms = new MemoryStream(byteArrayIn);
			var returnImage = Image.FromStream(ms);
			return returnImage;
		}

		public static Image FromFile(string path)
		{
			return Bitmap.FromFile(path);
		}

		public static void SaveFile(Image img, string path)
		{
			((Bitmap) img).Save(path);
		}

	}
}