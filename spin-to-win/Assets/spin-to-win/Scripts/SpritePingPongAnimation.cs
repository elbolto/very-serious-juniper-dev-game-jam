using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpritePingPongAnimation : MonoBehaviour
{
    public Sprite[] frames = new Sprite[4];
    public float speed = 1f;

    SpriteRenderer _spriteRenderer;
    float _time;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // random start offset so multiple instances don't animate in sync
        _time = Random.value * (frames.Length - 1) * 2f;
    }

    void Update()
    {
        _time += Time.deltaTime * speed;
        int index = Mathf.RoundToInt(Mathf.PingPong(_time, frames.Length - 1));
        _spriteRenderer.sprite = frames[index];
    }
}
