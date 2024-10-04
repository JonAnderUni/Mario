using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set;}
    public int vida;
    public int vidaMax;
    public Image[] corazones;
    public Sprite corazonVacio;
    public Sprite corazonLleno;
    public GameObject corazonesUI;
    public int mundo { get; private set; }
    public int escenario { get; private set;} 
    public int llaves {get; private set; }
    
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
    private void Update() {
        if (SceneManager.GetActiveScene().buildIndex != 0){
            corazonesUI.SetActive(true);
        } else {
            corazonesUI.SetActive(false);
        }
        for(int i = 0; i < corazones.Length; i++){
            if(i< vida){
                corazones[i].sprite = corazonLleno;
            }else{
                corazones[i].sprite = corazonVacio;
            }
            if(i< vidaMax){
                corazones[i].enabled = true;
            }else{
                corazones[i].enabled = false;
            }
        }

    }
    
    private void OnDestroy() {
        if(Instancia == this) {
            Instancia = null;
        }
    }
    private void Start() {
        SceneManager.LoadScene(0);
    }
    public void NewGame() {
        if(mundo == 1 && escenario == 1) {
            vida = 3;
            vidaMax = 3;
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
    public void RecogerLlave(){
        llaves++;
    }

}
