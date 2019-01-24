using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{

    [SerializeField]
    private Transform[] triangle = new Transform[20];

    [SerializeField]
    private List<Transform> ourGuys = new List<Transform>();

    [SerializeField]
    private Material notThisMaterial;

    private List<Color> colorKit = new List<Color>();

    private Transform[] vertex = new Transform[12];

    [SerializeField]
    private bool isEnded = false;

    [SerializeField]
    private GameObject endText;

    [SerializeField]
    private GameObject restartButton;

    private Camera _camera;
    private Vector3 startPoint;
    private Vector3 currentPoint;

    // int k = 0;
    private Transform currentVertex;

    private void Start()
    {
        //if (SceneManager.GetActiveScene().name == "ARScene")
        //    _camera = GameObject.Find("ARCamera").GetComponent<Camera>();
        //else
        //    _camera = GameObject.Find("Camera").GetComponent<Camera>();
        _camera = Camera.main.GetComponent<Camera>();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        for (int i = 0; i < 12; i++)
        {
            if (i < 10)
                vertex[i] = GameObject.Find("Empty_00" + i).transform;
            else
                vertex[i] = GameObject.Find("Empty_0" + i).transform;
        }
        for (int i = 0; i < 20; i++)
        {
            if (i < 10)
                triangle[i] = GameObject.Find("Icosphere_00" + i).transform;
            else
                triangle[i] = GameObject.Find("Icosphere_0" + i).transform;
        }
        Prepare();
        currentVertex = null;
        //Check();
        //Debug.Log((vertex[0].transform.position-vertex[11].transform.position).sqrMagnitude);
    }

    public void Restart()
    {
        Prepare();
        isEnded = false;
        endText.SetActive(false);
        restartButton.SetActive(false);
    }

    public void Turn(int value)
    {
        if (value < 0 && !isEnded)
            currentVertex.transform.Rotate(0, 72, 0);
        else
            currentVertex.transform.Rotate(0, -72, 0);
    }

    public void ChangeMode()
    {
        if (SceneManager.GetActiveScene().name == "ARScene")
            SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(0);
    }

    private void FixedUpdate()
    {
        //if (currentVertex != null)
        //{
        //    if (Input.GetButtonDown("Fire1"))
        //    {
        //        if (Input.mousePosition.x < Screen.width/2)
        //        {
        //            if (Input.mousePosition.x < Screen.width/4)
        //                currentVertex.transform.Rotate(0, 72, 0);
        //        }
        //        else
        //        {
        //            if (Input.mousePosition.x > Screen.width-Screen.width/4)
        //                currentVertex.transform.Rotate(0, -72, 0);
        //        }
        //    }
        //    //if (Input.GetButton("Fire2"))
        //    //    currentVertex.transform.Rotate(0, -72, 0);
        //    // Debug.Log(currentVertex.transform.rotation.eulerAngles.y);
        //}
        if (Input.GetButtonDown("Fire1"))
        {
            // Debug.Log(Input.mousePosition);
            startPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && !isEnded)
            {
                // Transform objectHit = hit.transform;

                // Debug.Log(hit.transform.name);
                if (currentVertex != null)
                    currentVertex.GetChild(0).gameObject.SetActive(false);
                hit.transform.GetChild(0).gameObject.SetActive(true);
                ChooseVertex(hit.transform);
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
            transform.Rotate(0, /*((transform.rotation.x > 0) ? -1 : 1)**/(startPoint.x - currentPoint.x) / 100, 0, Space.World);
            transform.Rotate((currentPoint.y - startPoint.y) / 100, 0, 0, Space.World);
        }
        if (!isEnded)
            Check();
    }

    private void Check()
    {
        int colorCounter = 0;
        int coolVertexCounter = 0;
        Vector3 firstVertexPos = new Vector3();
        Vector3 secondVertexPos = new Vector3();
        for (int i = 0; i < 12; i++)
        {
            colorCounter = 0;
            for (int j = 0; j < 20; j++)
            {
                if ((vertex[i].transform.position-triangle[j].position).sqrMagnitude < 1)
                {
                    colorKit.Add(triangle[j].GetComponent<MeshRenderer>().material.color);
                    if (colorKit.Count > 1)
                    {
                        if (colorKit[colorKit.Count - 1] != colorKit[colorKit.Count - 2] || colorKit[colorKit.Count-1] == notThisMaterial.color)
                            break;
                        colorCounter++;
                    }
                }
            }
            colorKit.Clear();
            if (colorCounter >= 4)
            {
                if (coolVertexCounter > 0)
                    secondVertexPos = vertex[i].transform.position;
                else
                    firstVertexPos = vertex[i].transform.position;
                coolVertexCounter++;
            }
            if (coolVertexCounter >= 2)
            {
                if ((firstVertexPos-secondVertexPos).sqrMagnitude > 3)
                    EndGame();
                break;
            }
            //else if (coolVertexCounter >= 1)
            //    Debug.Log("Almost");
        }
    }

    private void EndGame()
    {
        isEnded = true;
        if (currentVertex != null)
            currentVertex.GetChild(0).gameObject.SetActive(false);
        endText.SetActive(true);
        restartButton.SetActive(true);
    }

    private void Prepare()
    {
        for (int i = 0; i < 12; i++)
        {
            ChooseVertex(vertex[i]);
            currentVertex.transform.Rotate(0, 72 * Random.Range(0, 5), 0);
        }
    }

    public void ChooseVertex(Transform vtx)
    {
        if (!isEnded)
        {
            currentVertex = vtx;
            if (currentVertex != null)
                ParentTriangles();
        }
    }

    private void ParentTriangles()
    {
        ourGuys.Clear();
        for (int i = 0; i < 20; i++)
        {
            //Debug.Log(transform.position + " " + triangle[i].position);
            if ((currentVertex.transform.position-triangle[i].position).sqrMagnitude < 1)
            {
                ourGuys.Add(triangle[i]);
                triangle[i].SetParent(currentVertex);
            }
        }
    }
}
