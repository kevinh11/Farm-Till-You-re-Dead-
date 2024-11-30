using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pertumbuhan : MonoBehaviour
{
    public GameObject[] plants;
    private IEnumerator prosesPertumbuhanCoroutine;

    void Start()
    {
        // Set all plants to inactive at the start
        foreach (GameObject plant in plants)
        {
            plant.SetActive(false);
        }

        prosesPertumbuhanCoroutine = PertumbuhanProses(2.0f, 0);
        StartCoroutine(prosesPertumbuhanCoroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PertumbuhanProses(float time, int proses)
    {
        while (proses <= 2)
        {
            GameObject obj = plants[proses];
            obj.SetActive(true);
            yield return new WaitForSeconds(time);
            if(proses != 2){
                obj.SetActive(false);
            }
            proses++;
        }
    }
}


// ketika player menekan F, maka fungsi Pertumbuhan.cs akan berjalan
// referensi:
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Pertumbuhan : MonoBehaviour
// {
//     public GameObject parentObject;
//     private GameObject[] plants;
//     private IEnumerator prosesPertumbuhanCoroutine;

//     void Start()
//     {
//         // Get all child GameObjects of the parentObject
//         plants = GetChildGameObjects(parentObject);

//         // Set all plants to inactive at the start
//         foreach (GameObject plant in plants)
//         {
//             plant.SetActive(false);
//         }

//         prosesPertumbuhanCoroutine = PertumbuhanProses(2.0f, 0);
//         StartCoroutine(prosesPertumbuhanCoroutine);
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     private IEnumerator PertumbuhanProses(float time, int proses)
//     {
//         while (proses <= 2)
//         {
//             GameObject obj = plants[proses];
//             obj.SetActive(true);
//             yield return new WaitForSeconds(time);
//             if(proses != 2){
//                 obj.SetActive(false);
//             }
//             proses++;
//         }
//     }

//     private GameObject[] GetChildGameObjects(GameObject parent)
//     {
//         List<GameObject> children = new List<GameObject>();
//         foreach (Transform child in parent.transform)
//         {
//             children.Add(child.gameObject);
//         }
//         return children.ToArray();
//     }
// }