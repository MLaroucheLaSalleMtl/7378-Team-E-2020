using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFOV : MonoBehaviour
{
    public List<Transform> visibleObjects;
    [SerializeField]
    private bool smthInFOV;
    [SerializeField]
    private bool isPlayerInFOV;
    [SerializeField]
    private Transform suspectedObject;
    [SerializeField]
    private Transform player;
    [SerializeField]
    [Range(0, 360)]
    private float viewAngle;
    [SerializeField]
    [Range(0, 20)]
    private float viewRadius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(viewAngle, transform.up) * transform.forward * viewRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-viewAngle, transform.up) * transform.forward * viewRadius;

        //NPC's view angle
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        //Gizmos.color = (!isPlayerInFOV) ? Gizmos.color = Color.green : Gizmos.color = Color.red;

        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, (player.transform.position - transform.position).normalized * viewRadius);
        }

        if (suspectedObject != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, (suspectedObject.transform.position - transform.position).normalized * viewRadius);
        }

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * viewRadius);
    }

    public void InFOV()
    {
        Collider[] objInRadiusView = Physics.OverlapSphere(transform.position, viewRadius + 5f);

        for (int i = 0; i < objInRadiusView.Length; i++)
        {
            Transform obj = objInRadiusView[i].transform;
            Vector3 dirToTarget = (obj.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) <= viewAngle)
            {
                Ray ray = new Ray(transform.position, obj.transform.position - transform.position);
                //float disTarget = Vector3.Distance(transform.position, obj.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, viewRadius + 5f))
                {
                    if (hit.transform == obj.transform)
                    {
                        if (!visibleObjects.Contains(obj))
                            visibleObjects.Add(obj);
                        if(obj.tag == "Terrain")
                            visibleObjects.Remove(obj);
                    }
                }
            }
            else
                visibleObjects.Remove(obj);
        }
    }
    public void SetSuspectedObjectToNull() => suspectedObject = null;
    public Transform GetSuspectedObject() => suspectedObject;
    public void SetPlayerToNull() => player = null;
    public Transform GetPlayer() => player;
    public bool CanSeeSmth()
    {
        foreach (Transform visibleObj in visibleObjects)
        {
            if (Vector3.Distance(transform.position, visibleObj.transform.position) > 4 && Vector3.Distance(transform.position, visibleObj.transform.position) <= viewRadius + 5f)
            {
                //Recognize specified objects only
                if (visibleObj.tag == "Rock" || visibleObj.tag == "Coin" || visibleObj.tag == "Player")
                {
                    suspectedObject = visibleObj;
                    return true;
                }
            }
        }
        suspectedObject = null;
        return false;
    }

    public bool IsSuspectedObject(bool value) => smthInFOV = value;

    public bool CanSeePlayer()
    {
        foreach (Transform visibleObj in visibleObjects)
        {
            if (visibleObj.tag == "Player" && Vector3.Distance(transform.position, visibleObj.transform.position) <= viewRadius)
            {
                player = visibleObj;
                return true;
            }
        }
        return false;
    }
    private void FixedUpdate()
    {
        InFOV();
        isPlayerInFOV = CanSeePlayer();
        smthInFOV = CanSeeSmth();
    }
}
