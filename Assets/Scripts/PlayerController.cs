using System.Collections.Generic;
using Entities;
using Entities.SpawnSystem;
using KBCore.Refs;
using StateMechine;
using UnityEngine;
using Utilities;

public class PlayerController : ValidatedMonoBehaviour
{
    [Header("References")]
    [SerializeField,Self]private Rigidbody rb;
    [SerializeField,Self]private Animator animator;
    [SerializeField,Self]public Collider collider;
    [SerializeField, Self] public Health.Health health;
    [SerializeField,Anywhere]private InputReader input;
    
    [Header("Movement Settings")]
    [SerializeField]private float moveSpeed = 100f;
    [SerializeField]private float rotationSpeed = 10f;
    [SerializeField]private float smoothTime = 0.2f;
    
    [Header("Dash Settings")]
    [SerializeField]private float dashForce = 10f;
    [SerializeField]private float dashDuration = 0.5f;
    [SerializeField]private float dashCooldown = 0f;

    [Header("Weapon Settings")] 
    [SerializeField]private Weapon currentWeapon;
    [SerializeField]private GameObject weaponParent;
    [SerializeField]private WeaponSpawnManager weaponSpawner;
    
    [Header("Death Settings")]
    [SerializeField]private float hitStunDuration = 1f;
    
    private Vector3 _movement;
    private float _dashVelocity=1f;
    private float currentSpeed;
    private float velocity;
    
    private List<Timer> timers;
    private CountdownTimer dashTimer;
    private CountdownTimer dashCooldownTimer;

    private Dictionary<Collider, Weapon> weaponDictionary = new Dictionary<Collider, Weapon>();
    [SerializeField]private Weapon[] weaponsInGame;
    
    private CountdownTimer attackTimer;
    private CountdownTimer attackCooldownTimer;
    
    private CountdownTimer HitStunTimer;

    private StateMachine stateMachine;
    
    private const float ZeroF = 0f;
    private const float oneF = 1f;
    
    private int pickupLayer;
    
    private BaseState attackState;
    
    private readonly int _runningAnimation = Animator.StringToHash("Speed");
    private readonly string pickupLayerName = "PickUp";

    [SerializeField]private bool isDead;

    private void Awake()
    {
       
        health.OnHit.AddListener(StartHitStunTimer);
       
        SetupTimers();
        BuiltWeaponDictionary();
        
        pickupLayer = LayerMask.NameToLayer(pickupLayerName);
        
        //State Machine
        stateMachine = new StateMachine();
        
        //Declare states
        BaseState LocomotionState = new LocomotionState(this,animator);
        BaseState DashState = new DashState(this,animator);
        BaseState HitStunState = new HitStunState(this,animator);
        BaseState DeathState = new DeathStunState(this,animator);

        attackState = new AttackState(this,animator);
        

        At(LocomotionState, DashState, new FuncPredicate(() => dashTimer.IsRunning));
        
        At(LocomotionState, attackState, new FuncPredicate(() => attackTimer.IsRunning));
        
        Any(LocomotionState, new FuncPredicate(()=> !dashTimer.IsRunning && !attackTimer.IsRunning && !HitStunTimer.IsRunning && !isDead));

        Any(HitStunState, new FuncPredicate(()=> HitStunTimer.IsRunning && !isDead));
        
        Any(DeathState, new FuncPredicate(()=> isDead));
        
        stateMachine.SetState(LocomotionState);
    }

    private void BuiltWeaponDictionary()
    {
        foreach (Weapon weapon in weaponsInGame)
        {
            weaponDictionary.Add(weapon.weaponCollider, weapon);
        }
    }

    private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    private void SetupTimers()
    {
        //Setup Timer
        dashTimer = new CountdownTimer(dashDuration);
        dashCooldownTimer = new CountdownTimer(dashCooldown);

        attackTimer = new CountdownTimer(currentWeapon._weaponData.attackDuration);
        attackCooldownTimer = new CountdownTimer(currentWeapon._weaponData.attackCooldown);
        
        HitStunTimer = new CountdownTimer(hitStunDuration);
        
        timers = new List<Timer>(5) { dashTimer, dashCooldownTimer, attackCooldownTimer, attackTimer, HitStunTimer};
        
        dashTimer.OnTimerStop += () =>
        {
            dashCooldownTimer.Start();
            _dashVelocity = oneF;
        };

        
    }
    
    private void Start() => input.EnablePlayerActions();
    
    private void StartHitStunTimer()
    {
        if (health.IsDead)
        {
            isDead = true;
            return;
        }
            
        HitStunTimer.Start();
    } 
    
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
        }/*else if ( dashTimer.IsRunning)
        {
            dashTimer.Stop();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        _movement = new Vector3(input.Direction.x, 0, input.Direction.y);

        
        HandleTimers();
        UpdateAnimator();
        //Problem with getting stuck - too long
        /*if (!attackTimer.IsRunning)
        {
            _movement = new Vector3(input.Direction.x, 0, input.Direction.y);
            UpdateAnimator();
        }
        else
        {
            _movement = Vector3.zero;
        }*/
        
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        //HandleMovement();
        //HandleDash();
        
        stateMachine.FixedUpdate();
    }
    
    private void UpdateAnimator()
    {
        animator.SetFloat(_runningAnimation,currentSpeed);
    }

    private void HandleTimers()
    {
        foreach (Timer timer in timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }

    public void HandleDash()
    {
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
    
    public void HandleAttack()
    {
        //animator.SetTrigger(currentWeapon._animationName);
        
        currentWeapon.Attack();
    }

    public void HandleMovement()
    {
        //Move the Player
        Vector3 adjustedSpeed = _movement * (_dashVelocity * (moveSpeed * Time.fixedDeltaTime));
        if (adjustedSpeed.magnitude > ZeroF)
        { 
            rb.linearVelocity = new Vector3(adjustedSpeed.x, rb.linearVelocity.y, adjustedSpeed.z);
            
            HandleRotation(_movement);
            SmoothSpeed(_movement.magnitude);
        }
        else
        {
            SmoothSpeed(ZeroF);
        }
        
    }

    private void SmoothSpeed(float value)
    {
        currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
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
        attackState.SwitchAttackAnim(Animator.StringToHash(equippedWeapon._animationName));
       
        attackTimer = new CountdownTimer(equippedWeapon._weaponData.attackDuration);
        attackCooldownTimer = new CountdownTimer(equippedWeapon._weaponData.attackCooldown);
        timers[2] = attackTimer; //Hardcoded bad - FIXXXX
        timers[3] = attackCooldownTimer; //Hardcoded bad - FIXXXX
        
        
        //Create weapon, temp
        equippedWeapon.gameObject.transform.SetParent(weaponParent.transform);
        equippedWeapon.transform.localPosition = Vector3.zero;
        equippedWeapon.transform.localRotation = Quaternion.identity;
        //currentWeapon.weaponInstance = equippedWeapon.gameObject.GetComponent<WeaponHitboxHandler>(); //not good!
    }

    private void DropWeapon(Weapon weaponDrop)
    {
        /*weaponDrop.gameObject.transform.SetParent(null); //Add a parent for dropped weapons
        weaponDrop.transform.localRotation = Quaternion.identity;
        weaponDrop.GetComponent<Collider>().enabled = true;*/
        
        Destroy(weaponDrop.gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (pickupLayer == other.gameObject.layer)
        {
            WeaponEquip(weaponDictionary[other]);
        }
        
    }
}