using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour

{
    [Range(0.1f, 10f)]
    public float speed;

    public GameObject cameraGO;

    private Vector3 VerticalDirection, HorizontalDirection;

    // Start is called before the first frame update
    void Start()
    {
        cameraGO = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            VerticalDirection = Vector3.ProjectOnPlane(cameraGO.transform.forward, Vector3.up);
            VerticalDirection = speed * Time.deltaTime * Input.GetAxis("Vertical") * Vector3.Normalize(VerticalDirection);

            HorizontalDirection = Vector3.ProjectOnPlane(cameraGO.transform.right, Vector3.up);
            HorizontalDirection = speed * Time.deltaTime * Input.GetAxis("Horizontal") * Vector3.Normalize(HorizontalDirection);

            // Apply front translation regarding camera facing forward on horizontal projection
            transform.Translate(VerticalDirection + HorizontalDirection);
        }
        else
        {
            VerticalDirection = Vector3.zero;
            HorizontalDirection = Vector3.zero;
        }
        
        

    }
}
