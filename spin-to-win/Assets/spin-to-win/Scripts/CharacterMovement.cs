using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    Camera _camera; 
    Rigidbody2D _rigidbody; 
    ParticleSystem _particles; 

    [Range(0, 10)]
    public float thrust;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
        _particles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        Mouse mouse = Mouse.current;
        var emission = _particles.emission;
        emission.enabled = mouse.leftButton.isPressed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Mouse mouse = Mouse.current;
        Vector3 mousePosition = mouse.position.ReadValue();
        Vector3 clickWorldPos = _camera.ScreenToWorldPoint(mousePosition);
        Vector3 pos = transform.position; 
        Vector3 forceDirection = Vector3.Normalize(pos - clickWorldPos);
        
        if (mouse.leftButton.isPressed)
        { 
            _rigidbody.AddForce(forceDirection * thrust);
        }

        Vector2 direction = forceDirection.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
    }
}
