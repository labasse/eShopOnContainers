import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class CatalogItem extends Component {
    constructor(props) {
        super(props);
        this.data = this.props.data;
    }

    render() {
        return (
            <div class="card">
                <img class="card-img-top" src={`http:localhost:8080/items/${this.data.pictureFileName}`} alt={ this.data.name }/>
                <div class="card-body">
                    <h5 class="card-title">{ this.data.name }</h5>
                    <p class="card-text">{this.data.description}</p>
                    <Link to={`cart/${this.data.id}`} class="btn btn-primary">Add to cart</Link>
                </div>
            </div>
        );
    }
}
