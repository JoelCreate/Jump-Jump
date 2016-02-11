using UnityEngine;
using System.Collections;

public class PlatformCollector : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == "Platform" || target.tag == "Deadly") {
			target.gameObject.SetActive (false);
		}

	}

}
