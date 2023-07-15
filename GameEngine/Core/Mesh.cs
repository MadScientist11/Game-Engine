namespace GameEngine.Core;

public class Mesh
{
    public float[] Vertices { get; private set; }
    public uint[] Triangles { get; private set; }


    public void SetVertices(float[] vertices)
    {
        Vertices = vertices;
    }

    public void SetTriangles(uint[] triangles)
    {
        Triangles = triangles;
    }
}