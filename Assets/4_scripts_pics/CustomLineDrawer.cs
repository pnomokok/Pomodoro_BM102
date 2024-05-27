using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class CustomLineDrawer : MaskableGraphic
{
    public List<Vector2> points;
    //public float lineWidth = 5f;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        if (points == null || points.Count < 2)
            return;

        float width = 5f;
        Vector2 prev = points[0];

        for (int i = 1; i < points.Count; i++)
        {
            Vector2 curr = points[i];
            AddLineSegment(vh, prev, curr, width);
            prev = curr;
        }
    }

    private void AddLineSegment(VertexHelper vh, Vector2 start, Vector2 end, float width)
    {
        Vector2 dir = (end - start).normalized;
        Vector2 normal = new Vector2(-dir.y, dir.x);

        Vector2 v0 = start - normal * width;
        Vector2 v1 = start + normal * width;
        Vector2 v2 = end - normal * width;
        Vector2 v3 = end + normal * width;

        int idx = vh.currentVertCount;

        vh.AddVert(v0, color, new Vector2(0, 0));
        vh.AddVert(v1, color, new Vector2(0, 1));
        vh.AddVert(v2, color, new Vector2(1, 0));
        vh.AddVert(v3, color, new Vector2(1, 1));

        vh.AddTriangle(idx, idx + 1, idx + 2);
        vh.AddTriangle(idx + 2, idx + 1, idx + 3);
    }
}
