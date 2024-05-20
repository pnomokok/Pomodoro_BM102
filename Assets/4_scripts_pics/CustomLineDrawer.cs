using System.Collections.Generic;
using UnityEngine;

public class CustomLineDrawer : MonoBehaviour
{
    private Material lineMaterial;

    public void SetLineMaterial(Material material)
    {
        lineMaterial = material;
    }

    public void DrawLines(List<Vector3> positions)
    {
        GL.PushMatrix();
        lineMaterial.SetPass(0);
        GL.LoadOrtho();

        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        for (int i = 0; i < positions.Count - 1; i++)
        {
            GL.Vertex(positions[i]);
            GL.Vertex(positions[i + 1]);
        }
        GL.End();

        GL.PopMatrix();
    }
}
