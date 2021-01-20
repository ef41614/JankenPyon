using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CLobbyUIScript : MonoBehaviour
{
    /*
//部屋作成ウインドウ
//public GameObject CreateRoomPanel;  // 部屋作成ウインドウ
public Text RoomNameText;           // 作成する部屋名
//public Text PlayerNumberText;       // 最大入室可能人数表示用Text
public Button CreateRoomButton;     // 部屋作成ボタン
public GameObject BGM_SE_Manager;
BGM_SE_Manager BGM_SE_MSC;

byte MaxPlayers_inRoom = 4;         // 1ルームに入れる最大人数
string Chimei = "";
string Michi = "";
string Jikan = "";

int Rnd_Chimei;
int Rnd_Michi;
int Rnd_Jikan;

//☆################☆################  Start  ################☆################☆

void Start()
{
    BGM_SE_Manager = GameObject.Find("BGM_SE_Manager");
    BGM_SE_MSC = BGM_SE_Manager.GetComponent<BGM_SE_Manager>();
}

//部屋作成ボタンを押したときの処理
public void OnClick_CreateRoomButton()
{
    //作成する部屋の設定
    RoomOptions roomOptions = new RoomOptions();
    roomOptions.IsVisible = true;   //ロビーで見える部屋にする
    roomOptions.IsOpen = true;      //他のプレイヤーの入室を許可する
    roomOptions.MaxPlayers = MaxPlayers_inRoom;    //入室可能人数を設定
    //ルームカスタムプロパティで部屋作成者を表示させるため、作成者の名前を格納
    roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
    {
        { "RoomCreator",PhotonNetwork.NickName }
    };

    //ロビーにカスタムプロパティの情報を表示させる
    roomOptions.CustomRoomPropertiesForLobby = new string[]
    {
        "RoomCreator",
    };
    // 部屋名設定
    RudCreate_Chimei();
    RudCreate_Michi();
    //RudCreate_Jikan();
    //RoomNameText.text = "MyRoom";
    RoomNameText.text = Chimei + Michi + Jikan;

    // 部屋名がなければデフォルトの部屋名を設定
    if (string.IsNullOrEmpty(RoomNameText.text))
    {
        RoomNameText.text = "MyRoom";
    }

    //部屋作成
    PhotonNetwork.CreateRoom(RoomNameText.text, roomOptions, null);
}


public void RudCreate_Chimei()  //変数「 Rnd_Chimei 」の値を元に、 全10パターンの間で場合分けをする
{
    Rnd_Chimei = UnityEngine.Random.Range(1, 10);

    if (BGM_SE_MSC.stage_No == 0)  // 街の街道
    {
        switch (Rnd_Chimei)
        {
            case 1: //
                Chimei = "コンパトリ";
                break;
            case 2: //
                Chimei = "ペリアーテ";
                break;
            case 3: //
                Chimei = "サクローナ";
                break;
            case 4: //
                Chimei = "モンテルニ";
                break;
            case 5: //
                Chimei = "ジェナポリ";
                break;
            case 6: //
                Chimei = "クレモデナ";
                break;
            case 7: //
                Chimei = "ディカーラ";
                break;
            case 8: //
                Chimei = "ディポバッソ";
                break;
            case 9: //
                Chimei = "スカレッコ";
                break;
            default:
                // その他処理
                break;
        }
    }

    if (BGM_SE_MSC.stage_No == 1)   // エジプトのピラミッド
    {
        switch (Rnd_Chimei)
        {
            case 1: //
                Chimei = "ラッシメピレ";
                break;
            case 2: //
                Chimei = "ペエニ";
                break;
            case 3: //
                Chimei = "ドラクーアー";
                break;
            case 4: //
                Chimei = "リングアコ";
                break;
            case 5: //
                Chimei = "コツヨン";
                break;
            case 6: //
                Chimei = "ニュポ";
                break;
            case 7: //
                Chimei = "ヘテー";
                break;
            case 8: //
                Chimei = "ストーララト";
                break;
            case 9: //
                Chimei = "ソコゾダ";
                break;
            default:
                // その他処理
                break;
        }
    }
}


public void RudCreate_Michi()  //変数「 Rnd_Michi 」の値を元に、 全10パターンの間で場合分けをする
{
    Rnd_Michi = UnityEngine.Random.Range(1, 10);

    if (BGM_SE_MSC.stage_No == 0)   // 街の街道
    {
        switch (Rnd_Michi)
        {
            case 1: //
                Michi = " いなかみち";
                break;
            case 2: //
                Michi = " ストリート";
                break;
            case 3: //
                Michi = " しょうてんがい";
                break;
            case 4: //
                Michi = " ゆうほどう";
                break;
            case 5: //
                Michi = " さんぽみち";
                break;
            case 6: //
                Michi = " ２ばんがい";
                break;
            case 7: //
                Michi = " ひがしどおり";
                break;
            case 8: //
                Michi = " ちゅうおうどおり";
                break;
            case 9: //
                Michi = " うらみち";
                break;
            default:
                // その他処理
                break;
        }
    }

    if (BGM_SE_MSC.stage_No == 1)   // エジプトのピラミッド
    {
        switch (Rnd_Michi)
        {
            case 1: //
                Michi = " 王の間";
                break;
            case 2: //
                Michi = " 王妃の間";
                break;
            case 3: //
                Michi = " 大回廊";
                break;
            case 4: //
                Michi = " 地下の間";
                break;
            case 5: //
                Michi = " 玄室";
                break;
            case 6: //
                Michi = " 上昇通路";
                break;
            case 7: //
                Michi = " 水平通路";
                break;
            case 8: //
                Michi = " 下降通路";
                break;
            case 9: //
                Michi = " 控えの間";
                break;
            default:
                // その他処理
                break;
        }
    }
}


public void RudCreate_Jikan()  //変数「 Rnd_Jikan 」の値を元に、 全5パターンの間で場合分けをする
{
    if (BGM_SE_MSC.stage_No == 0)
    {
        Rnd_Jikan = UnityEngine.Random.Range(1, 6);

        switch (Rnd_Jikan)
        {
            case 1: //
                Jikan = " ひるさがり";
                break;
            case 2: //
                Jikan = " おひるどき";
                break;
            case 3: //
                Jikan = " 15じ";
                break;
            case 4: //
                Jikan = " ゆうがた";
                break;
            case 5: //
                Jikan = " あさ";
                break;
            default:
                // その他処理
                break;
        }
    }
}
*/

}