namespace GameEngine.Core;

public class Mesh
{
    public float[] Vertices { get; private set; }
    public uint[] Triangles { get; private set; }

    public float[] Colors { get; private set; }
    public float[] UV { get; private set; }


    public void SetVertices(float[] vertices)
    {
        Vertices = vertices;
    }

    public void SetTriangles(uint[] triangles)
    {
        Triangles = triangles;
    }

    public void SetColors(float[] colors)
    {
        Colors = colors;
    }

    public void SetTextureCoords(float[] coords)
    {
        UV = coords;
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
            if (UV != null)
            {
                attributes.Add(UV[0 + texOffset]);
                attributes.Add(UV[1 + texOffset]);
            }
            
        }

        return attributes;
    }
}