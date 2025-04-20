[System.Serializable]
public abstract class BaseUpgrader
{
    public CycleComponent cycle;
    public PepperStorage storage;
    public Shelter shelter;
        
    protected int price, startPrice, count = 0, maxCount = 100;
    public int Price => price;
    public int Count => count;

    public BaseUpgrader(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter, int _startPrice, int _count, int _maxCount)
    {
        cycle = _cycle;
        storage = _storage;
        shelter = _shelter;
        
        count = _count;
        maxCount = _maxCount;
        startPrice = _startPrice;
        price = _startPrice;
        for (int i = 0; i < count; i++) { price = (int)(price * 1.5f); }
    }
    
    public void IncreaseUpgrader()
    {
        if (storage.PepperCount >= price * cycle.CycleData.costFactor && count < maxCount)
        {
            storage.SpendPepper(price);
            count++;
            price = startPrice;
            for (int i = 0; i < count; i++) { price = (int)(price * 1.5f); }
            Upgrade();
        }
    }
    public abstract void Upgrade();

    public void LowerUpgrader()
    {
        if (count > 0)
        {
            count--;
            price = startPrice;
            for (int i = 0; i < count; i++) { price = (int)(price * 1.5f); }
            Worse();
        }
    }
    public abstract void Worse();
}