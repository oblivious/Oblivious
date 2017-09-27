import React, { Component } from 'react';
import './styles.scss';
import config from 'config';

class Main extends Component {
    render() {
        console.log("I'm rendering");
        return (
            <div className="page">
                <img src="/img/react.png" height="300" />
                <h1 className="page__title">Hello world! Your environment is on {config.env} </h1>
                {this.props.children}
            </div>
        )
    }
}

export default Main
