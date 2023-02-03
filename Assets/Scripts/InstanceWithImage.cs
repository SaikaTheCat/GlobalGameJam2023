using System.Collections;
using UnityEngine;

public class InstanceWithImage : MonoBehaviour
{
	public SpriteRenderer spriteRenderer;

	private void Start()
	{
		Invoke("FadeOut", 0.1f);
	}

	private void FadeOut()
	{
		StartCoroutine(LerpAlphaToZero());
	}

	private IEnumerator LerpAlphaToZero()
	{
		float elapsedTime = 0;
		Color color = spriteRenderer.color;
		while (elapsedTime < 0.2f)
		{
			elapsedTime += Time.deltaTime;
			color.a = Mathf.Lerp(1, 0, elapsedTime / 0.2f);
			spriteRenderer.color = color;
			yield return null;
		}
		Destroy(gameObject);
	}
}
