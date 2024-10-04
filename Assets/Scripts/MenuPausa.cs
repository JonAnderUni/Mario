using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public static bool EstaPausado = false;
    public GameObject pausaMenuUI;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(EstaPausado){
                Continuar();
            }
            else {
                Pausar();
            }
        }
    }
    public void Continuar(){
        Time.timeScale = 1f;
        pausaMenuUI.SetActive(false);
        EstaPausado = false;
    }
    public void Pausar(){
        Time.timeScale = 0f;
        pausaMenuUI.SetActive(true);
        EstaPausado = true;
    }

    public void CargarMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void SalirJuego(){
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
