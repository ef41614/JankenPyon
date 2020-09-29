using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BGM_SE_Manager : MonoBehaviour
{
    public static BGM_SE_Manager instance = null;

    private AudioSource audioSource = null;
    AudioSource loopAudioSource;

    public GameObject Volume_Panel;

    // BGM
    public AudioClip Dadadadau;             // Launcher シーンBGM
    public AudioClip SasazukaHighwayPark;   // Mike シーンBGM
    public AudioClip FunAndLight;           // Battle シーンBGM
    public AudioClip Fanfare_Roop;

    // SE
    public AudioClip whistle;
    public AudioClip Fanfare_solo;


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
    }

    private void Start()
    {
        audioSource.loop = false;
        loopAudioSource.loop = true;
        //Volume_Panel = GameObject.Find("Volume_Panel");
    }

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


    #region // SE *******************************
    public void whistle_SE()
    {
        audioSource.PlayOneShot(whistle);
    }

    public void Fanfare_solo_SE()
    {
        audioSource.PlayOneShot(Fanfare_solo);
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
}