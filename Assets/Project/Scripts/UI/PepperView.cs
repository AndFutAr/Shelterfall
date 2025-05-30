using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class PepperView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pepperText;
    [SerializeField] private float _animDuration;

    private float _viewedCount;
    private float _actualCount;
    private Coroutine _anim;

    public PepperStorage _pepperStorage;

    public void GetStorage(PepperStorage storage)
    {
        _pepperStorage = storage;
        ChangeText(_pepperStorage.PepperCount);
    }

    private void Update()
    {
        if (_pepperStorage != null)
            ChangeText(_pepperStorage.PepperCount);
        else ChangeText(0);
    }

    private void OnEnable()
    {
        if (_pepperStorage != null)
        {
            _pepperStorage.OnPepperEarned += PepperEarned;
            _pepperStorage.OnPepperSpent += PepperSpent;

            ChangeText(_pepperStorage.PepperCount);
        }
    }

    private void OnDestroy()
    {
        if (_pepperStorage != null)
        {
            _pepperStorage.OnPepperEarned -= PepperEarned;
            _pepperStorage.OnPepperSpent -= PepperSpent;
        }
    }

    private void PepperEarned(float newValue)
    {
        if (_anim != null)
            StopCoroutine(_anim);
        
        _actualCount = newValue;
        _anim = StartCoroutine(routine: ChangingPepper());
    }

    private void PepperSpent(float newValue)
    {
        if (_anim != null)
            StopCoroutine(_anim);

        _actualCount = newValue;
        _anim = StartCoroutine(routine: ChangingPepper());
    }
    private IEnumerator ChangingPepper()
    {
        float targetTime = _animDuration;
        var timer = 0f;
        float prevCount = _viewedCount;
        while (_viewedCount != _actualCount && timer < targetTime)
        {
            yield return null;
            timer += Time.deltaTime;
            ChangeText(count: (int)Mathf.Lerp(a: prevCount, b: _actualCount, t: Mathf.Clamp(value: timer / targetTime, min: 0f, max: 1f)));
        }

        _anim = null;
    }

    private void ChangeText(float count)
    {
        _viewedCount = count;
        if (count * 100 % 10 != 0) _pepperText.text = count.ToString();
        else if (count * 10 % 10 != 0) _pepperText.text = count + "0";
        else _pepperText.text = count + ",00";
    }
}