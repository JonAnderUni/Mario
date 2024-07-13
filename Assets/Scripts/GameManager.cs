using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    public int mundo { get; private set; }
    public int escenario { get; private set;} 
    public int vida { get; private set; }
    private void Awake() {
        if(Instance == null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnDestroy() {
            if(Instance != null) {
                Instance = null;
            }
    }
    private void Start() {
        NewGame();
    }
    public void NewGame() {
        if(mundo == 1 && escenario == 1) {
            vida = 6;
        }
        CargarEscenario(mundo, escenario +1);
    }
    public void CargarEscenario(int mundo, int escenario) {
        this.mundo = mundo;
        this.escenario = escenario;

        SceneManager.LoadScene($"{mundo}-{escenario}");
    }
    public void ResetNivel(){
        //TODO
    }
    
    public void GameOver() {
        //TODO
    }

}
