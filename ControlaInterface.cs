using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{

    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public Text textoTempoSobrevivencia;
    public Text textoPontuacaoMaxima;
    private float tempoPontuacaoSalvo;
    private int quantidadeDeZumbisMortos = 0;
    public Text TextoQuantidadeZumbisMortos; 


    // Start is called before the first frame update
    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
        Time.timeScale = 1;
        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

   

    public void AtualizarSliderVidaJogador() {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void AtualizarQuantidadeDeZumbisMortos()
    {
        quantidadeDeZumbisMortos++;
        TextoQuantidadeZumbisMortos.text = "X " + quantidadeDeZumbisMortos;
    }

    public void GameOver() 
    {
        
        PainelDeGameOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundo = (int)(Time.timeSinceLevelLoad % 60);

        textoTempoSobrevivencia.text = "Voce sobreviveu por " + minutos + "min e " + segundo + "s";

        AjustarPontuacaoMaxima(minutos, segundo);



    }

    void AjustarPontuacaoMaxima(int min, int seg)
    {
        if(Time.timeSinceLevelLoad > tempoPontuacaoSalvo)
        {
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            textoPontuacaoMaxima.text = string.Format("Seu melhor tempo é {0}min e {1}s!", min, seg);
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalvo); 
        }

        if(textoPontuacaoMaxima.text == "")
        {
            min = (int)(tempoPontuacaoSalvo / 60);
            seg = (int)(tempoPontuacaoSalvo % 60);
            textoPontuacaoMaxima.text = string.Format("Seu melhor tempo é {0}min e {1}s!", min, seg);

        }
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("game");
    }

}
