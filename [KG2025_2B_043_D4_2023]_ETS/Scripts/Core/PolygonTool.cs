namespace Godot.Core;

using Godot;
using System;
using System.Collections.Generic;
using System.Numerics; // Import System.Numerics for Matrix4x4
using System.Linq;

public partial class PolygonTool : RefCounted
{
    private TransformasiFast _transformasi = new TransformasiFast();
    private BentukDasar _bentukDasar = new BentukDasar();

    // Function to sort circle points for DrawPolygon
    public List<Godot.Vector2> SortCirclePointsForDrawPolygon(List<Godot.Vector2> unsortedPoints)
    {
        if (unsortedPoints.Count == 0)
            return unsortedPoints;
        
        // Find the center by averaging all points
        Godot.Vector2 center = new Godot.Vector2(0, 0);
        foreach (Godot.Vector2 point in unsortedPoints)
        {
            center += point;
        }
        center /= unsortedPoints.Count;
        
        // Create a list to store points with their angles
        List<(Godot.Vector2 point, float angle)> pointsWithAngles = new List<(Godot.Vector2, float)>();
        
        // Calculate the angle of each point relative to the center
        foreach (Godot.Vector2 point in unsortedPoints)
        {
            float deltaX = point.X - center.X;
            float deltaY = point.Y - center.Y;
            float angle = Mathf.Atan2(deltaY, deltaX);
            
            // Convert angle to range [0, 2π) for easier sorting
            if (angle < 0)
                angle += 2 * Mathf.Pi;
                
            pointsWithAngles.Add((point, angle));
        }
        
        // Sort by angle
        pointsWithAngles.Sort((a, b) => a.angle.CompareTo(b.angle));
        
        // Extract the sorted points
        List<Godot.Vector2> sortedPoints = new List<Godot.Vector2>();
        foreach (var item in pointsWithAngles)
        {
            sortedPoints.Add(item.point);
        }
        
        return sortedPoints;
    }

    // Function to sort ellipse points for DrawPolygon
    public List<Godot.Vector2> SortEllipsePointsForDrawPolygon(List<Godot.Vector2> unsortedPoints)
    {
        // This function is identical to SortCirclePointsForDrawPolygon as the sorting process is the same
        return SortCirclePointsForDrawPolygon(unsortedPoints);
    }
    // Function to sort half-circle points for DrawPolygon without using LINQ

    // For drawing the circle
    public List<Godot.Vector2> LingkaranPolygon(Godot.Vector2 titikAwal, float radius)
    {
        // Generate points using your original midpoint algorithm
        List<Godot.Vector2> points = _bentukDasar.Lingkaran(titikAwal, radius);
        
        // Sort the points for DrawPolygon
        List<Godot.Vector2> sortedPoints = SortCirclePointsForDrawPolygon(points);
        
        return sortedPoints;
    }

    // For drawing the ellipse
    public List<Godot.Vector2> ElipsPolygon(Godot.Vector2 titikAwal, float radiusX, float radiusY)
    {
        // Generate points using your original midpoint algorithm
        List<Godot.Vector2> points = _bentukDasar.Elips(titikAwal, radiusX, radiusY);
        
        // Sort the points for DrawPolygon
        List<Godot.Vector2> sortedPoints = SortEllipsePointsForDrawPolygon(points);
        
        return sortedPoints;
    }

    public List<Godot.Vector2> PolygonRotatedShape(List<Godot.Vector2> rotatedShape)
    {
        // Re-sort the rotated points to ensure they form a continuous outline
        // First, find the center point of the rotated shape
        Godot.Vector2 center = new Godot.Vector2(0, 0);
        foreach (Godot.Vector2 point in rotatedShape)
        {
            center += point;
        }
        center /= rotatedShape.Count;
        
        // Create a list to store points with their angles
        List<(Godot.Vector2 point, float angle)> pointsWithAngles = new List<(Godot.Vector2, float)>();
        
        // Calculate the angle of each point relative to the center
        foreach (Godot.Vector2 point in rotatedShape)
        {
            float deltaX = point.X - center.X;
            float deltaY = point.Y - center.Y;
            float angle = Mathf.Atan2(deltaY, deltaX);
            
            // Convert angle to range [0, 2π) for easier sorting
            if (angle < 0)
                angle += 2 * Mathf.Pi;
                
            pointsWithAngles.Add((point, angle));
        }
        
        // Sort by angle
        pointsWithAngles.Sort((a, b) => a.angle.CompareTo(b.angle));
        
        // Extract the sorted points
        List<Godot.Vector2> sortedPoints = new List<Godot.Vector2>();
        foreach (var item in pointsWithAngles)
        {
            sortedPoints.Add(item.point);
        }
        
        return sortedPoints;
    }
}