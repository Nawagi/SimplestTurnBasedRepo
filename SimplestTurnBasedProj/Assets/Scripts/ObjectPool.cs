//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Example of how to use
/*
GameObject newBullet = ObjectPool.Instance.GetPooledItem("Bullet");
if (newBullet != null)
{
    newBullet.transform.position = bulletSpawn.position;
    newBullet.transform.rotation = transform.rotation;
    newBullet.SetActive(true);
}
*/

[System.Serializable]
public class PoolItem
{
    public GameObject prefab;
    public int amount;
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public List<PoolItem> items;
    public List<GameObject> pooledItems;

    private void Awake()
    {
        Instance = this;

        pooledItems = new List<GameObject>();

        foreach (PoolItem item in items)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.transform.parent = transform;
                obj.SetActive(false);
                pooledItems.Add(obj);
            }
        }
    }

    public GameObject GetPooledItem(string tag)
    {
        //return an inactive item (gameobject)
        for (int i = 0; i < pooledItems.Count; i++)
        {
            if (!pooledItems[i].activeInHierarchy && pooledItems[i].tag == tag)
                return pooledItems[i];
        }

        //else return a newly instantiated item (gameobj)
        foreach (PoolItem item in items)
        {
            if (item.prefab.tag == tag)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
                return obj;
            }
        }

        return null;
    }
}