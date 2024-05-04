using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : SingleTon<DamageTextManager>
{
    public ObjectPooling pooling {  get; set; }

    private void Start()
    {
        pooling = GetComponent<ObjectPooling>();
    }
}
