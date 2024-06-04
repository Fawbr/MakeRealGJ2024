using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] Transform cam = null;
    [SerializeField] Transform camHolder = null;
    NewPlayerScript playerMovement;
    [SerializeField] Animator headBob;

    private void Start()
    {
        playerMovement = GetComponent<NewPlayerScript>();
    }

    private void Update()
    {
        if (playerMovement.RawMoveInput != new Vector2(0,0))
        {
            headBob.SetBool("isMoving", true);
        }
        else
        {
            headBob.SetBool("isMoving", false);
            cam.localPosition = Vector3.MoveTowards(cam.localPosition, camHolder.position, 0.5f);
        }
    }
}
