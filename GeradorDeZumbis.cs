using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeZumbis : MonoBehaviour
{
    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float distanciaDeGeracao = 3;
    private float distanciaDojogadorParaGeracao = 20;
    private GameObject jogador;
    private int quantidadeMaximaDeZumbisVivos = 3;
    private int quantidadeDeZumbisVivos = 0;
    private float tempoProximoAumentoDeDificuldade = 15;
    private float contadorAumentarADificuldade;


    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        contadorAumentarADificuldade = tempoProximoAumentoDeDificuldade;
        for(int i=0; i<quantidadeMaximaDeZumbisVivos; i++)
        {
            StartCoroutine(GerarNovoZumbi());
        }

    }

    // Update is called once per frame
    void Update()
    {
        bool possoGerarZumbis = Vector3.Distance(transform.position, jogador.transform.position) > distanciaDojogadorParaGeracao;
        if(possoGerarZumbis == true && quantidadeDeZumbisVivos < quantidadeMaximaDeZumbisVivos)
        {
            contadorTempo += Time.deltaTime;
            if (contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine (GerarNovoZumbi());
                contadorTempo = 0;
            }
        }

        if (Time.timeSinceLevelLoad > contadorAumentarADificuldade)
        {
            quantidadeDeZumbisVivos++;
            contadorAumentarADificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDeDificuldade;
        }


        
    }

    IEnumerator GerarNovoZumbi()
    {
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        while(colisores.Length > 0)
        {
            posicaoDeCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
            yield return null;
        }

        ControlaInimigo zumbi = Instantiate(Zumbi, posicaoDeCriacao, transform.rotation).GetComponent<ControlaInimigo>();
        zumbi.meuGerador = this;

        quantidadeDeZumbisVivos++;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
    }


    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;
        return posicao;
    }

    public void DiminuitQuantidadeDeZumbisVivos()
    {
        quantidadeDeZumbisVivos--;
    }

}
