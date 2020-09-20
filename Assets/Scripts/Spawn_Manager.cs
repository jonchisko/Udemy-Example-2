using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject psPrefab;
    private GameObject _player;

    private void Awake()
    {
        if (psPrefab == null) Debug.LogError("Spawn_Manager::Start() -> ps is null.");
        if (spawnPoint == null) Debug.LogError("Spawn_Manager::Start() -> spawnPoint is null.");
        _player = Instantiate(psPrefab, spawnPoint.transform.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PlayerScript.respawnEvent += RespawnPlayer;
    }

    private void OnDisable()
    {
        PlayerScript.respawnEvent -= RespawnPlayer;
    }

    void RespawnPlayer()
    {
        _player.SetActive(false);
        _player.transform.position = spawnPoint.transform.position;
        if(_player.GetComponent<PlayerScript>().GetLives() > 0) _player.SetActive(true);
    }

}
