using System.Collections;
using UnityEngine;

public class Puerta : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            if(GameManager.Instancia.escenario == 3){
                other.gameObject.SetActive(false);
                
            }
            StartCoroutine(CargarSiguienteNivel(other.transform));
        }
    }
    
    private IEnumerator CargarSiguienteNivel(Transform jugador){
        jugador.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(1f);
        
        GameManager.Instancia.SiguienteNivel();
    }
}
