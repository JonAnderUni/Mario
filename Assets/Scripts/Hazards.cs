using UnityEngine;

public class Hazards : MonoBehaviour
{
    public void Awake(){
        enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision){
        
        if(collision.CompareTag("Player")){
            Debug.Log("Entre");
            Jugador jugador = collision.gameObject.GetComponent<Jugador>();
            jugador.Hit();
        }
    }
}
