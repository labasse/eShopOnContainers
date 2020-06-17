import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { FetchData } from './components/FetchData';
import { Cart } from './components/Cart';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

    render() {        
    return (
        <Layout>
            <Route exact path='/' component={FetchData} />
            <Route path='/cart/:productId?' render={(props) => <Cart buyerId={this.props.buyerId} productId={props.match.params.productId} />} />
        </Layout>
    );
  }
}
