using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoment : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera cameraDoctor, cameraPatient;
    void Start()
    {
        if (Display.displays.Length >= 2)
        {
            Display.displays[1].Activate(1920, 1080, 60);
        }
    }

    void Update()
    {
        Vector3 pos = cameraPatient.transform.position;
        Quaternion rot = cameraPatient.transform.rotation;
        cameraDoctor.transform.SetPositionAndRotation(pos, rot);
    }
}
