namespace Godot.Motif2D;

using Godot;
using Godot.Core;
using Godot.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public partial class Awan : Motif
{
    // Parameters for the cloud's appearance
    public float Ukuran { get; set; } = 1.0f;
    public Color WarnaAwan { get; set; } = ColorUtils.ColorStorage(10);

    // Center point of the cloud
    public Godot.Vector2 Center { get; set; } = new Godot.Vector2(200, 200);

    // Parameters for relative positioning and radius of each circle
    public float Jarak1 { get; set; } = 50f;
    public float Sudut1 { get; set; } = 180f;
    public float Radius1 { get; set; } = 40f;

    public float Jarak2 { get; set; } = 35f;
    public float Sudut2 { get; set; } = 135f;
    public float Radius2 { get; set; } = 30f;

    public float Jarak3 { get; set; } = 0f;
    public float Sudut3 { get; set; } = 90f;
    public float Radius3 { get; set; } = 50f;

    public float Jarak4 { get; set; } = 35f;
    public float Sudut4 { get; set; } = 45f;
    public float Radius4 { get; set; } = 30f;

    public float Jarak5 { get; set; } = 50f;
    public float Sudut5 { get; set; } = 0f;
    public float Radius5 { get; set; } = 40f;
    private List<Godot.Vector2> points;

    public override void _Ready()
    {
        points = GeneratePoints(); // Initialize points here
        SetProcess(true);
    }

    public override void UpdatePoints(float delta)
    {
        // 1. Create the transformation matrices
        Matrix4x4 translationMatrix = TransformasiFast.Identity();
        transformasi.Translation(ref translationMatrix, 50 * delta, 0); // Translate in x direction

        // PrintUtils.PrintVector2List(points, "Points before rotation:");

        Matrix4x4 rotationMatrix = TransformasiFast.Identity();
        transformasi.RotationClockwise(ref rotationMatrix, Mathf.DegToRad(45) * delta, Center); // Rotate clockwise

        // PrintUtils.PrintVector2List(points, "Points after rotation:");

        // PrintUtils.PrintMatrix(rotationMatrix, "Rotation Matrix:");

        // 2. Apply the transformations in the correct order (rotate, then translate)
        List<Godot.Vector2> rotatedPoints = transformasi.GetTransformPoint(rotationMatrix, points);
        points = transformasi.GetTransformPoint(translationMatrix, rotatedPoints); 
    }

    public override List<Godot.Vector2> GeneratePoints()
    {
        return AwanBlueprint();
    }

    public List<Godot.Vector2> GetPoints()
    {
        return points;
    }

    public TransformasiFast GetTransformasi()
    {
        return transformasi;
    }

    private List<Godot.Vector2> AwanBlueprint()
    {
        List<Godot.Vector2> res = new List<Godot.Vector2>();

        // Calculate relative positions using distance and angle
        Godot.Vector2 Posisi1 = new Godot.Vector2(Jarak1 * Mathf.Cos(Mathf.DegToRad(Sudut1)), Jarak1 * Mathf.Sin(Mathf.DegToRad(Sudut1)));
        Godot.Vector2 Posisi2 = new Godot.Vector2(Jarak2 * Mathf.Cos(Mathf.DegToRad(Sudut2)), Jarak2 * Mathf.Sin(Mathf.DegToRad(Sudut2)));
        Godot.Vector2 Posisi3 = new Godot.Vector2(Jarak3 * Mathf.Cos(Mathf.DegToRad(Sudut3)), Jarak3 * Mathf.Sin(Mathf.DegToRad(Sudut3)));
        Godot.Vector2 Posisi4 = new Godot.Vector2(Jarak4 * Mathf.Cos(Mathf.DegToRad(Sudut4)), Jarak4 * Mathf.Sin(Mathf.DegToRad(Sudut4)));
        Godot.Vector2 Posisi5 = new Godot.Vector2(Jarak5 * Mathf.Cos(Mathf.DegToRad(Sudut5)), Jarak5 * Mathf.Sin(Mathf.DegToRad(Sudut5)));

        // Generate circles and ellipse using relative positions
        res.AddRange(primitif.CircleMidPoint((int)(Center.X + Posisi1.X), (int)(Center.Y + Posisi1.Y), (int)Radius1));
        res.AddRange(primitif.CircleMidPoint((int)(Center.X + Posisi2.X), (int)(Center.Y + Posisi2.Y), (int)Radius2));
        res.AddRange(primitif.EllipseMidpoint((int)(Center.X + Posisi3.X), (int)(Center.Y + Posisi3.Y), (int)Radius3, (int)Radius3));
        res.AddRange(primitif.CircleMidPoint((int)(Center.X + Posisi4.X), (int)(Center.Y + Posisi4.Y), (int)Radius4));
        res.AddRange(primitif.CircleMidPoint((int)(Center.X + Posisi5.X), (int)(Center.Y + Posisi5.Y), (int)Radius5));

        // Connect the circles with a line
        res.AddRange(primitif.LineBresenham(Center.X + Posisi1.X, Center.Y + Posisi1.Y - 1, Center.X + Posisi5.X, Center.Y + Posisi5.Y - 1));

        return res.Distinct().ToList();
    }
}