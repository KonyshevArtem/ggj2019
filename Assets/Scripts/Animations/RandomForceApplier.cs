using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForceApplier : MonoBehaviour
{

    public float MaxForce;

    private Rigidbody rg;
    private ActionRepeater actionRepeater;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
        actionRepeater = new ActionRepeater(() => 0.5f, ApplyForce);
    }

    void Update()
    {
        actionRepeater.Tick(Time.deltaTime);
    }

    void ApplyForce()
    {
        rg.AddForce(Random.Range(-MaxForce, MaxForce), Random.Range(-MaxForce, MaxForce), Random.Range(-MaxForce, MaxForce), ForceMode.Impulse);
    }

}
