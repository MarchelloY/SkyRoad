using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioSource soundAudioSource;
    public static float ValueVolumeSound
    {
        get => PlayerPrefs.GetFloat("volumeSound");
        private set => PlayerPrefs.SetFloat("volumeSound", value);
    }

    private static float ValueVolumeMusic
    {
        get => PlayerPrefs.GetFloat("volumeMusic");
        set => PlayerPrefs.SetFloat("volumeMusic", value);
    }

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private Scrollbar volumeSoundScrollbar;
    [SerializeField] private Scrollbar volumeMusicScrollbar;

    private void Start()
    {
        soundAudioSource = GameObject.Find("Sounds").GetComponent<AudioSource>();
        volumeSoundScrollbar.value = ValueVolumeSound;
        volumeMusicScrollbar.value = ValueVolumeMusic;
        soundAudioSource.volume = ValueVolumeSound;
        musicAudioSource.volume = ValueVolumeMusic;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && Settings.isBoost)
            soundAudioSource.Stop();
        if (Input.GetKeyDown(KeyCode.Space) && !Settings.isBoost)
            soundAudioSource.Play();
    }
    
    public void OnSoundChanged()
    {
        var value = volumeSoundScrollbar.value;
        ValueVolumeSound = value;
        soundAudioSource.volume = value;
    }
    
    public void OnMusicChanged()
    {
        var value = volumeMusicScrollbar.value;
        ValueVolumeMusic = value;
        musicAudioSource.volume = value;
    }
}
