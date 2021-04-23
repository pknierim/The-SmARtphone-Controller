using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluetoothLatencyTest : MonoBehaviour
{
    [SerializeField]
    private Transform objectToMove;

    private int counter = 1;
    private int maxCounterValue = 100;

    private int sign = 1;

    void Start()
    {
        
    }

    private void Update()
    {

    }

    public void CountUp()
    {
        if (counter == 100)
        {
            sign = -1;
        }
        else if (counter == 1)
        {
            sign = 1;
        }

        objectToMove.position = new Vector3((counter - 50) / 200.0f, 0, 0);
        counter += sign;
        counter = counter % (maxCounterValue + 1);
        counter = counter == 0 ? 1 : counter;

        
    }

}
