using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private int iterations = 0;
    [SerializeField] WaypointMap waypointMap;
    public IDictionary<GameObject, GameObject> nodeParent = new Dictionary<GameObject, GameObject>();
    WaypointDetection waypointDetection;
    [SerializeField] private List<GameObject> nodeList;
    public Color red = new Color(255f, 0f, 0f);

    public List<GameObject> FindPath(GameObject startNode, GameObject endNode)
    {
        // FindPath creates a queue of all possible nodes and grabs the connections made between them through the WaypointMap system. 
        // A hashset is used in order to verify whether or not a node has already been "explored" to ignore it.
        nodeList.Clear();
        nodeParent.Clear();
        List<GameObject> nodePath = new List<GameObject>();
        HashSet<GameObject> exploredNodes = new HashSet<GameObject>();
        Queue<GameObject> queue = new Queue<GameObject>();
        GameObject currentNode;
        queue.Enqueue(startNode);
        while (queue.Count != 0)
        {
            currentNode = queue.Dequeue();
            // If the current node IS our end node, it means we've found all the connections necessary to reach there in the shortest amount of time, so it's time to generate that stored path.
            if (currentNode == endNode)
            {
                return GeneratePath(currentNode, startNode);
            }

            // The queue checks the connections between the current node, gets their parent node and connects them accordingly. Then they all get added to the queue.
            List<GameObject> travelNodes = currentNode.GetComponent<WaypointDetection>().nearbyWaypoints;
            foreach (GameObject node in travelNodes)
            {
                if(!exploredNodes.Contains(node))
                {
                    exploredNodes.Add(node);
                    nodeParent.Add(node, currentNode);
                    queue.Enqueue(node);
                }
            }
        }
        return null;
    }

    public List<GameObject> GeneratePath(GameObject searchNode, GameObject endNode)
    {
        foreach (var node in nodeParent)
        {
            if (node.Key == searchNode)
            {
                nodeList.Add(node.Key);
                if (node.Key == endNode)
                {
                    GameObject nextNode = null;
                    foreach(GameObject nodeInList in nodeList)
                    {
                        if (nextNode != null)
                        {
                            Debug.DrawLine(nodeInList.transform.position, nextNode.transform.position, red, 1000f);
                        }
                        nextNode = nodeInList;
                    }
                    return nodeList;
                }
                GeneratePath(node.Value, endNode);
            }
        }
        return null;
    }
}
