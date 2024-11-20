
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public BoxCollider2D boxCollider { get; private set;}
    public PlayerMovement movimiento { get; private set;}
    public SpriteRenderer miMaterial;
    public Color color;
    public bool muerto;
    private void Awake(){
        boxCollider = GetComponent<BoxCollider2D>(); 
        movimiento = GetComponent<PlayerMovement>();
        miMaterial = GetComponent<SpriteRenderer>();
    }
    public void Hit(){
        if(muerto){
            Muerte();
        }
    }

    

    private void Muerte(){
        miMaterial.color = color;
        Debug.Log(GameManager.Instancia);
        movimiento.enabled = false;
        GameManager.Instancia.ResetNivel(1f);
    }
}
