import { routerReducer } from 'react-router-redux'
import { combineReducers } from 'redux';
import todosReducer from 'components/todo/reducer'

const reducer = combineReducers(
    {
        routing: routerReducer,
        todos: todosReducer
    }
);

export default reducer;
