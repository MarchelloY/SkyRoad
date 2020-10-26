using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Flags]
public enum StateAsteroid
{
    None,
    Red = 1,
    Scale = 2,
    Blue = 4
}

public class AsteroidController : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject player;
    public float posAsteroidY = 4;

    [SerializeField] private StateAsteroid firstStateAsteroid = StateAsteroid.None;

    [SerializeField] private StateAsteroid secondStateAsteroid = StateAsteroid.None;

    private List<GameObject> _asteroids;


    private void Awake()
    {
        var selectedStates = firstStateAsteroid | secondStateAsteroid;
        if (selectedStates.HasFlag(StateAsteroid.Red)) Debug.Log("selected color red");
        if (selectedStates.HasFlag(StateAsteroid.Blue)) Debug.Log("selected color blue");
        if (selectedStates.HasFlag(StateAsteroid.Scale)) Debug.Log("asteroid scale * 2");
    }

    private void Start()
    {
        StartCoroutine(SpawnAsteroids());
        var list = new List<int> {1, 2, 3, 4, 7, 5, 8};
        Debug.Log(Utility.SumAllEvenNumbers(list));
    }

    private void Update()
    {
        _asteroids = GameObject.FindGameObjectsWithTag("Asteroid").ToList();
        //rotation, movement and removal of asteroids
        foreach (var item in _asteroids)
        {
            item.transform.Rotate(0, 250 * Time.deltaTime, 0);
            if (item.transform.position.z < -Settings.Size) Destroy(item);
            var temp = new Vector3(0, 0, -Settings.speed * Time.deltaTime);
            if (Settings.isBoost) temp *= 2;
            item.transform.position += temp;
        }

        if (Input.GetKeyDown(KeyCode.Space)) RemoveAsteroid();
    }

    private IEnumerator SpawnAsteroids()
    {
        //creation of new asteroids
        yield return new WaitForSeconds(Settings.isBoost ? Settings.speedSpawn / 2 : Settings.speedSpawn);
        float x = Random.Range(-10, 10);
        var y = posAsteroidY;
        float z = Settings.Size * Settings.NChunks;
        var posGenAsteroid = new Vector3(x, y, z);
        Instantiate(asteroid, posGenAsteroid, Quaternion.identity);
        StartCoroutine(SpawnAsteroids());
    }

    private void RemoveAsteroid()
    {
        if (_asteroids.Count != 0)
            Destroy(_asteroids.OrderBy(o => Vector3
                    .Distance(o.transform.position, player.transform.position))
                .First());
    }
}