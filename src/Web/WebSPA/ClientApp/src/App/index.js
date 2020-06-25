import React from "react";
import Oidc from "oidc-client";
import { useList } from "react-use";
import { useState } from "react";
import { CssBaseline, LinearProgress } from "@material-ui/core";
import { ThemeProvider } from "@material-ui/core/styles";
import Header from "../Header";
import ItemList from "../ItemList";
import { useQuery } from "react-query";
import { theme } from "./theme";
import { v4 as uuidv4 } from 'uuid'

import PopinCart from "../PopinCart";
import * as CONF from '../conf.js'

const fetchItems = () => {
    const baseUrlApi =
        process.env.REACT_APP_BASE_URL_API || CONF.API_GATEWAY;

    return fetch(`${baseUrlApi}api/v1/catalog/items`).then((response) => response.json());
};

const fetchCart = () => {
    const baseUrlApi =
        process.env.REACT_APP_BASE_URL_API || CONF.API_GATEWAY;
    return fetch(`${baseUrlApi}api/v1/baskets/${BuyerId}`).then((response) => response.json());
};

var BuyerId = uuidv4();

export default function App() {
    const { status: itemsStatus, data: items } = useQuery('Items', fetchItems);
    const { status: remoteCartStatus } = useQuery('Cart', fetchCart, { initialData: { buyerId: BuyerId, items: [] } });
    const [localCart, { push, reset }] = useList([]);
    const [popinCartOpen, setPopinCartOpen] = useState(false);
    const [username, setUsername] = useState(null);

    const updateUser = () => {
        mgr.getUser().then(
            u => setUsername(u ? u.profile.name : null),
            err => console.log(err)
        );
    };

    const showPopinCart = () => setPopinCartOpen(true);
    const hidePopinCart = () => setPopinCartOpen(false);
    
    const mgr = new Oidc.UserManager({
        authority: CONF.IDENTITY_SERVER,
        client_id: CONF.IDENTITY_CLIENTID,
        redirect_uri: CONF.API_GATEWAY,
        response_type: "id_token token",
        scope: "openid profile",
        post_logout_redirect_uri: CONF.API_GATEWAY,
    });

    const login  = () => { try { mgr.signinRedirect();  } catch (err) { console.log(err); } };
    const logout = () => { try { mgr.signoutRedirect(); } catch (err) { console.log(err); } };  
    const showOrders = () => {
  
    }

    try {
        mgr.signinRedirectCallback().then(
            updateUser,
            err => updateUser()
        );
    }
    catch (error) {
        
    }

    return (
    <ThemeProvider theme={theme}>
        <CssBaseline />
        <Header
            shoppingCartCount={localCart.length}
            displayPopinCart={showPopinCart}
             username={username}
            login={login}
            logout={logout}
            showOrders={showOrders}
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
