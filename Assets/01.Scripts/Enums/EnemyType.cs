using System.Collections.Generic;

public enum enemyType
{
    Ghost_blue,
    Ghost_pink,
    Ghost_red,
    Ghost_green,
    EggMan,
    Poo,
    MagnetBoomber,
    WaddleDee,
    AirShip,
}

[System.Serializable]
public class enemyInfo
{
    public int enterMinScore;
    public int enterMaxScore;
    public List<enemyType> enemyList = new List<enemyType>();
}