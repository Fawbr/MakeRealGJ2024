using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScanner : MonoBehaviour
{
    [SerializeField] GameObject scanner;
    [SerializeField] Material scannerMat;
    float scannerTimer;
    bool scannerPing;
    void Update()
    {
        scannerTimer += Time.deltaTime;
        if (scannerTimer > 3f)
        {
            scannerPing = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (scannerPing && collision.gameObject.tag == "HighlightObject")
        {
            Vector3 position = collision.contacts[0].point;
            GameObject scannerObject = Instantiate(scanner);
            Material[] objectMats = collision.gameObject.GetComponent<MeshRenderer>().materials;
            objectMats[1] = scannerMat;
            collision.gameObject.GetComponent<MeshRenderer>().materials = objectMats;
            scannerObject.transform.position = position;
            scannerPing = false;
            scannerTimer = 0f;
        }
    }
}
