using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Layers")]
    public AudioSource layer1;
    public AudioSource layer2;
    public AudioSource layer3;

    [Header("Speed Thresholds")]
    public float layer2Threshold = 3f;
    public float layer3Threshold = 7f;

    [Header("Fade")]
    public float fadeSpeed = 2f;

    [Header("References")]
    public CharacterMovement player;

    void Start()
    {
        layer1.loop = layer2.loop = layer3.loop = true;
        layer2.volume = 0f;
        layer3.volume = 0f;

        // start all layers in the same frame so they stay in sync
        layer1.Play();
        layer2.Play();
        layer3.Play();
    }

    void Update()
    {
        float speed = player != null ? player.Speed : 0f;

        float target2 = Mathf.Clamp01((speed - layer2Threshold) / (layer3Threshold - layer2Threshold)) * 0.8f;
        float target3 = Mathf.Clamp01((speed - layer3Threshold) / layer3Threshold) * 0.8f;

        layer2.volume = Mathf.Lerp(layer2.volume, target2, Time.deltaTime * fadeSpeed);
        layer3.volume = Mathf.Lerp(layer3.volume, target3, Time.deltaTime * fadeSpeed);
    }
}
