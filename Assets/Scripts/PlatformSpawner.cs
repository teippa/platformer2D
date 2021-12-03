using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{


    private PlatformPoolController[] _platformStacks;

    private int _activePlatformInd;

    // Start is called before the first frame update
    void Start()
    {

        _platformStacks = GetComponentsInChildren<PlatformPoolController>(true);
        Debug.Log(_platformStacks.Length);

        _activePlatformInd = SelectNextPlatformInd();

    }

    // Update is called once per frame
    void Update()
    {
        if (!_platformStacks[_activePlatformInd].inUse)
        {
            _platformStacks[_activePlatformInd].EnablePlatforms();
        }
        
        if (_platformStacks[_activePlatformInd].transform.position.y <= 0)
        {
            _activePlatformInd = SelectNextPlatformInd();
            //Debug.Log(_platformStacks[activePlatformInd].name);

            _platformStacks[_activePlatformInd].EnablePlatforms();
        }

        

    }

    private int SelectNextPlatformInd()
    {
        int nextPlatformInd;

        do {
            nextPlatformInd = Mathf.FloorToInt(Random.Range(0.002f, _platformStacks.Length-0.001f));
        } while (_platformStacks[nextPlatformInd].inUse);
        //*/
        
        return nextPlatformInd;
    }
}
