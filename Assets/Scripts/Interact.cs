using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{

    public bool isInteracting = false;


  

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            isInteracting = true;
        }
        else if(Input.GetKeyUp(KeyCode.F))
        {
            isInteracting = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Interactable" && isInteracting)
        {
            print("You are interacting");
        }
    }
}
