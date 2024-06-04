using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] Transform playerTarget;
    [SerializeField] GameObject currentWaypoint;
    [SerializeField] float enemySpeed = 3f;
    [SerializeField] LayerMask layerMask;
    bool isSeen = false;
    List<GameObject> nodeChoices = new List<GameObject>();
    List<GameObject> nodesTravelled = new List<GameObject>();
    GameObject[] allNodes;
    EnemyFieldOfView enemyFOV;
    [SerializeField] public bool isWandering = true;
    Rigidbody rb;
    Animator enemyAnimator;
    GameObject nextNode;
    private NavMeshAgent nMA;
    int nextNodeInt;
    float freezeTimer = 0f;
    float aggressionTimer = 0f;
    float huntingTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyFOV = GetComponent<EnemyFieldOfView>();
        nMA = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponentInChildren<Animator>();
        allNodes = GameObject.FindGameObjectsWithTag("Waypoints");

    }

    void Update()
    {
        nMA.speed = enemySpeed;
        Transform waypointTransform = currentWaypoint.transform;

        if (aggressionTimer > Random.Range(15f, 45f))
        {
            aggressionTimer = 0f;
            isWandering = false;
        }
        if (huntingTimer > Random.Range(15f, 30f))
        {
            huntingTimer = 0f;
            isWandering = true;
        }

        if (isWandering == true)
        {
            aggressionTimer += Time.deltaTime;
        }
        if (isWandering == false)
        {
            huntingTimer += Time.deltaTime;
        }

        if (enemyFOV.playerVisible == true || isWandering == false)
        {
            nMA.SetDestination(playerTarget.position);
            if (!isSeen)
            {
                enemySpeed = 10f;
            }
            
        }
        else if (enemyFOV.playerVisible == false)
        {   
            nMA.SetDestination(waypointTransform.position);
            //nMA.speed = 3;
        }
    }

    GameObject FindNextWayPoint()
    {
        int nodeAmount = nodesTravelled.Count;
        WaypointDetection waypointNodes = currentWaypoint.GetComponent<WaypointDetection>();
        foreach (GameObject node in waypointNodes.nearbyWaypoints)
        {
            nodeChoices.Add(node);
        }
        if (nodeChoices.Count > 1)
        {
            nextNodeInt = Random.Range(0, nodeChoices.Count);
        }
        else
        {
            nextNodeInt = 0;
        }

        nextNode = nodeChoices[nextNodeInt];
        nodeChoices.Clear();
        return nextNode;
    }
    private void EnemyTeleportAway()
    {
        List<GameObject> possibleNodes = new List<GameObject>();
        foreach (GameObject waypoints in allNodes)
        {
            if (Vector3.Distance(playerTarget.transform.position, waypoints.transform.position) > 50f)
            {
                possibleNodes.Add(waypoints);
            }
        }

        int chosenNode = Random.Range(0, possibleNodes.Count);
        nextNode = possibleNodes[chosenNode];
        //transform.position = new Vector3(possibleNodes[chosenNode].transform.position.x, transform.position.y, possibleNodes[chosenNode].transform.position.z);
        nMA.Warp(new Vector3(possibleNodes[chosenNode].transform.position.x, transform.position.y, possibleNodes[chosenNode].transform.position.z));
        possibleNodes.Clear();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, playerTarget.position, out hit))
            {
                if (hit.collider.gameObject.name == "Player")
                {
                    isSeen = true;
                    enemySpeed = 0f;
                    rb.MoveRotation(Quaternion.LookRotation(playerTarget.transform.position - transform.position));
                    enemyAnimator.SetTrigger("Idling");
                    nMA.velocity = new Vector3(0, 0, 0);
                    freezeTimer += Time.deltaTime;
                    if (freezeTimer >= 3f)
                    {
                        EnemyTeleportAway();
                        freezeTimer = 0f;
                    }

                }
                else
                {
                    freezeTimer = 0f;
                    enemySpeed = 5f;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Waypoints")
        {
            nodesTravelled.Add(currentWaypoint);
            currentWaypoint = FindNextWayPoint();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isSeen = false;
            enemyAnimator.SetTrigger("Moving");
            enemySpeed = 5f;
        }
    }

}
