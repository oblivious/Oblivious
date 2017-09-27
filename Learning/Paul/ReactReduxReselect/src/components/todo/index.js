import React from 'react';
import { connect } from 'react-redux';
import Todos from './todos';
import { getCompletedTodosCount, getTodosCount } from './selectors';
import { fetchGetTodos } from './actions';

const mapStateToProps = (state) => {
    const { todos } = state.todos;
    return {
      todos,
      completedTodosCount: getCompletedTodosCount(state),
      todosCount: getTodosCount(state)
    };
}

const mapDispatchToProps = (dispatch, ownProps) => {
    return {
        onMount: () => { dispatch(fetchGetTodos()) }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Todos);
