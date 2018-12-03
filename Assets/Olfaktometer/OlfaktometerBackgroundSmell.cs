using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OlfaktometerBackgroundSmell : MonoBehaviour
{
    private Byte cmdStart = 63;
    private int ventilDecimal;
    private string ventilInputToString;
    private Vector3 triggerPosition;
    private float triggerRadius;
    private float elapsed = 0f;

    [Tooltip("Werte von 0-255 \nReguliert die Intensität des Geruches")]
    [Range(0, 255)]
    public Byte Massendurchflussregler1 = 0;
    [Range(0, 255)]
    public Byte Massendurchflussregler2 = 0;
    
    [HideInInspector]
    public bool[] Ventile = new bool[8];



    private void Start()
    {
        Serial.Write(openVentile());
    }

    private Byte convertVentileInput()
    {
        BitArray arr = new BitArray(Ventile);
        Byte[] data = new byte[1];
        arr.CopyTo(data, 0);
        return data[0];

    }

    private Byte[] openVentile()
    {
        Byte[] byteArray = new Byte[4];
        byteArray[0] = cmdStart;
        byteArray[1] = convertVentileInput();
        byteArray[2] = Massendurchflussregler1;
        byteArray[3] = Massendurchflussregler2;
        return byteArray;
    }
    private Byte[] closeVentile()
    {
        Byte[] closeVentile = new Byte[4];
        closeVentile[0] = cmdStart;
        closeVentile[1] = 0;
        closeVentile[2] = 0;
        closeVentile[3] = 0;
        return closeVentile;
    }
    


}
