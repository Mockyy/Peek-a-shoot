using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] private float health = 50f;

    //La cible prend des dégats
    public void TakeDamage (float amount)
    {
        health -= amount;

        //Si la cible est le joueur, on applique l'effet de dégâts auquel on réduit l'alpha graduellement
        if (gameObject.tag == "Player")
        {
            StartCoroutine("Fade");
        }
        if(health <= 0)
        {
            //Si la cible est le joueur, on charge la scène de mort
            if (gameObject.tag == "Player")
            { 
                PlayerDeath();
            }
            else //Si c'est un ennemi, on le détruit
            {
                Die();
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void PlayerDeath()
    {
        Menu menu = GameObject.FindGameObjectWithTag("Manager").GetComponent<Menu>();
        menu.LoadScene("DeathScene");
    }

    //On réduit l'opacité de l'effet de dégâts grâce à une coroutine
    IEnumerator Fade()
    {
        PlayerController pc = GetComponent<PlayerController>();

        for (float ft = 1f; ft >= 0; ft -= 0.1f)
        {
            Color c = pc.damageEffect.material.color;
            c.a = ft;
            pc.damageEffect.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
