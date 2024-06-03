using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] Camera firstPersonCam, thirdPersonCam;
    [SerializeField] CinemachineVirtualCamera vcamFirstPerson, vcamThirdPerson;
    [SerializeField] FirstPersonMovement firstPersonMovement;
    [SerializeField] ThirdPersonMovement thirdPersonMovement;
    int largerPriority = 2, lesserPriority = 1; 
    bool isFirstPerson = false;

    // CameraSwitch calls the cinemachine brain to switch between the third and first person perspective
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchCamera(isFirstPerson);
        }
    }

    void SwitchCamera(bool camSwitch)
    {
        if (camSwitch)
        {
            vcamFirstPerson.Priority = largerPriority;
            vcamThirdPerson.Priority = lesserPriority;
            thirdPersonMovement.enabled = false;
            thirdPersonCam.enabled = false;
            firstPersonCam.enabled = true;
            firstPersonMovement.enabled = true;
            isFirstPerson = false;
        }
        else
        {
            vcamFirstPerson.Priority = lesserPriority;
            vcamThirdPerson.Priority = largerPriority;
            firstPersonMovement.enabled = false;
            thirdPersonCam.enabled = true;
            firstPersonCam.enabled = false;
            thirdPersonMovement.enabled = true;
            isFirstPerson = true;
        }
    }
}
