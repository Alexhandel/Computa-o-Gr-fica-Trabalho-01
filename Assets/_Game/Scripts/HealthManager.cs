using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;


public class HealthManager : MonoBehaviour {

	public
		GameObject healthBar;
	public
		Image healthBarFill;
	public
		AudioClip[]
			hurtSound;

	[SerializeField]
	private float 
		armorRating,
		maxHealth;
	[SerializeField]
	private AudioSource audioSource;

	private
		float currentHealth;

	// Use this for initialization
	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
		currentHealth = maxHealth;
		//healthBarFill = healthBar.GetComponentInChildren<Image>();
	}

	void AddDamage(float damage)
	{
		float trueDamage = damage * (1f-armorRating);
		currentHealth -= trueDamage;
		Debug.Log("Damage: " + trueDamage);
		if (currentHealth <= 0)
			Debug.Log("Ded.");
		
		healthBarFill.fillAmount = currentHealth / maxHealth;
		audioSource.clip = hurtSound [Random.Range(0, hurtSound.Length)];
		audioSource.Play();
	}

	void Update()
	{
		if (Input.GetKeyDown("k")) AddDamage (10f);
	}

}
