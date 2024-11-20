using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaFinal : MonoBehaviour
{
    public GameObject finalUI;
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            if(GameManager.Instancia.escenario == 3){

                other.gameObject.SetActive(false);
                EsperarSegundos(1f);
                finalUI.SetActive(true);
                
            }
            
        }
    }
    public void ReiniciarJuego(){
        finalUI.SetActive(false);
        GameManager.Instancia.CargarNivel1();

    }
    public void MenuPrincipal(){
        finalUI.SetActive(false);

        SceneManager.LoadScene(0);
    }
    public void SalirJuego(){
        finalUI.SetActive(false);

        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    private IEnumerator EsperarSegundos(float tiempo){
        yield return new WaitForSeconds(tiempo);
    }
}
