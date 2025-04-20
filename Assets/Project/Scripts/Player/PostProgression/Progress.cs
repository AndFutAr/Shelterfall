[System.Serializable]
public abstract class Progress
{
    public RaceController controller;
    protected int price, startPrice, count = 0;
    public int Count => count;
    protected bool isOneTime;
    
    public Progress(int _startPrice, int _count, bool _isOneTime)
    {
        startPrice = _startPrice;
        count = _count;
        price = startPrice;
        isOneTime = _isOneTime;
        for (int i = 0; i < count; i++) price *= 2;
    }
    public void UpgradeProgress()
    {
        if (controller.RaceData.PepperFragments >= price)
        {
            if (!isOneTime || (isOneTime && count == 0))
            {
                count++;
                price = startPrice;
                for(int i = 0; i < count; i++) price *= 2;
                ProgressUp();
            }
        }
    }
    public abstract void ProgressUp();
}