using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour {

	public float speed = 0f;

	void Update () {
		transform.localPosition += transform.up * speed * Time.deltaTime; 

	}
}
