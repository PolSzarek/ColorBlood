using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Invector.vItemManager
{
	public class TriggerShooterBullet : StateMachineBehaviour {

		public GameObject bulletPrefab;

		public float bulletLifetime = 2.0f;
		public float bulletSpeed = 6.0f;
		public Vector3 angleForBullet = new Vector3(0, 0, 0);

		private Transform player;
		private Transform rifle;
		private ColorAttack color;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
			Init();

			if (color != null) {
				Color bulletColor = new Color();
				switch (color.colorAttack) {
					case AttackColorEffect.RED:
					bulletColor = Color.red;
					break;
					case AttackColorEffect.BLUE:
					bulletColor = Color.blue;
					break;
					case AttackColorEffect.GREEN:
					bulletColor = Color.green;
					break;
					case AttackColorEffect.YELLOW:
					bulletColor = Color.yellow;
					break;
				}
				Fire(bulletColor);
			}
		}

		private void Init() {
			vItemManager itemManager = FindObjectOfType<vItemManager>();

			if (itemManager == null) {
				Debug.Log("No item manager");
			} else {
				player = itemManager.gameObject.transform;
				color = itemManager.gameObject.GetComponent<ColorAttack>();

				foreach (var eqpPnt in itemManager.equipPoints) {
					if (eqpPnt.equipPointName.Equals("RightArm")) {
						rifle = eqpPnt.equipmentReference.equipedObject.transform;
						break;
					}
				}
			}
		}

		private void Fire(Color color) {
			Quaternion rotation = player.rotation;
			rotation.eulerAngles = player.rotation.eulerAngles + angleForBullet;
			var bullet = (GameObject)Instantiate(bulletPrefab, rifle.position, rotation);

			// Add velocity to the bullet
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

			// Destroy the bullet after 2 seconds
			Destroy(bullet, bulletLifetime);   
		}
	}
}
