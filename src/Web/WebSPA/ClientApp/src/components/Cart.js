import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class Cart extends Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            basket: { buyerId: this.props.buyerId, items:[] }
        }
        this.removeBasketItem = this.removeBasketItem.bind(this);
    }

    componentDidMount() {
        this.populateBasketData(`api/v1/baskets/${this.props.buyerId}`)
            .then(() => this.addProduct());        
    }

    async removeBasketItem(productId) {
        let basket = this.state.basket;
        let index = basket.items.findIndex(basketItem => basketItem.productId === productId);

        if (index >= 0) {
            basket.items.splice(index, 1);
            
            if (!basket.items.length) {
                await fetch(`api/v1/baskets/${this.props.buyerId}`, { 'method': 'delete' });
                await this.setState({ basket: basket, loading: false });
            }
            else {
                await this.updateBasket(basket);
            }
        }        
    }

    renderItemsTable(basket) {
        return basket.items.length ? (
            <table class='table'>
                <thead>
                <tr>
                    <th>Product name</th>
                    <th>Unit price</th>
                    <th>Quantity</th>
                    <th></th>
                 </tr>
                </thead>
                <tbody>
                {basket.items.map(item =>
                    <tr>
                        <td>{item.productName}</td>
                        <td>{item.unitPrice}</td>
                        <td>{item.quantity}</td>
                        <td><button onClick={() => this.removeBasketItem(item.productId)} className='btn' >X</button></td>
                    </tr>
                )}
                </tbody>
                <tfoot>
                </tfoot>
            </table>
        ) : (
          <p>Empty cart, no way, <Link to='/'>let's fill it</Link> !</p>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderItemsTable(this.state.basket);

        return (
            <div>
                <h1>Your cart</h1>
                {contents}
            </div>
        );
    }

    async populateBasketData(request, params) {
        const response = await fetch(request, params);

        try {
            await this.setState({
                basket: await response.json(),
                loading: false
            });
        }
        catch (e) {
            console.log(e);
        }        
    }
    async updateBasket(basket) {
        await this.populateBasketData(`api/v1/baskets`, {
            method: 'post',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(basket)
        });
    }
    async addProduct() {
        if (this.props.productId) {
            let productId = parseInt(this.props.productId);
            let basket = this.state.basket;
            let index = basket.items.findIndex(basketItem => basketItem.productId === productId);

            if (index < 0) {
                basket.items.push({ productId: productId, quantity: 1 });
            }
            else {
                basket.items[index].quantity++;
            }
            await this.updateBasket(basket);
        }
    }
}
