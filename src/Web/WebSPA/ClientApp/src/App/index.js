import React from "react";
import { CssBaseline, LinearProgress } from "@material-ui/core";
import { ThemeProvider } from "@material-ui/core/styles";
import Header from "../Header";
import ItemList from "../ItemList";
import { useQuery } from "react-query";
import { theme } from "./theme";
import { useList } from "react-use";

import PopinCart from "../PopinCart";
import * as CONF from '../conf.js'

const BuyerId = (Math.round(Math.random() * 100000) + 1).toString();

const fetchItems = () => {
    const baseUrlApi =
        process.env.REACT_APP_BASE_URL_API || CONF.API_GATEWAY;

    return fetch(`${baseUrlApi}/api/v1/catalog/items`).then((response) => response.json());
};

const fetchCart = () => {
    const baseUrlApi =
        process.env.REACT_APP_BASE_URL_API || CONF.API_GATEWAY;
    return fetch(`${baseUrlApi}/api/v1/baskets/${BuyerId}`).then((response) => response.json());
};

export default function App() {
  const { status: itemsStatus, data: items } = useQuery('Items', fetchItems);
  const { status: remoteCartStatus, data: remoteCart } = useQuery('Cart', fetchCart, { initialData: { buyerId: BuyerId, items: [] } });
  const [ localCart, { push, reset }] = useList([]);
  const [popinCartOpen, setPopinCartOpen] = React.useState(false);
  
  const showPopinCart = () => setPopinCartOpen(true);
  const hidePopinCart = () => setPopinCartOpen(false);

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Header
        shoppingCartCount={localCart.length}
        displayPopinCart={showPopinCart}
          />
    {(itemsStatus === "loading" || remoteCartStatus === "loading") && <LinearProgress />}
    {itemsStatus !== "loading" && <ItemList data={items} addToCart={ push } />}
    <PopinCart
        open={popinCartOpen}
        hidePopinCart={hidePopinCart}
        reset={reset}
        cart={localCart}
        buyerId={BuyerId}
    />
    </ThemeProvider>
  );
}
