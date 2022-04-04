using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarenPool : MonoBehaviour
{
    List<GameObject> karens;
    [SerializeField] List<GameObject> karenPrefabs;
    int poolSize;

    public void CreatePool(int size)
    {
        poolSize = size;
        karens = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < size; i++)
        {
            int roll = Random.Range(0, karenPrefabs.Count);
            tmp = karenPrefabs[roll];
            tmp = Instantiate(tmp, transform);
            tmp.SetActive(false);
            karens.Add(tmp);
        }
    }

    public GameObject Instantiate(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!karens[i].activeInHierarchy)
            {
                karens[i].transform.position = position;
                karens[i].transform.rotation = rotation;
                karens[i].SetActive(true);
                return karens[i];
            }
        }
        return null;
    }
}
