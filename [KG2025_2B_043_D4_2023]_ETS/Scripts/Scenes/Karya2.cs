namespace Godot.Scene;

using Godot;
using Godot.Core;
using Godot.Utils;
using System;
using System.Collections.Generic;

public partial class Karya2 : Node2D
{
	private BentukDasar _bentukDasar = new BentukDasar();
	private Transformasi _transformasi = new Transformasi();
	private Primitif _primitif = new Primitif();
	private TransformTool _transformTool = new TransformTool();

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
		shape.AddRange(persegiPanjang1);
		var persegiPanjang2 = _bentukDasar.PersegiPanjang(trueIntiX-55, trueIntiY-100, 5, 200);
		shape.AddRange(persegiPanjang2);
		var persegiPanjang3 = _bentukDasar.PersegiPanjang(trueIntiX+35, trueIntiY-100, 5, 200);
		shape.AddRange(persegiPanjang3);

		// Persegi panjang horizontal
		var persegiPanjang4 = _bentukDasar.PersegiPanjang(trueIntiX-65, trueIntiY-35, 115, 7);
		shape.AddRange(persegiPanjang4);
		var persegiPanjang5 = _bentukDasar.PersegiPanjang(trueIntiX-15, trueIntiY+10, 65, 7);
		shape.AddRange(persegiPanjang5);
		var persegiPanjang6 = _bentukDasar.PersegiPanjang(trueIntiX-65, trueIntiY+70, 115, 10);
		shape.AddRange(persegiPanjang6);

		// Lingkaran kiri kanan
		var lingkaranKiri = _bentukDasar.Lingkaran(new Vector2(trueIntiX-65, trueIntiY+75), 5);
		shape.AddRange(lingkaranKiri);
		var lingkaranKanan = _bentukDasar.Lingkaran(new Vector2(trueIntiX+50, trueIntiY+75), 5);
		shape.AddRange(lingkaranKanan);

		// Persegi kiri kanan
		var persegiKiri = _bentukDasar.Persegi(trueIntiX-30, trueIntiY+65, 5);
		shape.AddRange(persegiKiri);
		var persegiKanan = _bentukDasar.Persegi(trueIntiX+15, trueIntiY+65, 5);
		shape.AddRange(persegiKanan);

		// Trapesium kiri
		var trapesiumKiri1 = _bentukDasar.TrapesiumSiku(new Vector2(trueIntiX-50, trueIntiY+35), 40, 65, 15);
		var rotatedTrapesiumKiri1 = _transformTool.Rotate(trapesiumKiri1, -90, new Vector2(trueIntiX-27.5f, trueIntiY+42.5f));
		shape.AddRange(rotatedTrapesiumKiri1);
		var trapesiumKiri2 = _bentukDasar.TrapesiumSiku(new Vector2(trueIntiX-57.5f, trueIntiY), 90, 100, 10);
		var rotatedTrapesiumKiri2 = _transformTool.Rotate(trapesiumKiri2, -90, new Vector2(trueIntiX-30f, trueIntiY+5));
		shape.AddRange(rotatedTrapesiumKiri2);

		// Trapesium kanan
		var trapesiumKanan1 = _bentukDasar.TrapesiumSiku(new Vector2(trueIntiX, trueIntiY+40), 25, 40, 15);
		var rotatedTrapesiumKanan1 = _transformTool.Rotate(trapesiumKanan1, -90, new Vector2(trueIntiX+17.5f, trueIntiY+47.5f));
		shape.AddRange(rotatedTrapesiumKanan1);
		var trapesiumKanan2 = _bentukDasar.TrapesiumSiku(new Vector2(trueIntiX-20, trueIntiY+5), 50, 60, 10);
		var rotatedTrapesiumKanan2 = _transformTool.Rotate(trapesiumKanan2, -90, new Vector2(trueIntiX+15, trueIntiY+10));
		shape.AddRange(rotatedTrapesiumKanan2);

		var rotatedShape = _transformTool.Rotate(shape, angklungRotation, new Vector2(trueIntiX, trueIntiY));		
		GraphicsUtils.PutPixelAll(this, rotatedShape, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(13));
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

		List<Vector2> bungaLingkaran = new List<Vector2>();
		var lingkaranInti = _bentukDasar.Lingkaran(trueInti, 10);
		bungaLingkaran.AddRange(lingkaranInti);

		var lingkaranKiri = _bentukDasar.Lingkaran(new Vector2(trueIntiX-15, trueIntiY), 15);
		bungaLingkaran.AddRange(lingkaranKiri);
		var lingkaranKanan = _bentukDasar.Lingkaran(new Vector2(trueIntiX+15, trueIntiY), 15);
		bungaLingkaran.AddRange(lingkaranKanan);
		var lingkaranAtas = _bentukDasar.Lingkaran(new Vector2(trueIntiX, trueIntiY-15), 15);
		bungaLingkaran.AddRange(lingkaranAtas);
		var lingkaranBawah = _bentukDasar.Lingkaran(new Vector2(trueIntiX, trueIntiY+15), 15);
		bungaLingkaran.AddRange(lingkaranBawah);
		var rotatedBungaLingkaran = _transformTool.Rotate(bungaLingkaran, bungaRotation, trueInti);
		GraphicsUtils.PutPixelAll(this, rotatedBungaLingkaran, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(99));

