using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovimientoEntidades : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direccion = Vector2.right;

    private new Rigidbody2D rigidbody;
    private Vector2 velocity;

    private void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
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
        velocity.x = direccion.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + velocity*Time.fixedDeltaTime);
        bool pared = rigidbody.RaycastEnemigo(direccion, LayerMask.GetMask("Default"));
        if(pared){
            
            direccion.x = -direccion.x;
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
}
