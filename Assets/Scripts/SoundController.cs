using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    [SerializeField] private AudioSource mainSound;
    [SerializeField] private AudioSource soundEffects;
    [Header("Sound resources")]
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private AudioClip destroyBase;
    [SerializeField] private AudioClip clipBullet;
    [SerializeField] private AudioClip collectBonus;

    #region Unity methods
    private void Awake()
    {
        if (instance == null)
            instance = this;
        mainSound.volume = PlayerPrefs.GetFloat("MainSound");
        soundEffects.volume = PlayerPrefs.GetFloat("EffectsSound");
    }

    private void OnEnable()
    {
        GameEvents.OnGameEnd += PlayBaseDestroy;
    }

    private void OnDisable()
    {
        GameEvents.OnGameEnd -= PlayBaseDestroy;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("MainSound", mainSound.volume);
        PlayerPrefs.SetFloat("EffectsSound", soundEffects.volume);
    }
    #endregion

    #region Private methods
    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        if(mainSound.isPlaying)
        {
            soundEffects.clip = clip;
            soundEffects.Play();
        }
        else
        {
            mainSound.clip = clip;
            mainSound.Play();
        }
    }
    #endregion

    #region Public methods
    public void PlayDestroy()
    {
        PlaySound(destroySound);
    }
    public void PlayBaseDestroy()
    {
        PlaySound(destroyBase);
    }
    public void PlayCollectBonus()
    {
        PlaySound(collectBonus);
    }
    public void PlayClip()
    {
        PlaySound(clipBullet);
    }
    public void SetMainVolume(float value)
    {
        mainSound.volume = value;
    }
    public void SetEffectsVolume(float value)
    {
        soundEffects.volume = value;
    }
    #endregion
}
