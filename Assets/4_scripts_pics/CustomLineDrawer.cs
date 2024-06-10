using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))] // Bu bileþenin CanvasRenderer bileþeni gerektirdiðini belirtir
public class CustomLineDrawer : MaskableGraphic
{
    // Çizim yapýlacak noktalarýn listesi
    public List<Vector2> points;

    // Mesh'i doldurmak için kullanýlan fonksiyon
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear(); // Mevcut mesh verilerini temizler

        // Eðer çizim yapýlacak yeterli nokta yoksa, fonksiyonu sonlandýrýr
        if (points == null || points.Count < 2)
            return;

        float width = 5f; // Çizgi geniþliði
        Vector2 prev = points[0]; // Ýlk noktayý alýr

        // Her bir nokta çiftini alarak çizgi segmentlerini ekler
        for (int i = 1; i < points.Count; i++)
        {
            Vector2 curr = points[i];
            AddLineSegment(vh, prev, curr, width);
            prev = curr;
        }
    }

    // Çizgi segmenti ekler
    private void AddLineSegment(VertexHelper vh, Vector2 start, Vector2 end, float width)
    {
        Vector2 dir = (end - start).normalized; // Ýki nokta arasýndaki yön
        Vector2 normal = new Vector2(-dir.y, dir.x); // Yönün dik açýsý

        // Çizgi segmentinin dört köþe noktasýný hesaplar
        Vector2 v0 = start - normal * width;
        Vector2 v1 = start + normal * width;
        Vector2 v2 = end - normal * width;
        Vector2 v3 = end + normal * width;

        int idx = vh.currentVertCount; // Mevcut vertex sayýsýný alýr

        // Dört köþe noktasýný mesh'e ekler
        vh.AddVert(v0, color, new Vector2(0, 0));
        vh.AddVert(v1, color, new Vector2(0, 1));
        vh.AddVert(v2, color, new Vector2(1, 0));
        vh.AddVert(v3, color, new Vector2(1, 1));

        // Ýki üçgen oluþturarak çizgi segmentini oluþturur
        vh.AddTriangle(idx, idx + 1, idx + 2);
        vh.AddTriangle(idx + 2, idx + 1, idx + 3);
    }
}
