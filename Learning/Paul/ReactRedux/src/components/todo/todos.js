import React, { Component } from 'react';
import { PropTypes } from 'prop-types'
import TodoItem from './todo-item';
import './styles.scss';

class Todos extends Component {
    constructor(props) {
        super(props);
    }

    changeState = () => {

    }

    componentDidMount() {
        const { onMount } = this.props;
        console.log(this.props.todos)
        
        onMount();
    }   

    render() {
         let todos = this.props.todos.map(todo => (<TodoItem key={todo.id} {...todo} />))

        return (
            <ul className="todos">
                {todos}
            </ul>);
    }
}

Todos.propTypes = {
    todos: PropTypes.array
}

export default Todos;
