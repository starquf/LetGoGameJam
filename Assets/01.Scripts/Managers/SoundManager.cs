using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private const string MASTER_NAME = "Master";
    private const string BGM_NAME = "BGM";
    private const string SFX_NAME = "SFX";

    private const string AUDIOMIXER_PATH = "AudioMixer/AudioMixer";
    private const string AUDIOSO_PATH = "AudioSO";

    private List<AudioSO> _audioSOList; //������ ��� ���� ���� ����Ʈ
    private Dictionary<string, AudioSO> _audioDic; //�������� ��ųʸ��� ����� �ٰ���

    private AudioSource _bgmSource; //BGM �����
    private List<AudioSource> _sfxSourceList; //SFX ����� ����Ʈ (�ѹ��� ����ȿ���� ���ü��� �����ϱ� List)

    [SerializeField]
    [Header("�ʱ� SFX����� ����")]
    private int _sfxSourceCount;

    private AudioMixer _audioMixer;
    private AudioMixerGroup _bgmMixer; //bgmMixerGroup
    private AudioMixerGroup _sfxMixer; //sfxMixerGroup

    private void Awake()
    {
        _sfxSourceList = new List<AudioSource>(); //�޸� �Ҵ�
        _audioDic = new Dictionary<string, AudioSO>();

        _audioMixer = Resources.Load<AudioMixer>(AUDIOMIXER_PATH); //�ͼ� �ε����ְ�
        AudioMixerGroup[] audioMixerGroups = _audioMixer.FindMatchingGroups(MASTER_NAME); //�ͼ� �׷� �迭�� �����´�

        _bgmMixer = audioMixerGroups[1]; //0���� ������
        _sfxMixer = audioMixerGroups[2]; 

        _audioSOList = Resources.LoadAll<AudioSO>(AUDIOSO_PATH).ToList(); //���� ���� �ε�

        print(_audioSOList.Count);

        for (int i = 0; i < _audioSOList.Count; i++)
        {
            _audioDic.Add(_audioSOList[i].audioName, _audioSOList[i]); //soundName�� Ű��, SO�� ����� ���
        }
    }

    private void Start()
    {
        CreateAudioSource(AudioType.BGM); //bgm ����� ����

        for (int i = 0; i < _sfxSourceCount; i++) //sfx ����� �ʱ� ������ŭ ����
        {
            CreateAudioSource(AudioType.SFX);
        }
    }

    /// <summary>
    /// Ư�� ������ ����ϴ� �Լ�
    /// </summary>
    /// <param name="audioName">������ �̸�(SO�� audioName)</param>
    public void Play(string audioName)
    {
        if(_audioDic.TryGetValue(audioName, out AudioSO audioSO)) //���� ��ġ�ϴ� ������ �ִٸ�
        {
            if(audioSO.audioType == AudioType.BGM) //bgm�̸� ������ ���Ƴ��� ������ش�
            {
                _bgmSource.clip = audioSO.clip;
                _bgmSource.Play();
            }
            else if(audioSO.audioType == AudioType.SFX) //sfx���
            {
                AudioSource sfxSource = null;

                for (int i = 0; i < _sfxSourceList.Count; i++) //�ϴ� ������� �ƴ� �������� ã�´�
                {
                    if(!_sfxSourceList[i].isPlaying)
                    {
                        sfxSource = _sfxSourceList[i];
                        break;
                    }
                }

                if(sfxSource == null) //������ ���� �ϳ� �����
                {
                    sfxSource = CreateAudioSource(AudioType.SFX);
                }

                sfxSource.clip = audioSO.clip;
                sfxSource.Play();
            }
        }
        else
        {
            Debug.LogError("AudioName�� �������� �ʽ��ϴ�");
        }
    }

    /// <summary>
    /// ����⸦ ���� ���ִ� �Լ�
    /// </summary>
    public void Play()
    {
        _bgmSource.Play();
        for (int i = 0; i < _sfxSourceList.Count; i++)
        {
            _sfxSourceList[i].Play();
        }
    }

    /// <summary>
    /// ����⸦ ���� �Ͻ����� ���ִ� �Լ�
    /// </summary>
    public void Pause()
    {
        _bgmSource.Pause();
        for (int i = 0; i < _sfxSourceList.Count; i++)
        {
            _sfxSourceList[i].Pause();
        }
    }

    /// <summary>
    /// ����⸦ ���� ���ߴ� �Լ�
    /// </summary>
    public void Stop()
    {
        _bgmSource.Stop();
        for (int i = 0; i < _sfxSourceList.Count; i++)
        {
            _sfxSourceList[i].Stop();
        }
    }

    /// <summary>
    /// ���� ���� �Լ�
    /// </summary>
    /// <param name="audioType">������ ����</param>
    /// <param name="value">�ּ� -40, �ִ� 0�� ��</param>
    public void VolumeControl(AudioType audioType, float value)
    {
        value = Mathf.Clamp(value, -40.0f, 0.0f);

        if(audioType == AudioType.MASTER)
        {
            _audioMixer.SetFloat(MASTER_NAME, value);
        }
        else if(audioType == AudioType.BGM)
        {
            _audioMixer.SetFloat(BGM_NAME, value);
        }
        else if(audioType == AudioType.SFX)
        {
            _audioMixer.SetFloat(SFX_NAME, value);
        }
    }

    /// <summary>
    /// ����⸦ ����� �Լ�
    /// </summary>
    /// <param name="audioType">���� ����</param>
    /// <returns></returns>
    private AudioSource CreateAudioSource(AudioType audioType)
    {
        AudioSource audioSource = null;

        if (audioType == AudioType.BGM)
        {
            GameObject bgmObject = new GameObject(BGM_NAME); //BGM ������Ʈ ����
            bgmObject.transform.parent = this.transform; //�θ�� �Ŵ����� ���ְ�

            _bgmSource = bgmObject.AddComponent<AudioSource>(); //����� �ٿ��ش�
            _bgmSource.playOnAwake = false; //�������ڸ��� ����Ǵ°� �ϴ� ��
            _bgmSource.loop = true; //bgm�̴ϱ� ���� ���ִ°�

            _bgmSource.outputAudioMixerGroup = _bgmMixer; //����� bgm�ͼ���
        }
        else if (audioType == AudioType.SFX)
        {
            GameObject sfxObject = new GameObject($"{SFX_NAME} ({_sfxSourceList.Count})"); //SFX ������Ʈ ����
            sfxObject.transform.parent = this.transform; //�θ�� ���� �Ŵ���

            audioSource = sfxObject.AddComponent<AudioSource>(); //����� �ٿ��ְ�
            audioSource.playOnAwake = false; //�������ڸ��� ����� �ʿ䵵 ����

            audioSource.outputAudioMixerGroup = _sfxMixer; //����� sfx�ͼ�

            _sfxSourceList.Add(audioSource); //����Ʈ�� �߰����ش�
        }

        return audioSource; //sfx�� �ƴϸ� null ����
    }
}
