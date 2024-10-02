using UnityEngine;

public class Hazards : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            Jugador jugador = collision.gameObject.GetComponent<Jugador>();
            jugador.muerto = true;
            jugador.Hit();
        }
    }
}