		List<Vector2> bungaSegitiga = new List<Vector2>();
		var segitigaUtama = _bentukDasar.SegitigaSamaKaki(new Vector2(trueIntiX, trueIntiY-35), 20, 20);
		for(int i = 0; i < 4; i++)
		{
			var rotatedSegitiga = _transformTool.Rotate(segitigaUtama, 90 * i + 45, trueInti);
			bungaSegitiga.AddRange(rotatedSegitiga);
		}
		var rotatedBungaSegitiga = _transformTool.Rotate(bungaSegitiga, -bungaRotation, trueInti);
		GraphicsUtils.PutPixelAll(this, rotatedBungaSegitiga, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(1));
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
		shape.AddRange(persegiPanjangFondasi);
		var trapesiumFondasi = _bentukDasar.TrapesiumSamaKaki(new Vector2(trueIntiX-425, trueIntiY-20), 850, 875, 20);
		shape.AddRange(trapesiumFondasi);

		// Membuat pilar kiri
		List<Vector2> pilarKiri = new List<Vector2>();
		var persegiPilar = _bentukDasar.PersegiPanjang(trueIntiX-375, trueIntiY-320, 30, 300);
		pilarKiri.AddRange(persegiPilar);
		var segitigaBawah = _bentukDasar.SegitigaSamaKaki(new Vector2(trueIntiX-360, trueIntiY-70), 50, 50);
		pilarKiri.AddRange(segitigaBawah);
		var segitigaAtas = _transformTool.Mirror(segitigaBawah, 0, new Vector2(trueIntiX-360, trueIntiY-170));
		pilarKiri.AddRange(segitigaAtas);
		shape.AddRange(pilarKiri);

		// Membuat pilar kanan yang merupakan hasil mirror pilar kiri
		var pilarKanan = _transformTool.Mirror(pilarKiri, 1, new Vector2(trueIntiX, trueIntiY));		
		shape.AddRange(pilarKanan);

		// Membuat atap
		var fondasiAtap = _bentukDasar.PersegiPanjang(trueIntiX-500, trueIntiY-330, 1000, 10);
		shape.AddRange(fondasiAtap);
		var segitigaKiri = _bentukDasar.SegitigaSiku(new Vector2(trueIntiX-475, trueIntiY-405), 300, 75);
		var mirroredSegitigaKiri = _transformTool.Mirror(segitigaKiri, 1, new Vector2(trueIntiX-325, trueIntiY-405));
		shape.AddRange(mirroredSegitigaKiri);
		var segitigaKanan = _bentukDasar.SegitigaSiku(new Vector2(trueIntiX+175, trueIntiY-405), 300, 75);
		shape.AddRange(segitigaKanan);
		var trapesiumAtap = _bentukDasar.TrapesiumSamaKaki(new Vector2(trueIntiX-125, trueIntiY-530), 250, 500, 200);
		shape.AddRange(trapesiumAtap);

		GraphicsUtils.PutPixelAll(this, shape, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(13));
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
		shape.AddRange(persegiBawah);

		List<Vector2> penyanggaKiri = new List<Vector2>();
		var persegiPenyangga = _bentukDasar.PersegiPanjang(trueIntiX-230, trueIntiY-170, 20, 200);
		penyanggaKiri.AddRange(persegiPenyangga);
		var lingkaranPenyangga = _bentukDasar.Lingkaran(new Vector2(trueIntiX-220, trueIntiY+40f), 12.5f);
		penyanggaKiri.AddRange(lingkaranPenyangga);
		shape.AddRange(penyanggaKiri);

		var penyanggaKanan = _transformTool.Mirror(penyanggaKiri, 1, new Vector2(trueIntiX, trueIntiY));
		shape.AddRange(penyanggaKanan);

		var persegiPenyimpan = _bentukDasar.PersegiPanjang(trueIntiX-210, trueIntiY-100, 420, 20);
		var persegiPenyimpanRotated = _transformTool.Rotate(persegiPenyimpan, 8, new Vector2(trueIntiX, trueIntiY-100));
		shape.AddRange(persegiPenyimpanRotated);

		GraphicsUtils.PutPixelAll(this, shape, GraphicsUtils.DrawStyle.StripStrip, ColorUtils.ColorStorage(13));
	}

	private void angklung8Nada(float intiX, float intiY, float tinggi)
	{
		List<Vector2> shape = new List<Vector2>();
		var inti = ConvertToKartesian(intiX, intiY);
		Vector2 trueInti = new Vector2(inti[0], inti[1]);
		float trueIntiX = inti[0];
		float trueIntiY = inti[1];

		var lingkaran = _bentukDasar.Lingkaran(new Vector2(trueIntiX, trueIntiY), 10);
		shape.AddRange(lingkaran);
		var persegiPanjang1 = _bentukDasar.PersegiPanjang(trueIntiX-10f, trueIntiY-15f-tinggi, 20, tinggi);
		shape.AddRange(persegiPanjang1);
		var persegiPanjang2 = _bentukDasar.PersegiPanjang(trueIntiX-2.5f, trueIntiY-45f-tinggi, 5, tinggi+45f);
		shape.AddRange(persegiPanjang2);

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
