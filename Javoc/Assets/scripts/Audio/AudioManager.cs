using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public enum AudioChannel { Master, Sfx, Bgm };
    public float masterVolumePercent { get; private set; }
    public float sfxVolumePercent { get; private set; }
    public float bgmVolumePercent { get; private set; }

    [SerializeField] bool test_deactivateMusic;
    [SerializeField] bool test_deactivateSound;
    [SerializeField] private SoundLibrary library;

    AudioSource sfx2DSource;
    AudioSource[] musicSources;
    int activeMusicSourdeIndex;

    public static AudioManager instance;

    Transform audioListener;
    Transform cameraT;
    
    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);

            musicSources = new AudioSource[2];
            for (int i = 0; i < 2; i++) {
                GameObject newMusicSource = new GameObject("Music Source " + (i + 1));
                musicSources[i] = newMusicSource.AddComponent<AudioSource>();
                newMusicSource.transform.parent = transform;
                musicSources[i].loop = true;
            }
            GameObject newSfx2DSource = new GameObject("2D SFX Source");
            sfx2DSource = newSfx2DSource.AddComponent<AudioSource>();
            newSfx2DSource.transform.parent = transform;

            audioListener = FindObjectOfType<AudioListener>().transform;

            cameraT = Camera.main.transform;

            masterVolumePercent = PlayerPrefs.GetFloat("Audio Master Volume", 1);
            sfxVolumePercent = PlayerPrefs.GetFloat("Audio SFX Volume", 1);
            bgmVolumePercent = PlayerPrefs.GetFloat("Audio BGM Volume", 1);
        }
    }

    void Update() {
        if (cameraT != null) {
            audioListener.position = cameraT.position;
        }
    }

    public static void SetVolume(float newVolume, AudioChannel channel) {
        if (instance == null) return;
        switch (channel) {
            case AudioChannel.Master:
                instance.masterVolumePercent = newVolume;
                break;
            case AudioChannel.Sfx:
                instance.sfxVolumePercent = newVolume;
                break;
            case AudioChannel.Bgm:
                instance.bgmVolumePercent = newVolume;
                break;
        }

        instance.musicSources[0].volume = GetBgmVolume();
        instance.musicSources[1].volume = GetBgmVolume();

        PlayerPrefs.SetFloat("Audio Master Volume", instance.masterVolumePercent);
        PlayerPrefs.SetFloat("Audio SFX Volume", instance.sfxVolumePercent);
        PlayerPrefs.SetFloat("Audio BGM Volume", instance.bgmVolumePercent);
        PlayerPrefs.Save();
    }

    public static AudioClip CurrentMusic() {
        if (instance == null) return null;
        return instance.musicSources[instance.activeMusicSourdeIndex].clip;
    }

    public static void PlayMusic(string clipName, float fadeDuration = 1) {
        if (instance == null) return;
        if (instance.test_deactivateMusic) return;
        AudioClip clip = instance.library.GetClipFromName(clipName);
        if (instance.musicSources[instance.activeMusicSourdeIndex].clip == clip) return;
        instance.activeMusicSourdeIndex = 1 - instance.activeMusicSourdeIndex;
        if (clip == null) instance.musicSources[instance.activeMusicSourdeIndex].Stop();
        else {
            instance.musicSources[instance.activeMusicSourdeIndex].clip = clip;
            instance.musicSources[instance.activeMusicSourdeIndex].Play();
        }

        instance.StartCoroutine(instance.AnimateMusicCrossfade(fadeDuration));
    }

    public static void PlaySound(AudioClip clip, Vector3 pos) {
        if (instance == null) return;
        if (clip != null) {
            AudioSource.PlayClipAtPoint(clip, pos, GetSfxVolume());
        }

    }

    public static void PlaySound(string soundName, Vector3 pos) {
        if (instance == null) return;
        PlaySound(instance.library.GetClipFromName(soundName), pos);
    }

    public static void PlaySound2D(string soundName) {
        if (instance == null) return;
        instance.sfx2DSource.PlayOneShot(instance.library.GetClipFromName(soundName), GetSfxVolume());
    }

    public static float GetBgmVolume() {
        return instance.bgmVolumePercent * instance.masterVolumePercent;
    }
    public static float GetSfxVolume() {
        return instance.sfxVolumePercent * instance.masterVolumePercent;
    }

    IEnumerator AnimateMusicCrossfade(float duration) {
        float percent = 0;
        while (percent < 1) {
            percent += Time.deltaTime * 1 / duration;
            musicSources[activeMusicSourdeIndex].volume = Mathf.Lerp(0, GetBgmVolume(), percent);
            musicSources[1 - activeMusicSourdeIndex].volume = Mathf.Lerp(GetBgmVolume(), 0, percent);
            yield return null;
        }
    }
}
