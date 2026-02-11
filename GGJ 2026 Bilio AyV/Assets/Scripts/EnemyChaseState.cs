using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class EnemyChaseState : MonoBehaviour, IState
{
    public Transform playerTarget; //sometimes the enemy will just inmolate themselves going directly at the player
    public Transform maskOnFloorTarget;
    public float tryAttackCooldown;
    
    [Header("Event Listeners")]
    public GameEvent MaskNewOwner;
    public GameEvent MaskDropped;
    public GameEvent NewThrowableAppeared;

    private NavMeshAgent navAgent;
    private Animator thisAnim;
    private MaskHolder thisMaskHolder;
    private EnemyStateManager stateManager;
    private PointerRotator pointerRotator;
    private ThrowableHolder thisThrowableHolder;
    private Transform currentTarget;
    private float tryAttackTimer;

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        thisMaskHolder = GetComponent<MaskHolder>();
        thisThrowableHolder = GetComponentInChildren<ThrowableHolder>();
        pointerRotator = GetComponentInChildren<PointerRotator>();
        thisAnim = GetComponent<Animator>();
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
        SetInitialTarget();
    }

    void OnEnable()
    {
        MaskNewOwner.OnGameEvent += ChangeTarget;
        MaskDropped.OnGameEvent +=  SetTargetToMaskPickUp;
        NewThrowableAppeared.OnGameEvent += ChangeTarget;
    }

    void OnDisable()
    {
        MaskNewOwner.OnGameEvent -= ChangeTarget;
        MaskDropped.OnGameEvent -=  SetTargetToMaskPickUp;
        NewThrowableAppeared.OnGameEvent -= ChangeTarget;
    }

    public void EnterState(BaseStateManager incomingStateManager)
    {
        if (stateManager == null)
        {
            stateManager = incomingStateManager as EnemyStateManager;
        }  
        tryAttackTimer = 0;
    }

    public void UpdateState()
    {
        if (currentTarget != null && !currentTarget.gameObject.activeInHierarchy)
        {
            currentTarget = null;
        }
        if(currentTarget == null) //If at any point current target dissapears, try to find a new one
        {
            ChangeTarget();
            return;
        }

        navAgent.SetDestination(currentTarget.position);
        pointerRotator.pointerposition = currentTarget.position;

        MoveAnimate();

        tryAttackTimer += Time.deltaTime;
        if (tryAttackTimer >= tryAttackCooldown)
        {
            TryToAttack();
            tryAttackTimer = 0;
        }

    }

    void MoveAnimate()
    {
        if(currentTarget == null)
        {
            thisAnim.SetFloat("movemement", 0);
        }
            
        thisAnim.SetFloat("movement", pointerRotator.pointerposition.x);
    }

    void TryToAttack()
    {
        float distanceToAttack = 3f;
        if(thisThrowableHolder.currentThrowableObject != null)
        {
            if(thisThrowableHolder.currentThrowableObject.GetComponent<Throwable>() is SwordThrowable)
                distanceToAttack = distanceToAttack*0.5f; //contestant will get closer if they carry a sword
                
            RaycastHit2D hit = Physics2D.Raycast(transform.position, currentTarget.position, distanceToAttack);
            if (hit.collider.gameObject.layer == 3)
                thisThrowableHolder.UseThrowable();
        }
    }

    void SetInitialTarget()
    {
        int randomChance = Random.Range(0, 10); //Pick a number from 0 to 9, take a target
        if(randomChance <= 4)
        {
            SetTargetToMaskPickUp();
        }
        else
        {
            SetTargetToItem();
        }
    }

    void ChangeTarget()
    {
        int randomChance = Random.Range(0, 10); //Pick a number from 0 to 9, take a target

        //Gotta get an item!"
        if (thisThrowableHolder.currentThrowableObject == null)
        {
            if(randomChance <= 2) //or kill myself
            {
                SetTargetToPlayer();
                return;
            }

            SetTargetToItem();
            return;
        } 

        //"I have an item! What do I do?"
        if (thisThrowableHolder.currentThrowableObject != null)
        {
            if (thisMaskHolder.hasMask)
            {
                if(randomChance <= 2)
                {
                    SetTargetToPlayer();
                    return;
                }

                SetTargetToNearestOpponent();
                return;  
            }
            else
            {
                if(randomChance <= 3)
                {
                    SetTargetToNearestOpponent();
                    return;
                }

                SetTargetToMaskHolder();
                return;
            }

        }
    }

    void SetTargetToPlayer()
    {
        Debug.Log("Just set target to player");
        currentTarget = playerTarget;
    }

    void SetTargetToMaskHolder()
    {
        Debug.Log("Just set target to mask holder");
        GameObject[] contestants = GameObject.FindGameObjectsWithTag("Contestant");
        foreach(GameObject contestant in contestants)
        {
            if (contestant.GetComponent<MaskHolder>().hasMask && contestant != gameObject) //is not myself
            {
                currentTarget = contestant.transform;
                return;
            }
        }
        ChangeTarget();
    }

    void SetTargetToNearestOpponent()
    {
        Debug.Log("Just set target to nearest opponent");
        int contestantLayer = LayerMask.NameToLayer("Contestant");
        int layerMask = 1 << contestantLayer;
        Collider2D[] nearestOpponent = Physics2D.OverlapCircleAll(transform.position, 25f, layerMask);
        if(nearestOpponent[1] != null)
        {
            Debug.Log("Nearest Opponent: "+ nearestOpponent[1].transform.name);
            currentTarget = nearestOpponent[1].transform;
        }
        else
        {
            ChangeTarget();
        }
    }

    void SetTargetToMaskPickUp()
    {
        Debug.Log("Just set target to MASK PICKUP");
        if (!thisMaskHolder.hasMask) //If you yourself are not the new owner of the mask
        {
            currentTarget = maskOnFloorTarget;
        }
        else
        {
            ChangeTarget();
        }
    }

    void SetTargetToItem()
    {
        Debug.Log("Just set target to ITEM PICKUP");
        int collectableLayer = LayerMask.NameToLayer("Collectable");
        int layerMask =  1 << collectableLayer;
        Debug.Log("AAA: "+ LayerMask.LayerToName(collectableLayer));
        Collider2D nearestItem = Physics2D.OverlapCircle(transform.position, 50f, layerMask);
        if(nearestItem != null)
        {
            Debug.Log("nearest Item: "+ nearestItem.transform.parent.name);
            currentTarget = nearestItem.transform;    
        }
    }


}
