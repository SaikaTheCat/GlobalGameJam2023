using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemManager : MonoBehaviour
{
    [SerializeField] private List<Image> gemImages;
    public static GemManager Instance;
    public static int gemAmount;
	public static bool gemPlusTriggered = false;
	public static bool gemMinusTriggered = false;
	// Start is called before the first frame update
	void Start()
    {
        Instance = this;
        gemAmount = 0;
    }

    public static void GemMinus()
    {
        if(!(gemAmount == 0))
            gemAmount -= 1;
    }
    public static void GemPlus()
    {
        if(!(gemAmount == 3))
        {
            gemAmount += 1;
        }
    }
    private void Update()
    {
		if (gemMinusTriggered)
		{
			gemMinusTriggered = false;
			gemImages[gemAmount].gameObject.SetActive(false);
		}
		else if (gemPlusTriggered)
		{
			gemPlusTriggered = false;
			gemImages[gemAmount - 1].gameObject.SetActive(true);
		}
		
    }
}
