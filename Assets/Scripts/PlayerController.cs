using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Horizontal Movement Settings")]
    [SerializeField] private float walkSpeed = 1;
    [Space(5)]

    [Header("Vertical Movement Settings")]
    [SerializeField] private float jumpForce = 10;
    private int jumpBufferCounter = 0;
    [SerializeField] private int jumpBufferFrames;
    private float coyoteTimeCounter = 0;
    [SerializeField] private float coyoteTime;
    private int airJumpCounter = 0;
    [SerializeField] private int maxAirJumps;
    [Space(5)]

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private LayerMask whatIsGround;
    [Space(5)]

    [Header("Camera Settings")]
    [SerializeField] private float playerFallSpeedThreshold = -10;


    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [Space(5)]

    public PlayerStateList pState;
    private Rigidbody2D rb;
    private float xAxis;
    private float gravity;
    private bool canDash;
    private bool dashed;

    public static PlayerController Instance;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        pState = GetComponent<PlayerStateList>();
        rb = GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
        //anim = GetComponent<Animator>();
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(pState.cutscene) return;
        GetInputs();
        UpdateJumpVariables();
        UpdateCameraYDampForPlayerFall();

        if(pState.dashing) return;
        Flip();
        Move();
        Jump();
        StartDash();
    }

    private void FixedUpdate()
    {
        if (pState.cutscene) return;
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    void Flip()
    {
        if(xAxis < 0)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if(xAxis > 0)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(walkSpeed * xAxis, rb.linearVelocity.y);
    }

    void UpdateCameraYDampForPlayerFall()
    {
        if (rb.linearVelocity.y < playerFallSpeedThreshold && !CameraManager.Instance.isLerpingYDamping && !CameraManager.Instance.hasLerpedYDamping)
        {
            StartCoroutine(CameraManager.Instance.LerpYDamping(true));
        }
        if (rb.linearVelocity.y >= 0 && !CameraManager.Instance.isLerpingYDamping && CameraManager.Instance.hasLerpedYDamping)
        {
            CameraManager.Instance.hasLerpedYDamping = false;
            StartCoroutine(CameraManager.Instance.LerpYDamping(false));
        }
    }

    void StartDash()
    {
        if(Input.GetButtonDown("Dash") && canDash && !dashed)
        {
            StartCoroutine(Dash());
            dashed = true;
        }

        if(Grounded())
        {
            dashed = false;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        pState.dashing = true;
        //anim.SetTrigger("Dashing");
        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = gravity;
        pState.dashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public IEnumerator WalkIntoNewScene(Vector2 _exitDir, float _delay)
    {
        if(_exitDir.y > 0)
        {
            rb.linearVelocity = jumpForce * _exitDir;
        }
        if(_exitDir.x != 0)
        {
            xAxis = _exitDir.x > 0 ? 1 : -1;

            Move();
        }

        Flip();
        yield return new WaitForSeconds(_delay);
        pState.cutscene = false;
    }

    public bool Grounded()
    {
        if(Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, whatIsGround)
        || Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround)
        || Physics2D.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Jump()
    {
        if(Input.GetButtonUp("Jump") && rb.linearVelocity.y > 3)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            pState.jumping = false;
        }

        if (!pState.jumping && jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce);
            pState.jumping = true;
        }
        if(!Grounded() && airJumpCounter < maxAirJumps && Input.GetButtonDown("Jump")) 
        {
            pState.jumping = true;
            airJumpCounter++;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce);
        }
    }


    void UpdateJumpVariables()
    {
        if(Grounded())
        {
            pState.jumping = false;
            coyoteTimeCounter = coyoteTime;
            airJumpCounter = 0;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if(Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferFrames;
        }
        else
        {
            jumpBufferCounter--;
        }
    }
}
