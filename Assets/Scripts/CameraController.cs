using UnityEngine;

public class CameraController : MonoBehaviour {

    private Camera _camera;
    private Controller controller;
    private Vector3 startPoint;
    private Vector3 currentPoint;
    //private Vector3 midPoint;

    private void Start()
    {
        _camera = transform.GetChild(0).GetComponent<Camera>();
        controller = GameObject.Find("Icos").GetComponent<Controller>();
        //midPoint = new Vector3(Screen.width/2, Screen.height/2, 0);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Debug.Log(Input.mousePosition);
            startPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Transform objectHit = hit.transform;

                // Debug.Log(hit.transform.name);
                controller.ChooseVertex(hit.transform);
            }
        }
        if (Input.GetButton("Fire1"))
        {
            //if (Input.mousePosition.x < midPoint.x-100)
            //    transform.Rotate(0, 1, 0, Space.World);
            //if (Input.mousePosition.x > midPoint.x+100)
            //    transform.Rotate(0, -1, 0, Space.World);
            //if (Input.mousePosition.y < midPoint.y-100)
            //    transform.Rotate(1, 0, 0);
            //if (Input.mousePosition.y > midPoint.y+100)
            //    transform.Rotate(-1, 0, 0);
            currentPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            //transform.Rotate((currentPoint.x-startPoint.x) > 0 ? 1 : -1, 0, 0);
            //transform.Rotate(0, (currentPoint.y-startPoint.y) > 0 ? -1 : 1, 0, Space.World);
            //if (Mathf.Abs(startPoint.x - currentPoint.x) > Mathf.Abs(startPoint.y - currentPoint.y))
            //    transform.Rotate(0, (currentPoint.y - startPoint.y) > 0 ? -1 : 1, 0, Space.World);
            //else
            //    transform.Rotate((currentPoint.x - startPoint.x) > 0 ? 1 : -1, 0, 0);
            transform.Rotate(0, /*((transform.rotation.x > 0) ? -1 : 1)**/(currentPoint.x - startPoint.x) / 100, 0, Space.World);
            transform.Rotate((startPoint.y - currentPoint.y) / 100, 0, 0);
        }
        //Debug.Log(transform.rotation.x+"-"+transform.rotation.y);
        //if (Input.GetKey(KeyCode.UpArrow))
        //    transform.Rotate(1, 0, 0);
        //if (Input.GetKey(KeyCode.DownArrow))
        //    transform.Rotate(-1, 0, 0);
        //if (Input.GetKey(KeyCode.LeftArrow))
        //    transform.Rotate(0, 1, 0, Space.World);
        //if (Input.GetKey(KeyCode.RightArrow))
        //    transform.Rotate(0, -1, 0, Space.World);
    }
}
