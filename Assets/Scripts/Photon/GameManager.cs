using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        PhotonNetwork.Instantiate(spawnPrefab.name, spawnPoint.position, Quaternion.identity);
    }
}
