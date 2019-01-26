using UnityEngine;

public class PlayerAgent : Agent
{
    public static PlayerAgent Instance;
    
    protected override void Start()
    {
        base.Start();
        Instance = this;
    }
    
    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                NavMeshAgent.SetDestination(hit.point);
            }
        }
    }
}
