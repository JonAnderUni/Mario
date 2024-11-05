
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public BoxCollider2D boxCollider { get; private set;}
    public PlayerMovement movimiento { get; private set;}
    public bool muerto;
    private void Awake(){
        boxCollider = GetComponent<BoxCollider2D>(); 
        movimiento = GetComponent<PlayerMovement>();
    }
    public void Hit(){
        if(muerto){
            Muerte();
        }
    }

    

    private void Muerte(){
        Debug.Log(GameManager.Instancia);
        movimiento.enabled = false;
        GameManager.Instancia.ResetNivel(3f);
    }
}
