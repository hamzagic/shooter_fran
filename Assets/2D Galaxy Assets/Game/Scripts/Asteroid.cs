using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private UIManager _uiManager;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private AudioClip _audioClip;

    void Start()
    {
        _speed = 4.0f;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UIManager not found");
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 0.4f);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            _uiManager.UpdateScore();
        }
        else if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 0.4f);
                player.Damage();
                Destroy(this.gameObject);
            }
        }
    }
}
