import { combineReducers } from 'redux'
import { GET_TODOS, ADD_TODO, DELETE_TODO, COMPLETE_TODO } from './actions-complete'

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
                return [...state.slice(0, index), Object.assign({}, todo, { status: "complete" }), ...state.slice(index + 1)];
            }    
        }
        default:
            return state
    }
}

const todosReducer = combineReducers({
    todos
})

export default todosReducer