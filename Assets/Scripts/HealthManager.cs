using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private List<Image> healthImages;
    public static HealthManager Instance;
    public static int livesLeft;
	public static bool healthPlusTriggered = false;
	public static bool healthMinusTriggered = false;
	// Start is called before the first frame update
	void Start()
    {
        Instance = this;
        livesLeft = 4;
    }

    public static void HealthMinus()
    {
        if(!(livesLeft==0))
            livesLeft -= 1;
    }
    public static void HealthPlus()
    {
        if(!(livesLeft == 4))
        {
            livesLeft = 4;
        }
    }
    private void Update()
    {
		if (healthMinusTriggered)
		{
			healthMinusTriggered = false;
			healthImages[livesLeft].gameObject.SetActive(false);
		}
		else if (healthPlusTriggered)
		{
			healthPlusTriggered = false;
			healthImages[0].gameObject.SetActive(true);
			healthImages[1].gameObject.SetActive(true);
			healthImages[2].gameObject.SetActive(true);
			healthImages[3].gameObject.SetActive(true);
		}
		
    }
}
