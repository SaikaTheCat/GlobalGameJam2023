using System.Collections.Generic;

public class Player
{
    private readonly static Player _instance = new Player();

    public static Player Instance => _instance;

    public int maxScore;
    public List<int> levels;
}
