using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{

    public GameObject Jogador;
    private MovimentoPersonagem movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private Status statusInimigo;
    public AudioClip somDeMorte;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoEntrePosicoesAleatorias = 4;
    private float porcentagemGerarKitMedico = 0.2f;
    public GameObject KitMedicoPrefab;
    private ControlaInterface scriptControlaInterface;
    [HideInInspector]
    public GeradorDeZumbis meuGerador;
  


    // Start is called before the first frame update
    void Start()
    {
    
        Jogador = GameObject.FindWithTag("Jogador");
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        AleatorizarZumbi();
        statusInimigo = GetComponent<Status>();
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(Jogador.transform.position, transform.position);

        

        movimentaInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao);

        if(distancia > 15)
        {
            Vagar();
        }

        else if (distancia > 3)
        {
            Perseguir();
        }
        else
        {
            direcao = Jogador.transform.position - transform.position;
            animacaoInimigo.Atacar(true);


        }
    }

    void AtacaJogador()
    {
        //o dano do inimigo é aleatório entre 20 e 30
        int dano = Random.Range(20, 30);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);

        
    }

    void AleatorizarZumbi()
    {
        int geraTipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        statusInimigo.Vida -= dano;
        if(statusInimigo.Vida <= 0)
        { 
            Morrer();
        }
    }

    public void Morrer()
    {
        animacaoInimigo.Morrer();
        this.enabled = false;
        movimentaInimigo.Morrer();
        Destroy(gameObject, 2);
        ControlaAudio.instancia.PlayOneShot(somDeMorte);
        VerificaGeracaoKitMedico(porcentagemGerarKitMedico);
        scriptControlaInterface.AtualizarQuantidadeDeZumbisMortos();
        meuGerador.DiminuitQuantidadeDeZumbisVivos();
    }

    void VerificaGeracaoKitMedico(float porcentagemDeGeracao)
    {
        if (Random.value <= porcentagemDeGeracao)
        {
            Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
      
        }
        
    }

    void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if(contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoEntrePosicoesAleatorias + Random.Range(-1f, 2f);
        }
        
        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;
        if (ficouPertoOSuficiente == false)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
            
        }

    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao; 
    }

    void Perseguir()
    {
        direcao = Jogador.transform.position - transform.position;
        movimentaInimigo.Movimentar(direcao, statusInimigo.Velocidade);
        animacaoInimigo.Atacar(false);
    }
    
}
