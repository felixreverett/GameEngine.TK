using GameEngine.TK.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.Drawing.Imaging;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

namespace GameEngine.TK.Core.Management
{
    public static class TextureFactory
    {
        public static Texture2D Load(string textureName)
        {
            int handle = GL.GenTexture();
            GL.ActiveTexture(TextureUnit.Texture0); // Texture units
            GL.BindTexture(TextureTarget.Texture2D, handle); // Bind our texture
            using var image = new Bitmap(textureName);
            
            // Video guide downgraded System.Drawing.Image to 5.0, but this has critical errors
            image.RotateFlip(RotateFlipType.RotateNoneFlipY); // Flip our image so it's not upside down
            var data = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            // Make our pixels "nearest"
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);

            // Wrapping
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            // Auto-Generate Mipmaps (probably won't need for 2D game)
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return new Texture2D(handle);
        }
    }
}
