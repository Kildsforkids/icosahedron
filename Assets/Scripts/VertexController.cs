using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexController : MonoBehaviour {

    [SerializeField]
    private Transform[] triangle = new Transform[20];

    [SerializeField]
    private List<Transform> ourGuys = new List<Transform>();

    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            if (i < 10)
                triangle[i] = GameObject.Find("Icosphere_00" + i).transform;
            else
                triangle[i] = GameObject.Find("Icosphere_0" + i).transform;
        }
        for (int i = 0; i < 20; i++)
        {
            Debug.Log(transform.position + " " + triangle[i].position);
            if (Vector3.Distance(transform.position, triangle[i].position) < 1)
                ourGuys.Add(triangle[i]);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(0, 1, 0);
    }
}
