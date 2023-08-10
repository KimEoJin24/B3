using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataTransfer : MonoBehaviour
{
    public static dataTransfer D;

    public int dataToSend = 0;

    void Awake()
    {
        if (D == null)
        {
            D = this;
            DontDestroyOnLoad(D);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getDataToSend()
    {
        return dataToSend;
    }
}
