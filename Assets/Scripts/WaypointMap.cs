using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMap : MonoBehaviour
{
    [SerializeField] List<GameObject> waypoints = new List<GameObject>();
    [SerializeField] float threshold;
    
    float limitX = 30f;
    float limitZ = 30f;
    [SerializeField] LayerMask wallMask;
    Color color = new Color(255f, 255f, 255f);

    // Start is called before the first frame update
    void Start()
    {
        waypoints = GetWaypoints();
        CreateConnections();
    }

    // Update is called once per frame
    void Update()
    {
        
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
                if (waypointDistance < threshold)
                {
                    RaycastHit hit;
                    if (!Physics.Raycast(waypoint.transform.position, waypointComparison.transform.position, out hit, wallMask))
                    {
                        Debug.DrawLine(waypoint.transform.position, waypointComparison.transform.position, color, 1000f);
                    }
                }
            }
            /*
            // Gets the individual waypoints in the list, casts a box cast on them, gets all points from that boxcast.
            RaycastHit[] areaInputs = Physics.BoxCastAll(new Vector3(waypoint.transform.position.x, waypoint.transform.position.y, waypoint.transform.position.z), 
            new Vector3(boxCastLimits, boxCastLimits, boxCastLimits), -transform.up, Quaternion.identity);
            
            //Gets all of the waypoints by filtering through tag.
            List<RaycastHit> waypointsFound = areaInputs.ToList().FindAll(valid => valid.transform.tag == "Waypoint");

            //Way to confirm that the waypoints have indeed been found.
            Color RandColour = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

            //Iterates through all of the raycasthits in the boxcastall.
            foreach (RaycastHit foundPoint in waypointsFound)
            {
                // Changes the colour of hit points in order to verify they've been received.
                foundPoint.transform.GetComponent<Renderer>().material.color = RandColour;       

                RaycastHit waypointHit;
                //Draws a line so long as there isn't a wall blocking the raycast.
                Vector3 dir = (waypoint.transform.position - foundPoint.transform.position).normalized;
                if (!Physics.Raycast(waypoint.transform.position, dir, out waypointHit, Mathf.Infinity, wallMask))
                {
                    if (waypointHit) continue;
                    Debug.DrawLine(waypoint.transform.position, foundPoint.transform.position, color, 1000f);
                }

            }*/
            
            /*
            Debug.Log(areaInputs.Length);
            foreach (RaycastHit hit in areaInputs)
            {
                if (hit.transform.tag != "Waypoint") continue;
                

                if (Physics.Raycast(waypoint.transform.position, hit.transform.position, out RaycastHit waypointHit))
                {
                    Debug.Log("aaa");
                    Debug.DrawLine(waypoint.transform.position, waypointHit.transform.position, color, 1000f);

                    if (waypointHit.transform.tag != "Waypoint") continue;
                    
                    //Debug.DrawLine(waypoint.transform.position, waypointHit.transform.position, color, 20f);
                    
                }
                
            }*/
        }
    }

    void OnDrawGizmos()
    {

    }
}
