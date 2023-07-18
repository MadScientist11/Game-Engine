using System.Numerics;

namespace GameEngine.Core;

public class Transform : Component
{
    public Vector3 Position { get; set; }
    public Vector3 Scale { get; set; }
    public Quaternion Rotation { get; set; }

    public Matrix4x4 TRS => Matrix4x4.CreateScale(Scale) *
                            Matrix4x4.CreateFromQuaternion(Rotation) *
                            Matrix4x4.CreateTranslation(Position);

 

   
}