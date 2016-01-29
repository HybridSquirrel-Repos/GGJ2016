﻿using UnityEngine;
using System.Collections;

public class Player_Webshoot : MonoBehaviour 
{
	[SerializeField] private float shootSpeed;
	[SerializeField] private float maxWebScale;
	[SerializeField] private float minWebScale;
	[SerializeField] private float origShootColldown = 0.3f;
	[SerializeField] private Transform hitBall;

	[HideInInspector] public static bool shooting = false;

	private bool canShoot = true;

	private float shootCoolDown = 0.0f;

	void Start () 
	{

	}
	
	void Update () 
	{
		if (((Input.GetAxisRaw (Player_Controls.shootWeb) > 0 && canShoot) || shooting) && shootCoolDown == 0f)
		{
			shooting = true;
			if (transform.localScale.y <= maxWebScale)
			{
				ShootWeb (shootSpeed * Time.deltaTime);
				MoveObj (shootSpeed * Time.deltaTime * 2, hitBall);
				shooting = true;
				canShoot = true;
				print ("can shoot"); 
			} else
			{
				canShoot = false;
				//shooting = false;
				print ("can't shoot");
				shootCoolDown = origShootColldown;

			}
		}

		if (!canShoot)
		{
			
			if (transform.localScale.y > minWebScale)
			{
				ShootWeb ((-shootSpeed * Time.deltaTime));
				MoveObj (-shootSpeed * Time.deltaTime * 2, hitBall);

			} else
			{
				canShoot = true;
				shooting = false;
			}
		}

		if (shootCoolDown > 0)
		{
			shootCoolDown -= Time.deltaTime;
			if (shootCoolDown < 0.01f)
			{
				shootCoolDown = 0;
			}
		}

	}


	void ShootWeb (float speed)
	{
		transform.localScale = Util.Scale (new Vector3 (0, speed, 0), transform.localScale);
		MoveObj (speed, transform);
	}

	void MoveObj (float speed, Transform moveObj)
	{
		moveObj.Translate (0, speed, 0);
	}
}