using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private Rigidbody rigidbodyBala;
    public float Velocidade = 20;
    public AudioClip morteZumbi;


    void Start()
    {
        rigidbodyBala = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigidbodyBala.MovePosition(rigidbodyBala.position + transform.forward * Velocidade * Time.deltaTime);

    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        switch(objetoDeColisao.tag)
        {
            case "Inimigo":
                objetoDeColisao.GetComponent<ControlaInimigo>().TomarDano(1);
                break; 
            case "ChefeDeFase":
                objetoDeColisao.GetComponent<ControlaChefe>().TomarDano(1);
                break;
        }
           
        Destroy(gameObject);
    }
}
