namespace Godot.Scene;

using Godot;
using Godot.Core;
using Godot.Utils;
using System;
using System.Collections.Generic;

public partial class Karya3 : Node2D
{
	private BentukDasar _bentukDasar = new BentukDasar();
	private Transformasi _transformasi = new Transformasi();
	private Primitif _primitif = new Primitif();
	private TransformTool _transformTool = new TransformTool();
	private PolygonTool _polygonTool = new PolygonTool();

	// Variabel untuk animasi
	private float bungaRotation = 0;
	private float bungaRotationSpeed = 90;
	private float angklungRotation = 0;
	private float angklungRotationSpeed = 90;
	private bool isAngklungRotatingRight = true;
	
	public override void _Ready()
	{
		ScreenUtils.Initialize(GetViewport());
		GD.Print("Screen Width: " + ScreenUtils.ScreenWidth);
		QueueRedraw();
	}

	public override void _Process(double delta)
	{
		// Update rotation for animation
		bungaRotation += (float)delta * bungaRotationSpeed;

		// Update rotation for angklung
		if (isAngklungRotatingRight)
		{
			angklungRotation += (float)delta * angklungRotationSpeed;
			if (angklungRotation >= 10)
			{
				isAngklungRotatingRight = false;
			}
		}
		else
		{
			angklungRotation -= (float)delta * angklungRotationSpeed;
			if (angklungRotation <= -10)
			{
				isAngklungRotatingRight = true;
			}
		}

		// Request redraw
		QueueRedraw();
	}
	
	public override void _Draw()
	{
		MarginPixel();
		// KartesianBorder();

		motifAngklung();
		motifBunga();
		motifPendopo();
		motifAngklung8Nada();
	}

	private void motifAngklung()
	{
		for(int i = -4; i < 4; i++)
		{
			Angklung(i * 150 + 80, -325);
		}
	}

