namespace Godot.Motif2D;

using Godot;
using Godot.Core;
using Godot.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public partial class KupuKupu : Motif
{
    // Parameters (exposed in the Godot editor)
    public Color WarnaSayap { get; set; } = ColorUtils.ColorStorage(1);
    public float Ukuran { get; set; } = 1.0f;
    public float PanjangSayap { get; set; } = 4.0f;
    public float LebarSayap { get; set; } = 3.0f;
    public float PanjangBadan { get; set; } = 3.5f;
    public float LebarBadan { get; set; } = 0.5f;
    public float PanjangEkor { get; set; } = 1.0f;
    public float SudutSayap { get; set; } = 30.0f;

    public override void _Ready()
    {
        Gambar();
    }

    public void Gambar()
    {
        List<Godot.Vector2> points = new List<Godot.Vector2>();
        Matrix4x4 matrix = TransformasiFast.Identity();

        // Sayap Kanan
        List<Godot.Vector2> sayapKanan = SayapBlueprint(PanjangSayap, LebarSayap, SudutSayap);
        points.AddRange(sayapKanan);

        // Sayap Kiri
        matrix = TransformasiFast.Identity();
        transformasi.ReflectionToY(ref matrix);
        List<Godot.Vector2> sayapKiri = transformasi.GetTransformPoint(matrix, sayapKanan);
        points.AddRange(sayapKiri);

        // Badan
        List<Godot.Vector2> badan = BadanBlueprint(PanjangBadan, LebarBadan);
        points.AddRange(badan);

        // Kepala (Simplified - just a circle for now)
        List<Godot.Vector2> kepala = primitif.CircleMidPoint(0, (int)(PanjangBadan / 2), (int)(LebarBadan * 2));
        points.AddRange(kepala);

        // Ekor (Simplified - just a line for now)
        List<Godot.Vector2> ekor = primitif.LineDDA(0, -(int)(PanjangBadan / 2), 0, -(int)(PanjangBadan / 2) - (int)PanjangEkor);
        points.AddRange(ekor);

        // Draw the butterfly with color and style
        base.Gambar(points);
    }

    private List<Godot.Vector2> SayapBlueprint(float panjang, float lebar, float sudut)
    {
        List<Godot.Vector2> result = new List<Godot.Vector2>();
        List<Godot.Vector2> belahKetupat = primitif.BelahKetupat(0, 0, panjang, lebar);
        Matrix4x4 matrix = TransformasiFast.Identity();
        Godot.Vector2 coord = new Godot.Vector2(0, 0);

        // ... (Transformations for the wing using the provided parameters) ...

        return result.Distinct().ToList();
    }

    private List<Godot.Vector2> BadanBlueprint(float panjang, float lebar)
    {
        List<Godot.Vector2> result = new List<Godot.Vector2>();
        List<Godot.Vector2> elips = primitif.EllipseMidpoint(0, 0, (int)(lebar), (int)(panjang));
        Matrix4x4 matrix = TransformasiFast.Identity();
        Godot.Vector2 coord = new Godot.Vector2(0, 0);

        // ... (Transformations for the body using the provided parameters) ...

        return result.Distinct().ToList();
    }
}