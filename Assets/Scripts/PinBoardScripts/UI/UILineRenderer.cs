using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
[ExecuteAlways]
public class UILineRenderer : MaskableGraphic
{
    [Header("Line Settings")]
    public Color lineColor = Color.red;

    public RectTransform pointA;
    public RectTransform pointB;

    [Range(1f, 10f)]
    public float thickness = 3f;

    private Vector2[] positions = new Vector2[2];

    public void SetPoints(RectTransform a, RectTransform b)
    {
        pointA = a;
        pointB = b;
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

        // 4 точки пр€моугольника линии
        UIVertex[] verts = new UIVertex[4];
        verts[0].position = start - normal;
        verts[1].position = start + normal;
        verts[2].position = end + normal;
        verts[3].position = end - normal;

        for (int i = 0; i < 4; i++)
        {
            verts[i].color = lineColor;

        }

        vh.AddUIVertexQuad(verts);
    }

    private void Update()
    {
        // чтобы лини€ обновл€лась в реальном времени при движении карточек
        if (pointA != null && pointB != null)
            SetVerticesDirty();
    }
}
