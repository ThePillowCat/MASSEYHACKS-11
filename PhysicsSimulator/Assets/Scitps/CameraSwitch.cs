using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CinemachineFreeLook vcam2;
    [SerializeField] private CinemachineVirtualCamera vcam1;
    void Start()
    {
        vcam1.Priority = 10;
        vcam2.Priority = 0;
    }

    public void switchCamera()
    {
        if (vcam1.Priority > vcam2.Priority)
        {
            vcam2.Priority = 10;
            vcam1.Priority = 0;
        }
        else
        {
            vcam2.Priority = 0;
            vcam1.Priority = 10;
        }
    }
}
