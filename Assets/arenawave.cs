using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arenawave : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] spawns;
    [SerializeField] private int maxenemies;
    [SerializeField] private int enemiestokill;
    [SerializeField] int children;
    [SerializeField] GameObject parent;
    [SerializeField] Text text;
    int prevchild = 0;
    private void Update()
    {
        children = 0;
        foreach (Transform child in transform)
        {
            children += 1;
        }
        if (children < prevchild)
        {
            enemiestokill -= 1;
        }
        if(children < maxenemies && enemiestokill > 0)
        {
            createenemy();
            
        }
        text.text = $"{enemiestokill}";
        prevchild = children;
        if(enemiestokill <= 0)
        {
            Destroy(parent);
        }
    }
    void createenemy()
    {
        GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], spawns[Random.Range(0, spawns.Length)].transform.position, transform.rotation);
        enemy.transform.parent = transform;
    }
}
