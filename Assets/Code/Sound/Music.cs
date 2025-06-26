using UnityEngine;

public class Music : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip _initialClip;  // Первый звук (один раз)
    [SerializeField] private AudioClip _loopedClip;   // Второй звук (цикл)

    [Header("Settings")]
    [Range(0f, 1f)] public float volume = 1f;

    private AudioSource _audioSource;
    private bool _isPlayingInitial;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();

        _audioSource.volume = volume;
    }

    private void Start()
    {
        PlaySequence();
    }

    public void PlaySequence()
    {
        if (_initialClip == null || _loopedClip == null)
        {
            Debug.LogError("Audio clips not assigned!");
            return;
        }

        _audioSource.clip = _initialClip;
        _audioSource.loop = false;
        _audioSource.Play();

        _isPlayingInitial = true;
    }

    private void Update()
    {
        if (_isPlayingInitial && !_audioSource.isPlaying)
        {
            _audioSource.clip = _loopedClip;
            _audioSource.loop = true;
            _audioSource.Play();

            _isPlayingInitial = false;
        }
    }

    public void StopPlayback()
    {
        _audioSource.Stop();
        _isPlayingInitial = false;
    }

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        _audioSource.volume = volume;
    }
}