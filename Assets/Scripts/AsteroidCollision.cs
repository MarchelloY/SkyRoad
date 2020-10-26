using UnityEngine;

public class AsteroidCollision : ScoreController
{
    private void OnCollisionEnter(Collision hit)
    {
        //updating counters when passing an asteroid
        if (!hit.gameObject.CompareTag("Asteroid")) return;
        cScore += 5;
        asteroids++;
        PlayerPrefs.SetInt("allAsteroids", PlayerPrefs.GetInt("allAsteroids") + 1);
    }
}