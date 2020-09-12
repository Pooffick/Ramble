using UnityEngine;
using System.Collections;

public class RandomPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float t = Random.Range(0f, 1f);
	    foreach(BlockMover Block in GetComponentsInChildren<BlockMover>())
        {
            Block.timer = t;
        }
	}
}
