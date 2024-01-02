using UnityEngine;
using UnityEngine.Serialization;

public class MovimentarPersonagem : MonoBehaviour
{
    [Header("Variáveis de Movimento")] public float velocidade;
    public Rigidbody2D rb2D;
    private float _direcao;
    private Vector3 _olharDireita;
    private Vector3 _olharEsquerda;

    [Header("Variáveis de Pulo")] public float forcaDoPulo;
    public Transform detectorDeChao;
    public LayerMask oQueEhChao;
    public bool estaNoChao;
    [FormerlySerializedAs("_animator")] [SerializeField] private Animator animator;

    [Header("Variáveis de Ataque")] public Transform pontoDeAtaque;
    //public float alcanceDeAtaque = 0.5f;
    //public LayerMask _Os_inimigos;
    private bool _atacando;
    public float duracaoDoAtaque = 0.16f;
    private float _tempoDecorrido;
    //public int danoDoPlayer = 100;


    [Header("Controle De Áudio")] 
    public AudioSource audioSourceMovimento; 
    public AudioClip somMovimento;
    
    public AudioSource audioSourceJump;
    public AudioClip somJump;

    public AudioSource audioSourceAttack;
    public AudioClip somAttack;
    


    private void Start()
    {
        animator.SetBool("Ataque", false);
        _olharDireita = transform.localScale;
        _olharEsquerda = transform.localScale;
        _olharEsquerda.x = _olharEsquerda.x * -1;
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        estaNoChao = Physics2D.OverlapCircle(detectorDeChao.position, 0.2f, oQueEhChao);
        Movimento();
        Pulo();
        ControleDeAtaque();
    }

    void Movimento()
    {
        _direcao = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(_direcao * velocidade, rb2D.velocity.y);

        if (_direcao != 0 && estaNoChao)
        {
            animator.SetBool("andando", true);

            if (!audioSourceMovimento.isPlaying)
            {
                audioSourceMovimento.clip = somMovimento;
                audioSourceMovimento.Play();
            }
        }
        
        else
        {
            audioSourceMovimento.Stop();
            animator.SetBool("andando", false);
        }

        if (_direcao > 0)
        {
            transform.localScale = _olharDireita;
        }
        else if (_direcao < 0)
        {
            transform.localScale = _olharEsquerda;
        }

    }
    
    void Pulo()
    {
        if (Input.GetButtonDown("Jump") && estaNoChao)
        {
            rb2D.velocity = Vector2.up * forcaDoPulo;
            audioSourceJump.clip = somJump;
            audioSourceJump.Play();
        }

        animator.SetBool("pulando", !estaNoChao);
        
    }

    void ControleDeAtaque()
    {
        if (Input.GetKeyDown(KeyCode.W) && !_atacando)
        {
            AtivarAtaque();
            audioSourceAttack.clip = somAttack;
            audioSourceAttack.Play();
            
        }

        _tempoDecorrido += Time.deltaTime;

        if (_atacando && _tempoDecorrido >= duracaoDoAtaque)
        {
            DesativarAtaque();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void AtivarAtaque()
    {
        _atacando = true;
        animator.SetBool("Ataque", true);
        _tempoDecorrido = 0f;
    }

    void DesativarAtaque()
    {
        _atacando = false;
        animator.SetBool("Ataque", false);
    }
   

}