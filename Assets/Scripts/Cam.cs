using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public AudioClip boomSfx;
    bool shakingCam; 

    public void PlayBoomSfx()
    {
        GetComponent<AudioSource>().PlayOneShot(boomSfx);
    }

    	public void Shake(float duration, float amount, float intensity)
	{
		if (!shakingCam)
			StartCoroutine(ShakeCam(duration, amount, intensity));
	}

	IEnumerator ShakeCam(float dur, float amount, float intensity)
	{
		float t = dur;
		Vector3 originalPos = Camera.main.transform.localPosition;//pos initial
		Vector3 targetPos = Vector3.zero; // pos souhaité
		shakingCam = true;

		while (t > 0.0f)
		{
			if (targetPos == Vector3.zero)
			{
				targetPos = originalPos + (Random.insideUnitSphere * amount);
			}

			Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, targetPos, intensity * Time.deltaTime);

			if (Vector3.Distance(Camera.main.transform.localPosition, targetPos) < 0.02f)
			{
				targetPos = Vector3.zero;
			}

			t -= Time.deltaTime;
			yield return null;
		}

		Camera.main.transform.localPosition = originalPos;
		shakingCam = false;
	}
}
