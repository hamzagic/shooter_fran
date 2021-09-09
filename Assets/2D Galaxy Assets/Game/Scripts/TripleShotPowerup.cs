using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotPowerup : MonoBehaviour
{
    [SerializeField]
    private float _speed;


    [SerializeField]
    private int _powerupID;

    [SerializeField]
    private AudioClip _audioClip;

    void Start()
    {
        _speed = 3.0f;
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 0.5f);

            if (player != null)
            {
                if (_powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                } 
                else if (_powerupID == 1)
                {
                    player.SpeedBoostPowerupOn();
                } 
                else if (_powerupID == 2)
                {
                    player.EnableShied();
                } 
                else
                {
                    // disable all powerups
                }
                Destroy(this.gameObject);
            }
        }
    }
}
