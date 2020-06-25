import React from "react";
import { useState } from 'react';
import {
  makeStyles,
  AppBar,
  Toolbar,
  Typography,
  IconButton,
    Badge
} from "@material-ui/core";

import MenuSignIn from "../MenuSignIn";
import MenuUser from "../MenuUser";
import ShoppingCartIcon from "@material-ui/icons/ShoppingCart";
import { func, string, number } from "prop-types";

const useStyles = makeStyles({
  title: {
    flexGrow: 1,
  },
});

export default function Header({
    shoppingCartCount, displayPopinCart,
    userWrap, username, login, showOrders, logout
}) {
    const classes = useStyles();
    
    return (
        <AppBar position="sticky">
            <Toolbar>
            <Typography variant="h6" className={classes.title}>
                        eShopOnContainers
            </Typography>
                {username && <MenuUser userWrap={userWrap} username={username} showOrders={showOrders} logout={logout} />}
                {!username && <MenuSignIn login={login} />}
            <IconButton
                aria-label={`${shoppingCartCount} items`}
                color="inherit"
                onClick={displayPopinCart}
            >
                <Badge badgeContent={shoppingCartCount} color="secondary">
                <ShoppingCartIcon />
                </Badge>
            </IconButton>
            </Toolbar>
        </AppBar>
    );
}

Header.propTypes = {
  username: string,
  showOrders: func,
  logout: func,
  shoppingCartCount: number,
  DisplayPopinCart: func,
};

Header.defaultProps = {
  showOrders: Function.prototype,
  logout: Function.prototype,
  shoppingCartCount: 0,
  DisplayPopinCart: Function.prototype,
};
