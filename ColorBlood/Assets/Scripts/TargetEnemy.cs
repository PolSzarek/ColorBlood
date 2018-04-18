using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemy : MonoBehaviour {

    public List<Transform> targets;
    public Transform selectedTarget;
    public GameObject MainSelector;
    public GameObject PreviousSelector;
    public GameObject NextSelector;

    private Transform myTransform;
    private bool switched;

    // Use this for initialization
	void Start () {
        targets = new List<Transform>();
        selectedTarget = null;
        switched = false;

        myTransform = GetComponent<Transform>();

        AddAllEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxisRaw("RightHorizontal");

        if (Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            if (selectedTarget == null)
            {
                Target();
            }
            else
            {
                UnsetTarget();
            }
        } else if (selectedTarget && (input > 0.95 || input < -0.95) && !switched)
        {
            switched = true;
            SwitchTarget(input > 0);
        } else if (input < 0.95 && input > -0.95)
        {
            switched = false;
        }
    }

    public void AddAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            AddTarget(enemy.transform);
        }
    }
	
    public void AddTarget(Transform enemy)
    {
        targets.Add(enemy);
    }

    public void RemoveTarget(Transform enemy)
    {
        if (enemy == selectedTarget)
        {
            SortTargetsByDistance();
            int id = targets.IndexOf(selectedTarget);
            if (id != 0 && targets.Count != 1)
                SetTarget(targets[id - 1]);
            else if (targets.Count != 1)
                SetTarget(targets[id + 1]);
            else
                UnsetTarget();
        }
        targets.Remove(enemy);
    }

    private void SwitchTarget(bool closer)
    {
        if (targets.Count > 1)
        {
            SortTargetsByDistance();
            int id = targets.IndexOf(selectedTarget);
            if (closer)
            {
                SetTarget(targets[((id - 1) >= 0) ? (id - 1) : (targets.Count - 1)]);
            } else
            {
                SetTarget(targets[((id + 1) < targets.Count) ? (id + 1) : 0]);
            }
        }
    }

    private void Target()
    {
        if (selectedTarget == null)
        {
            SortTargetsByDistance();
            if (targets.Count > 0)
            {
                SetTarget(targets[0]);
            }
        }
    }

    private void SetTarget(Transform target)
    {
        if (selectedTarget)
            UnsetTarget();
        selectedTarget = target;
        selectedTarget.Rotate(0, 45, 0);
    }

    private void UnsetTarget()
    {
        selectedTarget.Rotate(0, -45, 0);
        selectedTarget = null;
        MainSelector.transform.position = new Vector3(0, -10, 0);
        PreviousSelector.transform.position = new Vector3(0, -10, 0);
        NextSelector.transform.position = new Vector3(0, -10, 0);
    }

    private void SortTargetsByDistance()
    {
        targets.Sort(delegate (Transform target1, Transform target2)
        {
            Vector3 origin = myTransform.position;
            if (selectedTarget)
                origin = selectedTarget.position;
            return (Vector3.Distance(target1.position, origin).CompareTo(Vector3.Distance(target2.position, origin)));
        });
        if (selectedTarget && targets.Count > 2)
        {
            int id = targets.IndexOf(selectedTarget);
            MainSelector.transform.position = selectedTarget.position;
            PreviousSelector.transform.position = targets[(id - 1 + targets.Count) % targets.Count].position;
            NextSelector.transform.position = targets[(id + 1) % targets.Count].position;
        }
    }
}