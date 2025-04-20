[System.Serializable]
public abstract class ElementUpgrader
{
    public CycleComponent cycle;
    public PepperStorage storage;
    public Shelter shelter;
        
    private int price, startPrice, count = 0;
    public int Price => price;
    public int Count => count;
    
    private float elementFactor;
    public float ElementFactor => elementFactor;

    public ElementUpgrader(CycleComponent _cycle, PepperStorage _storage, Shelter _shelter, int _startPrice, int _count)
    {
        cycle = _cycle;
        storage = _storage;
        shelter = _shelter;

        count = _count;
        startPrice = _startPrice;
        elementFactor = 1 - (count / 10.0f);
        price = _startPrice;
        for (int i = 0; i < count; i++) { price = (int)(price * 1.5f); }
    }

    public void IncreaseUpgrader()
    {
        if (storage.PepperCount >= price && count < 3)
        {
            storage.SpendPepper(price);
            count++;
            for (int i = 0; i < count; i++) { price = (int)(price * 1.5f); }
            elementFactor = 1 - (count / 10.0f);
        }
    }
    public void LowerUpgrader()
    {
        if (count > 0)
        {
            count--;
            price = startPrice;
            for (int i = 0; i < count; i++){ price = (int)(price * 1.5f); }
            elementFactor = 1 - (count / 10.0f);
        }
    }
}