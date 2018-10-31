using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] GameObject shieldItemPrefab;
    [SerializeField] float shieldSpawnDuration = 11f;
    [SerializeField] GameObject pillItemPrefab;
    [SerializeField] float pillSpawnDuration = 8f;
    [SerializeField] GameObject starItemPrefab;
    [SerializeField] float starSpawnDuration = 9f;

    [Header("Item SFX")]
    [SerializeField] AudioClip itemAppear;
    [SerializeField] [Range(0, 1)] float itemAppearSFXVolume = 0.8f;
    [SerializeField] AudioClip itemPickUp;
    [SerializeField] [Range(0, 1)] float itemPickupSFXVolume = 0.5f;
    [SerializeField] AudioClip itemDisappear;
    [SerializeField] [Range(0, 1)] float itemDisappearSFXVolume = 0.5f;

    [Header("Item VFX")]
    [SerializeField] GameObject itemAppearVFX;
    [SerializeField] GameObject itemDisappearVFX;
    [SerializeField] float durationOfVFX = 1f;

    [Header("Spawn period")]
    [SerializeField] float tMin = 1f;
    [SerializeField] float tMax = 10f;

    [Header("Spawn Area")]
    [SerializeField] float xMin = -6f;
    [SerializeField] float xMax = 6f;
    [SerializeField] float yMin = -10f;
    [SerializeField] float yMax = 10f;



    // Use this for initialization
    void Start ()
    {
        StartCoroutine(SpawnShieldItem());
        StartCoroutine(SpawnRepairItem());
        StartCoroutine(SpawnStarItem());
    }
	
	// Update is called once per frame
	void Update ()
    {
        RandomSpawnTime();

    }

    public void PickUpSFX()
    {
        AudioSource.PlayClipAtPoint(itemPickUp, Camera.main.transform.position, itemPickupSFXVolume);
    }

    private float RandomSpawnTime()
    {
        float randomTime = UnityEngine.Random.Range(tMin, tMax);
        return randomTime;
    }

    private Vector2 RandomSpawnPos()
    {
        var randomXPos = UnityEngine.Random.Range(xMin, xMax);
        var randomYPos = UnityEngine.Random.Range(yMin, yMax);
        Vector2 randomPos = new Vector2(randomXPos, randomYPos);
        return randomPos;
    }

    // Spawn Shield
    IEnumerator SpawnShieldItem()
    {
        while (true)
        {
            yield return new WaitForSeconds(RandomSpawnTime());

            StartCoroutine(ShieldItem());
        }

    }

    IEnumerator ShieldItem()
    {
        GameObject appearVFX = Instantiate(itemAppearVFX, RandomSpawnPos(), transform.rotation) as GameObject;
        GameObject spawnedShield = Instantiate(shieldItemPrefab, appearVFX.transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(itemAppear, Camera.main.transform.position, itemAppearSFXVolume);
        Destroy(appearVFX, durationOfVFX);

        yield return new WaitForSeconds(shieldSpawnDuration);

        if (spawnedShield != null)
        {
            GameObject disappearVFX = Instantiate(itemDisappearVFX, spawnedShield.transform.position, transform.rotation) as GameObject;
            Destroy(spawnedShield);
            AudioSource.PlayClipAtPoint(itemDisappear, Camera.main.transform.position, itemDisappearSFXVolume);
            Destroy(disappearVFX, durationOfVFX);
        }
    }

    // Spawn Repair Capsule
    IEnumerator SpawnRepairItem()
    {
        while (true)
        {
            yield return new WaitForSeconds(RandomSpawnTime());

            StartCoroutine(RepairItem());
        }

    }

    IEnumerator RepairItem()
    {
        GameObject appearVFX = Instantiate(itemAppearVFX, RandomSpawnPos(), transform.rotation) as GameObject;
        GameObject spawnedRepair = Instantiate(pillItemPrefab, appearVFX.transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(itemAppear, Camera.main.transform.position, itemAppearSFXVolume);
        Destroy(appearVFX, durationOfVFX);

        yield return new WaitForSeconds(pillSpawnDuration);

        if (spawnedRepair != null)
        {
            GameObject disappearVFX = Instantiate(itemDisappearVFX, spawnedRepair.transform.position, transform.rotation) as GameObject;
            Destroy(spawnedRepair);
            AudioSource.PlayClipAtPoint(itemDisappear, Camera.main.transform.position, itemDisappearSFXVolume);
            Destroy(disappearVFX, durationOfVFX);
        }
    }

    // Spawn Star Points
    IEnumerator SpawnStarItem()
    {
        while (true)
        {
            yield return new WaitForSeconds(RandomSpawnTime());

            StartCoroutine(StarItem());
        }

    }

    IEnumerator StarItem()
    {
        GameObject appearVFX = Instantiate(itemAppearVFX, RandomSpawnPos(), transform.rotation) as GameObject;
        GameObject spawnedStar = Instantiate(starItemPrefab, appearVFX.transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(itemAppear, Camera.main.transform.position, itemAppearSFXVolume);
        Destroy(appearVFX, durationOfVFX);

        yield return new WaitForSeconds(starSpawnDuration);

        if (spawnedStar != null)
        {
            GameObject disappearVFX = Instantiate(itemDisappearVFX, spawnedStar.transform.position, transform.rotation) as GameObject;
            Destroy(spawnedStar);
            AudioSource.PlayClipAtPoint(itemDisappear, Camera.main.transform.position, itemDisappearSFXVolume);
            Destroy(disappearVFX, durationOfVFX);
        }
    }
}
