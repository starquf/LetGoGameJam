[System.Serializable]
public class HeartInfo
{
    public int heart;
    public int maxHeartCnt;
    public int extraHeart;
    public int maxExtraHeartCnt;

    public void AddHeart()
    {
        if(heart < maxHeartCnt)
        {
            heart++;
        }
    }

    public void AddMaxHeartCnt()
    {
        maxHeartCnt++;
    }

    public void AddExtraHeart()
    {
        if(extraHeart < maxExtraHeartCnt)
        {
            extraHeart++;
        }
    }

    public void RemoveHeart()
    {
        if(heart + extraHeart > 0)
        {
            if (extraHeart > 0)
            {
                extraHeart--;
            }
            else
            {
                heart--;
            }
        }
    }
}
