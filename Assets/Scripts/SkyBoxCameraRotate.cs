using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxCameraRotate : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 1 * Time.deltaTime, 0, Space.World);
    }
}
