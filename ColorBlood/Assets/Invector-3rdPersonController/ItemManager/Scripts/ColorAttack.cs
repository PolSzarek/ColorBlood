using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invector.vItemManager
{
	public enum AttackColorEffect {
		RED = 0,
		BLUE = 1,
		GREEN = 2,
		YELLOW = 3
	}
	public class ColorAttack : MonoBehaviour {
		public AttackColorEffect colorAttack;
	}
}
