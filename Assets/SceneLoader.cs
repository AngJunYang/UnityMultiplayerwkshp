using System.Collections;
using System.Collections.Generic;
using FishNet.Managing.Logging;
using FishNet.Managing.Scened;
using FishNet.Object;
using FishNet;
using FishNet.Managing.Server;
using UnityEngine;
using FishNet.Object.Synchronizing;

public class SceneLoader : NetworkBehaviour
{

    public const string SCENE_NAME = "GameScene";


    public static void SwitchScene(NetworkObject obj)
    {

        SceneLoadData sld = new SceneLoadData(SCENE_NAME);
        sld.MovedNetworkObjects = new NetworkObject[] { obj };
        sld.ReplaceScenes = ReplaceOption.All;
        InstanceFinder.SceneManager.LoadGlobalScenes(sld);


    }
}
