using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundData
    {
        //���̕ʖ�
        public string soundName;

        //��L�̕ʖ��ɑΉ�������
        public AudioClip audioClip;
    }

    //���̕ʖ��Ƃ���ɑΉ����������C���X�y�N�^�[�ŕҏW�A�Ǘ����邽�߂̔z��
    [SerializeField]
    private SoundData[] soundDatas;

    //�X�s�[�J�[���쐬
    [SerializeField]
    private AudioSource audioSource;

    //SoundData�p�f�B�N�V���i���[
    private Dictionary<string, SoundData> soundDataDictionary;

    private void Awake()
    {
        soundDataDictionary = new Dictionary<string, SoundData>();

        foreach (SoundData soundData in soundDatas)
            soundDataDictionary.Add(soundData.soundName, soundData);
    }

    private void PlaySound(AudioClip audioClip)
    {
        //AudioSource���g�p���Ă���ꍇ�A��~����
        if (audioSource.isPlaying)
            audioSource.Stop();

        //clip��ݒ肵�āA�炷
        audioSource.clip = audioClip;
        audioSource.Play();
    }


    //soundDataDictionary�Ɋi�[���ꂽname���Đ�����
    public void Play(string name)
    {
        if (soundDataDictionary.TryGetValue(name, out var soundData))
            PlaySound(soundData.audioClip);
        else
            Debug.LogWarning($"{name}�͓o�^����Ă��܂���");
    }
}
