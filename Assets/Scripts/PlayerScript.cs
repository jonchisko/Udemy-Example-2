using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region Private variables
    // speed movement
    [SerializeField]
    private float _movementSpeed = 5.0f;
    // jump height
    [SerializeField]
    private float _jumpHeight = 5.0f;
    // gravity scaler
    [SerializeField]
    private float _gravity = 1.5f;
    [SerializeField]
    private float _finalSpeed = -20.0f;
    private float _cachedYspeed = -0.1f;
    // character controller
    private CharacterController _controller;
    [SerializeField]
    private float _minY = -30f;
    // double jump
    private bool _canDoubleJump = true;

    [SerializeField]
    private int _livesAmount = 3;
    private int _coinAmount = 0;

    public delegate void CoinAmountChanged(int amount);
    public delegate void PlayerEvent();
    public static CoinAmountChanged coinAmountEvent;
    public static CoinAmountChanged lifeAmountEvent;
    public static PlayerEvent respawnEvent;
    #endregion


    #region Unity callbacks
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if(_controller == null)
        {
            Debug.LogError("PlayerScript::Start() -> _controller is null.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        ComputeMovement();
        FallToDeath();
    }

    #endregion



    #region Player Logic - private methods

    void ComputeMovement()
    {
        // gravity
        Vector3 verticalMovement = Vector3.zero;
        if (!_controller.isGrounded)
        {
            // if not grounded, we are increasing our falling 
            _cachedYspeed -= _gravity * Time.deltaTime;
            if (_cachedYspeed <= _finalSpeed) _cachedYspeed = _finalSpeed;
            if (_canDoubleJump && Input.GetKeyDown(KeyCode.Space))
            {
                _cachedYspeed = _jumpHeight;
                _canDoubleJump = false;
            }
        }
        else
        {
            // reset final speed
            _cachedYspeed = -0.1f;
            _canDoubleJump = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _cachedYspeed = _jumpHeight;
            }
        }
        verticalMovement.y = _cachedYspeed;
        // horizontal movement by the player
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 moveDirection = horizontalInput * Vector3.right * _movementSpeed;

        // combine both x and y and call move
        _controller.Move((moveDirection + verticalMovement) * Time.deltaTime);
    }

    #endregion


    #region Coin Methods

    public void IncreaseCoinAmount(int amount)
    {
        _coinAmount += amount;
        coinAmountEvent?.Invoke(_coinAmount);
    }

    #endregion


    #region Life Methods

    void FallToDeath()
    {
        // can be called more than once before the character is respawned
        if(transform.position.y <= _minY)
        {
            Debug.Log("Fallen to death.");
            TakeALife();
        }
    }


    public void TakeALife()
    {
        _livesAmount--;
        lifeAmountEvent?.Invoke(_livesAmount);
        respawnEvent?.Invoke();
    }

    public int GetCoins()
    {
        return _coinAmount;
    }

    public int GetLives()
    {
        return _livesAmount;
    }

    #endregion

}
