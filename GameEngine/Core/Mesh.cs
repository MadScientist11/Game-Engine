using Silk.NET.OpenGL;
using static GameEngine.Core.Graphics;

namespace GameEngine.Core;

public class Mesh
{
    public uint Id => _vao;
    
    public float[]? Vertices { get; private set; }
    public uint[]? Triangles { get; private set; }
    public float[]? Colors { get; private set; }
    public float[]? Uv { get; private set; }

    public bool Exists { get; private set; }

    private uint _vao;
    private uint _vbo;
    private uint _ebo;
    


    public Mesh SetVertices(float[] vertices)
    {
        Vertices = vertices;
        return this;
    }

    public Mesh SetTriangles(uint[]? triangles)
    {
        Triangles = triangles;
        return this;
    }

    public Mesh SetColors(float[] colors)
    {
        Colors = colors;
        return this;
    }

    public Mesh SetTextureCoords(float[] coords)
    {
        Uv = coords;
        return this;
    }

    public unsafe void Build()
    {
        _vao = Gl.GenVertexArray();
        Gl.BindVertexArray(_vao);
        _vbo = Gl.GenBuffer();
        Gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);

        if (Vertices is null || Triangles is null)
        {
            throw new Exception("Mesh must have vertices and triangles");
        }

        float[] vertexAttributes = GetAttributes().ToArray();
        fixed (float* buf = vertexAttributes)
            Gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(vertexAttributes.Length * sizeof(float)), buf,
                BufferUsageARB.StaticDraw);

        
        _ebo = Gl.GenBuffer();
        Gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo);

        fixed (uint* buf = Triangles)
            Gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(Triangles.Length * sizeof(uint)), buf,
                BufferUsageARB.StaticDraw);


        uint stride = GetStride() * sizeof(float);
        const uint positionLoc = 0;
        Gl.EnableVertexAttribArray(positionLoc);
        Gl.VertexAttribPointer(positionLoc, 3, VertexAttribPointerType.Float, false, stride, (void*)0);

        if (Colors is not null)
        {
            const uint colorLoc = 1;
            Gl.EnableVertexAttribArray(colorLoc);
            Gl.VertexAttribPointer(colorLoc, 3, VertexAttribPointerType.Float, false, stride,
                (void*)(3 * sizeof(float)));
        }

        if (Uv is not null)
        {
            const uint uvLoc = 2;
            Gl.EnableVertexAttribArray(uvLoc);
            Gl.VertexAttribPointer(uvLoc, 2, VertexAttribPointerType.Float, false, stride, (void*)(6 * sizeof(float)));
        }

        Exists = true;
        ClearGlState();

        void ClearGlState()
        {
            Gl.BindVertexArray(0);
            Gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
            Gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
        }
    }

    public uint GetStride()
    {
        var uInt32 = (Convert.ToUInt32(Vertices != null) * 3);
        var int32 = (Convert.ToUInt32(Colors != null) * 3);
        var u = (Convert.ToUInt32(Uv != null) * 2);
        return uInt32 + int32 +
               u;
    }

    public List<float> GetAttributes()
    {
        List<float> attributes = new List<float>(Vertices.Length);
        for (int i = 0; i < Vertices.Length / 3; i++)
        {
            int offset = i * 3;
            attributes.Add(Vertices[0 + offset]);
            attributes.Add(Vertices[1 + offset]);
            attributes.Add(Vertices[2 + offset]);

            if (Colors != null)
            {
                attributes.Add(Colors[0 + offset]);
                attributes.Add(Colors[1 + offset]);
                attributes.Add(Colors[2 + offset]);
            }

            int texOffset = i * 2;
            if (Uv != null)
            {
                attributes.Add(Uv[0 + texOffset]);
                attributes.Add(Uv[1 + texOffset]);
            }
        }

        return attributes;
    }
}