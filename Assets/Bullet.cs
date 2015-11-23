using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    float speed = 25;
    void Start ()
    {
        Destroy(this.gameObject, 5);
    }
	void Update () {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
	}
}
