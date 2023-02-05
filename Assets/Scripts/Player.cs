using System.Collections.Generic;

public class Player
{
    private readonly static Player _instance = new Player();
    public static string nexLevel;
    public static Player Instance => _instance;

    public int maxScore;
    public List<int> levels;

}
