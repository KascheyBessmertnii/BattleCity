using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider mainSound;
    [SerializeField] private Slider effects;

    private void Awake()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        mainSound.value = PlayerPrefs.GetFloat("MainSound");
        effects.value = PlayerPrefs.GetFloat("EffectsSound");
    }

    private void OnEnable()
    {
        GameEvents.OnShowMenu += ShowMenu;
    }

    private void OnDisable()
    {
        GameEvents.OnShowMenu -= ShowMenu;
    }

    private void ShowMenu()
    {
        if (settingsPanel == null) return;
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
