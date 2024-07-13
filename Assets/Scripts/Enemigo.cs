using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public Vector2 velocidad;
    public float gravity = -9.81f;
    public void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void FixedUpdate()
    {
        
    }

    private void EnPantalla(){
        enabled = true;
    }
    private void FueraPantalla(){
        enabled = false;
    }
    
    private void Habilitar(){
        rigidbody.WakeUp();
    }
    private void Deshabilitar(){
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }
}
