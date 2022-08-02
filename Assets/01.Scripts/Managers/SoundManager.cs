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

    private List<AudioSO> _audioSOList; //관리할 모든 음원 정보 리스트
    private Dictionary<string, AudioSO> _audioDic; //음원별로 딕셔너리에 등록해 줄거임

    private AudioSource _bgmSource; //BGM 재생기
    private List<AudioSource> _sfxSourceList; //SFX 재생기 리스트 (한번에 여러효과음 나올수도 있으니까 List)

    [SerializeField]
    [Header("초기 SFX재생기 갯수")]
    private int _sfxSourceCount;

    private AudioMixer _audioMixer;
    private AudioMixerGroup _bgmMixer; //bgmMixerGroup
    private AudioMixerGroup _sfxMixer; //sfxMixerGroup

    private void Awake()
    {
        _sfxSourceList = new List<AudioSource>(); //메모리 할당
        _audioDic = new Dictionary<string, AudioSO>();

        _audioMixer = Resources.Load<AudioMixer>(AUDIOMIXER_PATH); //믹서 로드해주고
        AudioMixerGroup[] audioMixerGroups = _audioMixer.FindMatchingGroups(MASTER_NAME); //믹서 그룹 배열로 가져온다

        _bgmMixer = audioMixerGroups[1]; //0번은 마스터
        _sfxMixer = audioMixerGroups[2]; 

        _audioSOList = Resources.LoadAll<AudioSO>(AUDIOSO_PATH).ToList(); //음원 에셋 로드

        print(_audioSOList.Count);

        for (int i = 0; i < _audioSOList.Count; i++)
        {
            _audioDic.Add(_audioSOList[i].audioName, _audioSOList[i]); //soundName을 키로, SO를 밸류로 등록
        }
    }

    private void Start()
    {
        CreateAudioSource(AudioType.BGM); //bgm 재생기 생성

        for (int i = 0; i < _sfxSourceCount; i++) //sfx 재생기 초기 갯수만큼 생성
        {
            CreateAudioSource(AudioType.SFX);
        }
    }

    /// <summary>
    /// 특정 음원을 재생하는 함수
    /// </summary>
    /// <param name="audioName">음원의 이름(SO의 audioName)</param>
    public void Play(string audioName)
    {
        if(_audioDic.TryGetValue(audioName, out AudioSO audioSO)) //만약 일치하는 음원이 있다면
        {
            if(audioSO.audioType == AudioType.BGM) //bgm이면 음원을 갈아끼고 재생해준다
            {
                _bgmSource.clip = audioSO.clip;
                _bgmSource.Play();
            }
            else if(audioSO.audioType == AudioType.SFX) //sfx라면
            {
                AudioSource sfxSource = null;

                for (int i = 0; i < _sfxSourceList.Count; i++) //일단 재생중이 아닌 재생기부터 찾는다
                {
                    if(!_sfxSourceList[i].isPlaying)
                    {
                        sfxSource = _sfxSourceList[i];
                        break;
                    }
                }

                if(sfxSource == null) //없으면 새로 하나 만든다
                {
                    sfxSource = CreateAudioSource(AudioType.SFX);
                }

                sfxSource.clip = audioSO.clip;
                sfxSource.Play();
            }
        }
        else
        {
            Debug.LogError("AudioName이 존재하지 않습니다");
        }
    }

    /// <summary>
    /// 재생기를 전부 켜주는 함수
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
    /// 재생기를 전부 일시정지 해주는 함수
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
    /// 재생기를 전부 멈추는 함수
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
    /// 볼륨 조절 함수
    /// </summary>
    /// <param name="audioType">음원의 종류</param>
    /// <param name="value">최소 -40, 최대 0의 값</param>
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
    /// 재생기를 만드는 함수
    /// </summary>
    /// <param name="audioType">음원 종류</param>
    /// <returns></returns>
    private AudioSource CreateAudioSource(AudioType audioType)
    {
        AudioSource audioSource = null;

        if (audioType == AudioType.BGM)
        {
            GameObject bgmObject = new GameObject(BGM_NAME); //BGM 오브젝트 생성
            bgmObject.transform.parent = this.transform; //부모는 매니저로 해주고

            _bgmSource = bgmObject.AddComponent<AudioSource>(); //재생기 붙여준다
            _bgmSource.playOnAwake = false; //시작하자마자 재생되는건 일단 꺼
            _bgmSource.loop = true; //bgm이니까 루프 켜주는거

            _bgmSource.outputAudioMixerGroup = _bgmMixer; //출력은 bgm믹서로
        }
        else if (audioType == AudioType.SFX)
        {
            GameObject sfxObject = new GameObject($"{SFX_NAME} ({_sfxSourceList.Count})"); //SFX 오브젝트 생성
            sfxObject.transform.parent = this.transform; //부모는 사운드 매니저

            audioSource = sfxObject.AddComponent<AudioSource>(); //재생기 붙여주고
            audioSource.playOnAwake = false; //시작하자마자 재생될 필요도 없다

            audioSource.outputAudioMixerGroup = _sfxMixer; //출력은 sfx믹서

            _sfxSourceList.Add(audioSource); //리스트에 추가해준다
        }

        return audioSource; //sfx가 아니면 null 리턴
    }
}
