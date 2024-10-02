using UnityEngine;

public class Collecionables : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider){
        GameManager.Instancia.RecogerLlave();
        Destroy(gameObject);
    }
}
