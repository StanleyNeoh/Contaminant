using UnityEngine;

public class bulletphy : MonoBehaviour
{
    [Header("Referenced")]
    public Rigidbody bulletbody;

    [Header("Bullet Stats")]
    public float bulletSpeed;
    public float bulletLifeTime;
    public float bulletDamage;
    public float spreadFactor;


    [Header("Explosive")]
    public bool Explodes;
    public bool Sticky;
    public float ExplosionForce;
    public float ExplosionSize;
    public float ExplosiveDamage;
    public GameObject ExplosionEffect;
    public string ExplosionSound;

    [Header("Vampiric")]
    public bool Vampiric;
    public float HPperShot;
    public float ChanceHeal;


    public bool Collided = false;
    private GameObject player;
    void Start()
    {
        player = PlayerSingleton.instance.player;
        Vector3 RandomVelo = new Vector3(Random.Range(-spreadFactor, spreadFactor), Random.Range(-spreadFactor, spreadFactor), Random.Range(-spreadFactor, spreadFactor));
        bulletbody.velocity = transform.forward * bulletSpeed + RandomVelo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Collided) {
            bulletLifeTime -= Time.deltaTime;
        }
        if (bulletLifeTime < 0) {
            if (Explodes) {
                FindObjectOfType<AudioManager>().SetnPlay(ExplosionSound, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
                if (ExplosionEffect != null) {
                    Instantiate(ExplosionEffect, transform.position, transform.rotation);
                }
                Collider[] nearby = Physics.OverlapSphere(transform.position, ExplosionSize);
                foreach (Collider Object in nearby) {
                    //To Check for whether Object is in LoS
                    Vector3 pointer = Object.transform.position - transform.position;
                    Ray ray = new Ray(transform.position, pointer);
                    RaycastHit[] InclusiveBetween = Physics.RaycastAll(ray, pointer.magnitude);

                    if (InclusiveBetween.Length <= 2) {
                        
                        UniversalDestructible Health = Object.GetComponent<UniversalDestructible>();
                        if (Health != null && Health.Health > 0) {
                            Health.Health -= ExplosiveDamage;
                        }

                        Rigidbody rb = Object.GetComponent<Rigidbody>();
                        if (rb != null) {
                            rb.AddExplosionForce(ExplosionForce, transform.position, ExplosionSize);
                        }
                    }     
                }
            }
            Destroy(gameObject);
        }      
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.GetComponent<bulletphy>() == null) {
            Collided = true;
        }
        if (Sticky) {
            gameObject.transform.parent = collision.gameObject.transform;
            bulletbody.isKinematic = true;
        }
    }
}   
