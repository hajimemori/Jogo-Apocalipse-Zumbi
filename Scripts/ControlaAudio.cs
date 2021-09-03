using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour
{
    private AudioSource meuAudioSource;
    public static AudioSource instancia;

    // Foi utilizado um modo de "chamar" os audios chamado de Singleton, em que o é instanciado uma variavel static para ser chamada em todos os outros scripts.
    // Evitando assim, ter um AudioSource para todo objeto isntanciado

    void Awake()
    {
        meuAudioSource = GetComponent<AudioSource>();
        instancia = meuAudioSource;
    }

}
