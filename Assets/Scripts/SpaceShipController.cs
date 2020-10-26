using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    public float maxAngleRot = 20;

    private void Update()
    {
        SpaceShipMoveCheck();
        if (Time.timeScale.Equals(1)) SpaceShipBoostCheck();
    }

    private void OnCollisionEnter(Collision hit)
    {
        //asteroid and spaceship collision check
        if (hit.gameObject.CompareTag("Asteroid"))
        {
            //turn on pause
            Time.timeScale = 0;
            //remove the asteroid
            Destroy(hit.gameObject);
            Settings.isGameOver = true;
        }
    }

    private void SpaceShipMoveCheck()
    {
        //ship rotation when moving
        var quat = Quaternion.Euler(0, 0, -maxAngleRot * Input.GetAxis("Horizontal"));
        transform.rotation = Quaternion.Lerp(transform.rotation, quat, Time.deltaTime * 10);
        //movement of the ship to the left and to the right with restrictions
        var currentPos = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * 50, 0, 0);
        var temp = currentPos + transform.position;
        if (temp.x > 16 || temp.x < -16) currentPos = Vector3.zero;
        transform.position += currentPos;
    }

    private void SpaceShipBoostCheck()
    {
        //check if the space was pressed
        if (Input.GetKeyUp(KeyCode.Space) && Settings.isBoost)
            Settings.isBoost = false;
        if (Input.GetKeyDown(KeyCode.Space) && !Settings.isBoost)
            Settings.isBoost = true;

        //camera view control
        var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        var fieldOfView = mainCamera.fieldOfView;
        fieldOfView +=
            fieldOfView < 70 && Settings.isBoost ? 0.7f :
            fieldOfView > 60 && !Settings.isBoost ? -0.7f : 0;
        mainCamera.fieldOfView = fieldOfView;

        //thruster control
        foreach (var obj in GameObject.FindGameObjectsWithTag("Flame"))
        {
            var v = obj.GetComponent<ParticleSystem>().main;
            v.startSpeed = !Settings.isBoost ? 0 : 5;
        }
    }
}