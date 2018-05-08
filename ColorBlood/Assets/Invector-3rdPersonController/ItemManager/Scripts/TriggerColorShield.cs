using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Invector.vItemManager
{
	public class TriggerColorShield : StateMachineBehaviour {

		public GameObject shieldPrefab;
		private ColorMove color;
		private Dictionary<Transform, GameObject> instances = new Dictionary<Transform, GameObject>();

		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
			if (Init(animator) && animator.GetBool("IsBlocking")) {

				if (color != null) {
					Color shieldColor = new Color();
					switch (color.colorAttack) {
						case MoveColorEffect.RED:
						shieldColor = Color.red;
						break;
						case MoveColorEffect.BLUE:
						shieldColor = Color.blue;
						break;
						case MoveColorEffect.GREEN:
						shieldColor = Color.green;
						break;
						case MoveColorEffect.YELLOW:
						shieldColor = Color.yellow;
						break;
					}
					createShield(animator.transform, shieldColor);
				}
			}
		}


		override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
			if (!instances.ContainsKey(animator.transform))
				return;

			Destroy(instances[animator.transform]);
			instances.Remove(animator.transform);
		}

		private bool Init(Animator animator) {
			var character = animator.transform;
			if (!instances.ContainsKey(character)) {
				color = character.GetComponent<ColorMove>();
				return true;
			} else {
				return false;
			}
		}

		private void createShield(Transform character, Color c) {
			var instance = Instantiate(shieldPrefab);
			Renderer rend = instance.transform.GetChild(0).GetComponent<MeshRenderer>();

			rend.material.shader = Shader.Find("Unlit/ShieldFX");
			rend.material.SetColor("_MainColor", c);

			instance.transform.SetParent(character);
			instance.transform.localPosition = new Vector3(0, 1, 0);
			instance.transform.localScale = new Vector3(1, 1.3f, 1);

			instances.Add(character, instance);
		}

		// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
		override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
			if ((animator.GetBool("WeakAttack") || animator.GetBool("StrongAttack"))) {
				if (instances.ContainsKey(animator.transform)) {
					Destroy(instances[animator.transform]);
					instances.Remove(animator.transform);
				}
			}
		}


		// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
		//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//
		//}

		// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
		//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//
		//}
	}
}