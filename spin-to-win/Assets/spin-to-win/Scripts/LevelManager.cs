using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] segmentPrefabs;
    [SerializeField] float gridSize = 20f;
    [SerializeField] Transform ship;

    readonly Dictionary<Vector2Int, GameObject> _active = new();
    Vector2Int _lastBlockOrigin = new(int.MinValue, int.MinValue);

    void Start()
    {
        if (ship == null)
        {
            Debug.LogError("LevelManager: ship reference is not set.");
            return;
        }

        RefreshSegments();
    }

    void Update()
    {
        if (ship == null) return;
        RefreshSegments();
    }

    void RefreshSegments()
    {
        Vector2Int blockOrigin = BlockOrigin(ship.position);
        if (blockOrigin == _lastBlockOrigin) return;

        _lastBlockOrigin = blockOrigin;

        var needed = new HashSet<Vector2Int>
        {
            blockOrigin,
            blockOrigin + Vector2Int.right,
            blockOrigin + Vector2Int.up,
            blockOrigin + Vector2Int.right + Vector2Int.up,
        };

        var toRemove = new List<Vector2Int>();
        foreach (var cell in _active.Keys)
        {
            if (!needed.Contains(cell))
                toRemove.Add(cell);
        }

        foreach (var cell in toRemove)
        {
            Destroy(_active[cell]);
            _active.Remove(cell);
        }

        foreach (var cell in needed)
        {
            if (!_active.ContainsKey(cell))
                SpawnSegment(cell);
        }
    }

    // Returns the bottom-left cell of the 2x2 block, chosen so the ship is
    // always near the center. The block shifts when the ship crosses a cell midpoint.
    Vector2Int BlockOrigin(Vector3 pos)
    {
        return new Vector2Int(
            Mathf.FloorToInt(pos.x / gridSize - 0.5f),
            Mathf.FloorToInt(pos.y / gridSize - 0.5f)
        );
    }

    void SpawnSegment(Vector2Int cell)
    {
        if (segmentPrefabs == null || segmentPrefabs.Length == 0)
        {
            Debug.LogWarning("LevelManager: no segment prefabs assigned.");
            return;
        }

        var prefab = segmentPrefabs[Random.Range(0, segmentPrefabs.Length)];
        // Center of cell so prefabs with local origin at center tile correctly
        var worldPos = new Vector3((cell.x + 0.5f) * gridSize, (cell.y + 0.5f) * gridSize, 0f);
        _active[cell] = Instantiate(prefab, worldPos, Quaternion.identity, transform);
    }
}
