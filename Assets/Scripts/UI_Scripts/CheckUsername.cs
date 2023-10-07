using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class CheckUsername : MonoBehaviour
{
    [System.Serializable]
    public class Info{
        public string username;
        public int accept;
    }
    public void Start(){
        if(System.IO.File.Exists(Application.persistentDataPath + "/credentials.json")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        };
    }

    public void ClickIniciarJogo()
    {
        GameObject Canvas = Camera.main.gameObject.transform.Find("Canvas").gameObject;
        GameObject Username_GO = Canvas.transform.Find("username").gameObject;
        GameObject Accept_GO = Canvas.transform.Find("accept").gameObject;
        string accept = Accept_GO.transform.Find("Label").gameObject.GetComponent<TextMeshProUGUI>().text;
        string username = Username_GO.GetComponent<TMP_InputField>().text;
        int accept_bool = 1;
        if(accept == "Aceito compartilhar meus dados"){
            accept_bool = 1;    
        }
        else{
            accept_bool = 0;
        }
        if(username == ""){
            EditorUtility.DisplayDialog ("Alerta", "Por Favor digite um username não vazio", "Ok"); 
            Debug.Log(accept_bool);
        }
        else{
            Info user_info = new Info();
            user_info.username = username;
            user_info.accept = accept_bool;
            string str_info = JsonUtility.ToJson(user_info);
            Debug.Log(Application.persistentDataPath);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/credentials.json", str_info);
            
            string url = "https://southamerica-east1-disco-bedrock-364702.cloudfunctions.net/add_usernames"; // Replace with your API endpoint
            string json = str_info;
            byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);

            UnityWebRequest request = new UnityWebRequest(url, "POST");
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");



            request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("POST request successful!");
                Debug.Log(request.downloadHandler.text); // Response from server
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
