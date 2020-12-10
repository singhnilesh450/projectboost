using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class oscilator : MonoBehaviour
{
    [SerializeField] Vector3 movmentVector;

    [Range(0,1)] [SerializeField] float movementFactor;

    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offSet =movementFactor*movmentVector;
        transform.position = startingPos + offSet;
    }
}
