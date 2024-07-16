using System.Collections;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            StartCoroutine(CargarSiguienteNivel(other.transform));
        }
    }
    
    private IEnumerator CargarSiguienteNivel(Transform jugador){
        yield return new WaitForSeconds(3f);

        jugador.gameObject.SetActive(false);
        
    }
}
