using UnityEngine;
using UnityEngine.AI;
public class EnemyBehaviour : MonoBehaviour
{

    //Object reference.
    public NavMeshAgent Agent;
    public GameObject huntEyes;
    public GameObject [] Points;

    //Navigation Tweaks
    public int Patience; //Controls how long npc will wait between actions. a good range is between 1 and Y
    public float WalkingSpeed; //Speed while walking
    public float RunningSpeed; //Speed while chasing


    //Navigation Control
    private GameObject CurrentObjective;   //Current point goal. NPC will pathfind to it when possible
    public enum statePool
    {
        Wandering,
        Waiting,
        Searching,
        Inspecting,
        Fighting,
        Hunting
    }
    private statePool currentState = statePool.Waiting;
    private float waitedTime = 0;
    private float targetTime = 1;
    private float rayRangeWander = 20.0f;
    private float rayRangeHunt = 40.0f;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        //If there are no valid points to walk to, the AI will not be able to function fully.
        if (Points.Length == 0)
        {
            Debug.LogError("WARNING, " + this + "HAS NO POINTS TO WALK TO");
            return;
        }
        Agent.speed = WalkingSpeed;
    }

    // Update is called once per frame
    void Update()
    {


        // ==============
        // == RAYCAST ===
        // ==============

        raycast(this.transform.position + new Vector3(0, 0.5f, 0), transform.TransformDirection(Vector3.forward));

        // ==============
        // == ACTION DELAY ===
        // ==============

        //Set a small delay between actions
        if (targetTime >= 0)
        {
            waitedTime += Time.deltaTime;
            if(waitedTime < targetTime)
            {
                return;
            }
            else
            {
                targetTime = -1;
            }
            
        }
        else
        {
            waitedTime = 0;
        }

        // ==============
        // == BEHAVIOUR CONTROLLER ===
        // ==============

        switch (currentState)
        {
            case statePool.Wandering:

                //If NPC is finished walking, wait for an update
                if (reachedDestination())
                {
                    updateState(statePool.Waiting);
                    break;
                }
                else { break; }

            case statePool.Waiting:

                updateState(statePool.Wandering);
                break;

            case statePool.Fighting:
                //If NPC has an objective, attack it
                if(CurrentObjective != null)
                {
                    huntEyes.transform.LookAt(CurrentObjective.transform.position);
                    if (huntEyes.transform.localEulerAngles.y >= 280 || huntEyes.transform.localEulerAngles.y <= 80)
                    {
                        raycast(huntEyes.transform.position, huntEyes.transform.TransformDirection(Vector3.forward));
                    }
                }

                break;


        }
        //If NPC is focused, but has not yet seen the objective, go to last known position

        

        //If NPC has no clear line of sight, look for objective.

        //If NPC cannot find objective fast enough, go to inspect state




        //If not focused, it should walk around

        
    }//Update

    void Wander()
    {
        int nextTarget = Random.Range(0, Points.Length - 1);
        Agent.SetDestination(Points[nextTarget].transform.position);
    }

    void setWaitingTime(float patienceMult)
    {
        float rngMult = Random.Range(0.75f,1.25f);
        targetTime = (Patience * patienceMult) * rngMult;
    }

    bool reachedDestination()
    {
        if(Agent.remainingDistance <= Agent.stoppingDistance)
        { return true; }
        else
        { return false; }
    }

    void updateState(statePool newState)
    {
        switch (newState)
        {
            case statePool.Waiting:
                setWaitingTime(1.2f);
                break;
            case statePool.Wandering:
                Agent.speed = WalkingSpeed;
                Wander();
                break;
            case statePool.Fighting:
                
                Agent.speed = RunningSpeed;
                if (CurrentObjective != null)
                {
                    Agent.SetDestination(CurrentObjective.transform.position);
                }
                
                break;
        }
        currentState = newState;
    }
    void raycast(Vector3 origin,Vector3 direction)
    {
        //Cast a ray for detection
        float rayRange = (currentState != statePool.Fighting) ? rayRangeWander : rayRangeHunt;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(origin, direction , out hit, rayRange))
        {
            Debug.DrawLine(origin, hit.point, Color.yellow);

            Collider IntersectedObject = hit.collider;

            //If NPC finds the player, change into chase sequence
            if (IntersectedObject.tag == "Player")
            {
                if (currentState != statePool.Fighting)
                {
                    setWaitingTime(0.5f);
                }
                CurrentObjective = IntersectedObject.gameObject;
                updateState(statePool.Fighting);
            }
        }
        else
        {
            //NPC sees nothing of interest
            Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.TransformDirection(Vector3.forward) * 10, Color.white);
        }

    }
}
