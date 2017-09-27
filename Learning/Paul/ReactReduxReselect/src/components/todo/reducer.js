import { combineReducers } from 'redux'
import { GET_TODOS, ADD_TODO, DELETE_TODO, COMPLETE_TODO, TOGGLE_TODO } from './actions-complete'

function todos(state = [], action) {
    switch (action.type) {
        case GET_TODOS:
        {
            return action.todos || state;
        }
        case COMPLETE_TODO:
        {
            let index = state.findIndex(item => item.id === action.id);
            if (index > -1) {
                let todo = state[index];
                let newState = [...state];
                newState[index] = Object.assign({}, todo, { status: "complete" });
                return newState;
            }
        }
        case TOGGLE_TODO:
        {
          let index = state.findIndex(item => item.id === action.id);
          if (index > -1) {
            let toggledTodo = state[index];
            let newStatus = toggledTodo.status === "incomplete" ? "complete" : "incomplete";
            let newState = [...state];
            newState[index] = Object.assign({}, toggledTodo, { status: newStatus });
            return newState;
          }
        }
        default:
            return state
    }
}

const todosReducer = combineReducers({
    todos
});

export default todosReducer
