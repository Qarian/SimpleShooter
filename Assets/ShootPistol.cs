using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShootPistol : MonoBehaviour {

    AudioSource source;
    ParticleSystem particle;
    Camera cam;

    [Header("Wspólne dla wszystkich broni")]
    public Renderer pistolMesh;
    public Renderer sniperMesh;
    public AudioClip reloadsound;
    public AudioClip changeGun;
    public Animator hand;
    public Transform gun;
    public GameObject aim;

    public Text ammoText;

    [Header("Pistol 1")]
    [SerializeField] float fireRate1;
    [SerializeField] int ammoClip1;
    [SerializeField] float reloadTime1;
    [SerializeField] float hitForce1;
    [SerializeField] float damage1;
    [SerializeField] float range1;


    [Header("Sniper 2")]
    [SerializeField] float fireRate2;
    [SerializeField] int ammoClip2;
    [SerializeField] float reloadTime2;
    [SerializeField] float hitForce2;
    [SerializeField] float damage2;
    [SerializeField] float range2;

    float shootTime=0;
    int ammo;
    float fireRate;
    int ammoClip;
    float reloadTime;
    float hitForce;
    float damage;
    float range;

    int index=0;

    void Start()
    {
        source = GetComponent<AudioSource>();
        particle = GetComponent<ParticleSystem>();
        cam = GetComponentInParent<Camera>();

        fireRate = fireRate1;
        ammoClip = ammoClip1;
        reloadTime = reloadTime1;
        hitForce = hitForce1;
        damage = damage1;
        range = range1;
        ammo = ammoClip;
        ammoText.text = "Ammo: " + ammo;
    }


	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > shootTime && ammo > 0)
        {
            Shoot();
        }

        else if (Input.GetKeyUp("r"))
        {
            Reload();
        }

        #region ChangeGun

        else if (Input.mouseScrollDelta != new Vector2(0, 0))
        {
            Debug.Log("dsdsddd");

            if(Input.mouseScrollDelta == new Vector2(0, -1))
            {
                if (index==0)
                    ChangeMesh(transform.childCount-1);
                else
                    ChangeMesh(index-1);
            }
            if (Input.mouseScrollDelta == new Vector2(0, 1))
            {
                if (index == transform.childCount-1)
                    ChangeMesh(0);
                else
                    ChangeMesh(index + 1);
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (index != 0)
                ChangeMesh(0);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (index != 1)
                ChangeMesh(1);
        }

        #endregion
    }

    void ChangeMesh(int num)
    {
        transform.GetChild(index).gameObject.SetActive(false);
        index = num;
        StartCoroutine(ReloadMesh());
        transform.GetChild(index).gameObject.SetActive(true);
    }


    IEnumerator ReloadMesh()
    {
        hand.SetBool("Reload", true);
        shootTime = Time.time + reloadTime;
        source.PlayOneShot(changeGun);
        yield return new WaitForSeconds(reloadTime);
        ammo = ammoClip;
        ammoText.text = "Ammo: " + ammo;
        hand.SetBool("Reload", false);
    }

    void Shoot()
    {
        source.Play();
        particle.Emit(600);
        shootTime = Time.time + fireRate;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.transform.GetComponent<Rigidbody>() != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);

                if (hit.transform.GetComponent<Enemy>() != null)
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    enemy.Hit(damage);
                }
            }
        }
        ammo--;
        ammoText.text = "Ammo: " + ammo;
    }

    private void Reload()
    {
        ammo = ammoClip1;
        ammoText.text = "Ammo: " + ammo;
        source.PlayOneShot(reloadsound);
        shootTime = Time.time + reloadTime;
        StartCoroutine(ReloadMesh());
    }
}
