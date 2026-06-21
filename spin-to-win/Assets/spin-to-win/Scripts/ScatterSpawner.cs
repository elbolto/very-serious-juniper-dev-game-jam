using UnityEngine;

public class ScatterSpawner : MonoBehaviour
{
    public GameObject prefab;
    public int count = 20;
    public Vector2 bounds = new Vector2(10f, 10f);

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(-bounds.x, bounds.x),
                Random.Range(-bounds.y, bounds.y),
                0f
            );
            Instantiate(prefab, position, Quaternion.identity, transform);
        }
    }
}
