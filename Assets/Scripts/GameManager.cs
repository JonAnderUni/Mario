using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set;}

    public int mundo { get; private set; }
    public int escenario { get; private set;} 
    public int vida { get; private set; }
    private void Awake() {
        if(Instancia != null) {
            DestroyImmediate(gameObject);
        } else {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        mundo = 1;
        escenario = 1;
    }
    private void OnDestroy() {
        if(Instancia == this) {
            Instancia = null;
        }
    }
    private void Start() {
        NewGame();
    }
    public void NewGame() {
        if(mundo == 1 && escenario == 1) {
            vida = 6;
        }
        CargarEscenario(mundo, escenario);
    }
    public void CargarEscenario(int mundo, int escenario) {
        this.mundo = mundo;
        this.escenario = escenario;

        SceneManager.LoadScene($"{mundo}-{escenario}");
    }
    public void SiguienteNivel(){
        CargarEscenario(mundo, escenario+1);
    }
    public void ResetNivel(float delay){
        Invoke(nameof(ResetNivel), delay);
    }
    private void ResetNivel(){
        vida--;
        if(vida > 0) {
            CargarEscenario(mundo, escenario);
        } else {
            GameOver();
        }
    }
    public void GameOver() {
        NewGame();
    }

}
