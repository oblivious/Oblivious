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
            <div>
              <ul className="todos">
                  {todos}
              </ul>
              <span>Completed todos: {this.props.completedTodosCount}/{this.props.todosCount}</span>
            </div>);
    }
}

Todos.propTypes = {
    todos: PropTypes.array
}

export default Todos;
