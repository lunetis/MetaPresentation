using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [Tooltip("If null, presenter spawns on this object's position and rotation.")]
    Transform presenterSpawnTransform;

    public GameObject testChar = null;
    [Range(1, 10)]
    public float scaleRange = 1;

    // Start is called before the first frame update
    void Start()
    {
        if(presenterSpawnTransform == null)
        {
            presenterSpawnTransform = transform;
        }
        SpawnPresenter();
    }

    public void SpawnPresenter()
    {
        if(PresentationDataObject.characterObject == null)
        {
            PresentationDataObject.characterObject = testChar;
        }

        GameObject presenter = Instantiate(PresentationDataObject.characterObject);

        presenter.transform.position = presenterSpawnTransform.position;
        presenter.transform.rotation = presenterSpawnTransform.rotation;
        presenter.transform.localScale *= scaleRange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
