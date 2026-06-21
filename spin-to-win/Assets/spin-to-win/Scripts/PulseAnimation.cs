using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class PulseAnimation : MonoBehaviour
{
     [Range(1,25)]
    public float minSize;
    
    [Range(1,100)]
    public float maxSize; 
   
    public AnimationCurve alpha; 
    public AnimationCurve size;

    [Range(1,25)]
    public float duration;

    [Range(1,25)]
    public float startOffset;

    SpriteRenderer _renderer;
    Color _color;

    void OnEnable()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _color = _renderer.color;        
    }
 
    void Update()
    {
        float currentTime = (Time.realtimeSinceStartup + startOffset) % duration / duration;

        _color.a = alpha.Evaluate(currentTime);
        _renderer.color = _color;

        gameObject.transform.localScale = Vector3.one * (size.Evaluate(currentTime) * maxSize + minSize);
    }
}
