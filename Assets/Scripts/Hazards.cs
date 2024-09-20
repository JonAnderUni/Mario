using UnityEngine;

public class Hazards : MonoBehaviour
{
    public void Awake(){
        enabled = true;
    }
    private void OnCollisionEnter2D(Collision2D collision){
        Debug.Log("Entre");
        if(collision.gameObject.CompareTag("Player")){
            Jugador jugador = collision.gameObject.GetComponent<Jugador>();
            jugador.Hit();
        }
    }
}
