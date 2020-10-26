using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioSource soundAudioSource;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private Scrollbar volumeSoundScrollbar;
    [SerializeField] private Scrollbar volumeMusicScrollbar;

    private void Start()
    {
        soundAudioSource = GameObject.Find("Sounds").GetComponent<AudioSource>();
        var valueSound = PlayerPrefs.GetFloat("volumeSound");
        var valueMusic = PlayerPrefs.GetFloat("volumeMusic");
        volumeSoundScrollbar.value = valueSound;
        volumeMusicScrollbar.value = valueMusic;
        soundAudioSource.volume = valueSound;
        musicAudioSource.volume = valueMusic;
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
        PlayerPrefs.SetFloat("volumeSound", value);
        soundAudioSource.volume = value;
    }
    
    public void OnMusicChanged()
    {
        var value = volumeMusicScrollbar.value;
        PlayerPrefs.SetFloat("volumeMusic", value);
        musicAudioSource.volume = value;
    }
}
