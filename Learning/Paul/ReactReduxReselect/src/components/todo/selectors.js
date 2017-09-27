import { createSelectorCreator, defaultMemoize } from 'reselect';
import { memoize }  from 'lodash';

const getTodos = state => state.todos.todos;

const hashFn = (...args) => {
  var result = args.reduce((acc, val) => acc + '-' + JSON.stringify(val), '');
  console.log("cache-key: " + result);
  return result;
};

const cachingSelectorCreator = createSelectorCreator(memoize, hashFn);

const getTodoStatuses = cachingSelectorCreator(
  getTodos,
  (todos) => todos.map(todo => todo.status)
);

export const getCompletedTodosCount = cachingSelectorCreator(
  getTodoStatuses,
  (statuses) => {
    var completed = statuses.filter(status => status === 'complete');
    return completed.length;
  }
);

export const getTodosCount = cachingSelectorCreator(
  getTodos,
  (todos) => todos.length
);
