using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private GameObject[] caps;
    [SerializeField] private InfoPlayerScriptableObject _info;
    [SerializeField] private GameObject _canvas;
    
    private int indexCap;
    private Text nicknameText;

    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        nicknameText = _canvas.transform.GetChild(0).gameObject.GetComponent<Text>();
        nicknameText.text = photonView.Owner.NickName;
        indexCap = _info.indexCap;
        photonView.RPC("SendIndexCap", RpcTarget.AllBuffered, indexCap);
    }

    private void Start() => ChangeClothes();

    private void Update() => _canvas.transform.LookAt(SmoothlyCameraFollow.instance.transform.position);

    [PunRPC]
    private void SendIndexCap(int index)
    {
        indexCap = index;
    }

    private void ChangeClothes()
    {
        for (int i = 0; i < caps.Length; i++)
            caps[i].SetActive(false);
        if (indexCap != 0)
            caps[indexCap].SetActive(true);
    }
}
