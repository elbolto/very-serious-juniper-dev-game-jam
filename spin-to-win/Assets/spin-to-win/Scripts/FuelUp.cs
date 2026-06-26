using UnityEngine;

public class FuelUp : MonoBehaviour
{
    public CharacterMovement characterMovement;

    void OnTriggerEnter2D(Collider2D col)
    {
            characterMovement.ResetFuel();
            Destroy(gameObject);
    }
}
