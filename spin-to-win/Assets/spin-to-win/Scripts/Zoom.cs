using UnityEngine;
using UnityEngine.InputSystem;

public class Zoom : MonoBehaviour {
    public float minSize = 10f;
    public float maxSize = 85f;
    public float zoomStep = 1f; 
    public float zoomSpeed = 10f;
    Camera cam;
    private float _targetSize;

    void Awake()
    {
        cam = Camera.main;
        _targetSize = cam.orthographicSize;
    }

    void Update()
    {
        float scrollDir = Mouse.current.scroll.ReadValue().y;

        if (scrollDir < 0 && _targetSize < maxSize)
        {
            _targetSize = Mathf.Min(maxSize, _targetSize + zoomStep);
        }

        if (scrollDir > 0 && _targetSize > minSize)
        {
            _targetSize = Mathf.Max(minSize, _targetSize - zoomStep);
        }

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, _targetSize, Time.deltaTime * zoomSpeed);
    }
}