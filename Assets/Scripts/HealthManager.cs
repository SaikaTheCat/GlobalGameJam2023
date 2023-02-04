using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private List<Image> healthImages;
    public static HealthManager Instance;
    static int livesLeft;
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
    private void Update()
    {
        if(!(livesLeft==4))
            Destroy(healthImages[livesLeft]);
    }
}
