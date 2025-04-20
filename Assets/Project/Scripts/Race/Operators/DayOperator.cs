using UnityEngine;

public class DayOperator : Operator
{
    [SerializeField] private Camera _camera;

    [SerializeField] private float pepperPerClick = 1, chancePepper = 1;

    public void SetOperator(float range, float range2)
    {
        pepperPerClick = range;
        chancePepper = range2;
    }

    private void Awake()
    {
        storage = transform.parent.GetComponent<PepperStorage>();
        shelter = transform.parent.GetComponent<Shelter>();
        _camera = Camera.main;
        
        shelter.transform.parent.GetComponent<RaceController>().SaveRace();
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
                    int chance = Random.Range(0, 100);
                    if (chance <= chancePepper * 100)
                    {
                        storage.AddPepper(pepperPerClick);
                    }
                }
            }
        }
    }
}
