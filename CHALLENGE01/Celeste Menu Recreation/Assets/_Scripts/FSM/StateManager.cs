using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {
    GameManager manager;
    public enum States {
        Title,
        Menu,
        LevelSelect,
        Credits,
        Exit
    }
    Dictionary<States, State> states = new Dictionary<States, State>();

    public StateManager(GameManager manager) {
        this.manager = manager;
        states[States.Title] = new TitleState(manager.TitleObjects, this);
        states[States.Menu] = new MenuState(manager.MenuObjects, this);
        /*states[States.LevelSelect] = new TypeState(player, this);
        states[States.Credits] = new WashDishesState(player, this);
        states[States.Exit] = new CleanRoomState(player, this);*/
        foreach (State state in states.Values) {
            state.Setup();
        }
    }

    public State Title() {
        return states[States.Title];
    }
}
