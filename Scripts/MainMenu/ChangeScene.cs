using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	// Update is called once per frame
	public void ChangeToScene (string sceneToChangetTo) 
    {
        Countdown();
        Application.LoadLevel(sceneToChangetTo);

	}

    private IEnumerator Countdown()
    {
        // fade out the game and load a new level
        float fadeTime = GameObject.Find("_Manager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
    }
}
