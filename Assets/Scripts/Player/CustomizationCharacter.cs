using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationCharacter : MonoBehaviour
{
    [SerializeField] private Dropdown capsDropdown;
    [SerializeField] private GameObject[] caps;

    [SerializeField] private InputField fieldNickname;
    [SerializeField] private Text nicknameText;

    [SerializeField] private InfoPlayerScriptableObject _info;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("NickName"))
        {
            PhotonNetwork.NickName = $"Player#{Random.Range(1000, 9999)}";
            PlayerPrefs.SetString("NickName", PhotonNetwork.NickName);
        }
        else
            PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
        nicknameText.text = PhotonNetwork.NickName;
        if (!PlayerPrefs.HasKey("CapIndex")) 
        {
            _info.indexCap = capsDropdown.value;
            PlayerPrefs.SetInt("CapIndex", _info.indexCap);
            for (int i = 0; i < caps.Length; i++)
                caps[i].SetActive(false);
        }
        else
        {
            _info.indexCap = PlayerPrefs.GetInt("CapIndex");

            if (_info.indexCap == 0)
            {
                for (int i = 0; i < caps.Length; i++)
                    caps[i].SetActive(false);
            }
            else
            {
                capsDropdown.value = _info.indexCap;
                caps[_info.indexCap].SetActive(true);
            }
        }
    }

    public void ChangeClothes()
    {
        _info.indexCap = capsDropdown.value;
        if (_info.indexCap == 0)
        {
            for (int i = 0; i < caps.Length; i++)
                caps[i].SetActive(false);
        }
        else
        {
            for (int i = 0; i < caps.Length; i++)
                caps[i].SetActive(false);
            caps[_info.indexCap].SetActive(true);
        }

        PlayerPrefs.SetInt("CapIndex", _info.indexCap);
    }

    public void OnChangedNickNameField()
    {
        PhotonNetwork.NickName = fieldNickname.text;
        nicknameText.text = PhotonNetwork.NickName;
        PlayerPrefs.SetString("NickName", PhotonNetwork.NickName);
    }
}
