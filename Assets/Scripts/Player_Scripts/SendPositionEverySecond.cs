using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class SendPositionEverySecond : MonoBehaviour
{
    [System.Serializable]
    public class position_info{
        public string username;
        public float pos_x;
        public float pos_y;
        public float pos_z;
    }
    [System.Serializable]
    public class Info{
        public string username;
        public int accept;
    }

    public float Initial_TimeToSendPosition = 1f;
    public float TimeToSendPosition = 1f;

    // Update is called once per frame
    void Update()
    {

        if(TimeToSendPosition == 0){

            //string jsonText = File.ReadAllText(Application.persistentDataPath + "/credentials.json");
            //Info data = JsonUtility.FromJson<Info>(jsonText);
            //string username = data.username;
            string username = PlayerPrefs.GetString("username");

            position_info user_position_info = new position_info();
            user_position_info.username = username;
            Transform player_transform = gameObject.transform;
            user_position_info.pos_x = player_transform.position[0];
            user_position_info.pos_y = player_transform.position[1];
            user_position_info.pos_z = player_transform.position[2];
            string str_info = JsonUtility.ToJson(user_position_info);
            string url = "https://southamerica-east1-disco-bedrock-364702.cloudfunctions.net/add_position_each_second"; // Replace with your API endpoint
            string json = str_info;
            Debug.Log(json);
            byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);

            UnityWebRequest request = new UnityWebRequest(url, "POST");
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SendWebRequest();

            TimeToSendPosition = Initial_TimeToSendPosition;
        }
        else{
            if(TimeToSendPosition > 0){
                TimeToSendPosition -= Time.deltaTime;
            }
            else{
                TimeToSendPosition = 0f;
            }
        }
    }
}
