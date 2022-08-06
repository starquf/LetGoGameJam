using System.Collections.Generic;

public enum enemyType
{
    NONE,
    Ghost_blue,
    Ghost_pink,
    Ghost_red,
    Ghost_green,
    EggMan,
    Poo,
    MagnetBoomber,
    WaddleDee,
    AirShip,
    Elite_Ghost,
    Elite_EggMan,
    Elite_AirShip,
    Elite_Poo,
    Elite_WaddleDee,
}
public enum enemyAttackType   // 적 종류 근접, 원거리 같은거
{
    MELEE, RANGED,
}

[System.Serializable]
public class enemyInfo
{
    public int enterMinScore;
    public List<enemyType> enemyList = new List<enemyType>();
}