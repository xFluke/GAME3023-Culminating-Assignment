using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    private MusicManager() { }
    private static MusicManager instance;
    public static MusicManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MusicManager>();
            }
            return instance;
        }

        private set {  }
    }

    [SerializeField]
    AudioSource musicSource;

    [SerializeField]
    AudioClip[] trackList;

    [SerializeField]
    AudioMixer musicMixer;

    [SerializeField]
    float volumeMin_dB = -80.0f;
    [SerializeField]
    float volumeMax_dB = 0.0f;


    [SerializeField]
    GameManager gameManager;



    public enum Track
    {
        OverWorld = 0,
        Battle = 1
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.onBattleSceneLoaded.AddListener(OnEnterBattleSceneHandler);
        gameManager.onOverworldSceneLoaded.AddListener(OnExitBattleSceneHandler);

        MusicManager[] musicManagers = FindObjectsOfType<MusicManager>();
        foreach (MusicManager mgr in musicManagers)
        {
            if (mgr != Instance)
            {
                Destroy(mgr.gameObject);
            }
        }

        DontDestroyOnLoad(transform.root);
    }

    public void OnEnterBattleSceneHandler(Enemy e, Ability[] a)
    {
        PlayTrack(Track.Battle);
    }

    public void OnExitBattleSceneHandler()
    {
        FadeInTrackOverSeconds(Track.OverWorld, 5.0f);
    }

    public void PlayTrack(MusicManager.Track trackID)
    {
        musicSource.clip = trackList[(int)trackID];
        musicSource.Play();
    }

    public void FadeInTrackOverSeconds(MusicManager.Track trackID, float duration)
    {
        musicSource.volume = 0.0f;
        PlayTrack(trackID);
        StartCoroutine(FadeInTrackOverSecondCoroutine(duration));
    }

    IEnumerator FadeInTrackOverSecondCoroutine(float duration)
    {
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float normalizedTime = timer / duration;

            musicSource.volume = Mathf.SmoothStep(0, 1, normalizedTime);

            // Fade volume
            yield return new WaitForEndOfFrame();
        }
    }

    public void SetMusicVolume(float volumeNormalized)
    {
        musicMixer.SetFloat("Music Volume", Mathf.Lerp(volumeMin_dB, volumeMax_dB, volumeNormalized));
    }
}
