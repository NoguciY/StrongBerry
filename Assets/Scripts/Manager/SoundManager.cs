using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundData
    {
        //音の別名
        public string soundName;

        //上記の別名に対応した音
        public AudioClip audioClip;
    }

    //音の別名とそれに対応した音をインスペクターで編集、管理するための配列
    [SerializeField]
    private SoundData[] soundDatas;

    //スピーカーを作成
    [SerializeField]
    private AudioSource audioSource;

    //SoundData用ディクショナリー
    private Dictionary<string, SoundData> soundDataDictionary;

    private void Awake()
    {
        soundDataDictionary = new Dictionary<string, SoundData>();

        foreach (SoundData soundData in soundDatas)
            soundDataDictionary.Add(soundData.soundName, soundData);
    }

    private void PlaySound(AudioClip audioClip)
    {
        //AudioSourceを使用している場合、停止する
        if (audioSource.isPlaying)
            audioSource.Stop();

        //clipを設定して、鳴らす
        audioSource.clip = audioClip;
        audioSource.Play();
    }


    //soundDataDictionaryに格納されたnameを再生する
    public void Play(string name)
    {
        if (soundDataDictionary.TryGetValue(name, out var soundData))
            PlaySound(soundData.audioClip);
        else
            Debug.LogWarning($"{name}は登録されていません");
    }
}
