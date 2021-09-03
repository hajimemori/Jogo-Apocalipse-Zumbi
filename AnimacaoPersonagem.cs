using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private Animator meuAnimator;

    void Awake()
    {
        meuAnimator = GetComponent<Animator>();
    }


    public void Atacar(bool estado)
    {
        meuAnimator.SetBool("Atacando", estado);
    }  
    public void Movimentar(Vector3 direcao)
    {
        meuAnimator.SetFloat("Movendo", direcao.magnitude); 
      
    }

    public void Morrer()
    {
        meuAnimator.SetTrigger("Morrer");
    }

}
