using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    private ChatClient chatClient;

    public TMP_InputField plrName;
    public TextMeshProUGUI connectionState;
    public TMP_InputField msgInput;
    public TextMeshProUGUI msgArea;
    
    public GameObject intoOanel;
    public GameObject msgPanel;

    private string worldchat;
    [SerializeField] private string userID;

    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground=true;
        if(string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat))
        {
            Debug.LogError("No AppId Provided");
            return;
        }

        worldchat = "world";
    }

    // Update is called once per frame
    void Update()
    {
        if(chatClient !=null)
        {
            chatClient.Service();
        }
    }

    public void GetConnected()
    {
        Debug.Log("Connecting");
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, 
            new Photon.Chat.AuthenticationVAlues(plrName.text));
    }
    public void OnConnected()
    {
        chatClient.Subscribe(new string[] {worldchat});
        chatClient.SetOnlineStatus(ChatUserStatus.Online);
    }
    public void OnSubscribed(string[] channels, bool[] results)
    {
        foreach (var channel in channels) 
        {
            this.chatClient.PublishMessage(channel,"joined");
        }
        connectionState.text="connected";
    }
    public void onGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i=0; i <senders.Length;i++)
        {
            msgArea.text += senders[i] + ": " + messages + ", ";
        }
    }
    public void SendMsg()
    {
        chatClient.PublishMessage(worldchat, msgInput.text);
    }
}
