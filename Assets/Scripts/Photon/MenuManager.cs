using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MenuManager : MonoBehaviourPunCallbacks
{
    [Header("Основные переменные")]

    [Space]

    [SerializeField] private int maxPlayers = 5;
    [SerializeField] private GameObject[] panels;

    private void Awake() => OpenPanel(0);

    private void Start()
    {
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();
        else
            OpenPanel(1);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Succesfully connected to server!");
        OpenPanel(1);
    }

    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom($"Room №{Random.Range(0, 1000)}", roomOptions, TypedLobby.Default);
    }

    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Can't find avaible room: creating new one");
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room founded!");
        PhotonNetwork.LoadLevel(1);
    }

    private void OpenPanel(int indexPanel)
    {
        for (int i = 0; i < panels.Length; i++)
            panels[i].SetActive(false);
        panels[indexPanel].SetActive(true);
    }

    public void QuitGame() => Application.Quit();
}
