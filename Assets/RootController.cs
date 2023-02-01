using UnityEngine;

public class RootController : MonoBehaviour
{
	public Sprite[] sprites;
	//public GameObject particleEffect;
	public Vector2 gridSize = new Vector2(1, 1);

	private int currentSprite = 0;
	private int lastSprite = 0;
	private Vector2 currentGridPos;
	private Vector2 lastGripPos;
	private int lastDirection = -1;
	private int currentDirection = 0;
	private GameObject lastObj;

	private void Start()
	{
		// Establecer la posición inicial del grid
		currentGridPos = new Vector2(0, 0);
	}

	private void Update()
	{
		
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			currentDirection = 5;
			MoveSprite(new Vector2(0, gridSize.y));
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			currentDirection = 6;
			MoveSprite(new Vector2(0, -gridSize.y));
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			currentDirection = 0;
			MoveSprite(new Vector2(gridSize.x, 0));
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			currentDirection = 1;
			MoveSprite(new Vector2(-gridSize.x, 0));
		}
	}

	private void MoveSprite(Vector2 direction)
	{
		// Instanciar el sprite y el efecto de particula
		GameObject newSprite = new GameObject();
		GameObject newSprite2 = new GameObject();
        switch (lastDirection)
        {
			case -1:
				currentSprite = currentDirection;
				break;
			case 5:
                switch (currentDirection)
                {
					case 5:
						currentSprite = currentDirection;
						lastSprite = 8;
						break;
					case 6:
						currentSprite = currentDirection;
						lastSprite = 8;
						break;
					case 0:
						currentSprite = 1;
						lastSprite = 2;
						break;
					case 1:
						currentSprite = 0;
						lastSprite = 3;
						break;
				}
				break;
			case 6:
				switch (currentDirection)
				{
					case 5:
						currentSprite = currentDirection;
						lastSprite = 8;
						break;
					case 6:
						currentSprite = currentDirection;
						lastSprite = 8;
						break;
					case 0:
						currentSprite = 1;
						lastSprite = 4;
						break;
					case 1:
						currentSprite = 0;
						lastSprite = 9;
						break;
				}
				break;
			case 0:
				switch (currentDirection)
				{
					case 5:
						currentSprite = 5;
						lastSprite = 9;
						break;
					case 6:
						currentSprite = 6;
						lastSprite = 3;
						break;
					case 0:
						currentSprite = 1;
						lastSprite = 7;
						break;
					case 1:
						currentSprite = 0;
						lastSprite = 7;
						break;
				}
				break;
			case 1:
				switch (currentDirection)
				{
					case 5:
						currentSprite = 5;
						lastSprite = 4;
						break;
					case 6:
						currentSprite = 6;
						lastSprite = 2;
						break;
					case 0:
						currentSprite = 1;
						lastSprite = 7;
						break;
					case 1:
						currentSprite = 0;
						lastSprite = 7;
						break;
				}
				break;

		}




		newSprite.AddComponent<SpriteRenderer>().sprite = sprites[currentSprite];
		newSprite.transform.position = (Vector3)currentGridPos + (Vector3)direction;
		lastDirection = currentDirection;


		if (lastDirection != -1)
        {
			if (lastObj) Destroy(lastObj);
			lastObj = newSprite;
			newSprite2 = new GameObject();
			newSprite2.AddComponent<SpriteRenderer>().sprite = sprites[lastSprite];
			newSprite2.transform.position = (Vector3)lastGripPos;
			
		}
		

		//GameObject particle = Instantiate(particleEffect, (Vector3)currentGridPos, Quaternion.identity);
		
		// Actualizar la posición actual del grid
		currentGridPos += direction;
		lastGripPos = currentGridPos;
		// Borrar el efecto de particula
		//Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);
	}
}
