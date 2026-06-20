using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
           Vector3 mousePosition = mouse.position.ReadValue();
        //    Ray ray = m_Camera.ScreenPointToRay(mousePosition);
        //    if (Physics.Raycast(ray, out RaycastHit hit))
        //    {
        //        // Use the hit variable to determine what was clicked on.
        //    }
            Debug.Log("Mouse Clicked");
        }
    }

    void OnClick()
    {
        
    }

    void FixedUpdate()
    {
        
    }
}
