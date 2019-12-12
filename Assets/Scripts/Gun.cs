using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage;
    public float range;
    public float impactForce = 30f;

    public GameObject impactEffect;

    public ParticleSystem flash;

    public LayerMask ignore;

    private Camera fpsCam;

    private void Start()
    {
        fpsCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)  && gameObject.tag == "Player")
        {
            PlayerShoot();
        }
    }

    public void PlayerShoot()
    {
        flash.Play();

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * hit.distance, Color.yellow);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.transform.GetComponent<Rigidbody>() != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject bh = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(bh, 2f);
        }
        else
        {
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range, Color.white);
        }
    }

    public void BotShoot()
    {

    }
}
