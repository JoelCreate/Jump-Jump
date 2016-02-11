using UnityEngine;
using System.Collections;

public class ScrollingBG : MonoBehaviour {

	public float speed = 0f;

	void Update () 
	{
		GetComponent<Renderer> ().material.mainTextureOffset = new Vector2 (0f, speed * Time.time);
	}

}