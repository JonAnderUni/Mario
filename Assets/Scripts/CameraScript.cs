using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 1.5f, -10f);
    public float tiempoSuave = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    private void Awake(){
        Application.targetFrameRate = 60;
        target =  GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate(){
        Vector3 cameraPosition = transform.position;
        Vector3 targetPosition = target.position + offset;
        

        cameraPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, tiempoSuave);
        
        cameraPosition.y = 10f;
        transform.position = cameraPosition;
        
    } 
}