	private void Angklung(float intiX, float intiY)
	{
		List<Vector2> shape = new List<Vector2>();
		var inti = ConvertToKartesian(intiX, intiY);
		float trueIntiX = inti[0];
		float trueIntiY = inti[1];

		// Persegi panjang vertikal 
		var persegiPanjang1 = _bentukDasar.PersegiPanjang(trueIntiX-5, trueIntiY-100, 5, 200);
		var persegiPanjang1Rotated = _transformTool.Rotate(persegiPanjang1, angklungRotation, new Vector2(trueIntiX, trueIntiY));
		DrawPolygon(persegiPanjang1Rotated.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		var persegiPanjang2 = _bentukDasar.PersegiPanjang(trueIntiX-55, trueIntiY-100, 5, 200);
		var persegiPanjang2Rotated = _transformTool.Rotate(persegiPanjang2, angklungRotation, new Vector2(trueIntiX, trueIntiY));
		DrawPolygon(persegiPanjang2Rotated.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		var persegiPanjang3 = _bentukDasar.PersegiPanjang(trueIntiX+35, trueIntiY-100, 5, 200);
		var persegiPanjang3Rotated = _transformTool.Rotate(persegiPanjang3, angklungRotation, new Vector2(trueIntiX, trueIntiY));
		DrawPolygon(persegiPanjang3Rotated.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		// Persegi panjang horizontal
		var persegiPanjang4 = _bentukDasar.PersegiPanjang(trueIntiX-65, trueIntiY-35, 115, 7);
		var persegiPanjang4Rotated = _transformTool.Rotate(persegiPanjang4, angklungRotation, new Vector2(trueIntiX, trueIntiY));
		DrawPolygon(persegiPanjang4Rotated.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		var persegiPanjang5 = _bentukDasar.PersegiPanjang(trueIntiX-15, trueIntiY+10, 65, 7);
		var persegiPanjang5Rotated = _transformTool.Rotate(persegiPanjang5, angklungRotation, new Vector2(trueIntiX, trueIntiY));
		DrawPolygon(persegiPanjang5Rotated.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		var persegiPanjang6 = _bentukDasar.PersegiPanjang(trueIntiX-65, trueIntiY+70, 115, 10);
		var persegiPanjang6Rotated = _transformTool.Rotate(persegiPanjang6, angklungRotation, new Vector2(trueIntiX, trueIntiY));
		DrawPolygon(persegiPanjang6Rotated.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		// Lingkaran kiri kanan
		var lingkaranKiri = _polygonTool.LingkaranPolygon(new Vector2(trueIntiX-65, trueIntiY+75), 5);
		var rotatedLingkaranKiri = _transformTool.Rotate(lingkaranKiri, angklungRotation, new Vector2(trueIntiX, trueIntiY));
		DrawPolygon(rotatedLingkaranKiri.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		var lingkaranKanan = _polygonTool.LingkaranPolygon(new Vector2(trueIntiX+50, trueIntiY+75), 5);
		var rotatedLingkaranKanan = _transformTool.Rotate(lingkaranKanan, angklungRotation, new Vector2(trueIntiX, trueIntiY));
		DrawPolygon(rotatedLingkaranKanan.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		// Persegi kiri kanan
		var persegiKiri = _bentukDasar.Persegi(trueIntiX-30, trueIntiY+65, 5);
		var rotatedPersegiKiri = _transformTool.Rotate(persegiKiri, angklungRotation, new Vector2(trueIntiX, trueIntiY));
		DrawPolygon(rotatedPersegiKiri.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		var persegiKanan = _bentukDasar.Persegi(trueIntiX+15, trueIntiY+65, 5);
		var rotatedPersegiKanan = _transformTool.Rotate(persegiKanan, angklungRotation, new Vector2(trueIntiX, trueIntiY));
		DrawPolygon(rotatedPersegiKanan.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		// Trapesium kiri
		var trapesiumKiri1 = _bentukDasar.TrapesiumSiku(new Vector2(trueIntiX-50, trueIntiY+35), 40, 65, 15);
		var rotatedTrapesiumKiri1 = _transformTool.Rotate(trapesiumKiri1, -90, new Vector2(trueIntiX-27.5f, trueIntiY+42.5f));
		var doubleRotatedTrapesiumKiri1 = _transformTool.Rotate(rotatedTrapesiumKiri1, angklungRotation, new Vector2(trueIntiX, trueIntiY));
		var sortedDoubleRotatedTrapesiumKiri1 = _polygonTool.PolygonRotatedShape(doubleRotatedTrapesiumKiri1);
		DrawPolygon(sortedDoubleRotatedTrapesiumKiri1.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		var trapesiumKiri2 = _bentukDasar.TrapesiumSiku(new Vector2(trueIntiX-57.5f, trueIntiY), 90, 100, 10);
		var rotatedTrapesiumKiri2 = _transformTool.Rotate(trapesiumKiri2, -90, new Vector2(trueIntiX-30f, trueIntiY+5));
		var doubleRotatedTrapesiumKiri2 = _transformTool.Rotate(rotatedTrapesiumKiri2, angklungRotation, new Vector2(trueIntiX, trueIntiY));
		var sortedDoubleRotatedTrapesiumKiri2 = _polygonTool.PolygonRotatedShape(doubleRotatedTrapesiumKiri2);
		DrawPolygon(sortedDoubleRotatedTrapesiumKiri2.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		// Trapesium kanan
		var trapesiumKanan1 = _bentukDasar.TrapesiumSiku(new Vector2(trueIntiX, trueIntiY+40), 25, 40, 15);
		var rotatedTrapesiumKanan1 = _transformTool.Rotate(trapesiumKanan1, -90, new Vector2(trueIntiX+17.5f, trueIntiY+47.5f));
		var doubleRotatedTrapesiumKanan1 = _transformTool.Rotate(rotatedTrapesiumKanan1, angklungRotation, new Vector2(trueIntiX, trueIntiY));
		var sortedDoubleRotatedTrapesiumKanan1 = _polygonTool.PolygonRotatedShape(doubleRotatedTrapesiumKanan1);
		DrawPolygon(sortedDoubleRotatedTrapesiumKanan1.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		
		var trapesiumKanan2 = _bentukDasar.TrapesiumSiku(new Vector2(trueIntiX-20, trueIntiY+5), 50, 60, 10);
		var rotatedTrapesiumKanan2 = _transformTool.Rotate(trapesiumKanan2, -90, new Vector2(trueIntiX+15, trueIntiY+10));
		var doubleRotatedTrapesiumKanan2 = _transformTool.Rotate(rotatedTrapesiumKanan2, angklungRotation, new Vector2(trueIntiX, trueIntiY));
		var sortedDoubleRotatedTrapesiumKanan2 = _polygonTool.PolygonRotatedShape(doubleRotatedTrapesiumKanan2);
		DrawPolygon(sortedDoubleRotatedTrapesiumKanan2.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});    
	}

	private void motifBunga()
	{
		bunga(-700, 350);
		bunga(700, 350);
	}

	private void bunga(float intiX, float intiY)
	{
		var inti = ConvertToKartesian(intiX, intiY);
		Vector2 trueInti = new Vector2(inti[0], inti[1]);
		float trueIntiX = inti[0];
		float trueIntiY = inti[1];

		var lingkaranKiri = _polygonTool.LingkaranPolygon(new Vector2(trueIntiX-15, trueIntiY), 15);
		var rotatedLingkaranKiri = _transformTool.Rotate(lingkaranKiri, bungaRotation, trueInti);
		var sortedRotatedLingkaranKiri = _polygonTool.PolygonRotatedShape(rotatedLingkaranKiri);
		DrawPolygon(sortedRotatedLingkaranKiri.ToArray(), new Color[]{ColorUtils.ColorStorage(99)});
		
		var lingkaranKanan = _polygonTool.LingkaranPolygon(new Vector2(trueIntiX+15, trueIntiY), 15);
		var rotatedLingkaranKanan = _transformTool.Rotate(lingkaranKanan, bungaRotation, trueInti);
		var sortedRotatedLingkaranKanan = _polygonTool.PolygonRotatedShape(rotatedLingkaranKanan);
		DrawPolygon(sortedRotatedLingkaranKanan.ToArray(), new Color[]{ColorUtils.ColorStorage(99)});

		var lingkaranAtas = _polygonTool.LingkaranPolygon(new Vector2(trueIntiX, trueIntiY-15), 15);
		var rotatedLingkaranAtas = _transformTool.Rotate(lingkaranAtas, bungaRotation, trueInti);
		var sortedRotatedLingkaranAtas = _polygonTool.PolygonRotatedShape(rotatedLingkaranAtas);
		DrawPolygon(sortedRotatedLingkaranAtas.ToArray(), new Color[]{ColorUtils.ColorStorage(99)});

		var lingkaranBawah = _polygonTool.LingkaranPolygon(new Vector2(trueIntiX, trueIntiY+15), 15);
		var rotatedLingkaranBawah = _transformTool.Rotate(lingkaranBawah, bungaRotation, trueInti);
		var sortedRotatedLingkaranBawah = _polygonTool.PolygonRotatedShape(rotatedLingkaranBawah);
		DrawPolygon(sortedRotatedLingkaranBawah.ToArray(), new Color[]{ColorUtils.ColorStorage(99)});

		var lingkaranInti = _polygonTool.LingkaranPolygon(trueInti, 10);
		var rotatedLingkaranInti = _transformTool.Rotate(lingkaranInti, bungaRotation, trueInti);
		var sortedRotatedLingkaranInti = _polygonTool.PolygonRotatedShape(rotatedLingkaranInti);
		DrawPolygon(sortedRotatedLingkaranInti.ToArray(), new Color[]{ColorUtils.ColorStorage(1)});

		
		var segitigaUtama = _bentukDasar.SegitigaSamaKaki(new Vector2(trueIntiX, trueIntiY-35), 20, 20);
		for(int i = 0; i < 4; i++)
		{
			var rotatedSegitiga = _transformTool.Rotate(segitigaUtama, 90 * i + 45, trueInti);
			var doubleRotatedSegitiga = _transformTool.Rotate(rotatedSegitiga, -bungaRotation, trueInti);
			DrawPolygon(doubleRotatedSegitiga.ToArray(), new Color[]{ColorUtils.ColorStorage(1)});
		}
	}

	private void motifPendopo()
	{
		pendopo(0, -175);
	}

	private void pendopo(float intiX, float intiY)
	{
		List<Vector2> shape = new List<Vector2>();
		var inti = ConvertToKartesian(intiX, intiY);
		Vector2 trueInti = new Vector2(inti[0], inti[1]);
		float trueIntiX = inti[0];
		float trueIntiY = inti[1];

		var persegiPanjangFondasi = _bentukDasar.PersegiPanjang(trueIntiX-450, trueIntiY, 900, 20);
		DrawPolygon(persegiPanjangFondasi.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		var trapesiumFondasi = _bentukDasar.TrapesiumSamaKaki(new Vector2(trueIntiX-425, trueIntiY-20), 850, 875, 20);
		var sortedTrapesiumFondasi = _polygonTool.PolygonRotatedShape(trapesiumFondasi);
		DrawPolygon(sortedTrapesiumFondasi.ToArray(), new Color[]{ColorUtils.ColorStorage(14)});

		// Membuat pilar kiri
		List<Vector2> pilarKiri = new List<Vector2>();
		var persegiPilarKiri = _bentukDasar.PersegiPanjang(trueIntiX-375, trueIntiY-320, 30, 300);
		DrawPolygon(persegiPilarKiri.ToArray(), new Color[]{ColorUtils.ColorStorage(13)});

		var segitigaBawahKiri = _bentukDasar.SegitigaSamaKaki(new Vector2(trueIntiX-360, trueIntiY-70), 50, 50);
		DrawPolygon(segitigaBawahKiri.ToArray(), new Color[]{ColorUtils.ColorStorage(13)});

		var segitigaAtasKiri = _transformTool.Mirror(segitigaBawahKiri, 0, new Vector2(trueIntiX-360, trueIntiY-170));
		DrawPolygon(segitigaAtasKiri.ToArray(), new Color[]{ColorUtils.ColorStorage(13)});
		
		var persegiPilarKanan = _bentukDasar.PersegiPanjang(trueIntiX+345, trueIntiY-320, 30, 300);
		DrawPolygon(persegiPilarKanan.ToArray(), new Color[]{ColorUtils.ColorStorage(13)});

		var segitigaBawahKanan = _bentukDasar.SegitigaSamaKaki(new Vector2(trueIntiX+360, trueIntiY-70), 50, 50);
		DrawPolygon(segitigaBawahKanan.ToArray(), new Color[]{ColorUtils.ColorStorage(13)});

		var segitigaAtasKanan = _transformTool.Mirror(segitigaBawahKanan, 0, new Vector2(trueIntiX+360, trueIntiY-170));
		DrawPolygon(segitigaAtasKanan.ToArray(), new Color[]{ColorUtils.ColorStorage(13)});

		// Membuat atap
		var fondasiAtap = _bentukDasar.PersegiPanjang(trueIntiX-500, trueIntiY-330, 1000, 10);
		DrawPolygon(fondasiAtap.ToArray(), new Color[]{ColorUtils.ColorStorage(14)});

		var segitigaKiri = _bentukDasar.SegitigaSiku(new Vector2(trueIntiX-475, trueIntiY-405), 300, 75);
		var mirroredSegitigaKiri = _transformTool.Mirror(segitigaKiri, 1, new Vector2(trueIntiX-325, trueIntiY-405));
		var sortedMirroredSegitigaKiri = _polygonTool.PolygonRotatedShape(mirroredSegitigaKiri);
		DrawPolygon(sortedMirroredSegitigaKiri.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		var segitigaKanan = _bentukDasar.SegitigaSiku(new Vector2(trueIntiX+175, trueIntiY-405), 300, 75);
		var sortedSegitigaKanan = _polygonTool.PolygonRotatedShape(segitigaKanan);
		DrawPolygon(sortedSegitigaKanan.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		var trapesiumAtap = _bentukDasar.TrapesiumSamaKaki(new Vector2(trueIntiX-125, trueIntiY-530), 250, 500, 200);
		var sortedTrapesiumAtap = _polygonTool.PolygonRotatedShape(trapesiumAtap);
		DrawPolygon(sortedTrapesiumAtap.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});
	}

	private void motifAngklung8Nada()
	{
		float intiX = 0;
		float intiY = -100;
		TempatAngklung8Nada(intiX, intiY);
		for(int i = -4; i < 4; i++)
		{
			angklung8Nada(intiX + i*45 + 20, intiY+25, 90-i*10);
		}
	}

	private void TempatAngklung8Nada(float intiX, float intiY)
	{
		List<Vector2> shape = new List<Vector2>();
		var inti = ConvertToKartesian(intiX, intiY);
		Vector2 trueInti = new Vector2(inti[0], inti[1]);
		float trueIntiX = inti[0];
		float trueIntiY = inti[1];

		var persegiBawah = _bentukDasar.PersegiPanjang(trueIntiX-250, trueIntiY, 500, 10);
		DrawPolygon(persegiBawah.ToArray(), new Color[]{ColorUtils.ColorStorage(13)});

		var persegiPenyanggaKiri = _bentukDasar.PersegiPanjang(trueIntiX-230, trueIntiY-170, 20, 200);
		DrawPolygon(persegiPenyanggaKiri.ToArray(), new Color[]{ColorUtils.ColorStorage(13)});

		var lingkaranPenyanggaKiri = _polygonTool.LingkaranPolygon(new Vector2(trueIntiX-220, trueIntiY+40f), 12.5f);
		DrawPolygon(lingkaranPenyanggaKiri.ToArray(), new Color[]{ColorUtils.ColorStorage(13)});

		var persegiPenyanggaKanan = _bentukDasar.PersegiPanjang(trueIntiX+210, trueIntiY-170, 20, 200);
		DrawPolygon(persegiPenyanggaKanan.ToArray(), new Color[]{ColorUtils.ColorStorage(13)});

		var lingkaranPenyanggaKanan = _polygonTool.LingkaranPolygon(new Vector2(trueIntiX+220, trueIntiY+40f), 12.5f);
		DrawPolygon(lingkaranPenyanggaKanan.ToArray(), new Color[]{ColorUtils.ColorStorage(13)});

		var persegiPenyimpan = _bentukDasar.PersegiPanjang(trueIntiX-210, trueIntiY-100, 420, 20);
		var persegiPenyimpanRotated = _transformTool.Rotate(persegiPenyimpan, 8, new Vector2(trueIntiX, trueIntiY-100));
		DrawPolygon(persegiPenyimpanRotated.ToArray(), new Color[]{ColorUtils.ColorStorage(13)});
	}

	private void angklung8Nada(float intiX, float intiY, float tinggi)
	{
		List<Vector2> shape = new List<Vector2>();
		var inti = ConvertToKartesian(intiX, intiY);
		Vector2 trueInti = new Vector2(inti[0], inti[1]);
		float trueIntiX = inti[0];
		float trueIntiY = inti[1];

		var lingkaran = _polygonTool.LingkaranPolygon(new Vector2(trueIntiX, trueIntiY), 10);
		DrawPolygon(lingkaran.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		var persegiPanjang1 = _bentukDasar.PersegiPanjang(trueIntiX-10f, trueIntiY-15f-tinggi, 20, tinggi);
		DrawPolygon(persegiPanjang1.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		var persegiPanjang2 = _bentukDasar.PersegiPanjang(trueIntiX-2.5f, trueIntiY-45f-tinggi, 5, tinggi+45f);
		DrawPolygon(persegiPanjang2.ToArray(), new Color[]{ColorUtils.ColorStorage(15)});

		GraphicsUtils.PutPixelAll(this, shape, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(13));
	}

	public List<float> ConvertToKartesian(float xa, float ya)
	{
		float axis = Mathf.Ceil((float)ScreenUtils.ScreenWidth / 2);
		float ordinat = Mathf.Ceil((float)ScreenUtils.ScreenHeight / 2);
		xa = axis + xa;
		ya = ordinat - ya;

		return new List<float> { xa, ya };
	}

	public void KartesianBorder()
	{
		if (_primitif == null)
		{
			GD.PrintErr("Node _Primitif belum di-assign!");
		}
		int middleXAxis = ScreenUtils.ScreenWidth / 2;
		int middleYAxis = ScreenUtils.ScreenHeight / 2;
		
		List<Vector2> res = new List<Vector2>();

		res.AddRange(_primitif.LineDDA(middleXAxis, ScreenUtils.MarginTop, middleXAxis, ScreenUtils.MarginBottom));
		res.AddRange(_primitif.LineDDA(ScreenUtils.MarginLeft, middleYAxis, ScreenUtils.MarginRight, middleYAxis));
		
		GraphicsUtils.PutPixelAll(this, res, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(99));
	}

	public override void _ExitTree()
	{
		NodeUtils.DisposeAndNull(_bentukDasar, "_bentukDasar");
		NodeUtils.DisposeAndNull(_transformasi, "_transformasi");
		base._ExitTree();
	}

	private void MarginPixel(){
		var margin = _bentukDasar.Margin();
		GraphicsUtils.PutPixelAll(this, margin, GraphicsUtils.DrawStyle.DotDot, ColorUtils.ColorStorage(99));
	}
}	
