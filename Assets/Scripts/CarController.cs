using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCVForUnityExample;

public class CarController : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0));
    }

    private void FixedUpdate()
    {
        rb.AddForce(0, 0, transform.rotation.z);
    }
}
