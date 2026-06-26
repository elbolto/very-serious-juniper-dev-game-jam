using UnityEngine;

public class FuelUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
            CharacterMovement.Instance.ResetFuel();
            Destroy(gameObject);
    }
}
