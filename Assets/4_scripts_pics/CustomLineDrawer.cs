using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))] // Bu bile�enin CanvasRenderer bile�eni gerektirdi�ini belirtir
public class CustomLineDrawer : MaskableGraphic
{
    // �izim yap�lacak noktalar�n listesi
    public List<Vector2> points;

    // Mesh'i doldurmak i�in kullan�lan fonksiyon
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear(); // Mevcut mesh verilerini temizler

        // E�er �izim yap�lacak yeterli nokta yoksa, fonksiyonu sonland�r�r
        if (points == null || points.Count < 2)
            return;

        float width = 5f; // �izgi geni�li�i
        Vector2 prev = points[0]; // �lk noktay� al�r

        // Her bir nokta �iftini alarak �izgi segmentlerini ekler
        for (int i = 1; i < points.Count; i++)
        {
            Vector2 curr = points[i];
            AddLineSegment(vh, prev, curr, width);
            prev = curr;
        }
    }

    // �izgi segmenti ekler
    private void AddLineSegment(VertexHelper vh, Vector2 start, Vector2 end, float width)
    {
        Vector2 dir = (end - start).normalized; // �ki nokta aras�ndaki y�n
        Vector2 normal = new Vector2(-dir.y, dir.x); // Y�n�n dik a��s�

        // �izgi segmentinin d�rt k��e noktas�n� hesaplar
        Vector2 v0 = start - normal * width;
        Vector2 v1 = start + normal * width;
        Vector2 v2 = end - normal * width;
        Vector2 v3 = end + normal * width;

        int idx = vh.currentVertCount; // Mevcut vertex say�s�n� al�r

        // D�rt k��e noktas�n� mesh'e ekler
        vh.AddVert(v0, color, new Vector2(0, 0));
        vh.AddVert(v1, color, new Vector2(0, 1));
        vh.AddVert(v2, color, new Vector2(1, 0));
        vh.AddVert(v3, color, new Vector2(1, 1));

        // �ki ��gen olu�turarak �izgi segmentini olu�turur
        vh.AddTriangle(idx, idx + 1, idx + 2);
        vh.AddTriangle(idx + 2, idx + 1, idx + 3);
    }
}
