using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BGM_SE_Manager : MonoBehaviour
{
    public static BGM_SE_Manager instance = null;
    public int firstMatch = 0;
    public int firstRead_Selectjanken = 0;         // Selectjanken.cs が 2回呼ばれるのを防ぐためのフラグ
    public int firstRead_TestRoomController = 0;   // TestRoomController.cs が 2回呼ばれるのを防ぐためのフラグ

    public int stage_No = 0;                       // Battle ステージ

    private AudioSource audioSource = null;
    AudioSource loopAudioSource;

    public GameObject Volume_Panel;
    public GameObject Credit_Panel;

    // BGM
    public AudioClip Dadadadau;             // Launcher シーンBGM
    public AudioClip SasazukaHighwayPark;   // Mike シーンBGM
    public AudioClip FunAndLight;           // Battle シーンBGM
    public AudioClip Fanfare_Roop;
    public AudioClip iseki_ogg;
    public AudioClip NightTown_funk;
    public AudioClip AcrivePark_jog;

    // SE
    public AudioClip whistle;
    public AudioClip Fanfare_solo;
    public AudioClip StartRappa;
    public AudioClip bottonwo_oshitene;
    public AudioClip korede_iikana;
    public AudioClip TaihouFire;
    public AudioClip gold_fusoku;
    public AudioClip Tarai_Guwan;
    public AudioClip cure;
    public AudioClip CoinGet;
    public AudioClip Shiharai;
    public AudioClip Tired;
    public AudioClip Distribute_JankenCards_15;
    public AudioClip Distribute_JankenCards_18;
    public AudioClip Card_Mekuri;
    public AudioClip cancel;

    public string Aikotoba = "";  // あいことば（ルーム名になる）


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        loopAudioSource = GetComponent<AudioSource>();
        //stage_No = UnityEngine.Random.Range(0, 2);      // Battle ステージ

    }

    private void Start()
    {
        audioSource.loop = false;
        loopAudioSource.loop = true;
        //Volume_Panel = GameObject.Find("Volume_Panel");
    }

    #region // 音量調整

    public void find_Vol_Panel()
    {
        Volume_Panel = GameObject.Find("Volume_Panel");
    }

    public void Vol_Set_0()
    {
        audioSource.volume = 0;
        loopAudioSource.volume = 0;
    }

    public void Vol_Set_1()
    {
        audioSource.volume = 0.25f;
        loopAudioSource.volume = 0.25f;
    }

    public void Vol_Set_2()
    {
        audioSource.volume = 0.5f;
        loopAudioSource.volume = 0.5f;
    }

    public void Vol_Set_3()
    {
        audioSource.volume = 0.75f;
        loopAudioSource.volume = 0.75f;
    }

    public void Vol_Set_4()
    {
        audioSource.volume = 1;
        loopAudioSource.volume = 1;
    }
    #endregion


    #region // SE *******************************
    public void whistle_SE()
    {
        audioSource.PlayOneShot(whistle);
    }

    public void Fanfare_solo_SE()
    {
        audioSource.PlayOneShot(Fanfare_solo);
    }

    public void StartRappa_SE()
    {
        audioSource.PlayOneShot(StartRappa);
    }

    public void bottonwo_oshitene_SE()
    {
        audioSource.PlayOneShot(bottonwo_oshitene);
    }

    public void korede_iikana_SE()
    {
        audioSource.PlayOneShot(korede_iikana);
    }

    public void TaihouFire_SE()
    {
        audioSource.PlayOneShot(TaihouFire);
    }

    public void gold_fusoku_SE()
    {
        audioSource.PlayOneShot(gold_fusoku);
    }

    public void Tarai_Guwan_SE()
    {
        audioSource.PlayOneShot(Tarai_Guwan);
    }

    public void cure_SE()
    {
        audioSource.PlayOneShot(cure);
    }

    public void CoinGet_SE()
    {
        audioSource.PlayOneShot(CoinGet);
    }

    public void Shiharai_SE()
    {
        audioSource.PlayOneShot(Shiharai);
    }

    public void Tired_SE()
    {
        audioSource.PlayOneShot(Tired);
    }

    public void Distribute_JankenCards_15_SE()
    {
        audioSource.PlayOneShot(Distribute_JankenCards_15);
    }

    public void Distribute_JankenCards_18_SE()
    {
        audioSource.PlayOneShot(Distribute_JankenCards_18);
    }

    public void Card_Mekuri_SE()
    {
        audioSource.PlayOneShot(Card_Mekuri);
    }

    public void cancel_SE()
    {
        audioSource.PlayOneShot(cancel);
    }
    #endregion


    #region // BGM ******************************

    public void Dadadadau_BGM()               // Launcher シーンBGM
    {
        loopAudioSource.clip = Dadadadau;
        loopAudioSource.Play();
    }

    public void SasazukaHighwayPark_BGM()    // Mike シーンBGM
    {
        loopAudioSource.clip = SasazukaHighwayPark;
        loopAudioSource.Play();
    }

    public void FunAndLight_BGM()            // Battle シーンBGM
    {
        loopAudioSource.clip = FunAndLight;
        loopAudioSource.Play();
    }

    public void Stop_BGM()            // Battle シーンBGM
    {
        loopAudioSource.Stop();
    }

    public void Fanfare_Roop_BGM()
    {
        loopAudioSource.clip = Fanfare_Roop;
        loopAudioSource.Play();
    }

    public void iseki_ogg_BGM()
    {
        loopAudioSource.clip = iseki_ogg;
        loopAudioSource.Play();
    }

    public void NightTown_funk_BGM()
    {
        loopAudioSource.clip = NightTown_funk;
        loopAudioSource.Play();
    }

    public void AcrivePark_jog_BGM()
    {
        loopAudioSource.clip = AcrivePark_jog;
        loopAudioSource.Play();
    }
    #endregion

    /*
     *     public void OnVolSliderChanged()
        {
            //c.volume = volSlider.value;
            loopAudioSource.volume = volSlider.value;
        }
        /// <summary>
        /// SEを鳴らす
        /// </summary>
        public void PlaySE(AudioClip clip)
        {
            if (audioSource != null)
            {
                audioSource.PlayOneShot(clip);

            }
            else
            {
                Debug.Log("オーディオソースが設定されていません");
            }
        }

        /// <summary>
        /// BGMを鳴らす
        /// </summary>
        public void PlayBGM(AudioClip clip)
        {
            if (audioSource != null)
            {
                audioSource.Play();

            }
            else
            {
                Debug.Log("オーディオソースが設定されていません");
            }
        }

        public void Play_RoopBGM(AudioClip clip)
        {
                loopAudioSource.Play();
        }

        /// <summary>
        /// BGMを止める
        /// </summary>
        public void StopBGM(AudioClip clip)
        {
            Debug.Log("BGMを止めます");
            audioSource.Stop();
            loopAudioSource.Stop();
        }
        */

    public void AppearVolume_Panel()
    {
        Volume_Panel.SetActive(true);
    }

    public void CloseVolume_Panel()
    {
        Volume_Panel.SetActive(false);
    }

    public void AppearCredit_Panel()
    {
        Credit_Panel.SetActive(true);
    }

    public void CloseCredit_Panel()
    {
        Credit_Panel.SetActive(false);
    }
}