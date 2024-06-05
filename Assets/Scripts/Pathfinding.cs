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
    Color red = new Color(255f, 0f, 0f);

    public void FindPath(GameObject startNode, GameObject endNode)
    {
        List<GameObject> nodePath = new List<GameObject>();
        HashSet<GameObject> exploredNodes = new HashSet<GameObject>();
        Queue<GameObject> queue = new Queue<GameObject>();
        GameObject currentNode;
        queue.Enqueue(startNode);
        while (queue.Count != 0)
        {
            currentNode = queue.Dequeue();
            if (currentNode == endNode)
            {
                nodePath.Add(currentNode);
                GeneratePath(currentNode, startNode);
            }
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
    }

    public void GeneratePath(GameObject searchNode, GameObject endNode)
    {
        foreach (var node in nodeParent)
        {
            Debug.Log(node.Value, node.Key);
            if (node.Key == searchNode)
            {
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
                    return;
                }
                nodeList.Add(node.Value);
                GeneratePath(node.Value, endNode);
            }
        }
    }
}
