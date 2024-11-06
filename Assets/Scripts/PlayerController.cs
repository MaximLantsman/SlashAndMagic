using System.Collections.Generic;
using Entities;
using Entities.SpawnSystem;
using KBCore.Refs;
using UnityEngine;
using Utilities;
using WeaponSystem;

public class PlayerController : ValidatedMonoBehaviour
{
    [Header("References")]
    [SerializeField,Self]private Rigidbody rb;
    [SerializeField,Self]private Animator animator;
    [SerializeField,Anywhere]private InputReader input;
    
    [Header("Movement Settings")]
    [SerializeField]private float moveSpeed = 100f;
    [SerializeField] private float rotationSpeed = 10f;
    
    [Header("Dash Settings")]
    [SerializeField]private float dashForce = 10f;
    [SerializeField]private float dashDuration = 0.5f;
    [SerializeField]private float dashCooldown = 0f;

    [Header("Weapon Settings")] 
    [SerializeField]private Weapon currentWeapon;
    [SerializeField]private GameObject weaponParent;
    [SerializeField]private WeaponSpawnManager weaponSpawner;
    
    private Vector3 _movement;
    private float _dashVelocity=1f;
    
    private List<Timer> timers;
    private CountdownTimer dashTimer;
    private CountdownTimer dashCooldownTimer;

    private CountdownTimer attackTimer;
    private CountdownTimer attackCooldownTimer;

    private const float oneF = 1f;
    
    private readonly int _runningAnimation = Animator.StringToHash("IsRunning");
    
    private void Awake()
    {
        //WeaponSummon();
        //weaponSpawner.Spawn()
        //WeaponEquip(new );
        SetupTimers();    
    }

    private void SetupTimers()
    {
        //Setup Timer
        dashTimer = new CountdownTimer(dashDuration);
        dashCooldownTimer = new CountdownTimer(dashCooldown);

        if (currentWeapon != null)
        {
            attackTimer = new CountdownTimer(currentWeapon._weaponData.attackDuration);
            attackCooldownTimer = new CountdownTimer(currentWeapon._weaponData.attackCooldown);
        }
        else
        {
            attackTimer = new CountdownTimer(1); //NOT GOOD AT ALL
            attackCooldownTimer = new CountdownTimer(1);
        }
        
        timers = new List<Timer>(4) { dashTimer, dashCooldownTimer, attackCooldownTimer, attackTimer };
        
        dashTimer.OnTimerStop += () => dashCooldownTimer.Start();
    }
    
    private void Start() => input.EnablePlayerActions();

    private void OnEnable()
    {
        input.Attack += OnAttack;
        input.Dash += OnDash;
    }

    private void OnDisable()
    {
        input.Attack -= OnAttack;
        input.Dash -= OnDash;
    }

    private void OnAttack(bool preformed)
    {
        if (preformed && !attackCooldownTimer.IsRunning && currentWeapon != null)
        {
            attackCooldownTimer.Start();
            attackTimer.Start();
            
            HandleAttack();
        }
        
    }
    
    private void OnDash(bool preformed)
    {
        if (preformed && !dashTimer.IsRunning && !dashCooldownTimer.IsRunning)
        {
            dashTimer.Start();
        }else if (!preformed && dashTimer.IsRunning)
        {
            dashTimer.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleTimers();
        //Problem with getting stuck - too long
        if (!attackTimer.IsRunning)
        {
            _movement = new Vector3(input.Direction.x, 0, input.Direction.y);
            UpdateAnimator();
        }
        else
        {
            _movement = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
       
        HandleMovement();
        HandleDash();
            
    }
    
    private void UpdateAnimator()
    {
        animator.SetBool(_runningAnimation,_movement != Vector3.zero);
    }

    private void HandleTimers()
    {
        foreach (Timer timer in timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }

    private void HandleDash()
    {
        if (!dashTimer.IsRunning)
        {
            _dashVelocity = oneF;
            dashTimer.Stop();
            return;
        }

        if (dashTimer.IsRunning)
        {
            float launchpoint = 0.9f;
            if (dashTimer.Progress > launchpoint)
            {
                _dashVelocity = dashForce;
            }
            else
            {
                _dashVelocity += (1- dashTimer.Progress) * dashForce * Time.deltaTime;
            }
        }
    }
    
    private void HandleAttack()
    {
        animator.SetTrigger(currentWeapon._animationName);
        
        currentWeapon.Attack();
    }

    private void HandleMovement()
    {
        //Move the Player
        Vector3 velocity = _movement * (_dashVelocity * (moveSpeed * Time.fixedDeltaTime));
        
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);

        if (_movement != Vector3.zero)
        {
            HandleRotation(_movement);
        }
        
    }

    private void HandleRotation(Vector3 moveDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

    }
    
    public void WeaponEquip(Weapon equippedWeapon)
    {
        if (currentWeapon != null)
            DropWeapon(currentWeapon);
        
        currentWeapon = equippedWeapon;
        attackTimer = new CountdownTimer(equippedWeapon._weaponData.attackDuration);
        attackCooldownTimer = new CountdownTimer(equippedWeapon._weaponData.attackCooldown);
        timers[2] = attackTimer; //Hardcoded bad - FIXXXX
        timers[3] = attackCooldownTimer; //Hardcoded bad - FIXXXX

        //Create weapon, temp
        equippedWeapon.gameObject.transform.SetParent(weaponParent.transform);
        equippedWeapon.transform.localPosition = Vector3.zero;
        equippedWeapon.transform.localRotation = Quaternion.identity;
        currentWeapon.weaponInstance = equippedWeapon.gameObject.GetComponent<WeaponHitboxHandler>(); //not good!
    }

    private void DropWeapon(Weapon weaponDrop)
    {
        /*weaponDrop.gameObject.transform.SetParent(null); //Add a parent for dropped weapons
        weaponDrop.transform.localRotation = Quaternion.identity;
        weaponDrop.GetComponent<Collider>().enabled = true;*/
        
        Destroy(weaponDrop.gameObject);
    }
}