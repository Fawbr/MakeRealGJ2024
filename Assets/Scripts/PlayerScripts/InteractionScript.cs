using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public int bodiesCollected = 0;
    Camera playerCam;
    float time = 0f;
    bool disappear = false;
    [SerializeField] LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        playerCam = gameObject.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, playerCam.transform.TransformDirection(Vector3.forward), out hit, 10, layerMask))
            {
                if (hit.transform.tag == "Item")
                { 
                    bodiesCollected++;
                    hit.transform.gameObject.SetActive(false);
                    text.enabled = true;
                }
            }
        }

        if (text.enabled)
        {
            text.text = "Bodies collected " + bodiesCollected.ToString() + "/5";
            if (!disappear)
            {
                time += Time.deltaTime;
                text.alpha = (time / 2);
            }
            if (disappear)
            {
                time -= Time.deltaTime;
                text.alpha = (time / 2);
                if (text.alpha <= 0f)
                {
                    time = 0f; 
                    disappear = false;
                    text.enabled = false;
                }
            }

            if (time >= 3f)
            {
                disappear = true;
            } 
        }
    }

    void CheckObject()
    {
        
    }
}
