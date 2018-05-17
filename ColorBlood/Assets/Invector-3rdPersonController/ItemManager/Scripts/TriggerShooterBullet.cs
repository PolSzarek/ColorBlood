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
		private Transform rifle;
		private ColorMove color;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
			Init(animator.gameObject);

			if (color != null) {
				Color bulletColor = new Color();
				switch (color.colorAttack) {
					case MoveColorEffect.RED:
					bulletColor = Color.red;
					break;
					case MoveColorEffect.BLUE:
					bulletColor = Color.blue;
					break;
					case MoveColorEffect.GREEN:
					bulletColor = Color.green;
					break;
					case MoveColorEffect.YELLOW:
					bulletColor = Color.yellow;
					break;
				}
				Fire(animator.transform, bulletColor);
			}
		}

		private void Init(GameObject character) {
			vItemManager itemManager = character.GetComponent<vItemManager>();

			if (itemManager == null) {
				Debug.Log("No item manager");
			} else {
				color = character.GetComponent<ColorMove>();

				foreach (var eqpPnt in itemManager.equipPoints) {
					if (eqpPnt.equipPointName.Equals("RightArm")) {
						rifle = eqpPnt.equipmentReference.equipedObject.transform;
						break;
					}
				}
			}
		}

		private void Fire(Transform player, Color color) {
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
