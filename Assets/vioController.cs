using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class mqttVioPositionNedMessage
{
    public float n = 0;
    public float e = 0;
    public float d = 0;
}
[Serializable]
public class mqttVioAttitudeQuatMessage
{
    public float w = 0;
    public float x = 0;
    public float y = 0;
    public float z = 0;

}
[Serializable]
public class mqttVioAttitudeEulerMessage
{
    public float psi = 0;
    public float theta = 0;
    public float phi = 0;

}
public class mqttVioHeadingMessage
{
    public float degrees = 0;
}
public class vioController : MonoBehaviour
{

    public M2MqttUnity.Examples.M2MqttUnityTest mqttObj;

    public Transform droneTF;

    private float shiftRange(float minAng, float ang)
    {
        float maxAng = minAng + 360f;
        if (ang > maxAng)
        {
            ang = ang - 360f;
        }
        else if (ang < minAng)
        {
            ang = ang + 360f;
        }
        return ang;
    }
    private void sendPositionNed()
    {
        mqttVioPositionNedMessage msg = new mqttVioPositionNedMessage();
        msg.n = droneTF.position.z;
        msg.e = droneTF.position.x;
        msg.d = -droneTF.position.y;
        string msg_str = JsonUtility.ToJson(msg);
        mqttObj.publish_msg("vrc/vio/position/ned", msg_str);
    }
    // TODO need to test transform
    private void sendQuat()
    {
        mqttVioAttitudeQuatMessage msg = new mqttVioAttitudeQuatMessage();
        msg.x = droneTF.rotation.x;
        msg.x = droneTF.rotation.x;
        msg.x = droneTF.rotation.x;
        msg.x = droneTF.rotation.x;
    }
    private void sendEulerAtt()
    {
        mqttVioAttitudeEulerMessage msg = new mqttVioAttitudeEulerMessage();
        msg.psi = shiftRange(-180, droneTF.eulerAngles.z);
        msg.theta = shiftRange(-180, droneTF.eulerAngles.x);
        msg.phi = shiftRange(-180, droneTF.eulerAngles.y);
        string msg_str = JsonUtility.ToJson(msg);
        mqttObj.publish_msg("vrc/vio/orientation/eul", msg_str);
    }
    private void sendHeading()
    {
        mqttVioHeadingMessage msg = new mqttVioHeadingMessage();
        msg.degrees = droneTF.eulerAngles.y;
        string msg_str = JsonUtility.ToJson(msg);
        mqttObj.publish_msg("vrc/vio/heading", msg_str);
    }


    // Update is called once per frame
    void Update()
    {
        sendPositionNed();
        sendEulerAtt();
        sendHeading();
    }
}
