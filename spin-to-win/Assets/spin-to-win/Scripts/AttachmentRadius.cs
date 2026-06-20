using UnityEngine;

public class AttachmentRadius : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        //get necessary components attachmentSystem from ship and target from parent of collider object
        AttachmentSystem connection = col.GetComponent<AttachmentSystem>();
        Rigidbody2D target = transform.parent.GetComponent<Rigidbody2D>();

        //call Attach() from AttachmentSystem, create joint and draw line
        connection.Attach(target);
    }
}
