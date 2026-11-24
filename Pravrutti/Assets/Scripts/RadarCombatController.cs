using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RadarCombatController : MonoBehaviour
{
    public RandomEnemySpawner enemySpawner;
    public GameObject activeEnemy;

    [Header("Radar References")]
    public Transform radarPointer;
    public Transform radarBlips;

    [Header("Dot Prefabs")]
    public GameObject weakPointPrefab;
    public GameObject attackPointPrefab;

    [Header("Rotation")]
    public float rotationSpeed = 180f;
    public float pointerAngle = 0f;

    [Header("Radar Size")]
    public float radarRadius = 0.85f;

    [Header("Click Accuracy")]
    public float hitTolerance = 8f;

    [Header("Weak Point Settings")]
    public float weakPointInterval = 3f;
    public float weakPointLifetime = 8f;
    public int weakPointDamage = 10;

    [Header("Incoming Attack Settings")]
    public float attackInterval = 5f;
    public float attackTravelTime = 6f;
    public int attackDamage = 10;

    [Header("Combat References")]
    public EnemyCombatHealth enemy;
    public PlayerCombatHealth player;

    private class RadarDot
    {
        public GameObject gameObject;
        public float angle;
        public float radius;
        public float lifetime;
        public float travelTime;
        public bool isAttack;
    }

    private List<RadarDot> activeDots =
        new List<RadarDot>();

    private float weakPointTimer;
    private float attackTimer;

    public bool combatActive = false;

    public void SetEnemy(RandomEnemySpawner enemy, GameObject _activeEnemy)
    {
        enemySpawner = enemy;
        activeEnemy = _activeEnemy;
    }

    void Update()
    {
        if (!combatActive)
            return;

        RotatePointer();
        SpawnTimers();
        UpdateDots();

        if (clickAction.WasPressedThisFrame())
        {
            TryHitDot();
        }
    }

    void RotatePointer()
    {
        pointerAngle += rotationSpeed * Time.deltaTime;
        pointerAngle = Mathf.Repeat(pointerAngle, 360f);

        radarPointer.localRotation =
            Quaternion.Euler(0f, 0f, -pointerAngle);
    }

    void SpawnTimers()
    {
        weakPointTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;

        if (weakPointTimer >= weakPointInterval)
        {
            SpawnDot(false);
            weakPointTimer = 0f;
        }

        if (attackTimer >= attackInterval)
        {
            SpawnDot(true);
            attackTimer = 0f;
        }
    }

    void SpawnDot(bool isAttack)
    {
        GameObject prefab = isAttack
            ? attackPointPrefab
            : weakPointPrefab;

        float angle = Random.Range(0f, 360f);

        float radius = isAttack
            ? radarRadius
            : Random.Range(radarRadius * 0.45f,
                           radarRadius * 0.9f);

        GameObject newDot = Instantiate(
            prefab,
            radarBlips
        );

        RadarDot dot = new RadarDot();

        dot.gameObject = newDot;
        dot.angle = angle;
        dot.radius = radius;
        dot.lifetime = weakPointLifetime;
        dot.travelTime = attackTravelTime;
        dot.isAttack = isAttack;

        activeDots.Add(dot);

        UpdateDotPosition(dot);
    }

    void UpdateDots()
    {
        for (int i = activeDots.Count - 1; i >= 0; i--)
        {
            RadarDot dot = activeDots[i];

            if (dot.gameObject == null)
            {
                activeDots.RemoveAt(i);
                continue;
            }

            if (dot.isAttack)
            {
                dot.travelTime -= Time.deltaTime;

                float progress = Mathf.Clamp01(
                    1f - dot.travelTime / attackTravelTime
                );

                dot.radius = Mathf.Lerp(
                    radarRadius,
                    0f,
                    progress
                );

                UpdateDotPosition(dot);

                if (dot.travelTime <= 0f)
                {
                    player.TakeDamage(attackDamage);
                    RemoveDot(i);
                }
            }
            else
            {
                dot.lifetime -= Time.deltaTime;

                if (dot.lifetime <= 0f)
                {
                    RemoveDot(i);
                }
            }
        }
    }

    void UpdateDotPosition(RadarDot dot)
    {
        float radians = dot.angle * Mathf.Deg2Rad;

        float x = Mathf.Sin(radians) * dot.radius;
        float y = Mathf.Cos(radians) * dot.radius;

        dot.gameObject.transform.localPosition =
            new Vector3(x, y, 0f);
    }

    void TryHitDot()
    {
        RadarDot bestDot = null;
        float smallestAngle = hitTolerance;

        foreach (RadarDot dot in activeDots)
        {
            float difference = Mathf.Abs(
                Mathf.DeltaAngle(pointerAngle, dot.angle)
            );

            if (difference <= smallestAngle)
            {
                smallestAngle = difference;
                bestDot = dot;
            }
        }

        if (bestDot == null)
        {
            Debug.Log("Miss!");
            return;
        }

        if (bestDot.isAttack)
        {
            Debug.Log("Attack intercepted!");
        }
        else
        {
            Debug.Log("Weak point hit!");

            enemy.TakeDamage(weakPointDamage);
        }

        int index = activeDots.IndexOf(bestDot);
        RemoveDot(index);
    }

    void RemoveDot(int index)
    {
        if (index < 0 || index >= activeDots.Count)
            return;

        if (activeDots[index].gameObject != null)
        {
            Destroy(activeDots[index].gameObject);
        }

        activeDots.RemoveAt(index);
    }

    public void StartCombat()
    {
        combatActive = true;

        weakPointTimer = 0f;
        attackTimer = 0f;
    }

    public void StopCombat()
    {
        combatActive = false;

        foreach (RadarDot dot in activeDots)
        {
            if (dot.gameObject != null)
            {
                Destroy(dot.gameObject);
            }
        }

        activeDots.Clear();
    }

    void Start()
    {
        StartCombat();
    }

    private InputAction clickAction;

    void Awake()
    {
        clickAction = new InputAction(
            name: "RadarClick",
            type: InputActionType.Button,
            binding: "<Mouse>/leftButton"
        );
    }

    void OnEnable()
    {
        clickAction.Enable();
    }

    void OnDisable()
    {
        clickAction.Disable();
    }
}