import 'babel-polyfill';
import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import Routes from './routes';
import store from './store';
import "sass/styles.scss";

let renderApp = () => {    
    ReactDOM.render(
        <Provider store={store}>
            <Routes />
        </Provider>
        , document.getElementById('app')
    );
};

let render;

if (module.hot) {
    console.log("hot reload enabled");
    const renderError = (error) => {
        const RedBox = require("redbox-react");
        ReactDOM.render(<RedBox error={error} />, document.getElementById('app'));
    };

    render = () => {
        try {
            renderApp();
        }
        catch (error) {
            renderError(error);
        }
    };

    render();
    module.hot.accept();
}
else {
    console.log("hot reload disabled");
    renderApp();
}