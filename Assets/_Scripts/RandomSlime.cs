using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSlime : MonoBehaviour
{
    public GameObject spawnEffect;
    public AudioClip spawnSound;
    public List<GameObject> hatList = new List<GameObject>();
    public List<Material> matList = new List<Material>();
    public List<Material> faceList = new List<Material>();

    SkinnedMeshRenderer skinRenderer;


    private void Start()
    {
        skinRenderer = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
    }
    public void RandomFeatures()
    {
        //spawn effect
        AudioSource.PlayClipAtPoint(spawnSound, Camera.main.transform.position);
        Instantiate(spawnEffect, transform);

        //delete previous hat
        if(this.transform.GetChild(2).childCount > 0)
        {
            Destroy(this.transform.GetChild(2).GetChild(0).gameObject);
        }
        //place random hat
        int ranHat = Random.Range(0, hatList.Count);
        GameObject tempHat = Instantiate(hatList[ranHat], this.transform.GetChild(2));

        //assign color and face
        Material[] mats = skinRenderer.materials;

        mats[0] = matList[Random.Range(0, matList.Count)];
        mats[1] = faceList[Random.Range(0, faceList.Count)];

        //replace mats
        skinRenderer.materials = mats;
    }
}
