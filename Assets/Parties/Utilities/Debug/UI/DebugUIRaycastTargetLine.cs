	

using UnityEngine;
using UnityEngine.UI;

public class DebugUIRaycastTargetLine : MonoBehaviour
{
    static Vector3[] fourCorners = new Vector3[4];

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        foreach (MaskableGraphic g in GameObject.FindObjectsOfType<MaskableGraphic>())
        {
            if (g.raycastTarget)
            {
                RectTransform rectTransform = g.transform as RectTransform;
                rectTransform.GetWorldCorners(fourCorners);
                Gizmos.color = Color.blue;
                for (int i = 0; i < 4; i++)
                    Gizmos.DrawLine(fourCorners[i], fourCorners[(i + 1) % 4]);

            }
        }
    }
#else

    private Material material;

    private void Start()
    {
        material = Resources.Load("line") as Material;
    }

    private void Update()
    {
        foreach (MaskableGraphic g in GameObject.FindObjectsOfType<MaskableGraphic>())
        {
            if (g.raycastTarget)
                {
                    RectTransform rectTransform = g.transform as RectTransform;
                    rectTransform.GetWorldCorners(fourCorners);
                    LineRenderer lr = rectTransform.GetComponent<LineRenderer>();
                    if (null == lr)
                    {
                        lr = rectTransform.gameObject.AddComponent<LineRenderer>();
                    }
                    lr.positionCount = fourCorners.Length;
                    lr.SetPositions(fourCorners);
                    lr.useWorldSpace = true;
                    lr.loop = true;
                    lr.material = material;
                }
        }
    }
#endif
}


