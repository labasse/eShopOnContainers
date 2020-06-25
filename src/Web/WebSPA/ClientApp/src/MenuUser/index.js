import React from "react";
import { useState } from 'react';
import { string, func } from "prop-types";
import {
  Menu,
  MenuItem,
  Divider,
  IconButton,
  Tooltip,
  Badge
} from "@material-ui/core";
import AccountCircle from "@material-ui/icons/AccountCircle";

export default function MenuUser({ userWrap, username, showOrders, logout }) {
    const [anchorEl, setAnchorEl] = React.useState(null);
      
  return (
    <div>
      <Tooltip title={ username }>
        <IconButton
            aria-label="items"
            color="inherit"
            onClick={ e => setAnchorEl(e.currentTarget) }
        >
          <Badge badgeContent={ username } color="secondary">
            <AccountCircle />
          </Badge>
        </IconButton>
      </Tooltip>
      <Menu
          id="simple-menu"
          anchorEl={anchorEl}
          keepMounted
          open={Boolean(anchorEl)}
          onClose={ () => setAnchorEl(null) }
       >
          <MenuItem onClick={e => { showOrders(); setAnchorEl(null); }}>My orders</MenuItem>
          <Divider/>
          <MenuItem onClick={e => { logout(); setAnchorEl(null); }}>Log out</MenuItem>
      </Menu>
    </div>
  );
}

MenuUser.propTypes = {
  login: string.isRequired,
    userWrap: func.isRequired,
    username: func.isRequired,
  showOrders: func,
  logout: func
};

MenuUser.defaultProps = {
  showOrders: Function.prototype,
  logout: Function.prototype
};
