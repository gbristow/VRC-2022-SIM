using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightController : MonoBehaviour
{
    public M2MqttUnity.Examples.M2MqttUnityTest mqttObj;
    public GameObject lightObj;
    private Light light;


    void Start()
    {
        light = lightObj.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mqttObj.lightMsgValues != null)
        {
            lightObj.GetComponent<Light>().color = new Color(mqttObj.lightMsgValues.wrgb[1], mqttObj.lightMsgValues.wrgb[2], mqttObj.lightMsgValues.wrgb[3]);
        }
    }
}