using UnityEngine;

public class BloquesLlave : MonoBehaviour
{
    [SerializeField] public int llavesRequeridas;
    private void OnCollisionEnter2D(Collision2D collision){
        Debug.Log("Entro");
        if (collision.gameObject.CompareTag("Player") && GameManager.Instancia.llaves == llavesRequeridas){
            Destroy(gameObject);
            GameManager.Instancia.llaves = 0;
        }
    }
}
