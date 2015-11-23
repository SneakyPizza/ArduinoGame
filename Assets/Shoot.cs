using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
    public Transform bullet;
    public Transform gun;

    float shootAngle = 0;
    float shootCooldown = 0.25f;
    float currentShootCooldown = 0;
    int ignoreCount = 50;
    int horizontalDir;
    int verticalDir;
	void Start () {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SerialPortUnity>().HorizontalAxis2 += SetHorizontalShoot;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SerialPortUnity>().VerticalAxis2 += SetVerticalShoot;
    }
    void Update()
    {
        Aim();
    }
	
	void SetHorizontalShoot(int axis)
    {
        if (axis < ignoreCount && axis > -ignoreCount)
        {
            horizontalDir = 0;
        }
        else
        {
            horizontalDir = Mathf.Abs(axis) / axis;
        }
	}
    void SetVerticalShoot(int axis)
    {
        if (axis < ignoreCount && axis > -ignoreCount)
        {
            verticalDir = 0;
        }
        else
        {
            verticalDir = Mathf.Abs(axis) / axis;
        }
    }
    void Aim()
    {
        Debug.Log("vertical: " + verticalDir + " horizontal: " + horizontalDir);
        shootAngle = 0;
        if (verticalDir == 1)
        {
            shootAngle = 180;
            if (horizontalDir == 1)
            {
                shootAngle = 225;
            }
            else if (horizontalDir == -1)
            {
                shootAngle = 135;
            }
        }
        else if (verticalDir == -1)
        {
            shootAngle = 0;
            if (horizontalDir == 1)
            {
                shootAngle = 315;
            }
            else if (horizontalDir == -1)
            {
                shootAngle = 45;
            }
        }
        else
        {
            if (horizontalDir == 1)
            {
                shootAngle = 270;
            }
            else if (horizontalDir == -1)
            {
                shootAngle = 90;
            }
        }
        Fire();
        gun.transform.eulerAngles = new Vector3(0, 0, shootAngle);
    }
    void Fire()
    {
        if (currentShootCooldown < Time.time)
        {
            Instantiate(bullet, gun.transform.position, gun.transform.rotation);
            currentShootCooldown = Time.time + shootCooldown;
        }
    }
}
