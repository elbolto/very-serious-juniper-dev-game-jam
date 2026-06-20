using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AttachmentSystem : MonoBehaviour
{
    private DistanceJoint2D _joint; //same distance between two objects
    private LineRenderer _line; //draw line (temporary, later there is going to be a sprite)

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
        if(_joint == null)
        {
            return;
        }
        //connect line between two points
        _line.SetPosition(0, transform.position);
        _line.SetPosition(1, _joint.connectedBody.position);
    }

    public void Attach(Rigidbody2D target)
    {
        //if already connected return, there wont be any double connections
        if(_joint != null)
        {
            return;
        }

        //add joint at runtime, connect to target and let distance be calculated automatically
        _joint = gameObject.AddComponent<DistanceJoint2D>();
        _joint.connectedBody = target;
        _joint.autoConfigureDistance = true;

        //enable line
        _line.enabled = true;
    }
}
