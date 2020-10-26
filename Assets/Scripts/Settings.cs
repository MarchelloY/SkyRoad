using UnityEngine;

public class Settings : MonoBehaviour
{
    public const int Size = 40;
    public const int NChunks = 20;

    public static float speed;
    public static float speedSpawn;

    public static bool isBoost = false;
    public static bool isGameOver = false;
    public static bool isNewRecord = false;

    //returns the position of the last chunk
    public static Vector3 GetLastPosSpawn()
    {
        return new Vector3(0, 0, Size * NChunks);
    }
}