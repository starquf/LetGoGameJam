public enum expType
{
    NOMAL,
    ELITE,
}

[System.Serializable]
public class ExpInfo
{
    public float dropPersent;
    public int amount;
    public expType type;
}