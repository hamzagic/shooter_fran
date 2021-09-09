using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private float _fireRate = 0.25f;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private GameObject[] _engines;

    private UIManager _uiManager;

    private float _canFire = 0.0f;

    public bool canTripleShot;

    public bool canSpeedBoost;

    public bool hasShield;

    [SerializeField]
    private int _playerLives;

    private AudioSource _audioSource;

    private int _hitCount;

    void Start()
    {
        _speed = 7.0f;
        transform.position = new Vector3(0, 0, 0);
        canTripleShot = false;
        canSpeedBoost = false;
        hasShield = false;
        _fireRate = 0.15f;
        _playerLives = 3;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(_playerLives);
        }

        _audioSource = GetComponent<AudioSource> ();
        _hitCount = 0;

        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.ResumeGame();

    }

    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire )
        {
            _audioSource.Play();
            if (canTripleShot)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);

                //Instantiate(_laserPrefab, transform.position + new Vector3(0.65f, 0.22f, 0), Quaternion.identity);
               // Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
               // Instantiate(_laserPrefab, transform.position + new Vector3(-0.65f, 0.22f, 0), Quaternion.identity);

                _canFire = Time.time + _fireRate;
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
                _canFire = Time.time + _fireRate;

            }
        } 
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //canSpeedBoost == true ? _speed = 10.0f : _speed = 7.0f;
        if (canSpeedBoost)
        {
            _speed = 10.0f;
        } 
        else
        {
            _speed = 8.0f;
        }

        transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
        transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.10f)
        {
            transform.position = new Vector3(transform.position.x, -4.10f, 0);
        }

        if (transform.position.x < -9.6f)
        {
            transform.position = new Vector3(-9.6f, transform.position.y, 0);
        }
        else if (transform.position.x > 9.6f)
        {
            transform.position = new Vector3(9.6f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if (hasShield)
        {
            hasShield = false;
            _shieldGameObject.SetActive(false);
            return;
        }
        if (!hasShield) _hitCount++;

        if (_hitCount == 1)
        {
            _engines[0].SetActive(true);
        }
        else if (_hitCount == 2)
        {
            _engines[1].SetActive(true);
        }
       
        _playerLives--;
        _uiManager.UpdateLives(_playerLives);

        if (_playerLives < 1)
        {
           Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
           _uiManager.ShowMainMenu();
           Destroy(this.gameObject); 
        }
    }

    public void EnableShied()
    {
        hasShield = true;
        _shieldGameObject.SetActive(true);
    }

    public void SpeedBoostPowerupOn()
    {
        canSpeedBoost = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        canTripleShot = false;
    }
}
