
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public BoxCollider2D boxCollider { get; private set;}
    public PlayerMovement movimiento { get; private set;}
    public AnimacionMuerte animacionMuerte { get; private set;}
    public SpriteRendererJugador spriteRendererJugador;
    
    public bool muerto => animacionMuerte.enabled;

    private void Awake(){
        boxCollider = GetComponent<BoxCollider2D>(); 
        movimiento = GetComponent<PlayerMovement>();
        animacionMuerte = GetComponent<AnimacionMuerte>();
    }
    public void Hit(){
        if(muerto){
            Muerte();
        }
    }

    private void Muerte(){
        spriteRendererJugador.enabled = false;
        animacionMuerte.enabled = true;
        GameManager.Instance.ResetNivel(3f);
    }
}
