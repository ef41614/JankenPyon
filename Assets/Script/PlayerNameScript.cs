using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;
using UnityEngine.EventSystems;

public class PlayerNameScript : MonoBehaviour
{
    public PhotonView myPV;
    public PhotonTransformView myPTV;
    public Text PName1;
    public Text PName2;
    public Text PName3;
    public Text PName4;
    //public GameObject PhotonPropertyManager;
    //PhotonProperty PhotonPropertyMSC;

    void Awake()
    {
        this.myPV = GetComponent<PhotonView>();
    }

    //☆################☆################  Start  ################☆################☆

    void Start()
    {
        //PhotonPropertyMSC = PhotonPropertyManager.GetComponent<PhotonPropertyManager>();
        Debug.Log("初期 名前確認");
        Debug.Log("PName1 ：" + PName1.text);
        Debug.Log("PName2 ：" + PName2.text);
        Debug.Log("PName3 ：" + PName3.text);
        Debug.Log("PName4 ：" + PName4.text);
        Debug.Log("PhotonNetwork.NickName PlayerNameScript：" + PhotonNetwork.NickName);
        Debug.Log("***************");

        if (PName1.text == "")
        {
            PName1.text = PhotonNetwork.NickName;
        }
        else if (PName2.text == "")
        {
            PName2.text = PhotonNetwork.NickName;
        }
        else if (PName3.text == "")
        {
            PName3.text = PhotonNetwork.NickName;
        }
        else if (PName4.text == "")
        {
            PName4.text = PhotonNetwork.NickName;
        }
        else
        {
            Debug.Log("5人以上は入らないよぅ");
        }

        Debug.Log("置き換え後 名前確認");
        Debug.Log("PName1 ：" + PName1.text);
        Debug.Log("PName2 ：" + PName2.text);
        Debug.Log("PName3 ：" + PName3.text);
        Debug.Log("PName4 ：" + PName4.text);
    }


    //####################################  Update  ###################################

    void Update()
    {


    }

    //####################################  other  ####################################
    //#################################################################################

}
// End