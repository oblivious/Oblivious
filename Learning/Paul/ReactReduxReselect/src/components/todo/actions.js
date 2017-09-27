import jsontodos from './todos.json';
export const GET_TODOS = 'GET_TODOS';
export const ADD_TODO = 'ADD_TODO';
export const DELETE_TODO = 'DELETE_TODO';
export const COMPLETE_TODO = 'DELETE_TODO';

export function getTodos(todos) {
    return {
        type: GET_TODOS,
        todos
    }
}

export function addTodo(todo) {
    return {
        type: ADD_TODO,
        todo
    }
}

export function deleteTodo(id) {
    return {
        type: DELETE_TODO,
        id
    }
}

export function completeTodo(id) {
    return {
        type: COMPLETE_TODO,
        id
    }
}

export function fetchGetTodos() {
    return (dispatch, getState) => {
        return Promise.resolve(jsontodos)
            .then(data => dispatch(getTodos(jsontodos)));
    }
}