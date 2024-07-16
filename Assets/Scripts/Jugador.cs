
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public SpriteRendererJugador spriteRendererJugador;
    private AnimacionMuerte animacionMuerte;

    private void Awake(){
        animacionMuerte = GetComponent<AnimacionMuerte>(); 
    }
    public void Hit(){
        if(GameManager.Instance.vida == 0){
            Muerte();
        } else {
            GameManager.Instance.ResetNivel(3f);
        }
    }

    private void Muerte(){
        spriteRendererJugador.enabled = false;
        animacionMuerte.enabled = true;
        GameManager.Instance.ResetNivel(3f);
    }
}
