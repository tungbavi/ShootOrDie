using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem flash;
    public float fireRate = 4.5f;
    private float nextTimeToFire = 0f;

    public int maxAmmo = 80;
    public int currentAmmo;
    public float reloadTime = 4.4f;
    private AudioManager audioManager;
    private bool isReloading = false;
    public Text bullets;
    public Text reloading;
    void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }
   
    // Start is called before the first frame update
    void Start()
    {

        flash.gameObject.SetActive(false);
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isReloading)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoott();
            }
            if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
            {

                StartCoroutine(Reload());
            }
        }
        bullets.text = "Bullet: " + currentAmmo + " / " + maxAmmo;
       
        
    }
    void Shoott()
    {
        flash.gameObject.SetActive(true);
        flash.Play();
        audioManager.Play("fight");
        currentAmmo--;
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward, out hit, range)){
            //Debug.Log(hit.transform.name);
            IDamgable enermy = hit.collider.GetComponent<IDamgable>();
            if(enermy != null)
            {
                enermy.TakeDamage(damage);
            }
        }
        
    }
    IEnumerator Reload()
    {
        isReloading = true;
        audioManager.Play("reload");
        //yield return new WaitForSeconds(reloadTime);    
        while (reloadTime > 0)
        {           
            reloading.text = "reloading: "+ reloadTime.ToString("F1");
            yield return null;
            reloadTime -= Time.deltaTime;
        }
        reloading.text = "";
        
        currentAmmo = maxAmmo;
        reloadTime = 4.4f;
        isReloading = false;
        //isReloading = false;
    }
    
   
}
