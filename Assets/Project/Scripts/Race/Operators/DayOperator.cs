using UnityEngine;

public class DayOperator : Operator
{
    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject _clickImpactPrefab;
    [SerializeField] private float pepperPerClick = 1, chancePepper = 1;

    private ParticleSystem _clickImpact;
    
    public void SetOperator(float range, float range2)
    {
        pepperPerClick = range;
        chancePepper = range2;
    }

    private void Awake()
    {
        storage = transform.parent.GetComponent<PepperStorage>();
        shelter = transform.parent.GetComponent<Shelter>();
        ui_controller = transform.parent.parent.GetComponent<RaceController>().ui;
        _camera = Camera.main;

        _clickImpact = Instantiate(_clickImpactPrefab).GetComponent<ParticleSystem>();
        _clickImpact.gameObject.SetActive(true);
        _clickImpact.transform.forward = Vector3.up;
        _clickImpact.Stop();
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit HallHit;
            Ray HallRay = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(HallRay, out HallHit))
            {
                if (HallHit.transform.gameObject.layer == LayerMask.NameToLayer("Mine"))
                {
                    _clickImpact.transform.position = HallHit.point;
                    //_clickImpact.transform.forward = HallHit.normal;
                    _clickImpact.Play();
                    
                    int chance = Random.Range(0, 100);
                    if (chance <= chancePepper * 100)
                    {
                        storage.AddPepper(pepperPerClick);
                        ui_controller.IncreasePepCoin(pepperPerClick, Input.mousePosition);
                    }
                }
            }
        }
    }
}
