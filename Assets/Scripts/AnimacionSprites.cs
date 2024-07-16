using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimacionSprites : MonoBehaviour
{
   public Sprite[] sprites;
   public float framerate=1f/6f;
   private SpriteRenderer spriteRenderer;
   private int frame;
   private void Awake(){
      spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
   } 
   private void OnEnable(){
      InvokeRepeating(nameof(Animar), framerate, framerate);
   }

   private void OnDisable(){
      CancelInvoke();
   }
   private void Animar(){
      frame++;
      if(frame >= sprites.Length){
         frame = 0;
      }
      if(frame>=0 && frame < sprites.Length){
         spriteRenderer.sprite = sprites[frame];
      }  
   }
}