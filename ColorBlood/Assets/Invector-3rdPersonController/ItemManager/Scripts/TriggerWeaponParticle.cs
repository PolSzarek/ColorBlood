using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invector.vItemManager
{
	public enum WeaponParticleType {
		PLAYER_EFFECT,
		WEAPON_EFFECT
	}

	public class TriggerWeaponParticle : StateMachineBehaviour {

		public int moveStep;
		public float timeEffect = 3f;

		private GameObject player;
		private GameObject sword;

		private WeaponParticleSystem wps;

 		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
			Init();

			if (wps != null && moveStep < wps.moves.Count) {				
				Debug.Log("Checking particles: size " + wps.moves.Count + ", contain: " + wps.moves);

				foreach(var particle in wps.moves[moveStep].particles) {
					Debug.Log("Particle on move " + moveStep + ": " + particle);
					var objParticle = particle.getAttackEffect((int)player.GetComponent<ColorAttack>().colorAttack);

					Debug.Log("Attack of color : " + objParticle.name);

					var instance = Instantiate(objParticle);

					switch(instance.GetComponent<ParticleIdentifier>().landmark) {
						case WeaponParticleType.PLAYER_EFFECT:
							var localPosition = instance.transform.localPosition;
							var localRotation = instance.transform.localRotation;
							instance.transform.SetParent(player.transform);

							instance.transform.localPosition = localPosition;
							instance.transform.localRotation = localRotation;

							instance.transform.SetParent(null);
							break;
						case WeaponParticleType.WEAPON_EFFECT:
							break;
					}

					Destroy(instance, timeEffect);
				}
			}
        }

		private void Init() {
			vItemManager itemManager = FindObjectOfType<vItemManager>();

			if (itemManager == null) {
				Debug.Log("No item manager");
			} else {
				player = itemManager.gameObject;

				foreach (var eqpPnt in itemManager.equipPoints) {
					if (eqpPnt.equipPointName.Equals("RightArm")) {
						sword = eqpPnt.equipmentReference.equipedObject;
						break;
					}
				}
			}

			wps = sword.GetComponentInChildren<WeaponParticleSystem>();
		}	
	}
}
