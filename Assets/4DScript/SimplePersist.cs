﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SimplePersist : MonoBehaviour {
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
}
