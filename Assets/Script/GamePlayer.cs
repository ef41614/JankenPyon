using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

[RequireComponent(typeof(SpriteRenderer))]
public class GamePlayer : MonoBehaviourPunCallbacks
{
    
    public TextMeshPro nameLabel = default;
    //int P1_Te1 = 0;

    //private ProjectileManager projectileManager;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
      //  projectileManager = GameObject.FindWithTag("ProjectileManager").GetComponent<ProjectileManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // プレイヤー名の横にスコアを表示する
        int score = photonView.Owner.GetScore();
        int s1_Te1 = photonView.Owner.Get_P1_Te1();
        int s2_Te1 = photonView.Owner.Get_P2_Te1();

        nameLabel.text = $"{photonView.Owner.NickName}({score.ToString()})";
        Debug.Log("GamePlayer 出席確認");
        Debug.Log("s1_Te1 : " + s1_Te1);
        Debug.Log("s2_Te1 : " + s2_Te1);
        
    }

    // 省略


    [PunRPC]
    private void HitByProjectile(int projectileId, int ownerId)
    {
        //projectileManager.Remove(projectileId, ownerId);
        if (photonView.IsMine)
        {
            PhotonNetwork.LocalPlayer.OnTakeDamage();
        }
        else if (ownerId == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            PhotonNetwork.LocalPlayer.OnDealDamage();
        }
    }

    public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps)
    {
        if (target.ActorNumber != photonView.OwnerActorNr) { return; }

        // スコアが更新されていたら、スコア表示も更新する
        if (changedProps.TryGetScore(out int score))
        {
            nameLabel.text = $"{photonView.Owner.NickName}({score.ToString()})";
        }

        if (changedProps.TryGet_P1_Te1(out int s1_Te1))
        {
            Debug.Log("s1_Te1 更新");
            Debug.Log("s1_Te1 : " + s1_Te1);
        }

        if (changedProps.TryGet_P2_Te1(out int s2_Te1))
        {
            Debug.Log("s2_Te1 更新");
            Debug.Log("s2_Te1 : " + s2_Te1);
        }


        // 色相値が更新されていたら、スプライトの色を変化させる
        if (changedProps.TryGetHue(out float hue))
        {
            spriteRenderer.color = Color.HSVToRGB(hue, 1f, 1f);
        }
    }
}