using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using Photon.Pun;
using Photon.Realtime;

// 頭上のチャットを管理するマネージャー
public class CPlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    //頭上のUIのPrefab
    public GameObject PlayerUiPrefab;
    //現在のHP
    public int HP = 100;
    private PhotonView myPV;
    //Localのプレイヤーを設定
    public static GameObject LocalPlayerInstance;

    //チャット同期用変数
    public string ChatText;
    public Color TextColor;
    private bool isRunning;

    //頭上UIオブジェクト
    private GameObject _uiGo;
    public string PlayerName;

    float StartTime = 0.0f;
    // この距離より遠いPlyerUIは表示しない
    public float UIActiveDistance = 15.0f;

    #region プレイヤー初期設定
    void Awake()
    {
        myPV = gameObject.GetComponent<PhotonView>();
        if (myPV.IsMine)
        {
            CPlayerManager.LocalPlayerInstance = this.gameObject;
        }
    }
    #endregion
    #region 頭上UIの生成
    void Start()
    {
        PlayerUiPrefab.GetComponent<CPlayerUIScript>().SetTarget(this);
    }
    #endregion

    void Update()
    {
        if (!myPV.IsMine) //このオブジェクトがLocalでなければ実行しない
        {
            return;
        }
        //LocalVariablesを参照し、現在のHPを更新
        HP = CLocalVariables.currentHP;


        if (isRunning)
        {
            float f = Time.realtimeSinceStartup - StartTime;
            if (3 < f)
            {
                // チャットを消去する
                myPV.RPC("DeleteChat", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    public void DeleteChat(PhotonMessageInfo mi)
    {
        isRunning = false;
        ChatText = string.Empty;
    }

    // ChatRPC RPC呼出側：送信者　RPC受信側：受信者
    [PunRPC]
    public void Chat(int your_id,
        Vector3 senderposition,
        string newLine, int chat_type, PhotonMessageInfo mi)
    {
        switch (chat_type)
        {
            case 0://全チャとして受信
                ReceiveChat(newLine, chat_type, mi);
                break;
            case 1://範囲チャとして受信
                   //myPlayerとsenderの距離から受信するか判断
                if (Vector3.Distance(gameObject.transform.position, senderposition) < 10)
                {
                    ReceiveChat(newLine, chat_type, mi);
                }
                break;
            case 2:// 個別チャット
                   //if (PhotonNetwork.player.ID == mi.sender.ID || PhotonNetwork.player.ID == your_id)
                   // 個別チャット
                if (PhotonNetwork.LocalPlayer.ActorNumber == mi.Sender.ActorNumber || PhotonNetwork.LocalPlayer.ActorNumber == your_id)
                {
                    ReceiveChat(newLine, chat_type, mi);
                }
                break;
        }
    }
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.LocalPlayer.ActorNumber.ToString());
    }
    void ReceiveChat(string _newLine, int chat_type, PhotonMessageInfo _mi)
    {
        switch (chat_type)
        {
            case 0://全チャとして受信
                TextColor = Color.white;
                break;
            case 1://範囲チャとして受信
                TextColor = Color.blue;
                break;
            case 2:// 個別チャット
                TextColor = Color.green;
                break;
        }
        ChatText = _newLine;
        StartTime = Time.realtimeSinceStartup;
        isRunning = true;
    }
    // 頭上Chatの表示
    public void setChat(int your_id, Vector3 senderposition, string inputLine, int chat_type)
    {
        //chatRPC
        myPV.RPC("Chat", RpcTarget.All, your_id,
        senderposition,
        inputLine, chat_type);
    }
    #region OnPhotonSerializeView同期
    //プレイヤーのHP,チャットを同期
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.HP);
        }
        else
        {
            this.HP = (int)stream.ReceiveNext();
        }
    }
    #endregion
}