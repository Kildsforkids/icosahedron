using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCameraController : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.rotation = GameObject.Find("GameObject").transform.rotation;
        transform.position = GameObject.Find("GameObject").transform.position;
    }
}
