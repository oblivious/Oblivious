import { createStore, applyMiddleware, compose } from 'redux';
import { browserHistory } from 'react-router';
import { routerMiddleware } from 'react-router-redux';
import thunkMiddleware from 'redux-thunk'
import reducer from './global-reducer';

const loggerMiddleware = (store) => (next) => (action) => {
    console.log("Action:", action);
    console.log("State before:", store.getState());
    next(action);
    console.log("State after:", store.getState());
}

//Immutability can begin here
const initialState = {};

const createStoreWithMiddleware = compose(
    applyMiddleware(thunkMiddleware, loggerMiddleware, routerMiddleware(browserHistory))
)(createStore);

// using redux-devtools Chrome app (https://github.com/zalmoxisus/redux-devtools-extension)
const store = createStoreWithMiddleware(
    reducer,
    initialState,
    window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
);

export default store;
