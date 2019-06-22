using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private bool m_CanPlaySound = true;

    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_AudioClip;

    public Color m_ActiveColor;
    public Color m_DeactiveColor;
    public Text m_SoundOnButton;
    public Text m_SoundOffButton;

    private void Awake()
    {
        Instance = this;
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        m_CanPlaySound = PlayerPrefs.GetString("can_play", true.ToString()) == true.ToString() ? true : false;

        if (m_CanPlaySound)
        {
            m_SoundOnButton.color = m_ActiveColor;
            m_SoundOffButton.color = m_DeactiveColor;
        }
        else
        {
            m_SoundOnButton.color = m_DeactiveColor;
            m_SoundOffButton.color = m_ActiveColor;
        }
    }

    /// <summary>
    /// it controlls by Brick GameObjects when a collision entered!
    /// </summary>
    public void PlayAudioClip()
    {
        if(m_CanPlaySound)
            m_AudioSource.PlayOneShot(m_AudioClip);
    }
    
    public void ChangeAudioClipSetting(bool canPlaySound)
    {
        PlayerPrefs.SetString("can_play", canPlaySound.ToString());
        m_CanPlaySound = canPlaySound;

        if (m_CanPlaySound)
        {
            m_SoundOnButton.color = m_ActiveColor;
            m_SoundOffButton.color = m_DeactiveColor;
        }
        else
        {
            m_SoundOnButton.color = m_DeactiveColor;
            m_SoundOffButton.color = m_ActiveColor;
        }
    }
}