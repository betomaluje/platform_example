using UnityEngine;
using System.Collections;

public class DebugGizmo : MonoBehaviour
{
 private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.down) * 2;
        Gizmos.DrawLine(transform.position, direction);
    }
}
