using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using Netcode.Transports.Facepunch;

public class ToxicNetworkManager : NetworkManager
{
    private Steamworks.SteamId _aekeronId = 76561198024887155;
    private Steamworks.SteamId _toxicId = 76561199416120183;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ButtonCallback_HostGame()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += LoadSceneAsHost;

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private void LoadSceneAsHost(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= LoadSceneAsHost;

        StartHost();
    }

    public void ButtonCallback_JoinGame()
    {
        FacepunchTransport fp = GetComponent<FacepunchTransport>();

        if(Steamworks.SteamClient.SteamId == _aekeronId)
        {
            fp.targetSteamId = _toxicId;
        }
        else
        {
            fp.targetSteamId = _aekeronId;
        }

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += LoadSceneAsClient;

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }    

    private void LoadSceneAsClient(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= LoadSceneAsClient;

        StartClient();
    }
}
