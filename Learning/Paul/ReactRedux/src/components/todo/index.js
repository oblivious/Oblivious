import React from 'react';
import { connect } from 'react-redux';
import Todos from './todos';
import { fetchGetTodos } from './actions';

const mapStateToProps = (state) => {
    const { todos } = state.todos;
    return { todos }
}

const mapDispatchToProps = (dispatch, ownProps) => {
    return {
        onMount: () => {
            dispatch(fetchGetTodos())
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Todos)