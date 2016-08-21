using UnityEngine;
using System.Collections;

public class LogoLoader : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        StartCoroutine("Countdown");
	}

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(5);
        Application.LoadLevel (1);
    }
}
