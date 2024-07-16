using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRendererJugador : MonoBehaviour
{
    private PlayerMovement movimiento;
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite quieto;
    public AnimacionSprites run;
    private void Awake(){
        movimiento = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void LateUpdate(){
        
    }
}
