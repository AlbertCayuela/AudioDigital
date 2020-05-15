////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageFlashOnHit : MonoBehaviour, IDamageable
{
	public float flashDuration = 0.1f;

    public AudioClip axe_hit_sound = null;
    public AudioClip dagger_hit_sound = null;
    public AudioClip hammer_hit_sound = null;
    public AudioClip pickaxe_hit_sound = null;
    public AudioClip sword_hit_sound = null;

    #region private variables
    private List<Material> materials;
    private List<Color> originalColors;
    private bool isFlashing;
    #endregion

    void OnEnable()
    {
        materials = new List<Material>();
        originalColors = new List<Color>();

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].gameObject.layer != LayerMask.NameToLayer("Weapon"))
            {
                Material[] mats = renderers[i].materials;
                for (int j = 0; j < mats.Length; j++)
                {
                    if (mats[j].HasProperty("_EmissionColor"))
                    {
                        materials.Add(mats[j]);
                        originalColors.Add(mats[j].GetColor("_EmissionColor"));
                    }
                }
            }
        }
    }


    public void OnDamage(Attack attack)
    {
        if (!isFlashing)
        {
            StartCoroutine(DamageFlash());

            AudioSource audioSource = GetComponent<AudioSource>();
            if (PlayerManager.Instance.equippedWeaponInfo.weaponType == WeaponTypes.Axe)
            {
                audioSource.PlayOneShot(axe_hit_sound, 0.7F);
            }
            else if (PlayerManager.Instance.equippedWeaponInfo.weaponType == WeaponTypes.Dagger)
            {
                audioSource.PlayOneShot(dagger_hit_sound, 0.7F);
            }
            else if (PlayerManager.Instance.equippedWeaponInfo.weaponType == WeaponTypes.Hammer)
            {
                audioSource.PlayOneShot(hammer_hit_sound, 0.7F);
            }
            else if (PlayerManager.Instance.equippedWeaponInfo.weaponType == WeaponTypes.PickAxe)
            {
                audioSource.PlayOneShot(pickaxe_hit_sound, 0.7F);
            }
            else if (PlayerManager.Instance.equippedWeaponInfo.weaponType == WeaponTypes.Sword)
            {
                audioSource.PlayOneShot(sword_hit_sound, 0.7F);
            }
        }
    }

    IEnumerator DamageFlash()
    {
        if (materials.Count > 0)
        {
            isFlashing = true;

            for (int i = 0; i < materials.Count; i++)
            {
                materials[i].SetColor("_EmissionColor", Color.white);
            }

            yield return new WaitForSeconds(flashDuration);

            for (int i = 0; i < materials.Count; i++)
            {
                materials[i].SetColor("_EmissionColor", originalColors[i]);
            }

            isFlashing = false;
        }
    }
}
