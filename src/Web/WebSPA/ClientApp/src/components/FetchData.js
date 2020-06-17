import React, { Component } from 'react';
import { CatalogItem } from './CatalogItem';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { items: [], loading: true };
  }

  componentDidMount() {
      this.populateCatalogData();
  }

  static renderItemsTable(items) {
    return (
        <div class='row'>
            {items.map(item =>
                <CatalogItem data={item} />
          )}
        </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderItemsTable(this.state.items);

    return (
      <div>
        <h1 id="tabelLabel" >Our catalog</h1>
        {contents}
      </div>
    );
  }

  async populateCatalogData() {
    const response = await fetch('api/v1/catalog/items');
    const data = await response.json();
    this.setState({ items: data, loading: false });
  }
}
