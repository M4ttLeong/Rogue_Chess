using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    private void Awake()
    {
        serverButton.onClick.AddListener(() =>
        {
            Debug.Log("Starting a server!");
            NetworkManager.Singleton.StartServer();
        });

        hostButton.onClick.AddListener(() =>
        {
            Debug.Log("Starting a host!");
            NetworkManager.Singleton.StartHost();
        });

        clientButton.onClick.AddListener(() =>
        {
            Debug.Log("Starting a client!");
            NetworkManager.Singleton.StartClient();
        });
    }

}
