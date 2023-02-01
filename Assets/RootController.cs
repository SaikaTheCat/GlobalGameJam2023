using UnityEngine;

public class RootController : MonoBehaviour
{
	public Sprite[] sprites;
	public GameObject particleEffect;

	private int currentSprite = 0;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			currentSprite = 1;
			SpawnSprite(sprites[currentSprite]);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			currentSprite = 2;
			SpawnSprite(sprites[currentSprite]);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			currentSprite = 3;
			SpawnSprite(sprites[currentSprite]);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			currentSprite = 4;
			SpawnSprite(sprites[currentSprite]);
		}
	}

	private void SpawnSprite(Sprite sprite)
	{
		// Instanciar el sprite y el efecto de particula
		GameObject newSprite = new GameObject();
		newSprite.AddComponent<SpriteRenderer>().sprite = sprite;
		newSprite.transform.position = transform.position;

		GameObject particle = Instantiate(particleEffect, transform.position, Quaternion.identity);

		// Borrar el sprite anterior
		Destroy(gameObject);
	}
}
