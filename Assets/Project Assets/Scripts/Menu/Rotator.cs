using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _axis = new Vector3();
    [SerializeField] [Range(1,100)] private int _speed = 10;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate( _axis * _speed * 100 * Time.deltaTime);

    }
}
