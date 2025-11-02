using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class UILineRenderer : MaskableGraphic
{
    public RectTransform pointA;
    public RectTransform pointB;
    public float thickness = 3f;
    public Color lineColor = Color.red;

    protected override void Awake()
    {
        base.Awake();
        color = lineColor; // базовый цвет
    }

    public void SetPoints(RectTransform a, RectTransform b)
    {
        pointA = a; pointB = b;
        SetVerticesDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        if (pointA == null || pointB == null) return;

        Vector2 start = transform.InverseTransformPoint(pointA.position);
        Vector2 end = transform.InverseTransformPoint(pointB.position);
        Vector2 dir = (end - start).normalized;
        Vector2 normal = new Vector2(-dir.y, dir.x) * (thickness / 2f);

        UIVertex v = UIVertex.simpleVert;
        v.color = lineColor;

        var verts = new UIVertex[4];
        verts[0] = v; verts[0].position = start - normal;
        verts[1] = v; verts[1].position = start + normal;
        verts[2] = v; verts[2].position = end + normal;
        verts[3] = v; verts[3].position = end - normal;

        vh.AddUIVertexQuad(verts);
    }

    private void Update()
    {
        if (pointA != null && pointB != null)
            SetVerticesDirty();
    }
}
