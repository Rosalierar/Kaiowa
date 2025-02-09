using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerLogic2024 : MonoBehaviour
{ 
    //pegar Propriedades
    private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spritePlayer;
    Animator anim;

    //movimentacao
    [SerializeField] private float moveSpeed;
    private float dirX;
    private bool facingRight = false;

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
    private bool isGroundCheck;

    //para mudar animacao
    bool xPressed;
    int isWalkingHash;

    //parar de mover quando a cutscene comecar
    //public bool podesemover = false;

    //bool entrou = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        spritePlayer = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        
        jumpless = totalJump;
    }

    // Update is called once per frame
    void Update()
    {
        DirectionCheck();
        CanJump();
        GetInputMove();

        if (rb.velocity.y > 0)
        {
            rb.gravityScale = gravityScale;

        }
        else
        {
            rb.gravityScale = fallGravityScale;
        }
    }
    private void FixedUpdate()
    {
        MoveSides();
        CheckArea();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, groundDist);
    }

    void GetInputMove()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W))
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
        if (facingRight && dirX < 0)
        {
            Flip();
        }
        if (!facingRight && dirX > 0)
        {
            Flip();
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
        anim.SetBool(isWalkingHash, true);
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (dirX == 0)
        {
            anim.SetBool("isWalking", false);
        }
    }
    #endregion MoverVertical

    #region MoverVertical
    void CanJump()
    {
        if (isGroundCheck && rb.velocity.y <= 0)
            jumpless = totalJump;

        if (jumpless <= 0)
            canJump = false;
        else
            canJump = true;
    }
    void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpless--;
        }
        
    }
    #endregion MoverVertical
}
