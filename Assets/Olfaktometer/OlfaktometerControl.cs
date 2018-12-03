using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OlfaktometerControl : MonoBehaviour {
    private Byte cmdStart=63;
    private int ventilDecimal;
    private string ventilInputToString;
    private Vector3 triggerPosition;
    private float triggerRadius;
    private float elapsed = 0f;

    [Tooltip("Werte von 0-255 \nReguliert die Intensität des Geruches")]
    [Range(0, 255)]
    public Byte Massendurchflussregler1 =0;
    [Range(0, 255)]
    public Byte Massendurchflussregler2 = 0;
    public bool distanceOdorStrength = false;
    public bool fieldOfView = false;
    
    [HideInInspector]
    public bool[] Ventile=new bool[8];
   
    
    private void OnTriggerEnter(Collider col)
    {

        Debug.Log("ENTER");


        triggerPosition = GameObject.Find("Trigger").transform.position;
        triggerRadius = Vector3.Distance(triggerPosition, col.transform.position);
       
        
        
      
        Serial.Write(openVentile());
       
        
    }

    private void OnTriggerStay(Collider col)
    {
        
            elapsed += Time.deltaTime;
            if (elapsed >= 1f)
            {
                elapsed = elapsed % 1f;
                SmellStrenghtUpdate(col);
            }

        
        

        
        Debug.Log(Massendurchflussregler1);

    }
    void SmellStrenghtUpdate(Collider col)
    {
        if (distanceOdorStrength == true)
        {
            fieldOfViewStrength(col);
            strengthDistanceRatio(getColliderPos(col));
        }
        if (fieldOfView == true)
        {

            fieldOfViewStrength(col);
        }
            
            Serial.Write(openVentile());
        
    }

    private void OnTriggerExit()
    {
        Serial.Write(closeVentile());
       // Debug.Log("exit");
    }
    private float getColliderPos(Collider col)
    {
       
        Vector3 position = col.transform.position;
        float currentDistance = Vector3.Distance(triggerPosition, position);
        
        return currentDistance;

    }
    
    private Byte convertVentileInput()
    {
        BitArray arr = new BitArray(Ventile);
        Byte[] data = new byte[1];
        arr.CopyTo(data, 0);
        return data[0];
        
        
      

    }
    private void strengthDistanceRatio(float currentDistance)
    {
        
        float percentOfDistance = currentDistance/triggerRadius;
       // Debug.Log(percentOfDistance);
        if (percentOfDistance >= 0.9)
        {
            Massendurchflussregler1 = 25;
        }
        if (percentOfDistance >= 0.8 && percentOfDistance < 0.9)
        {
            Massendurchflussregler1 = 50;
        }
        if (percentOfDistance >= 0.7 && percentOfDistance < 0.8)
        {
            Massendurchflussregler1 = 75;
        }
        if (percentOfDistance >= 0.6 && percentOfDistance < 0.7)
        {
            Massendurchflussregler1 = 100;
        }
        if (percentOfDistance >= 0.5 && percentOfDistance < 0.6)
        {
            Massendurchflussregler1 = 125;
        }
        if (percentOfDistance >= 0.4 && percentOfDistance < 0.5)
        {
            Massendurchflussregler1 = 150;
        }
        if (percentOfDistance >= 0.3 && percentOfDistance < 0.4)
        {
            Massendurchflussregler1 = 175;
        }
        if (percentOfDistance >= 0.2 && percentOfDistance < 0.3)
        {
            Massendurchflussregler1 = 200;
        }
        if (percentOfDistance >= 0.1 && percentOfDistance < 0.2)
        {
            Massendurchflussregler1 = 225;
        }
        if (percentOfDistance < 0.1)
        {
            Massendurchflussregler1 = 250;
        }

    }
    private Byte[] openVentile()
    {
        Byte[] byteArray=new Byte[4];
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
    private void fieldOfViewStrength(Collider col)
    {
        GameObject thePlayer = GameObject.Find("Player");
        FieldOfView fieldOfView = thePlayer.GetComponent<FieldOfView>(); ;
        
          

        if (fieldOfView.visibleTargets.Contains(this.gameObject.transform.parent)){
            
            if (Massendurchflussregler1 <= 180)
            {
                Massendurchflussregler1 += 75;
            }
            else
            {
                Massendurchflussregler1 = 255;
            }
        }
        else
        {
            if (Massendurchflussregler1 > 75)
            {
                Massendurchflussregler1 -= 75;
            }
            else
            {
                Massendurchflussregler1 = 0;
            }
        }
        
        
    }
   

}
