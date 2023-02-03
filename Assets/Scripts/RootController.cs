using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
	public Sprite[] sprites;
	//public GameObject particleEffect;
	public Vector2 gridSize = new Vector2(1, 1);
	public GameObject badDirectionText;
	public GameObject powerUpAdded;
	public Transform rootPointer;

	private int currentSprite = 0;
	private int lastSprite = 0;
	private Vector2 currentGridPos;
	private Vector2 lastGripPos;
	private int lastDirection = -1;
	private int currentDirection = 0;
	private GameObject lastObj;
	private bool badDirecton = false;
	private bool powerAdded = false;
	private List<Vector2> listGripPos = new List<Vector2>();
	private List<Vector2> listPowers = new List<Vector2>();

	int i = 0;
	int j = 0;
	private void Start()
	{
		// Establecer la posición inicial del grid
		currentGridPos = new Vector2(0, 0);
		rootPointer.transform.position = (Vector2)currentGridPos;
		//generar la grilla
		generatePowerUps();
	}

	private void Update()
	{
		if (badDirecton)
		{
			i += 1;
			Debug.Log("eeee =" + i);
			if (i >= 30)
			{
				badDirecton = false;
				i = 0;
				badDirectionText.SetActive(false);
			}

		}
		if (powerAdded)
		{
			j += 1;
			Debug.Log("eeee =" + j);
			if (j >= 100)
			{
				powerAdded = false;
				j = 0;
				powerUpAdded.SetActive(false);
			}

		}
		/*if (Input.GetKeyDown(KeyCode.UpArrow))
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
		}*/

		
	}
	public void MakeAMove(KeyCode keyCode)
	{
		//5 arriba, 6 abajo, 0 derecha, 1 izquierda
		switch(keyCode)
		{
			case KeyCode.UpArrow:
				currentDirection = 5;
				MoveSprite(new Vector2(0, gridSize.y));
				break;
			case KeyCode.DownArrow:
				currentDirection = 6;
				MoveSprite(new Vector2(0, -gridSize.y));
				break;
			case KeyCode.RightArrow:
				currentDirection = 0;
				MoveSprite(new Vector2(gridSize.x, 0));
				break;
			case KeyCode.LeftArrow:
				currentDirection = 1;
				MoveSprite(new Vector2(-gridSize.x, 0));
				break;

		}
		
	}

	private void MoveSprite(Vector2 direction)
	{
		// Instanciar el sprite y el efecto de particula

		badDirecton = false;
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
						badDirecton = true;
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
						badDirecton = true;
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
						badDirecton = true;
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
						badDirecton = true;
						break;
					case 1:
						currentSprite = 0;
						lastSprite = 7;
						break;
				}
				break;

		}

		if (listGripPos.Contains((Vector3)currentGridPos + (Vector3)direction))
		{
			badDirecton = true;
		}
		if (!badDirecton)
		{

			GameObject newSprite = new GameObject();
			newSprite.AddComponent<SpriteRenderer>().sprite = sprites[currentSprite];
			newSprite.transform.position = (Vector3)currentGridPos + (Vector3)direction;
			rootPointer.transform.position = (Vector2)newSprite.transform.position;
			if (lastDirection != -1)
			{

				GameObject newSprite2 = new GameObject();
				if (lastObj) Destroy(lastObj);
				lastObj = newSprite;
				newSprite2 = new GameObject();
				newSprite2.AddComponent<SpriteRenderer>().sprite = sprites[lastSprite];
				newSprite2.transform.position = (Vector3)lastGripPos;

			}
			lastDirection = currentDirection;
			if (listPowers.Contains((Vector3)currentGridPos + (Vector3)direction))
			{
				powerUpAdded.SetActive(true);
				powerAdded = true;
			}

			// Actualizar la posición actual del grid
			currentGridPos += direction;

			lastGripPos = currentGridPos;
			listGripPos.Add(currentGridPos);

			// Borrar el efecto de particula
			//Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);
		}
		else
		{
			Debug.Log("bad direction kapeee");
			badDirectionText.SetActive(true);

		}

	}

	private void generatePowerUps()
	{
		Vector2 position;
		for (int i = 0; i < 3; i++)
		{
			position = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));

			GameObject mineral1 = new GameObject();
			mineral1.name = "mineral1";
			mineral1.AddComponent<SpriteRenderer>().sprite = sprites[10];
			mineral1.transform.position = (Vector3)position;
			mineral1.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			listPowers.Add(position);
			Debug.Log("pos" + i + " " + listPowers[i].ToString());
		}
	}
}
