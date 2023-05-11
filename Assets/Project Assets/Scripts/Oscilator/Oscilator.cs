using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    private Vector3 _startPosition;
    [SerializeField]
    private Vector3 _movementVector;
    [SerializeField]
    [Range(0,1)] private float _movementFactor;
    [SerializeField] private float period = 2f;


    // Start is called before the first frame update
    void Start()
    {
        AssignStartPosition();
       
    }

    // Update is called once per frame
    void Update()
    {
        OffsetObject();
    }

    private void OffsetObject()
    {
        if(period <= Mathf.Epsilon) { return; }

        float Cycles = Time.time / period; // continually growing over time

        const float tau = Mathf.PI * 2;  // constant value of 6.283

        float rawSinWave = Mathf.Sin(Cycles * tau);  //going from -1 to 1

        _movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1

        Vector3 offset = _movementVector * _movementFactor;
        transform.position = _startPosition + offset;
    }

    private void AssignStartPosition()
    {
        _startPosition = transform.position;
    }

}
