import React, { Component } from 'react';
import Todos from 'components/todo';
import './styles.scss';

class TodoPage extends Component {
    render() {
        return (
            <div className="todos">
                <h1 className="todos__title">Your Todos</h1>
                <Todos />
            </div>
        )
    }
}

export default TodoPage
