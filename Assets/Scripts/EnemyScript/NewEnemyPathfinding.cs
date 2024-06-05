using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewEnemyPathfinding : MonoBehaviour
{
    [Header("Pathfinding Navigation")]
    [SerializeField] WaypointMap waypointMap;
    [SerializeField] Pathfinding pathfinding;
    [SerializeField] Transform playerTarget;
    [SerializeField] GameObject currentWaypoint;

    [Header("Enemy Variables")]
    [SerializeField] NavMeshAgent navMesh;
    [SerializeField] float enemySpeed;
    bool isSeen = false;

    [Header("Navigation Variables")]
    float distanceToClosestNode = 100f;
    float detectionTimer = 0f;
    [SerializeField] GameObject closestWaypointToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        detectionTimer += Time.deltaTime;
        if (detectionTimer >= 5f)
        {
            closestWaypointToPlayer = GetClosestNodeToPlayer();
            pathfinding.FindPath(currentWaypoint, closestWaypointToPlayer);
            detectionTimer = 0f;
        }
    }

    GameObject GetClosestNodeToPlayer()
    {
        distanceToClosestNode = 100f;
        GameObject closestNode = null;
        foreach (GameObject waypoint in waypointMap.waypoints)
        {
            float nodeDistance = (playerTarget.position - waypoint.transform.position).magnitude;
            Vector3 direction = (playerTarget.position - waypoint.transform.position);
            if (Physics.Raycast(waypoint.transform.position, direction, out RaycastHit hit, nodeDistance, waypointMap.wallMask))
                continue;
            if (nodeDistance < distanceToClosestNode)
            {
                distanceToClosestNode = nodeDistance;
                closestNode = waypoint;
            }
        }
        return closestNode;
    }

    void MoveThroughPath()
    {
    
    }
}
