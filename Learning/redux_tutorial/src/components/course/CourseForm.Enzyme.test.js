import expect from 'expect';
import React from 'react';
import {mount, shallow} from 'enzyme';
import CourseForm from './CourseForm';

function setup(saving = false) {
  const props = {
    course: {}, saving: saving, errors: {}, onSave: () => {}, onChange: () => {}
  };

  return shallow(<CourseForm {...props} />);
}

describe('CourseForm via Enzyme', () => {
  it('renders form and h1', () => {
    const root = setup();
    expect(root.node.type).toBe('form');
    expect(root.childAt(0).node.type).toEqual('h1');
  });

  it('save button is labeled "Save" when not saving', () => {
    const wrapper = setup(false);
    expect(wrapper.find('input').props().value).toBe('Save');
  });

  it('save button is labeled "Saving..." when saving', () => {
    const wrapper = setup(true);
    expect(wrapper.find('input').props().value).toBe('Saving...');
  });
});
