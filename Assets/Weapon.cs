using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float fireRate = 0;
    public float damage = 10;
    public LayerMask whatToHit;

    float timeToFire = 0;
    Transform firePoint;
    private Vector2 gunPosition;

    //private GunRotation playerScript;
    private UnityStandardAssets._2D.PlatformerCharacter2D playerScript2;
    private GameObject gunPos;
    private GameObject parentGameObject;

    public Transform BulletTrailPrefab;


    // Use this for initialization
    void Start () {
        firePoint = transform.Find("FirePoint");
        if(firePoint == null)
        {
            Debug.LogError("No FirePoint found");
        }

        //playerScript = GetComponent<GunRotation>();
        parentGameObject = transform.parent.gameObject;
        playerScript2 = parentGameObject.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();

    }

	// Update is called once per frame
	void Update () {
        gunPos = GameObject.Find("GunCentre");
        
        if (fireRate == 0)
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
        gunPosition = gunPos.transform.position;
        
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(gunPosition, firePointPosition - gunPosition, 100, whatToHit);
        Effect();

         if (hit.collider != null)
         {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            
            Debug.Log("We hit " + hit.collider.name + " and did " + damage + "damage");
         }
    }
    void Effect()
    {
        bool isFacingRight = playerScript2.getIsFacingRight();
        if (isFacingRight == true)
        {
            Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            firePoint.rotation *= Quaternion.Euler(0, 0, 180);
            Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
            firePoint.rotation *= Quaternion.Euler(0, 0, 180);
        }
    }

}
