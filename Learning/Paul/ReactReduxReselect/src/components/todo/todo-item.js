import React, { Component } from 'react';
import { PropTypes } from 'prop-types'
import { toggleTodo } from './actions-complete';
import { connect } from 'react-redux';
import './styles.scss';

class TodoItem extends Component {
  constructor(props) {
    super(props);
  }

  setComplete = () => {
      const { toggleTodo, id } = this.props;
      toggleTodo(id)
  }

  componentDidMount() {
  }

  render() {
    return (
      <li className={`todos__item todos__item--${this.props.status}`}
          key={this.props.id}
          onClick={this.setComplete}>
        <span>{this.props.name}</span>
      </li>
    );
  }
}

TodoItem.propTypes = {
  id: PropTypes.number.isRequired,
  name: PropTypes.string.isRequired,
  status: PropTypes.string.isRequired
}


const mapDispatchToProps = (dispatch, ownProps) => {
    return {
        toggleTodo: (id) => {
            dispatch(toggleTodo(id));
        }
    }
}

export default connect(null, mapDispatchToProps)(TodoItem)
