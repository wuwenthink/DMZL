using UnityEngine;

namespace Common
{
    /// <summary>
    /// 声音管理器
    /// </summary>
    public class AudioManager:MonoSingleton<AudioManager>
    {
        AudioSource voice;
        AudioSource music;

        /// <summary>
        /// 音效强度0.0~1.0
        /// </summary>
        public float VoiceVolume
        {
            get
            {
                if ( PlayerPrefs.HasKey("VoiceVolume") ) return PlayerPrefs.GetFloat("VoiceVolume");
                else return 0.5f;
            }

            set
            {
                voice.volume = value;
                PlayerPrefs.SetFloat("VoiceVolume",value);
            }
        }

        /// <summary>
        /// 音乐强度0.0~1.0
        /// </summary>
        public float MusicVolume
        {
            get
            {
                if ( PlayerPrefs.HasKey("MusicVolume") ) return PlayerPrefs.GetFloat("MusicVolume");
                else return 0.5f;
            }
            set
            {
                music.volume = value;
                PlayerPrefs.SetFloat("MusicVolume",value);
            }
        }

        protected override void Initialize ()
        {
            voice = gameObject.AddComponent<AudioSource>();
            voice.volume = VoiceVolume;
            music = gameObject.AddComponent<AudioSource>();
            music.volume = MusicVolume;
            music.loop = true;    
        }
        
        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="audio"></param>
        public void PlayVoice(AudioClip audio) 
        {
            voice.clip = audio;
            voice.Play();
        }
        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="audio"></param>
        public void PlayMusic ( AudioClip audio )
        {
            music.clip = audio;
            music.Play();
        }
    }
}
