using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public AudioClip backgroundMusicClip; // AudioClip chứa nhạc nền
    private AudioSource audioSource; // AudioSource để phát nhạc

    void Start()
    {
        GameStart();
    }

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Đảm bảo chỉ có 1 instance
            return;
        }

        DontDestroyOnLoad(gameObject); // Đảm bảo không bị phá hủy khi chuyển scene

        // Khởi tạo AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true; // Lặp lại nhạc nền
        audioSource.playOnAwake = false; // Không phát tự động
    }

    public void GameStart()
    {
        ReloadScene.Instance.SettingStatus(false);
        PlayAudio(backgroundMusicClip); // Phát nhạc nền
    }

    public void PlayerIsDead()
    {
        ReloadScene.Instance.SettingStatus(true);
        ReloadScene.Instance.SettingText("Game Over");
        StopAudio(); // Dừng nhạc khi người chơi thua
    }

    public void Winner()
    {
        ReloadScene.Instance.SettingStatus(true);
        ReloadScene.Instance.SettingText("Winner");
        StopAudio(); // Dừng nhạc khi người chơi thắng
    }

    // Phương thức phát nhạc
    public void PlayAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    // Phương thức dừng nhạc
    public void StopAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
