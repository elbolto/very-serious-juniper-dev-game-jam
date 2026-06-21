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

    public SpriteRenderer[] renderers; 

    void OnEnable()
    {
    }
 
    void Update()
    {
        for (var i=0; i<renderers.Length; i++)
        {
            float startOffset = duration/(float)renderers.Length*i;
            float currentTime = (Time.realtimeSinceStartup + startOffset) % duration / duration;
            Color color = renderers[i].color;
            color.a = alpha.Evaluate(currentTime);
            renderers[i].color = color;

            renderers[i].gameObject.transform.localScale = 
                Vector3.one * (size.Evaluate(currentTime) * maxSize + minSize);        
        }

    }
}
