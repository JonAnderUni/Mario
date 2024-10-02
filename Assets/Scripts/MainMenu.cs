using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Jugar(){
        Invoke(nameof(JuegoNuevo), 0.5f);
    }
    private void JuegoNuevo(){
        GameManager.Instancia.NewGame();
    }
    public void Salir(){
        Application.Quit();
    }
}
