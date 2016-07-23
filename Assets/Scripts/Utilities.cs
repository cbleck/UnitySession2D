using UnityEngine;
using System.Collections;

public class Utilities : MonoBehaviour 
{
	public static Utilities instance;

	public Texture2D screenShot;

	public delegate void CaptureScreenShotCallback ();
	public CaptureScreenShotCallback captureScreenShotCallback;

	void Start()
	{
		instance = this;
        screenShot = new Texture2D(Screen.width, Screen.height);
	}

	public void TakeScreenShot()
	{
		StartCoroutine ("WaitToCapture");
	}

	IEnumerator WaitToCapture()
	{
		yield return new WaitForEndOfFrame ();
		screenShot.ReadPixels (new Rect(0,0,Screen.width, Screen.height), 0,0,false);
		screenShot.Apply ();
		captureScreenShotCallback ();
	}


}
