using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 3f, -10f);
    public float tiempoSuave = 0.25f;
    private Vector3 velocity = Vector3.zero;

    private Transform target;

    private void Awake(){
        target =  GameObject.FindWithTag("Player").transform;
    }

    private void Update(){
        Vector3 cameraPosition = transform.position;
        Vector3 targetPosition = target.position + offset;
        

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, tiempoSuave);

    } 
}
