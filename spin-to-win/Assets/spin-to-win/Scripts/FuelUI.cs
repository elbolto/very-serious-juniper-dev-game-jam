using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    private Slider slider;
    public CharacterMovement characterMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = characterMovement.fuel;
        slider.minValue = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Mathf.Clamp(characterMovement.fuel, slider.minValue, slider.maxValue);
    }
}
