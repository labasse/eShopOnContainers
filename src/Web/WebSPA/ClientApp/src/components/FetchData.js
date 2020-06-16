import React, { Component } from 'react';

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
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Price</th>
            <th>Brand name</th>
          </tr>
        </thead>
        <tbody>
          {items.map(item =>
              <tr key={item.date}>
                  <td>{item.id}</td>
                  <td>{item.name}</td>
                  <td>{item.price}</td>
                  <td>{item.brandName}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderItemsTable(this.state.items);

    return (
      <div>
        <h1 id="tabelLabel" >Our catalog</h1>
        <p>This component demonstrates fetching data from the server.</p>
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
