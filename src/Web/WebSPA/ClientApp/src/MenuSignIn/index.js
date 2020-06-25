import React from "react";
import { func } from "prop-types";
import {
    Menu,
    MenuItem,
    Divider,
    Tooltip,
    IconButton,
} from "@material-ui/core";
import PersonOutline from "@material-ui/icons/PersonOutline";

export default function MenuSignIn({ login }) {
    const [anchorEl, setAnchorEl] = React.useState(null);

    return (
        <div>
            <Tooltip title="Register / Sign In">
                <IconButton
                    aria-label="items"
                    color="inherit"
                    onClick={e => setAnchorEl(e.currentTarget)}
                >
                    <PersonOutline />
                </IconButton>
            </Tooltip>
            <Menu
                id="simple-menu"
                anchorEl={anchorEl}
                keepMounted
                open={Boolean(anchorEl)}
                onClose={() => setAnchorEl(null)}
            >
                <MenuItem disabled>Register</MenuItem>
                <Divider />
                <MenuItem onClick={e => { console.log(login); login(); setAnchorEl(null); }}>Sign in</MenuItem>
            </Menu>
        </div>
    );
}

MenuSignIn.propTypes = {
    login: func
};

MenuSignIn.defaultProps = {
    login: Function.prototype
};
