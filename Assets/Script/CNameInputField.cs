using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
public class CNameInputField : MonoBehaviourPunCallbacks
{
    #region Private変数定義
    static string playerNamePrefKey = "PlayerName";
    InputField _inputField;
    #endregion
    #region MonoBehaviourコールバック
    void Start()
    {
        string defaultName = "";
        _inputField = this.GetComponent<InputField>();
        //前回プレイ開始時に入力した名前をロードして表示
        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }
    }
    #endregion
    #region Public Method
    public void SetPlayerName()
    {
        PhotonNetwork.NickName = _inputField.text + " ";     //今回ゲームで利用するプレイヤーの名前を設定
        PlayerPrefs.SetString(playerNamePrefKey, _inputField.text);    //今回の名前をセーブ
        PlayerPrefs.Save();
        Debug.Log("現在 Launcher です。はじめのニックネーム入力のところ");
        Debug.Log("PhotonNetwork.NickName ランチャー：" + PhotonNetwork.NickName);
        Debug.Log("ニックネーム確認_Launcher");   //playerの名前の確認。（動作が確認できればこの行は消してもいい）
    }
    #endregion
}