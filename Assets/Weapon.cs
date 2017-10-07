using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float fireRate = 0;
    public float damage = 10;
    public LayerMask whatToHit;

    float timeToFire = 0;
    Transform firePoint;

    //private GunRotation playerScript;
    static private UnityStandardAssets._2D.PlatformerCharacter2D playerScript2;
    private GameObject gunPos;
    private GameObject parentGameObject;

    public Transform BulletTrailPrefab;


    // Use this for initialization
    void Awake () {
        firePoint = transform.Find("FirePoint");
        if(firePoint == null)
        {
            Debug.LogError("No FirePoint found");
        }

        //playerScript = GetComponent<GunRotation>();
        parentGameObject = transform.parent.gameObject;
        playerScript2 = GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();

        gunPos = GameObject.Find("GunCentre");

    }
	
	// Update is called once per frame
	void Update () {
        //Shoot();
		if(fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if(Input.GetButton ("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
	}
    void Shoot()
    {
        Vector2 gunPosition = gunPos.transform.position;
        bool isFacingRight = playerScript2.getIsFacingRight();

        //Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, gunPosition - firePointPosition, 100, whatToHit);
        Effect();
        

        //Debug.DrawLine(firePointPosition, (gunPosition - firePointPosition) * 100, Color.cyan);
        //if (hit.collider != null)
        //{
            //Debug.DrawLine(firePointPosition, hit.point, Color.red);
            //Debug.Log("We hit " + hit.collider.name + " and did " + damage + "damage");
        //}
    }
    void Effect()
    {
        Instantiate(BulletTrailPrefab, (firePoint.position), firePoint.rotation);
    }

}
