using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamManager : MonoBehaviour
{

    private void Awake() {
        DontDestroyOnLoad(this);
        try {
            Steamworks.SteamClient.Init(1757560);
        } catch(System.Exception e) {
            Debug.Log("Falha ao rodar steam");
        } 
    }

    private void OnApplicationQuit() {
        try {
            Steamworks.SteamClient.Shutdown();
        } catch {

        }
    }

    private void Update() {
        Steamworks.SteamClient.RunCallbacks();
    }
}
