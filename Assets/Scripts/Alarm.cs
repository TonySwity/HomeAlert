using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _speedAlarm = 0.2f;
    private float _targetVolume;
    private float _minVolume = 0;
    private float _maxVolume = 1;
    private Coroutine _signal;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopCoroutineSignaled();   

        if (collision.TryGetComponent<Player>(out Player player))
        {
            _audioSource.Play();

            if (_audioSource.volume < _maxVolume)
            {
                _targetVolume = _maxVolume;
            }

            StartCoroutineSignaled();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutineSignaled();

        if (collision.TryGetComponent<Player>(out Player player))
        {

            if (_audioSource.volume > _minVolume)
            {
                _targetVolume = _minVolume;
            }

            StartCoroutineSignaled();
        }
    }

    private IEnumerator Signaled()
    {
        while(_audioSource.volume !=  _targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _speedAlarm * Time.deltaTime);

            yield return null;
        }
    }

    private void StopCoroutineSignaled()
    {
        if (_signal != null)
        {
            StopCoroutine(_signal);
            _signal = null;
        }
    }

    private void StartCoroutineSignaled()
    {
        if (_signal == null)
        {
            _signal = StartCoroutine(Signaled());
        }
    }
}
