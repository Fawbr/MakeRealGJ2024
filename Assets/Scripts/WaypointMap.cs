using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMap : MonoBehaviour
{
    [SerializeField] public List<GameObject> waypoints = new List<GameObject>();
    [SerializeField] float threshold;

    [SerializeField] GameObject TestStart, TestEnd;
    
    
    float limitXPos = 30f, limitXNeg = -30f, limitZPos = 30f, limitZNeg = -30f;
    
    GameObject xPosWaypoint = null, xNegWaypoint = null, zPosWaypoint = null, zNegWaypoint = null; 
    [SerializeField] public LayerMask wallMask, waypointMask;
    Color white = new Color(255f, 255f, 255f);
    Color red = new Color(255f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        waypoints = GetWaypoints();
        CreateConnections();
        gameObject.GetComponent<Pathfinding>().FindPath(TestStart, TestEnd);
    }

    private List<GameObject> GetWaypoints()
    {
        // Gets list of waypoints through children of parent object.
        List<GameObject> getGameObjects = new List<GameObject>();
        foreach (Transform child in transform)
        {
            getGameObjects.Add(child.gameObject);
        }
        return getGameObjects;
    }

    private void CreateConnections()
    {
        Debug.Log($"Amount of waypoints is: {waypoints.Count}");
        foreach (GameObject waypoint in waypoints)
        {
            foreach(GameObject waypointComparison in waypoints)
            {
                if (waypoint == waypointComparison) continue;

                float waypointDistance = (waypoint.transform.position - waypointComparison.transform.position).magnitude;
                Vector3 waypointDirection = (waypointComparison.transform.position - waypoint.transform.position);
                if (waypointDistance < threshold)
                {
                    RaycastHit hit;
                    if (!Physics.Raycast(waypoint.transform.position, waypointDirection, out hit, wallMask))
                    {   
                        if (Physics.Raycast(waypoint.transform.position, transform.right, out hit, waypointMask))
                        {
                            if (hit.transform.tag == "Waypoint")
                                xPosWaypoint = hit.transform.gameObject;
                        }
                        if (Physics.Raycast(waypoint.transform.position, -transform.right, out hit, waypointMask))
                        {
                            if (hit.transform.tag == "Waypoint")
                                xNegWaypoint = hit.transform.gameObject;
                        }
                        if (Physics.Raycast(waypoint.transform.position, transform.forward, out hit, waypointMask))
                        {
                            if (hit.transform.tag == "Waypoint")
                                zPosWaypoint = hit.transform.gameObject;
                        }
                        if (Physics.Raycast(waypoint.transform.position, -transform.forward, out hit, waypointMask))
                        {
                            if (hit.transform.tag == "Waypoint")
                                zNegWaypoint = hit.transform.gameObject;
                        }
                    }
                }
            }
            WaypointDetection waypointDetection = waypoint.GetComponent<WaypointDetection>();
            if (zPosWaypoint != null)
                waypointDetection.nearbyWaypoints.Add(zPosWaypoint);
            if (zNegWaypoint != null)
                waypointDetection.nearbyWaypoints.Add(zNegWaypoint);
            if (xPosWaypoint != null)
                waypointDetection.nearbyWaypoints.Add(xPosWaypoint);
            if (xNegWaypoint != null)
                waypointDetection.nearbyWaypoints.Add(xNegWaypoint);
            
            xNegWaypoint = null;
            xPosWaypoint = null;
            zNegWaypoint = null;
            zPosWaypoint = null;

        }
    }

    void OnDrawGizmos()
    {

    }
}
