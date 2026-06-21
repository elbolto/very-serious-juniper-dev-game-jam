using UnityEngine;
using UnityEngine.InputSystem;

public class AttachmentSystem : MonoBehaviour
{
    private DistanceJoint2D _joint; //same distance between two objects
    private LineRenderer _line; //draw line (temporary, later there is going to be a sprite)
    private Rigidbody2D _target;
    private bool _onCooldown;

    public bool isConnected { get; private set; } //read-only from outside class
    void Awake()
    {
        //set line renderer at runtime and set thickness etc.
        _line = gameObject.AddComponent<LineRenderer>();
        _line.positionCount = 2;
        _line.startWidth = 0.05f;
        _line.endWidth = 0.05f;
        _line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null) 
        {
            return;
        }
        //connect line between two points
        _line.SetPosition(0, transform.position);
        _line.SetPosition(1, _target.position);
        
        //detach on space bar
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Detach();
        }
    }

    public void Attach(Rigidbody2D target)
    {
        //if already connected return, there wont be any double connections
        if(isConnected || _onCooldown)
        {
            return;
        }
        _target = target;

        isConnected = true;

        //add joint at runtime, connect to target and let distance be calculated automatically
        _joint = target.gameObject.AddComponent<DistanceJoint2D>();
        _joint.connectedBody = GetComponent<Rigidbody2D>();
        _joint.autoConfigureDistance = false;
        _joint.distance = Vector2.Distance(transform.position, target.position);

        //enable line
        _line.enabled = true;
    }

    public async void Detach()
    {
        if(!isConnected)
        {
            return;
        }
        
        Destroy(_joint);
        _target = null;
        isConnected = false;
        _line.enabled = false;
        
        _onCooldown = true;
        await Awaitable.WaitForSecondsAsync(1f);
        _onCooldown = false;
    }
}
