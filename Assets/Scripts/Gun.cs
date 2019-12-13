using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float range;
    public float fireRate;
    [SerializeField] private float accuracy;

    [SerializeField] private GameObject impactEffect;

    [SerializeField] private ParticleSystem flash;

    [SerializeField] private LayerMask ignore;

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

        //Envoi d'un raycast du joueur droit devant lui
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * hit.distance, Color.yellow);

            //Si il touche un objet possédant un script Target, il prend des dégâts
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            //instanciation de l'effet d'impact sur l'objet
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
        flash.Play();

        //On envoie un raycast depuis l'objet vers le joueur et on ajoute un offset pour qu'il puisse louper
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 offset = new Vector2(Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy));
        RaycastHit hit;

        if (Physics.Raycast(transform.position, (player.position - transform.position) + offset, out hit, range))
        {
            if (hit.transform.tag == "Player")
            {
                Debug.Log("Touché");
                Target target = hit.transform.GetComponent<Target>();
                target.TakeDamage(damage);
            }
            else
            {
                Debug.Log("Loupé");
            }
        }
    }
}
