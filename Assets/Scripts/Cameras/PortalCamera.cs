using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform otherPortal;
    public Transform playerCamera;
    public Transform portal;

    private void Update()
    {
        float angularDifBetweenPortalsRotation = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        Vector3 offsetPortal = playerCamera.position - otherPortal.position;

        transform.position = portal.position + offsetPortal;
        Quaternion portalRotation = Quaternion.AngleAxis(angularDifBetweenPortalsRotation, Vector3.up);
        Vector3 newCamera = portalRotation * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCamera, Vector3.up);
    }
}
