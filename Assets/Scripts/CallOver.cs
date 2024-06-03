using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CallOver : MonoBehaviour
{
    [SerializeField] GameObject richard;
    [SerializeField] GameObject jack;

    public int speed;

    bool movingJack = false;
    bool movingRichard = false;
    public void CallJack()
    {
        
        NavMeshAgent jackNav = jack.GetComponent<NavMeshAgent>();
        Vector3 richardPos = new Vector3(richard.transform.position.x, richard.transform.position.y, richard.transform.position.z);
        jackNav.enabled = true;
        jackNav.speed = speed;
        jackNav.destination = richardPos;
        //jackNav.Move(richardPos);
        movingJack = true;
        
        if(jack.transform.position == richard.transform.position)
        {
            jackNav.enabled = false;
        }
        
        
    }
    public void CallRichard()
    {
       
        NavMeshAgent richardNav = richard.GetComponent<NavMeshAgent>();
        Vector3 jackPos = new Vector3(jack.transform.position.x, jack.transform.position.y, jack.transform.position.z);
        richardNav.enabled = true;
        richardNav.speed = speed;
        richardNav.destination = jackPos;
        //richardNav.Move(jackPos);
        //Vector3.MoveTowards(richard.transform.position, jackPos, speed);
        if (richard.transform.position == jack.transform.position)
        {
            richardNav.enabled = false;
        }
      
    }
    /*
    private void Update()
    {
        Vector3 richardPos = new Vector3(richard.transform.position.x, jack.transform.position.y, richard.transform.position.z);
        Vector3 jackPos = new Vector3(jack.transform.position.x, richard.transform.position.y, jack.transform.position.z);
        if (movingJack)
        {
    Vector3.MoveTowards(jack.transform.position, richardPos, speed);
        }
        if (movingRichard) 
        {
            Vector3.MoveTowards(richard.transform.position, jackPos, speed);

        }
    }
    */

}