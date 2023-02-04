using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> healthSprites;
    public static HealthManager Instance;
    private int livesLeft;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        livesLeft = 7;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
