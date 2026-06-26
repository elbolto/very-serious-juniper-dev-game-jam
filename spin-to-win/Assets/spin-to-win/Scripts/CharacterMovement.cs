using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    Camera _camera;
    Rigidbody2D _rigidbody;
    ParticleSystem _particles;

    [Range(0, 10)]
    public float thrust;
    public float maxFuel;
    private float _fuel;
    public float Fuel => _fuel; //readonly from outside
    public float Speed => _rigidbody != null ? _rigidbody.linearVelocity.magnitude : 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
        _particles = GetComponent<ParticleSystem>();
        _fuel = maxFuel;
    }

    void Update()
    {
        ThrustParticles();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_fuel > 0)
        {
            Thrust();
        }
    }

    void Thrust()
    {
        Mouse mouse = Mouse.current;
        Vector3 mousePosition = mouse.position.ReadValue();
        Vector3 clickWorldPos = _camera.ScreenToWorldPoint(mousePosition);
        Vector3 pos = transform.position; 
        Vector3 forceDirection = Vector3.Normalize(pos - clickWorldPos);
        
        if (mouse.leftButton.isPressed)
        { 
            _rigidbody.AddForce(forceDirection * thrust);
            _fuel = Mathf.Max(0f, _fuel - Time.fixedDeltaTime);

        }

        Vector2 direction = forceDirection.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
    }

    void ThrustParticles()
    {
        Mouse mouse = Mouse.current;
        var emission = _particles.emission;
        emission.enabled = _fuel > 0 && mouse.leftButton.isPressed;
    }

    public void ResetFuel()
    {
        _fuel = maxFuel;
    }
}
