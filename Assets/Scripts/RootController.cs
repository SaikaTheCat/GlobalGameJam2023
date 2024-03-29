using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class RootController : MonoBehaviour
{
	public Sprite[] sprites;
	//public GameObject particleEffect;
	public Vector2 gridSize = new Vector2(1, 1);
	public Transform rootPointer;
	public GameObject head;
	public Tilemap stones;
	public Tilemap waters;
	public Tilemap gems;
	public Tilemap limit;
	public Tilemap nextLevel;
	public int level;

	private int currentSprite = 0;
	private int lastSprite = 0;
	private Vector2 currentGridPos;
	private Vector2 lastGripPos;
	private int lastDirection = -1;
	private int currentDirection = 0;
	private GameObject lastObj;
	private bool badDirecton = false;
	private List<Vector2> listGripPos = new List<Vector2>();
	private List<Vector2> listPowers = new List<Vector2>();
	private int limitx = 10;
	private int limity = 7;
	private Vector3 tempPost;
	
	


	int i = 0;
	int j = 0;
	private void Start()
	{
		// Establecer la posici�n inicial del grid
		currentGridPos = new Vector2(0.5f, 0.5f);
		rootPointer.transform.position = (Vector2)currentGridPos;
		//generar la grilla
		lastObj= head;
		lastObj.AddComponent<SpriteRenderer>();
	}
	private void Miss()
    {
		PlayerEvents.playerDamaged.Invoke(1f, gameObject);
		ScoreManager.Miss();
		HealthManager.HealthMinus();
		HealthManager.healthMinusTriggered = true;
    }
	private void Hit()
	{
		ScoreManager.Hit();
		HealthManager.HealthPlus();
		HealthManager.healthPlusTriggered = true;
	}
	private void MissGem()
    {
		GemManager.GemMinus();
		GemManager.gemMinusTriggered = true;
	}
	private void HitGem()
	{
		GemManager.GemPlus();
		GemManager.gemPlusTriggered = true;
	}


	private void Update()
	{
		
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
		tempPost = (Vector3)currentGridPos + (Vector3)direction;
		if (listGripPos.Contains(tempPost))
		{
			badDirecton = true;
		}
		Debug.Log("tempos x " + tempPost.x + stones.gameObject.name);
		Debug.Log($"root livesleft {HealthManager.livesLeft}");
		if (!badDirecton)
        {
			
			Vector3Int stonesMap = stones.WorldToCell((Vector3)currentGridPos + (Vector3)direction);
			if(stones.GetTile(stonesMap) != null)
            {
				Debug.Log($"root hay un stoneee {GemManager.gemAmount} :  {GemManager.gemAmount >= 1}");
				if (GemManager.gemAmount >= 1)
                {
					MissGem();
					stones.SetTile(stonesMap, null);
					Debug.Log($"root hay un stoneee con gemas {HealthManager.livesLeft}");
				}
                else
                {
					badDirecton = true;
					Debug.Log($"root hay un stoneee sin gemas {HealthManager.livesLeft}");
				}
				

			}
			Vector3Int waterMap = waters.WorldToCell((Vector3)currentGridPos + (Vector3)direction);
			if (waters.GetTile(waterMap) != null)
			{
				
				Hit();
				waters.SetTile(waterMap, null);
				Debug.Log($" root hay un water{HealthManager.livesLeft}");
			}
			Vector3Int gemMap = waters.WorldToCell((Vector3)currentGridPos + (Vector3)direction);
			if (gems.GetTile(gemMap) != null)
			{
				HitGem();
				gems.SetTile(gemMap, null);
				Debug.Log($" root hay un gema{GemManager.gemAmount}");
			}
			Vector3Int limitMap = waters.WorldToCell((Vector3)currentGridPos + (Vector3)direction);
			if (limit.GetTile(limitMap) != null)
			{
				badDirecton = true;
				Miss();
				Debug.Log($" root hay un limite");
			}
			Vector3Int nextLevelMap = waters.WorldToCell((Vector3)currentGridPos + (Vector3)direction);
			if (nextLevel.GetTile(nextLevelMap) != null)
			{
				Player.nexLevel = "level"+ (level+1);
				SceneManager.LoadScene("WinScene");
			}

		}

		if (!badDirecton )
		{

			lastObj.GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
			lastObj.transform.position = tempPost;
			rootPointer.transform.position = (Vector2)lastObj.transform.position;
			if (lastDirection != -1)
			{

				GameObject newSprite2 = new GameObject();
				//if (lastObj) Destroy(lastObj);
				//lastObj = newSprite;
				newSprite2 = new GameObject();
				newSprite2.AddComponent<SpriteRenderer>().sprite = sprites[lastSprite];
				newSprite2.transform.position = (Vector3)lastGripPos;

			}
			lastDirection = currentDirection;
			

			// Actualizar la posici�n actual del grid
			currentGridPos += direction;

			lastGripPos = currentGridPos;
			listGripPos.Add(currentGridPos);

			// Borrar el efecto de particula
			//Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);
		}
		else
		{
			Debug.Log("bad direction kapeee");
			badDirecton = false;
			Miss();
		}

	}
}
