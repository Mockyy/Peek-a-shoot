using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private Renderer mr;

    [SerializeField] private Gun gun;

    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float aggroRange = 30f;

    private bool aggro = false;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit los;

        Debug.DrawRay(transform.position, transform.forward, Color.blue);

        //Player aggro
        if (Physics.Raycast(transform.position, target.position - transform.position, out los, aggroRange)
            && los.transform.tag == "Player")
        {
            aggro = true;

        }
        else
        {
            aggro = false;
        }

        if (Physics.Raycast(transform.position, transform.forward, ground))
        {
            transform.Rotate(Vector3.left);
        }

        if (aggro)
        {
            Debug.DrawRay(transform.position, target.position - transform.position, Color.red);
            Debug.Log(los.transform.name);
            mr.material.color = Color.red;

            speed = 1f;

            StartCoroutine("Shoot");

            transform.LookAt(target.transform.position);
        }
        else
        {
            Debug.DrawRay(transform.position, target.position - transform.position, Color.yellow);

            mr.material.color = Color.yellow;

            speed = 3f;

            StopCoroutine("Shoot");
        }

        transform.position += transform.forward * speed * Time.deltaTime;
    }

    IEnumerator Shoot()
    {
        for(;;)
        {
            //gun.Shoot();

            yield return new WaitForSeconds(30f);
        }
    }
}
