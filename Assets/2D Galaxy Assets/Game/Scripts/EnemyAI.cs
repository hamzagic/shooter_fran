using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _audioClip;

    void Start()
    {
        _speed = 5.0f;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6.10f)
        {
            float randomX = Random.Range(-10.0f, 10.0f);
            transform.position = new Vector3(randomX, 8.0f, 0);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 0.4f);
            Destroy(this.gameObject);
        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 0.4f);
            Destroy(this.gameObject);
            _uiManager.UpdateScore();
        }
    }
}
