using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class payloadController : MonoBehaviour
{
    public M2MqttUnity.Examples.M2MqttUnityTest mqttObj;
    public int ServoNum;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform droneTF, payloadContainerTF, payloadTF;
    // Start is called before the first frame update
    bool isCarrying;
    private bool lastState;

    public bool drop;
    private void loadPayload()
    {
        isCarrying = true;
        rb.isKinematic = true;
        coll.isTrigger = true;

        transform.SetParent(payloadContainerTF);
        transform.localPosition = Vector3.zero;
        lastState = drop;
    }
    private void dropPayload()
    {
        Debug.Log("Drop");
        transform.SetParent(null);
        isCarrying = false;
        rb.isKinematic = false;
        coll.isTrigger = false;

        // rb.velocity = droneTF.GetComponent<Rigidbody>().velocity;
        lastState = drop;
    }
    void Start()
    {
        loadPayload();
        lastState = drop;
    }

    // Update is called once per frame
    void Update()
    {
        if (mqttObj != null)
        {
            if (mqttObj.servoMsgValues != null)
            {
                if (mqttObj.servoMsgValues.servo == ServoNum)
                {
                    if (mqttObj.servoMsgValues.action == "open")
                    {
                        drop = true;
                    }
                    if (mqttObj.servoMsgValues.action == "close")
                    {
                        drop = false;
                    }

                }
            }
        }


        if (drop != lastState)
        {
            if (drop)
            {
                dropPayload();
            }
            else
            {
                loadPayload();
            }

        }
    }
}
