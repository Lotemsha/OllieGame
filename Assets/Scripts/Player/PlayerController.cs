using UnityEngine;
using UnityEngine.InputSystem;
using CoreClasses.Models;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private PlayerManager _playerLogic;
    private Vector2 _moveDirection;
    private Rigidbody2D rb; 
    private Animator _animator;

    public bool canMove = true;
    public int TotalXP;

    [Header("State")]
    public bool isInside = false;

    [Header("Movement Settings")]
    [Tooltip("מהירות הבסיס ביוניטי - משפיעה על התחושה הפיזית")]
    public float baseSpeed = 2f;
    [Tooltip("מהירות מינימלית כדי שאולי לא תיתקע במקום")]
    public float minAllowedSpeed = 0.8f;
    
    public AnxietySettings anxietySettings;

    // משתנה עזר לשמירת המהירות שחושבה ב-Update לשימוש ב-FixedUpdate
    private float _finalCalculatedSpeed;

    void Start()
    {
        // התחברות לשחקן הקיים ב-DLL או יצירת חדש
        if (GameController.Instance != null && GameController.Instance.gameManager.Player != null)
        {
            _playerLogic = GameController.Instance.gameManager.Player;
            Debug.Log("Connected to Ollie from DLL! HP: " + _playerLogic.Health);
        }
        else
        {
            _playerLogic = new PlayerManager("Ollie");
            if (GameController.Instance != null) GameController.Instance.gameManager.Player = _playerLogic;
        }

        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        _playerLogic.SpeedLimit(minAllowedSpeed, 20f);

        Invoke("RandomBlink", Random.Range(2f, 6f));
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        _moveDirection = value.Get<Vector2>();
    }

    void Update()
    {
        if (_playerLogic == null) return;

        // קבלת המכפיל המאוחד מה-DLL (שכולל חרדה, בריאות וסביבה)
        float dllMultiplier = _playerLogic.SpeedEffectMultiplier;
        
        // חישוב המהירות הסופית ביחס ל-baseSpeed של יוניטי
        _finalCalculatedSpeed = Mathf.Max(baseSpeed * dllMultiplier, minAllowedSpeed);

        UpdateMovementAnimations();

        _playerLogic.ApplyEnvironmentEffect(_playerLogic.CurrentEnvironment, Time.deltaTime);

        if (!_playerLogic.IsAlive && canMove)
        {
            canMove = false;
            rb.linearVelocity = Vector2.zero;
        }
    }
   
    void FixedUpdate()
    {
        if (canMove && _moveDirection != Vector2.zero)
        {
            rb.linearVelocity = _moveDirection * _finalCalculatedSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
   
    public void AddXP(int amount)
    {
        TotalXP += amount;
        GameController.Instance.player.GainXP(amount);
    }
    private void UpdateMovementAnimations()
    {
        if (!canMove) return;
        float speedMagnitude = _moveDirection.magnitude;
        _animator.SetFloat("Speed", speedMagnitude);

        if (speedMagnitude > 0f)
        {
            _animator.SetFloat("moveX", _moveDirection.x);
            _animator.SetFloat("moveY", _moveDirection.y);
        }
    }

    void RandomBlink()
    {
        _animator.SetTrigger("doBlink");
        Invoke("RandomBlink", Random.Range(3f, 8f));
    }
}