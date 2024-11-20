using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    private new Camera camera;
    private new Rigidbody2D rigidbody;
    private new BoxCollider2D collider;
    public LineRenderer line;
    private float inputAxis;
    private float inputDash;
    //Parametros dash
    public float dashFuerza = 24f;
    public float dashTiempo = 0.2f;
    public float dashCooldown = 1f;
    //Parametros velocidad
    private Vector2 velocity;
    public float moveSpeed = 8f;
    //Parametros salto
    public float maxJumpHeight = 6f;
    public float maxJumpTime = 1f;
    public float velocidadDespuesGancho = 2f;
    public float jumpForceConstante = 1.9f;
    public float constanteInercia = 10f;
    public float jumpForce => (jumpForceConstante * maxJumpHeight / (maxJumpTime / 2));
    public float gravity => (-2f * maxJumpHeight / Mathf.Pow(maxJumpTime/2f, 2));

    //Parametros gancho
    [SerializeField] public LayerMask grappleMask;
    [SerializeField] public float maxDistance = 10f;
    public float grappleSpeed = 10f;
    public float grappleShootSpeed = 200f;
    Vector2 target;
    private bool techoHit;

    //Estados
    public bool grounded {get; private set;}
    public bool enBloque {get; private set;}
    public bool jumpTrigger {get; private set;}
    public bool canJumpMidAir{get; private set;}
    public bool retracting {get; private set;}
    public bool canDash {get; private set;}
    public bool isDashing {get; private set;}
    public bool isGrappling {get; private set;}
    
    //Animator
    [SerializeField] public Animator animator;
    private static readonly int anim_idle = Animator.StringToHash("Jugador_Idle");
    private static readonly int anim_run = Animator.StringToHash("Jugador_Run");
    private static readonly int anim_fall = Animator.StringToHash("Jugador_Fall");
    private static readonly int anim_jump = Animator.StringToHash("Jugador_Jump");
    private static readonly int anim_dash = Animator.StringToHash("Jugador_Dash");
    private int estadoActual;
    private float tiempoAnimacion;
    
    private void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        camera = Camera.main;
        canJumpMidAir = false;
        canDash = true;
        isDashing = false;
        isGrappling = false;
        line = GetComponent<LineRenderer>();
        grappleMask = LayerMask.GetMask("Default");
        animator.GetComponent<Animator>();
    }
    private void Update(){
        
        
        var estado = GetEstado();
        if(isDashing){
            return;
        }
        
        HorizontalMovement();
        grounded = collider.BoxCast(Vector2.down);
        
        enBloque = collider.BoxCast(Vector2.down, LayerMask.GetMask("Bloques"));

        techoHit = collider.BoxCast(Vector2.up,  LayerMask.GetMask("techo"));
        if(!grounded){
            AirJump();
        }

        if(grounded || enBloque){
            GroundedMovement();
        }
        jumpTrigger = false;
        if(Input.GetMouseButtonDown(0)){
            StartGrapple();
        }
        if(retracting){
            Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);
            transform.position = grapplePos;
            line.SetPosition(0, transform.position);
            if(Vector2.Distance(transform.position, target) < 1f){
                retracting = false;
                isGrappling = false;
                line.enabled = false;
            }
            canDash = true;
        }

        ApplyGravity();

        if(estado == estadoActual) return;
        animator.CrossFade(estado, 0, 0);
        estadoActual = estado;
        
    }
    private void FixedUpdate(){
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;
        
        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x + 0.5f);

        rigidbody.MovePosition(position);
    }
    
    private void ApplyGravity(){
        if(techoHit == true){
            velocity.y = 0f;
        }

        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        
        float multiplier = falling ? 2f : 1f;

        if(!isGrappling){
            velocity.y += gravity * multiplier * Time.deltaTime;
            velocity.y = Mathf.Max(velocity.y, gravity / 2f);
        }
        
    }

    private void GroundedMovement(){
        

        velocity.y = Mathf.Max(velocity.y, 0f);
        if(grounded){
            canJumpMidAir = true;

        }
        
        
        if(Input.GetButton("Jump")){
            velocity.y = jumpForce;
            jumpTrigger = true;
            canJumpMidAir = true;
        }
        
    }

    private void HorizontalMovement(){
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime * constanteInercia);

        inputDash = Input.GetAxis("Fire3");

        if(inputDash == 1 && canDash){
            StartCoroutine(Dash());
                       
        }

        if(rigidbody.Raycast(Vector2.right * velocity.x)){
            velocity.x = 0;
        }
        animator.SetFloat("Velocidad", Mathf.Abs(velocity.x));

        if(velocity.x < 0f){
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
  
        } else if(velocity.x > 0f){
            transform.eulerAngles = Vector3.zero;
        }
        
    }

    private IEnumerator Dash(){
        canDash = false;
        isDashing = true;
        float originalGravity = rigidbody.gravityScale;
        rigidbody.gravityScale = 0f;
        if(transform.eulerAngles.y == 0){
            velocity = Vector2.right * dashFuerza;
        } else if (transform.eulerAngles.y == 180){
            velocity = Vector2.left * dashFuerza;
        }
        animator.CrossFade(anim_dash, 0, 0);
       
        yield return new WaitForSeconds(dashTiempo);
        animator.CrossFade(GetEstado(), 0, 0);
        rigidbody.gravityScale = originalGravity;
        isDashing = false;
        if(transform.eulerAngles.y == 0){
            velocity.x = 8f;
        } else if (transform.eulerAngles.y == 180){
            velocity.x = -8f;
        }

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }   

    private void AirJump(){
        if(canJumpMidAir && Input.GetButtonDown("Jump")){
            
            velocity.y = jumpForce;
            canJumpMidAir = false;
        }
    }
    //Codigo del gancho
    private void StartGrapple(){
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grappleMask);
        if(hit.collider != null){
            
            canDash = false;
            isGrappling = true;
            target = hit.point;
            line.enabled =true;
            line.positionCount = 2;
            StartCoroutine(Grapple());
            canJumpMidAir = true;
            Vector2 fallingVelocity = new Vector2(velocity.x /2, velocidadDespuesGancho);
            velocity = fallingVelocity; 
            
            
        }
        
    }
    private IEnumerator Grapple(){
        canDash = false;
        float t =0f;
        float time = 10f;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);
        
        Vector2 newPos;
        for(; t< time; t += grappleShootSpeed *2 * Time.deltaTime){
            
            newPos = Vector2.Lerp(transform.position, target, t/time);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, newPos);
            
            yield return null;
        }
        line.SetPosition(1, target);
        retracting = true;
    }
    //Codigo Estados de animacion
    private int GetEstado(){
        if(Time.time < tiempoAnimacion) return estadoActual;
        if(jumpTrigger) return anim_jump;
        if(grounded) return Input.GetAxis("Horizontal") == 0 ? anim_idle : anim_run;
        return velocity.y > 0 ? anim_jump : anim_fall;
        
    }
}
