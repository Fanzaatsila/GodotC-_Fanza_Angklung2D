namespace Godot.Core;

using Godot;
using System;
using System.Collections.Generic;
using System.Numerics; // Import System.Numerics for Matrix4x4

public partial class TransformTool : RefCounted
{
    private TransformasiFast _transformasi = new TransformasiFast();

    public List<Godot.Vector2> Rotate(List<Godot.Vector2> shape, float angle, Godot.Vector2? pivot = null)
    {
        var trueAngle = (Mathf.Pi / 180) * angle;

        Godot.Vector2 truePivot = pivot ?? new Godot.Vector2(shape[0].X, shape[0].Y);

        Matrix4x4 rotationMatrix = TransformasiFast.Identity();
        _transformasi.RotationClockwise(ref rotationMatrix, -trueAngle, truePivot);

        List<Godot.Vector2> transformedPoints = _transformasi.GetTransformPoint(rotationMatrix, shape);

        return transformedPoints;
    }

    public List<Godot.Vector2> Translate(List<Godot.Vector2> shape, float x, float y)
    {
        Matrix4x4 translationMatrix = TransformasiFast.Identity();
		_transformasi.Translation(ref translationMatrix, x, -y);

        List<Godot.Vector2> transformedPoints = _transformasi.GetTransformPoint(translationMatrix, shape);

        return transformedPoints;
    }

    public List<Godot.Vector2> Scale(List<Godot.Vector2> shape, float scaleX, float scaleY, Godot.Vector2? pivot = null)
    {
        Godot.Vector2 truePivot = pivot ?? new Godot.Vector2(shape[0].X, shape[0].Y);
        
        Matrix4x4 scalingMatrix = TransformasiFast.Identity();
        _transformasi.Scaling(ref scalingMatrix, scaleX, scaleY, truePivot);

        List<Godot.Vector2> transformedPoints = _transformasi.GetTransformPoint(scalingMatrix, shape);

        return transformedPoints;
    }

    public List<Godot.Vector2> Mirror(List<Godot.Vector2> shape, int direction, Godot.Vector2? pivot = null)
{
    Matrix4x4 mirrorMatrix = TransformasiFast.Identity();

    Godot.Vector2 truePivot = pivot ?? shape[0]; // Gunakan pivot atau titik awal shape

    // Translasi ke origin
    _transformasi.Translation(ref mirrorMatrix, -truePivot.X, -truePivot.Y);

    switch (direction)
    {
        case 0: // Mirror X-axis
            _transformasi.ReflectionToX(ref mirrorMatrix);
            break;
        case 1: // Mirror Y-axis
            _transformasi.ReflectionToY(ref mirrorMatrix);
            break;
        case 2: // Mirror Origin
            _transformasi.ReflectionToOrigin(ref mirrorMatrix);
            break;
    }

    // Translasi kembali ke posisi awal
    _transformasi.Translation(ref mirrorMatrix, truePivot.X, truePivot.Y);

    return _transformasi.GetTransformPoint(mirrorMatrix, shape);
}

}