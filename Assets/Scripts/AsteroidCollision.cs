using UnityEngine;

public class AsteroidCollision : ScoreController
{
    [SerializeField] private AudioClip countClip;
    private void OnCollisionEnter(Collision hit)
    {
        //updating counters when passing an asteroid
        if (!hit.gameObject.CompareTag("Asteroid")) return;
        cScore += 5;
        asteroids++;
        AudioController.soundAudioSource.PlayOneShot(countClip, AudioController.ValueVolumeSound);
        PlayerPrefs.SetInt("allAsteroids", PlayerPrefs.GetInt("allAsteroids") + 1);
    }
}