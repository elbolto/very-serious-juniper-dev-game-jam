using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    public CharacterMovement characterMovement;
    public Player player;
    private Slider slider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = characterMovement.maxFuel;
        slider.minValue = 0f;
        player.OnRespawned += Reset;

    }

    // Update is called once per frame
    void Update()
    {
        slider.value = characterMovement.Fuel;
    }

    public void Reset()
    {
        characterMovement.ResetFuel();
        slider.value = characterMovement.maxFuel;
    }
}
