import React from 'react';
import { Router, Route, IndexRoute, browserHistory } from 'react-router'
import { syncHistoryWithStore } from 'react-router-redux';
import store from './store';
import Main from 'pages/main';
import TodoPage from 'pages/todo-page';

const history = syncHistoryWithStore(browserHistory, store);

export default function Routes(props) {
  return (
    <Router history={history} >      
        <Route path="/" component={Main} title="React and Redux">
           <Route path="todos" component={TodoPage} title="TodoPage" />          
        </Route>       
    </Router>
  );
}
