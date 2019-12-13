using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float speed;
    [SerializeField] private float gravity = -9.81f;
    private Vector3 velocity;
    private bool isGrounded;
    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    public Image damageEffect;
  
    // Start is called before the first frame update
    void Start()
    {
        //On applique la transparence aux effets de sang sur l'écran
        controller = GetComponent<CharacterController>();
        Color c = damageEffect.material.color;
        c.a = 0;
        damageEffect.material.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        //Vérification si le joueur touche le sol
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Si il est au sol, on lui donne une légère vélocité pour l'y maintenir
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        //Récupération des inputs de déplacement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Déplacement
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //Saut
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Application de la gravité
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
