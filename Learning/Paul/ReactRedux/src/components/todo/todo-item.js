import React, { Component } from 'react';
import { PropTypes } from 'prop-types'
import { completeTodo } from './actions-complete';
import { connect } from 'react-redux';
import './styles.scss';

class TodoItem extends Component {
  constructor(props) {
    super(props);
  }

  setComplete = (id) => {
      const { completeTodo } = this.props;
      completeTodo(id)
  }

  componentDidMount() {    
  }

  render() {
    return (<li className={`todos__item todos__item--${this.props.status}`} key={this.props.id} onClick={() => this.setComplete(this.props.id)}>{this.props.name}</li>);
  }
}

TodoItem.propTypes = {
  id: PropTypes.number.isRequired,
  name: PropTypes.string.isRequired,
  status: PropTypes.string.isRequired
}


const mapDispatchToProps = (dispatch, ownProps) => {   
    return {        
        completeTodo: (id) => {
            dispatch(completeTodo(id));
        }
    }
}

export default connect(null, mapDispatchToProps)(TodoItem)