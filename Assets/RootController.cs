using UnityEngine;

public class RootController : MonoBehaviour
{
	public Sprite[] sprites;
	//public GameObject particleEffect;
	public Vector2 gridSize = new Vector2(1, 1);

	private int currentSprite = 0;
	private Vector2 currentGridPos;

	private void Start()
	{
		// Establecer la posición inicial del grid
		currentGridPos = new Vector2(0, 0);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			currentSprite = 1;
			MoveSprite(new Vector2(0, gridSize.y));
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			currentSprite = 2;
			MoveSprite(new Vector2(0, -gridSize.y));
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			currentSprite = 3;
			MoveSprite(new Vector2(gridSize.x, 0));
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			currentSprite = 4;
			MoveSprite(new Vector2(-gridSize.x, 0));
		}
	}

	private void MoveSprite(Vector2 direction)
	{
		// Instanciar el sprite y el efecto de particula
		GameObject newSprite = new GameObject();
		newSprite.AddComponent<SpriteRenderer>().sprite = sprites[currentSprite];
		newSprite.transform.position = (Vector3)currentGridPos + (Vector3)direction;

		//GameObject particle = Instantiate(particleEffect, (Vector3)currentGridPos, Quaternion.identity);

		// Actualizar la posición actual del grid
		currentGridPos += direction;

		// Borrar el efecto de particula
		//Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);
	}
}
