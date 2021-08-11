public class GameEvents
{
    public delegate void EnemyDestroy();
    public static EnemyDestroy OnEnemyDetroy;

    public delegate void PlayerDestroy();
    public static PlayerDestroy OnPlayerDestroy;

    public delegate void UpdateUI();
    public static UpdateUI OnUIUpdate;

    public delegate void GameEnd();
    public static GameEnd OnGameEnd;

    public delegate void SpawnBonus();
    public static SpawnBonus OnSpawnBonus;

    public delegate void RemoveBonus();
    public static RemoveBonus OnRemoveBonus;

    public delegate void ShowSettingsMenu();
    public static ShowSettingsMenu OnShowMenu;

    public delegate void NewHiScore();
    public static NewHiScore OnNewHiScore;
}
