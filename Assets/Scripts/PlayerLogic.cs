using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerLogic : MonoBehaviour
{
    //pegar Propriedades
    public Rigidbody2D rb;
    [SerializeField] public SpriteRenderer spritePlayer;
    Animator anim;
    
    //movimentacao
    [SerializeField] private float moveSpeed;
    public float dirX;
    private bool facingRight = true;

    //pulo
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float groundDist;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpForce;
    [SerializeField] private int totalJump;
    [SerializeField] private float gravityScale = 5;
    [SerializeField] private float fallGravityScale = 15;

    private int jumpless;

    private bool canJump;
    [SerializeField] private bool isGroundCheck;

    //para mudar animacao
    bool xPressed;
    int isWalkingHash;
    int doFirstMovimentHash;
    
    //parar de mover quando a cutscene comecar
    public bool podesemover = false;
    
    //nao se mover quando o jogo comecar
    [SerializeField] private bool doFirstMoviment;//nao usado
    [SerializeField] float inicio = 0f;
    [SerializeField] bool entrou = false;

    //causar repulsao ao colidir com o inimigo
    public float kBForce;
    public float kBCount;
    public float kBTime;
    public bool isKnockRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        spritePlayer = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        doFirstMovimentHash = Animator.StringToHash("doFirstMove");

        jumpless = totalJump;
    }

    // Update is called once per frame
    void Update()
    {
        DirectionCheck();
        CanJump();
        GetInputMove();
        if (entrou && !doFirstMoviment)
        {
           inicio += Time.unscaledDeltaTime;
        }

        if (rb.velocity.y > 0)
        {
            rb.gravityScale = gravityScale;
        }
        else
        {
            rb.gravityScale = fallGravityScale;
        }
        JumpAnim();
    }

    private void FixedUpdate()
    {
        KnockLogic();
        CheckArea();
    }

    #region Konck
    void KnockLogic()
    {
        if (kBCount < 0)
        {
            MoveSides();
        }
        else if (kBForce >= 0 && doFirstMoviment)
        {
            Health health = GetComponent<Health>();
            if (!health.invincible)
            {
                if (isKnockRight)
                {
                    rb.velocity = new Vector2(-kBForce, kBForce);
                }

                if (!isKnockRight)
                {
                    rb.velocity = new Vector2(kBForce, kBForce);
                }
            }

        }

        kBCount -= Time.deltaTime;
    }
    #endregion knock 
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, groundDist);
    }

    public void SetPodeMover(bool podeMover)
    {
        podesemover = podeMover;
    }
    public void ClicouNaTecla(bool clicou)
    {
        entrou = clicou;
    }
    void GetInputMove()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
    }

    #region Checks
    void CheckArea()
    {
        isGroundCheck = Physics2D.OverlapCircle(GroundCheck.position, groundDist, groundLayer);
    }

    //verificar direcao de onde esta olhando
    void DirectionCheck()
    {
        if (doFirstMoviment && podesemover)
        {
            if (facingRight && dirX < 0)
            {
                Flip();
            }
            if (!facingRight && dirX > 0)
            {
                Flip();
            }
        }
    }

    #endregion Checks

    #region MoverHorinzontal

    //virar personagem
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    void MoveSides()
    {
        if (podesemover == true && doFirstMoviment)
        {
            anim.SetBool(isWalkingHash, true);
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }
        if (dirX == 0 && doFirstMoviment)
        {
            anim.SetBool("isWalking", false);
        }
        else if (!doFirstMoviment && dirX != 0)
        {
            anim.SetBool(doFirstMovimentHash, true);
            entrou = true;
            
            if (inicio >= 3f)
            {
                try
                {
                    anim.SetTrigger("GettingUp");
                }
                catch { Debug.Log("nao tem"); }
                doFirstMoviment = true;
                podesemover = true;
                inicio = 0f;
            }
        }
    }

    #endregion MoverHorizontal

    #region MoverVertical
    void CanJump()
    {
        if (isGroundCheck && rb.velocity.y <= 0)
            jumpless = totalJump;

        if (jumpless <=0)
            canJump = false;
        else 
            canJump = true;
    }
    void Jump()
    {
        if (doFirstMoviment && podesemover)
        {
            if (canJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpless--;
            }
        }
    }

    void JumpAnim()
    {
        anim.SetFloat("VerticalAnim", rb.velocity.y);
        anim.SetBool("GroundCheck", isGroundCheck);
    }
    #endregion MoverVertical

}
