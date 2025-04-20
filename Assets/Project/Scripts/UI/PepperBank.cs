using UnityEngine;

public class PepperBank : MonoBehaviour
{
    public PepperView _pepperView;
    public PepperStorage _storage;

    public void NewStorage(PepperStorage storage, float pepperCount)
    {
        _storage = storage;
        _storage.SetupPepper(pepperCount);
        _pepperView.GetStorage(_storage);
    }
}