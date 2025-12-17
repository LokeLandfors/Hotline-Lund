using UnityEngine;

public class PathPointGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.GetChild(i).transform.position, 0.3f);
        }
    }
}
