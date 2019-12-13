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
    [SerializeField] private bool canShoot = true;

    private bool aggro = false;
    private float timer;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        timer = gun.fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit los;

        Debug.DrawRay(transform.position, transform.forward, Color.blue);

        //On envoie un raycast entre l'ennemi et le joueur, si ce dernier est à portée, l'ennemi prend l'aggro
        if (Physics.Raycast(transform.position, target.position - transform.position, out los, aggroRange)
            && los.transform.tag == "Player")
        {
            aggro = true;

        }
        else
        {
            aggro = false;
        }

        //On envoie un raycast de l'ennemi devant lui, si il croise du terrain, il pivote sur la gauche
        if (Physics.Raycast(transform.position, transform.forward, ground))
        {
            transform.Rotate(Vector3.left);
        }

        //Si l'ennemi à l'aggro du joueur
        if (aggro)
        {
            //Il devient rouge et immobile
            Debug.DrawRay(transform.position, target.position - transform.position, Color.red);
            mr.material.color = Color.red;

            speed = 0f;

            //if (canShoot) StartCoroutine("Shoot");    //Tentive de cadence de tir grâce à une coroutine

            //Cadence de tir
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                gun.BotShoot();
                timer = gun.fireRate;
            }

            //L'ennemi se tourne vers le joueur
            transform.LookAt(target.transform.position);
        }
        else
        {
            //Si l'ennemi n'a pas l'aggro, il devient jaune et avance
            Debug.DrawRay(transform.position, target.position - transform.position, Color.yellow);

            mr.material.color = Color.yellow;

            speed = 3f;

        }

        //Déplacement en avant
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    //Tentative de cadence de tir
    IEnumerator Shoot()
    {
        for (int i = 0; i < 10; i++)
        {
            gun.BotShoot();

            yield return new WaitForSeconds(100f);
        }
    }
}
