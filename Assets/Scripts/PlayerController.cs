using UnityEngine;
using UnityEngine.InputSystem;
using CoreClasses.Models;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private PlayerManager _playerLogic;
    private Vector2 _moveDirection;
    private Animator _animator;
    public bool canMove = true;

    [Header("State")]
    public bool isInside = false; // האם אולי בתוך הבית?

    [SerializeField] private float _speedMultiplier = 0.5f;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private float _drainRate = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerLogic = new PlayerManager("Ollie");
        Debug.Log("Character initialized: " + _playerLogic.Name + " with " + _playerLogic.Health + " HP");
        _animator = GetComponent<Animator>();
        
        // Blinking animation
        Invoke("RandomBlink", Random.Range(2f, 6f));

        if (_healthSlider != null)
        {
            _healthSlider.maxValue = _playerLogic.Health;
            _healthSlider.value = _playerLogic.Health;
        }
    }

    public void OnMove(InputValue value)
    {
        _moveDirection = value.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            // תנועה ואנימציות
            Vector3 movement = new Vector3(_moveDirection.x, _moveDirection.y, 0);
            transform.position += movement * (_playerLogic.Speed * _speedMultiplier) * Time.deltaTime;
            float currentSpeed = _moveDirection.magnitude;
            _animator.SetFloat("Speed", currentSpeed);

            if (currentSpeed > 0f)
            {
                _animator.SetFloat("moveX", _moveDirection.x);
                _animator.SetFloat("moveY", _moveDirection.y);
            }
        }

        //   בר החיים 
        if (_playerLogic != null && _playerLogic.Health > 0f && !isInside)
        {
            // אם היא בחוץ - ודאי שהבר מוצג
            if (_healthSlider != null && !_healthSlider.gameObject.activeSelf)
                _healthSlider.gameObject.SetActive(true);

            float damage = _drainRate * Time.deltaTime;
            _playerLogic.Health -= damage;

            if (_healthSlider != null)
            {
                _healthSlider.value = _playerLogic.Health;
            }
        }
        else if (isInside) // אם היא בפנים
        {
            // אם הבר עדיין דולק - כבי אותו
            if (_healthSlider != null && _healthSlider.gameObject.activeSelf)
                _healthSlider.gameObject.SetActive(false);
        }
        else if (_playerLogic.Health <= 0f)
        {
            Debug.Log("Ollie is exhausted!");
        }
    }
    void RandomBlink()
    {
        _animator.SetTrigger("doBlink");

        // קורא לעצמו שוב כדי ליצור לופ אינסופי של זמנים משתנים
        Invoke("RandomBlink", Random.Range(3f, 8f));
    }

    public void ForceLookDirection(float x, float y)
    {
        if (_animator != null)
        {
            _animator.SetFloat("moveX", x);
            _animator.SetFloat("moveY", y);
        }
    }
}
