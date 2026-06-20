using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    Camera _camera; 
    Rigidbody2D _rigidbody; 

    [Range(0, 10)]
    public float thrust;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnDisable()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.isPressed)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Vector3 clickWorldPos = _camera.ScreenToWorldPoint(mousePosition);
            Vector3 pos = transform.position; 

            Vector3 forceDirection = pos - clickWorldPos;
            _rigidbody.AddForce(forceDirection * thrust);
        }
    }
}
