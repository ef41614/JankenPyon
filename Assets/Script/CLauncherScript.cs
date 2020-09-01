using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class CLauncherScript : MonoBehaviourPunCallbacks
{
    #region Public変数定義

    //Public変数の定義はココで
    public static int int_MyCharaAvatar = -1;
    static int int_player1_CharaAvatar = -1;
    static int int_player2_CharaAvatar = -1;
    static int int_player3_CharaAvatar = -1;
    static int int_player4_CharaAvatar = -1;

    #endregion

    #region Private変数
    //Private変数の定義はココで
    private bool firstPush = false;
    #endregion

    void Start()
    {
        firstPush = false; //初期化
    }


    #region Public Methods
    //ログインボタンを押したときに実行される
    public void Connect()
    {
        Debug.Log("CLauncherScript で「Play」が押されました。");
        if (!firstPush)
        {
            Debug.Log("PhotonNetwork.NickName Play：" + PhotonNetwork.NickName);
            if (string.IsNullOrWhiteSpace(PhotonNetwork.NickName))
            {
                Debug.Log("名前が空欄です");
            }
            else
            {
                if (!PhotonNetwork.IsConnected)
                {           //Photonに接続できていなければ
                    PhotonNetwork.ConnectUsingSettings();   //Photonに接続する
                    Debug.Log("Photonに接続しました。");
                }
            }
            firstPush = true; //ボタン押下済みフラグ
        }
    }

    public void Select_CharaAvatar_utako()
    {
        int_MyCharaAvatar = 1;
    }

    public void Select_CharaAvatar_unitychan()
    {
        int_MyCharaAvatar = 2;
    }

    public void Select_CharaAvatar_pchan()
    {
        int_MyCharaAvatar = 3;
    }

    public void Select_CharaAvatar_mobuchan()
    {
        int_MyCharaAvatar = 4;
    }

    public static int get_int_MyCharaAvatar()
    {
        return int_MyCharaAvatar;
    }
    #endregion

    #region Photonコールバック
    //ルームに入室前に呼び出される
    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions();
        Debug.Log("OnConnectedToMasterが呼ばれました");
        options.PublishUserId = true; // ★お互いにユーザＩＤが見えるようにする。
        options.MaxPlayers = 4; // ★最大人数もきちんと定義しておく。
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        PhotonNetwork.JoinOrCreateRoom("room", options, TypedLobby.Default);
    }

    //ルームに入った時に呼ばれる
    public override void OnJoinedRoom()
    {
        Debug.Log("ルームに入りました。");
        //battleシーンをロード
        PhotonNetwork.LoadLevel("Battle");
    }

    #endregion
}