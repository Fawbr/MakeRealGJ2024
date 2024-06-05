using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointDetection : MonoBehaviour
{
    [SerializeField] public List<GameObject> nearbyWaypoints = new List<GameObject>();
    public int GCost, FCost, HCost;
    public GameObject cameFromWaypoint;
    public int CalculateFCost()
    {
        return GCost + HCost; 
    }
}
