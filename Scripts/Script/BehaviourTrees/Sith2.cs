using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sith2 : MonoBehaviour
{
    public Door aDore;
    public Jedi aJedi;
    public GameObject lightsaber;
    public GameObject holocron;
    Task currentTask;
    bool executingBehaviour = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!executingBehaviour)
            {
                executingBehaviour = true;
                currentTask = BuildTask_GetHolocron();

                EventBus.StartListening(currentTask.TaskFinished, OnTaskFinished);
                currentTask.run();
            }
        }
    }

    void OnTaskFinished()
    {
        EventBus.StopListening(currentTask.TaskFinished, OnTaskFinished);
        executingBehaviour = false;
    }

    Task BuildTask_GetHolocron()
    {
        List<Task> taskList = new List<Task>();

        Task isDoorNotLocked = new isFalse(aDore.isLocked);
        Task waitABeat = new Wait(0.5f);
        Task openDoor = new OpenDoor(aDore);
        taskList.Add(isDoorNotLocked);
        taskList.Add(waitABeat);
        taskList.Add(openDoor);
        Sequence openUnlockedDoor = new Sequence(taskList);

        taskList = new List<Task>();
        Task isDoorClosed = new isTrue(aDore.isClosed);
        Task darkside = new DarkSide(this.gameObject);
        Task bargeDoor = new PushDoor(aDore.GetComponent<Rigidbody>());
        Task darksidetwo = new DarkSideTwo(this.gameObject);
        taskList.Add(isDoorClosed);
        taskList.Add(waitABeat);
        taskList.Add(darkside);
        taskList.Add(darksidetwo);
        taskList.Add(waitABeat);
        taskList.Add(bargeDoor);
        Sequence bargeClosedDoor = new Sequence(taskList);

        taskList = new List<Task>();
        taskList.Add(openUnlockedDoor);
        taskList.Add(bargeClosedDoor);
        Selector openTheDoor = new Selector(taskList);

        taskList = new List<Task>();
        Task isJedi = new isTrue(aJedi.isHere);
        Task lightS = new Lightsaber(lightsaber);
        Task kill = new Destroy(aJedi.gameObject);
        Task lightOff = new LightsaberOff(lightsaber);
        Task moveToJedi = new MoveKinematicToObject(this.GetComponent<Kinematic>(), aJedi.gameObject);
        taskList.Add(isJedi);
        taskList.Add(moveToJedi);
        taskList.Add(lightS);
        taskList.Add(waitABeat);
        taskList.Add(kill);
        taskList.Add(waitABeat);
        taskList.Add(lightOff);
        Sequence killJedi = new Sequence(taskList);

        taskList = new List<Task>();
        Task isJediNot = new isFalse(aJedi.isHere);
        taskList.Add(isJediNot);
        Sequence noJedi = new Sequence(taskList);

        taskList = new List<Task>();
        taskList.Add(killJedi);
        taskList.Add(noJedi);
        Selector dieJediDie = new Selector(taskList);

        taskList = new List<Task>();
        Task moveToDoor = new MoveKinematicToObject(this.GetComponent<Kinematic>(), aDore.gameObject);
        Task moveToTreasure = new MoveKinematicToObject(this.GetComponent<Kinematic>(), holocron.gameObject);
        taskList.Add(moveToDoor);
        taskList.Add(waitABeat);
        taskList.Add(openTheDoor);
        taskList.Add(waitABeat);
        taskList.Add(moveToJedi);
        taskList.Add(waitABeat);
        taskList.Add(dieJediDie);
        taskList.Add(waitABeat);
        taskList.Add(moveToTreasure);
        Sequence getTreasureBehindClosedDoor = new Sequence(taskList);

        taskList = new List<Task>();
        Task isDoorOpen = new isFalse(aDore.isClosed);
        taskList.Add(isDoorOpen);
        taskList.Add(moveToJedi);
        taskList.Add(waitABeat);
        taskList.Add(dieJediDie);
        taskList.Add(waitABeat);
        taskList.Add(moveToTreasure);
        Sequence getTreasureBehindOpenDoor = new Sequence(taskList);

        taskList = new List<Task>();
        taskList.Add(getTreasureBehindOpenDoor);
        taskList.Add(getTreasureBehindClosedDoor);
        Selector getTreasure = new Selector(taskList);

        return getTreasure;
    }
}
