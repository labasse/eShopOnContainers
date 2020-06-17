import React from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
} from "@material-ui/core";
import { bool, node, string, arrayOf, func } from "prop-types";

export default function Popin({ open, title, children, actions, onClose }) {
  return (
    <Dialog
      open={open}
      onClose={onClose}
      aria-labelledby="alert-dialog-title"
      aria-describedby="alert-dialog-description"
    >
      <DialogTitle id="alert-dialog-title">{title}</DialogTitle>
      <DialogContent id="alert-dialog-description">{children}</DialogContent>
      <DialogActions>
        {actions.map(({ label, onClick, primary }) => (
          <Button
            key={label}
            color={primary ? "primary" : "secondary"}
            onClick={onClick}
          >
            {label}
          </Button>
        ))}
      </DialogActions>
    </Dialog>
  );
}

Popin.propType = {
  open: bool,
  title: string,
  description: string,
  actions: arrayOf({
    label: string,
    onClick: func,
  }),
  onClose: func,
};

Popin.defaultProps = {
  open: false,
  title: null,
  description: node.isRequired,
  actions: [],
  onClose: Function.prototype,
};
