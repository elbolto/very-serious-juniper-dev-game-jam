using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public event Action OnRespawned;
    public float respawnDelay = 3f;

    readonly StateMachine _stateMachine = new StateMachine();
    readonly State _inGame = new State();
    readonly State _dead = new State();
    readonly State _waitForRespawn = new State();

    float _respawnTimer;
    Vector3 _spawnPosition; 
    CharacterMovement _movement; 

    void Awake()
    {
        // save spawn position  
        _spawnPosition = transform.position;
        _movement = GetComponent<CharacterMovement>();

        ////////////////////////
        /// Gameplay State
        ////////////////////////
        _inGame.OnEnter = () =>
        {
            Debug.Log("Enter InGame State");
     
            // spawn ship
            transform.position = _spawnPosition;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            
            GetComponent<SpriteRenderer>().enabled = true;
            _movement.enabled = true;
     
            OnRespawned?.Invoke();
            // activate controls
        };

        _inGame.OnExit = () =>
        {
            _movement.enabled = false;
        };

        _inGame.Update = () =>
        {
           if (_movement.Speed < 0.4 && _movement.Fuel <= 0) 
            {
                Debug.Log("Faster, bitch!!");
                _stateMachine.Transition(_dead);            
            }
        };

        ////////////////////////
        /// Dead State
        ////////////////////////
        _dead.OnEnter = () => {
            Debug.Log("Enter Dead State");
            // kill ship
            GetComponent<SpriteRenderer>().enabled = false;

            _stateMachine.Transition(_waitForRespawn);
        };

        _dead.OnExit = () => { };
        _dead.Update = () => { };

        ////////////////////////
        /// Wait for Respawn State
        ////////////////////////
        _waitForRespawn.OnEnter = () => 
        {
            Debug.Log("Enter Respawn State");
            _respawnTimer = respawnDelay;
        };

        _waitForRespawn.OnExit = () => { };
        _waitForRespawn.Update = () =>
        {
            _respawnTimer -= Time.deltaTime;
            if (_respawnTimer <= 0f) 
            {
                _stateMachine.Transition(_inGame);
            }
        };

        _stateMachine.Transition(_inGame);
    }

    void Update()
    {
        _stateMachine?.Current?.Update();

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            _stateMachine.Transition(_inGame);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (_stateMachine.Current == _inGame && other.name == "Boundaries")
        {
            _stateMachine.Transition(_dead);            
        }
    }

    //for my score manager
    public bool IsAlive() 
    { 
        return _stateMachine.Current == _inGame; 
    }
}
