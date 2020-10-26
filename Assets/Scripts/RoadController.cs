using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    public GameObject chunk;
    private List<GameObject> _chunks;
    private Vector3 _lastPosChunk;

    private void Start()
    {
        GenerateRoad(Settings.Size, Settings.NChunks);
        //initial configuration
        _chunks = GameObject.FindGameObjectsWithTag("Chunk").ToList();
        _lastPosChunk = Settings.GetLastPosSpawn();
    }

    private void Update()
    {
        MoveRoad();
    }

    private void GenerateRoad(int step, int nChunks)
    {
        //initial road generation
        for (var i = -step; i < step * nChunks; i += step)
            Instantiate(chunk, new Vector3(0, 0, i), Quaternion.identity);
    }

    private void MoveRoad()
    {
        //moving each chunk and road loop
        foreach (var item in _chunks)
        {
            if (item.transform.position.z < -40f)
                item.transform.position = _lastPosChunk + new Vector3(0, 0, item.transform.position.z + Settings.Size);
            var temp = new Vector3(0, 0, -Settings.speed * Time.deltaTime);
            if (Settings.isBoost) temp *= 2;
            item.transform.position += temp;
        }
    }
}