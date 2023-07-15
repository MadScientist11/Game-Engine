using Silk.NET.OpenGL;
using static GameEngine.Core.Graphics;

namespace GameEngine.Core;

public class Texture2D : IDisposable
{
    public readonly uint Id;

    public unsafe Texture2D(string path)
    {
        Bitmap bp = new Bitmap(path);
        Id = Gl.GenTexture();
        Gl.BindTexture(TextureTarget.Texture2D, Id);
        fixed (byte* px = bp.Data)
            Gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)bp.Size.Width, (uint)bp.Size.Height,
                0, bp.RGBA ? PixelFormat.Rgba : PixelFormat.Rgb, PixelType.UnsignedByte, px);

        Gl.TexParameter(TextureTarget.Texture2D, GLEnum.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        Gl.TexParameter(TextureTarget.Texture2D, GLEnum.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
        Gl.TexParameter(TextureTarget.Texture2D, GLEnum.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
        Gl.TexParameter(TextureTarget.Texture2D, GLEnum.TextureMagFilter, (int)TextureMagFilter.Linear);

        Gl.GenerateMipmap(TextureTarget.Texture2D);
        

        Gl.BindTexture(TextureTarget.Texture2D, 0);
    }

    public static void Bind(uint textureId, int textureUnit)
    {
        Gl.ActiveTexture(TextureUnit.Texture0 + textureUnit);
        Gl.BindTexture(TextureTarget.Texture2D, textureId);
    }

    public static void UnBind()
    {
        Gl.BindTexture(TextureTarget.Texture2D, 0);
    }

    public void Dispose()
    {
        Gl.DeleteTexture(Id);
    }
}