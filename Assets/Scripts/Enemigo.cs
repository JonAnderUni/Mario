using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public Vector2 velocidad;
    public float gravity = -9.81f;

    public Sprite golpeadoSprite;

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            Jugador jugador = collision.gameObject.GetComponent<Jugador>();
            if(collision.transform.DotTest(transform, Vector2.down)){
                Muerte();
            } else {
                jugador.muerto = true;
                jugador.Hit();
                
            }
        }
        
    }
    public void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    
    
    private void OnEnable(){
        rigidbody.WakeUp();
    }
    private void OnDisable(){
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }
    private void Muerte(){
        GetComponent<Collider2D>().enabled = false;
        GetComponent<MovimientoEntidades>().enabled = false;
        GetComponent<AnimacionSprites>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = golpeadoSprite;
        Destroy(gameObject, 0.5f);
    }
}
