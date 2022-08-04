public enum expType
{
    NOMAL,
    ELITE,
}

[System.Serializable]
public class ExpInfo
{
    public int amount;
    public expType type;
}