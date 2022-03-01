using UnityEngine;


public class Jump2 : MonoBehaviour
{
    [Header("Chase Stats")]
    public float ChaseRad;
    public float SoundRange;
    public bool Agro = false;
    public int jumping = 0; //0 not jumping,  1 start jump , 2 mid jump

   

    [Header("JumpComputation")]
    public float JumpCD;
    private float JumpCDtimer = 0;
    public int ChanceJump;
    public GameObject target;
    private Vector3 TargetsDisplacement;

    public float[] gravityArr = new float[3];
    private float gravity;
    public float[] JumpHeightArr = new float[3];
    private float jumpheight;
    public float JumpRange;

    //public float Speedlimit;

    private Vector3 NetVelo;

    [Header("SlamShock Stats")]
    public float SlamShockRad;
    public float SlamShockDamage;
    public float KBXZVelo;
    public float KBYVelo;
    public string JumpSound;
    public string LandSound;

    [Header("Surrounding Check")]
    public float CheckRad;
    public Vector3 CheckOffSet;
    public LayerMask groundMask;
    public float CheckDelay;
    private float CheckDelaytimer;


    private CharacterController CController;
    void Start()
    {
        CController = GetComponent<CharacterController>();
        target = PlayerSingleton.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        TargetsDisplacement = (target.transform.position - transform.position);
        if (TargetsDisplacement.magnitude <= ChaseRad && Agro == false)
        {
            Agro = true;
        }
        if (jumping == 0 && Agro)
        {
            if (JumpCDtimer > 0)
            {
                JumpCDtimer -= Time.deltaTime;
            }
            else
            {
                int Chance = UnityEngine.Random.Range(0, 101);
                if (Chance < ChanceJump)
                {
                    float volume = Mathf.Clamp((SoundRange - TargetsDisplacement.magnitude) / SoundRange, 0f, 1f);
                    FindObjectOfType<AudioManager>().SetnPlay(JumpSound, Mathf.Pow(volume, 3)* PlayerPrefs.GetFloat("Volume",100)/100 , 1);
                    gravity = gravityArr[Random.Range(0, gravityArr.Length)];
                    jumpheight = JumpHeightArr[Random.Range(0, JumpHeightArr.Length)];
                    NetVelo = JumpHeightMotionCalc(gravity,TargetsDisplacement, jumpheight, JumpRange);
                    jumping = 1;
                    CheckDelaytimer = CheckDelay;

                }
                else
                {
                    JumpCDtimer = JumpCD;
                }
            }
        }
        else if (jumping == 1 && Agro)
        {
            CController.Move(NetVelo * Time.deltaTime);
            NetVelo -= gravity * Vector3.up * Time.deltaTime;
            if (CheckDelaytimer > 0)
            {
                CheckDelaytimer -= Time.deltaTime;
            }
            else if (Physics.CheckSphere(transform.position+CheckOffSet, CheckRad, groundMask) && CheckDelaytimer <= 0 || NetVelo.y <=-1000)
            {
                float volume = Mathf.Clamp((SoundRange - TargetsDisplacement.magnitude) / SoundRange, 0f, 1f);
                FindObjectOfType<AudioManager>().SetnPlay(LandSound, Mathf.Pow(volume, 3) * PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
                jumping = 0;
                JumpCDtimer = JumpCD;
                if (TargetsDisplacement.magnitude <= SlamShockRad)
                {
                    target.GetComponent<HDPlayer>().Health -= SlamShockDamage;
                    target.GetComponent<HDPlayer>().UpdateHP = true;
                    target.GetComponent<PMV2>().Knockback(new Vector3(TargetsDisplacement.normalized.x * KBXZVelo, KBYVelo, TargetsDisplacement.normalized.z * KBXZVelo));
                }
            }
        }
    }
    
    Vector3 SpeedLimithMotionCalc(float gravity, Vector3 displacement, float SpeedLimit)
    {
        Vector3 Direction = (Vector3.up + displacement.normalized).normalized;
        float Y = displacement.y;
        float X = new Vector3(displacement.x, 0, displacement.z).magnitude;
        float Sin = Direction.y;
        float Cos = new Vector3(Direction.x, 0, Direction.z).magnitude;

        Vector3 Motion = Mathf.Min(SpeedLimit, X * Mathf.Sqrt(gravity / (2 * Sin * Cos * X - 2 * Cos * Cos * Y))) * Direction;
        return Motion;
    }

    Vector3 JumpHeightMotionCalc(float gravity, Vector3 displacement, float jumpheight, float JumpRange) {
        float YSpeed = Mathf.Sqrt(2 * gravity * jumpheight);
        float timetaken = (-(YSpeed) - Mathf.Sqrt(YSpeed * YSpeed - 4 * (-gravity / 2) * (-displacement.y))) / (-gravity);
        Vector3 HoriDisplacement = new Vector3(displacement.x, 0, displacement.z);
        Vector3 Motion = HoriDisplacement.normalized * Mathf.Min(JumpRange, HoriDisplacement.magnitude) / timetaken + Vector3.up * YSpeed;
        return Motion;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChaseRad);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position+CheckOffSet, CheckRad);
    }
}

