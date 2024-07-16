using System.Collections;
using UnityEngine;

public class AnimacionMuerte : MonoBehaviour
{
    private void OnEnable()
    {
        DisablePhysics();
        StartCoroutine(Animate());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();

        for (int i = 0; i < colliders.Length; i++) {
            colliders[i].enabled = false;
        }

        if (TryGetComponent(out Rigidbody2D rigidbody)) {
            rigidbody.isKinematic = true;
        }

        if (TryGetComponent(out PlayerMovement playerMovement)) {
            playerMovement.enabled = false;
        }

        if (TryGetComponent(out MovimientoEntidades movimientoEntidad)) {
            movimientoEntidad.enabled = false;
        }
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 3f;

        float jumpVelocity = 10f;
        float gravity = -36f;

        Vector3 velocity = Vector3.up * jumpVelocity;

        while (elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
