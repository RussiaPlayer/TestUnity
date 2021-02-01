using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public LineRenderer lr;
    public DistanceJoint2D joint;
    public GameObject projectiel;

    private void LateUpdate()
    {
        DrawRope();
    }

    public void EnableRope()
    {
        joint.connectedAnchor = projectiel.transform.position;
        joint.enabled = true;
    }

    public void DisableRope()
    {
        joint.enabled = false;
    }

    void DrawRope()
    {
        lr.SetPosition(0, this.transform.position);
        lr.SetPosition(1, projectiel.transform.position);
    }
}
