using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCreateController : MonoBehaviour
{
    [SerializeField] private Vector2Int mapSize = new Vector2Int(26, 26);
    [SerializeField] private int squareSize = 1;
    [SerializeField, Range(-10f, 10f)] private float offsetX = 0;
    [SerializeField, Range(-10f, 10f)] private float offsetZ = 0;
    [SerializeField] private LayerMask buildLayer;

    [Header("Ui create buttons")]
    [SerializeField] private Button btnCreateWall;
    [SerializeField] private Button btnCreateBrone;
    [Space(10)]
    [SerializeField] private Button btnSave;
    [SerializeField] private Button btnClose;
    [Space(10)]
    [SerializeField] private TMP_InputField iLevelNum;


    private Camera mainCamera;

    private GameObject currentPrefab;
    private ObjectsTypes currentType;
    private Vector2Int prefabSize;
    private float prefabOffset = 0f;
    private bool prefabMultiply = true;

    private Level level;

    private void Awake()
    {
        SceneData.LoadPrefabs();
        level = new Level(mapSize);

        //Create base
        SpawnObjectInPosition(SceneData.basePrefab, new Vector3(mapSize.x / 2, 0, 1), new Vector2Int(2, 2), ObjectsTypes.Base);
        //Create player spawn points
        SpawnObjectInPosition(SceneData.p1Spawner, new Vector3(mapSize.x / 2 - 3, 0, 1), new Vector2Int(2, 2), ObjectsTypes.Player1);
        SpawnObjectInPosition(SceneData.p2Spawner, new Vector3(mapSize.x / 2 + 3, 0, 1), new Vector2Int(2, 2), ObjectsTypes.Player2);
        //Create enemy spawn points
        SpawnObjectInPosition(null, new Vector3(1, 0, mapSize.x - 1), new Vector2Int(2, 2), ObjectsTypes.Enemy1);
        SpawnObjectInPosition(null, new Vector3(mapSize.x-1, 0, mapSize.y - 1), new Vector2Int(2, 2), ObjectsTypes.Enemy2);
        SpawnObjectInPosition(null, new Vector3(mapSize.x / 2, 0, mapSize.y - 1), new Vector2Int(2, 2), ObjectsTypes.Enemy3);
        mainCamera = Camera.main;

        btnCreateWall?.onClick.AddListener(() => CreateObject(ObjectsTypes.BrickWall, SceneData.wallPrefab, Vector2Int.one, 0.5f, true));
        btnCreateBrone?.onClick.AddListener(() => CreateObject(ObjectsTypes.BroneWall, SceneData.bronePrefab, Vector2Int.one, 0.5f, true));
        btnClose?.onClick.AddListener(() => CloseLevel());
        btnSave?.onClick.AddListener(() => SaveLevel());
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
                currentPrefab.transform.position = Utility.GetPositionOnMap(hit.point, mapSize, prefabOffset);
            }

            if (Input.GetMouseButtonDown(1))
            {
                ResetObject();
            }

            if (Input.GetMouseButtonDown(0) && hit.collider != null)
            {
                SpawnObjectInPosition(currentPrefab, currentPrefab.transform.position, prefabSize, currentType);
            }
        }
        else if(Input.GetMouseButtonDown(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            LayerMask layer = LayerMask.GetMask("Objects");

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, layer))
            {
                level.RemoveObject(hit.transform.position);
                Destroy(hit.transform.gameObject);
                
            }
        }
    }

    private void ResetObject()
    {
        Destroy(currentPrefab);
        currentPrefab = null;
        currentType = ObjectsTypes.None;
        prefabSize = Vector2Int.zero;
        prefabOffset = 0f;
        prefabMultiply = true;
    }

    #region UI buttons
    public void CreateObject(ObjectsTypes type, GameObject prefab, Vector2Int size, float offset, bool multiply)
    {
        ResetObject();

        if (prefab != null)
        {
            currentPrefab = Instantiate(prefab);
            currentType = type;
            prefabSize = size;
            prefabOffset = offset;
            prefabMultiply = multiply;
        }
    }

    public void CloseLevel()
    {
        SceneLoader.LoadSceneAsync(0);
    }

    public void SaveLevel()
    {
        if (string.IsNullOrEmpty(iLevelNum.text)) return;
        if (int.TryParse(iLevelNum.text, out int result))
        {
            level.SaveLevel(iLevelNum.text);
        }
    }
    #endregion

    private void SpawnObjectInPosition(GameObject prefab, Vector3 position, Vector2Int size, ObjectsTypes type)
    {
        if (!prefabMultiply && level.Contains(type)) return;

        if (level.SquareEmpty((int)position.x, (int)position.z))
        {
            int offset = 0;

            if (size.x > 1)
               offset = size.x / 2;

            if(prefab != null)
            {
                Instantiate(prefab, position, Quaternion.identity);
            }         

            for (int x = (int)position.x - offset; x < (int)position.x + size.x - offset; x++)
            {
                for (int y = (int)position.z; y > (int)position.z - size.y; y--)
                {
                    level.SetObject(new Vector2Int(x, y), position, type);
                }
            }
        }
    }
}
