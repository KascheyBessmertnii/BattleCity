using UnityEngine;

public class LevelCreateController : MonoBehaviour
{
    [SerializeField] private Vector2Int mapSize = new Vector2Int(26, 26);
    [SerializeField] private int squareSize = 1;
    [SerializeField, Range(-10f, 10f)] private float offsetX = 0;
    [SerializeField, Range(-10f, 10f)] private float offsetZ = 0;
    [SerializeField] private LayerMask buildLayer;

    private Camera mainCamera;
    private GameObject wallPrefab;
    private GameObject currentPrefab;

    private GameObject basePrefab;

    private Level level;

    private void Awake()
    {
        level = new Level(mapSize);

        wallPrefab = Resources.Load("Prefabs/Wall") as GameObject;
        basePrefab = Resources.Load("Prefabs/Base") as GameObject;

        SpawnObjectInPosition(basePrefab, new Vector3(mapSize.y / 2, 0, 1), new Vector2Int(2, 2), ObjectsTypes.Base);
        mainCamera = Camera.main;
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            Gizmos.DrawLine(new Vector3(offsetX + x * squareSize, 0, 0), new Vector3(offsetX + x * squareSize, 0, mapSize.y * squareSize + offsetZ));
        }

        for (int z = 0; z < mapSize.y; z++)
        {
            Gizmos.DrawLine(new Vector3(offsetX, 0, offsetZ + z * squareSize), new Vector3(offsetX + mapSize.x * squareSize, 0, offsetZ + z * squareSize));
        }
    }

    private void Update()
    {
        if (currentPrefab != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, buildLayer))
            {
                currentPrefab.transform.position = Utility.GetPositionOnMap(hit.point, mapSize, 0.5f);
            }

            if (Input.GetMouseButtonDown(1))
            {
                ResetObject();
            }

            if (Input.GetMouseButtonDown(0))
            {
                SpawnObjectInPosition(currentPrefab, currentPrefab.transform.position, new Vector2Int(1, 1), ObjectsTypes.BrickWall);
            }
        }
    }

    private void ResetObject()
    {
        Destroy(currentPrefab);
        currentPrefab = null;
    }

    #region UI buttons
    public void CreateWall()
    {
        currentPrefab = Instantiate(wallPrefab, transform.position, Quaternion.identity);
    }
    #endregion

    private void SpawnObjectInPosition(GameObject prefab, Vector3 position, Vector2Int size, ObjectsTypes type)
    {
        if (level.SquareEmpty((int)position.x, (int)position.z))
        {
            int offset = 0;

            if (size.x > 1)
                offset = size.x / 2;

        Instantiate(prefab, position, Quaternion.identity);

            for (int x = (int)position.x - offset; x < (int)position.x + size.x - offset; x++)
            {
                for (int y = (int)position.z; y > (int)position.z - size.y; y--)
                {
                    level.SetObject(x, y, type);
                }
            }
        }
    }
}
