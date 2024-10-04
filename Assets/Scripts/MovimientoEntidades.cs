using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovimientoEntidades : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direccion = Vector2.left;
    public GameObject nodo1;
    public GameObject nodo2;
    private new Rigidbody2D rigidbody;
    private Vector2 velocity;
    private Transform currentNodo;
    private void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
        currentNodo = nodo1.transform;
        enabled=false;
    }

    private void OnBecameVisible(){
        #if UNITY_EDITOR
        enabled = !EditorApplication.isPaused;
        #else
        enabled = true;
        #endif
    }
    private void OnBecameInvisible(){
        enabled = false;
    }
    private void OnEnable(){
        rigidbody.WakeUp();
    }
    private void OnDisable(){
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }
    private void FixedUpdate(){
        Vector2 direccionNodo = currentNodo.position - transform.position;
        velocity.x = direccion.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + velocity*Time.fixedDeltaTime);
        if(Vector2.Distance(transform.position, currentNodo.position) < 0.5f && currentNodo == nodo1.transform){
            currentNodo = nodo2.transform;
            direccion = -direccion;
        } else if(Vector2.Distance(transform.position, currentNodo.position) < 0.5f && currentNodo == nodo2.transform){
            currentNodo = nodo1.transform;
            direccion = -direccion;
        }
        if(rigidbody.Raycast(Vector2.down)){
            velocity.y = Mathf.Max(velocity.y, 0f);
        }

        if(direccion.x > 0f){
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        } else if(direccion.x < 0f){
            transform.localEulerAngles = Vector3.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Enemigos")){
            if(direccion.x > 0f){
                direccion = -direccion;
                transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            } else if(direccion.x < 0f){
                direccion = -direccion;
                transform.localEulerAngles = Vector3.zero;
            }
        }
        
    }
}
