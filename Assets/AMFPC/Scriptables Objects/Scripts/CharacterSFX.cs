using UnityEngine;

namespace AMFPC.Scriptables_Objects.Scripts
{
	public abstract class CharacterSFX : ScriptableObject
	{
		public AudioClip[] walkSFX;
		public AudioClip[] runSFX;
		public AudioClip[] jumpSFX;
		public AudioClip[] landSFX;
		public AudioClip[] slideSFX;
		public AudioClip[] deathSFX;
		public AudioClip[] damagedSFX;
	}
}
