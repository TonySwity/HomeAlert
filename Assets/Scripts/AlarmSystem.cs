using UnityEngine;
using System.Collections;


public class AlarmSystem : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _speedAlarm = 0.2f;
    private float _targetVolume;
    private float _minVolume = 0;
    private float _maxVolume = 1;
    private Coroutine _signal;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopCoroutine();   

        if (collision.TryGetComponent<Player>(out Player player))
        {
            _audioSource.Play();

            if (_audioSource.volume < _maxVolume)
            {
                _targetVolume = _maxVolume;
            }
            
            StartCoroutine(_targetVolume);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutine();

        if (collision.TryGetComponent<Player>(out Player player))
        {

            if (_audioSource.volume > _minVolume)
            {
                _targetVolume = _minVolume;
            }
            
            StartCoroutine(_targetVolume);
        }
    }

    private IEnumerator Signaled( float target)
    {
        while(_audioSource.volume !=  target)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, _speedAlarm * Time.deltaTime);

            yield return null;
        }
    }

    private void StopCoroutine()
    {
        if (_signal != null)
        {
            StopCoroutine(_signal);
            _signal = null;
        }
    }

    private void StartCoroutine(float target)
    {
        if (_signal == null)
        {
            _signal = StartCoroutine(Signaled(target));
        }
    }
}
