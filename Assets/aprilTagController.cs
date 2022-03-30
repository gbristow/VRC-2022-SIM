using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class mqttAprilTagsVisablePos
{
    public float x = 0;
    public float y = 0;
    public float z = 0;
}
[Serializable]
public class mqttAprilTagsVisable
{
    public int id = 0;
    public float horizontal_dist = 0;
    public float angle_to_tag = 0;
    public float heading = 0;
    [SerializeField]
    public mqttAprilTagsVisablePos pos_rel = new mqttAprilTagsVisablePos();
    [SerializeField]
    public mqttAprilTagsVisablePos pos_world = new mqttAprilTagsVisablePos();
}
public class aprilTagController : MonoBehaviour
{
    public int id;
    public M2MqttUnity.Examples.M2MqttUnityTest mqttObj;

    public Transform droneTF, AprilTagTF;

    public float visableRadius = 11;
    private mqttAprilTagsVisable msg = new mqttAprilTagsVisable();
    private float horz_dist_to_tag()
    {
        float hor_dist_to_tag = Mathf.Sqrt(Mathf.Pow(droneTF.position.x - AprilTagTF.position.x, 2) + Mathf.Pow(droneTF.position.y - AprilTagTF.position.y, 2));
        return hor_dist_to_tag;
    }
    private float angle_to_tag()
    {
        float heading_global = Mathf.Atan2(AprilTagTF.position.z - droneTF.position.z, AprilTagTF.position.x - droneTF.position.x) * 57.29578f;
        float heading_rel = droneTF.rotation.y - heading_global;
        return heading_rel;
    }
    private mqttAprilTagsVisablePos get_rel_pos()
    {
        Vector3 pos_rel = droneTF.position - AprilTagTF.position;
        mqttAprilTagsVisablePos msg_rel_pos = new mqttAprilTagsVisablePos();
        msg_rel_pos.x = pos_rel.x;
        msg_rel_pos.y = pos_rel.y;
        msg_rel_pos.z = pos_rel.z;
        return msg_rel_pos;
    }
    private mqttAprilTagsVisablePos get_world_pos()
    {
        mqttAprilTagsVisablePos msg_world_pos = new mqttAprilTagsVisablePos();
        msg_world_pos.x = AprilTagTF.position.x;
        msg_world_pos.y = AprilTagTF.position.y;
        msg_world_pos.z = AprilTagTF.position.z;
        return msg_world_pos;
    }
    private void fill_message()
    {
        msg.id = id;
        msg.horizontal_dist = horz_dist_to_tag();
        msg.angle_to_tag = angle_to_tag();
        msg.heading = AprilTagTF.rotation.y;
        msg.pos_rel = get_rel_pos();
        msg.pos_world = get_world_pos();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float hor_dist_to_tag__m = horz_dist_to_tag();
        // mqttAprilTagsVisable msg = initTags();
        if (hor_dist_to_tag__m < visableRadius)
        {
            fill_message();
            Debug.Log(msg.id.ToString());
            string msg_str = JsonUtility.ToJson(msg);
            mqttObj.publish_msg("test/test2", msg_str);
            Debug.Log(msg_str);
        }
    }
}
