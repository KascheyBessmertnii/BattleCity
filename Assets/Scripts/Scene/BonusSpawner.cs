using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [Header("Bonuses spawn field")]
    [SerializeField] private Transform topLeftPoint;
    [SerializeField] private Transform bottomRightPoint;

    [Header("Bonus array")]
    [SerializeField] private Bonus[] bonuses;

    private Bonus currentBonus;

    #region Unity methods
    private void OnEnable()
    {
        GameEvents.OnSpawnBonus += Spawn;
    }
    private void OnDisable()
    {
        GameEvents.OnSpawnBonus -= Spawn;
    }
    #endregion

    #region Private methods
    private void Spawn()
    {

        int rnd = (int)Random.Range(0, (float)bonuses.Length);

        if (currentBonus != null)
            Destroy(currentBonus.gameObject);

        currentBonus = Instantiate(bonuses[rnd], GetRandomPosition(), Quaternion.identity);
    }
    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(topLeftPoint.position.x, bottomRightPoint.position.x);
        float z = Random.Range(topLeftPoint.position.z, bottomRightPoint.position.z);

        return new Vector3(x, 1.1f, z);
    }
    #endregion
}