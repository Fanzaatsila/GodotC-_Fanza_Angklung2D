namespace Godot.Motif2D;

using Godot;
using Godot.Core;
using Godot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public partial class Motif : Node2D
{
    protected Primitif primitif = new Primitif();
    public TransformasiFast transformasi = new TransformasiFast();

    public Color Warna { get; set; } = ColorUtils.ColorStorage(1);
    public GraphicsUtils.DrawStyle GayaGambar { get; set; } = GraphicsUtils.DrawStyle.DotDot;
    public int PanjangStrip { get; set; } = 3;
    public int Celah { get; set; } = 0;
    public override void _Ready()
    {
    }
    // Virtual method to update points before drawing
    public virtual void UpdatePoints(float delta)
    {
        // Default implementation does nothing
    }

    public virtual void Gambar(List<Godot.Vector2> points)
    {
        GraphicsUtils.PutPixelAll(this, points, GayaGambar, Warna, PanjangStrip, Celah);
    }

    public void Transformasi(ref List<Godot.Vector2> points, Matrix4x4 matrix)
    {
        points = transformasi.GetTransformPoint(matrix, points);
    }

    // Separate method to generate points (can be overridden in derived classes)
    public virtual List<Godot.Vector2> GeneratePoints()
    {
        return new List<Godot.Vector2>(); // Default implementation returns an empty list
    }

    public new void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected new virtual void Dispose(bool disposing) // Fungsi Dispose() yang sebenarnya
    {
        if (disposing)
        {
            NodeUtils.DisposeAndNull(primitif, "_primitif");
        }
    }
}