using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Singleton instance - lets any script reach this without a manual reference
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources (auto-created, leave empty)")]
    [SerializeField] private AudioSource musicSource;   // for looping gameplay music
    [SerializeField] private AudioSource sfxSource;      // for one-shot sound effects

    [Header("Assign Clips In Inspector")]
    public AudioClip gameplayMusic;
    public AudioClip gameOverSound;
    public AudioClip hoopSuccessSound;
    public AudioClip purchaseSound;

    [Range(0f, 1f)] public float musicVolume = 0.6f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private void Awake()
    {
        // Enforce singleton - destroy duplicates if this script exists in multiple scenes
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Auto-create AudioSources so you don't have to wire them up manually
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.loop = false;
        }
    }

    // ---------- GAMEPLAY MUSIC ----------

    public void PlayGameplayMusic()
    {
        if (gameplayMusic == null) return;
        musicSource.clip = gameplayMusic;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    public void StopGameplayMusic()
    {
        musicSource.Stop();
    }

    // ---------- SOUND EFFECTS ----------

    public void PlayGameOver()
    {
        StopGameplayMusic(); // music ends when game over triggers
        PlaySfx(gameOverSound);
    }

    public void PlayHoopSuccess()
    {
        PlaySfx(hoopSuccessSound);
    }

    public void PlayPurchase()
    {
        PlaySfx(purchaseSound);
    }

    // Generic helper - plays any one-shot clip without cutting off overlapping sounds
    private void PlaySfx(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, sfxVolume);
    }
}