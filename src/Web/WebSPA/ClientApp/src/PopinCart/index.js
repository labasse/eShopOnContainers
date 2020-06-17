import { bool, func, arrayOf, shape, number, string } from "prop-types";
import { isNotEmpty } from "ramda-adjunct";
import Popin from "../Popin";
import Price from "../Price";
import React from "react";
import {
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@material-ui/core";

export default function PopinCart({ open, hidePopinCart, cart, reset }) {
  const actions = [
    { label: "Cancel", onClick: hidePopinCart },
    {
      label: "Checkout",
      primary: true,
      onClick: () => {
        reset();
        hidePopinCart();
      },
    },
  ];

  return (
    <Popin
      open={open}
      onClose={hidePopinCart}
      title="Checkout"
      actions={actions}
    >
      {isNotEmpty(cart) ? (
        <TableContainer component={Paper}>
          <Table size="small" aria-label="My items">
            <TableHead>
              <TableRow>
                <TableCell>Product</TableCell>
                <TableCell align="right">Price</TableCell>
                <TableCell align="right">Quantity</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {cart.map(({ id, productName, quantity, unitPrice }) => (
                <TableRow key={id}>
                  <TableCell component="th" scope="row">
                    {productName}
                  </TableCell>
                  <TableCell align="right">
                    <Price value={unitPrice} />
                  </TableCell>
                  <TableCell align="right">
                      {quantity}
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      ) : (
        "No product selected"
      )}
    </Popin>
  );
}

PopinCart.propType = {
  open: bool,
  hidePopinCart: func,
  cart: shape({
      buyerId: string,
      items: arrayOf(
          shape({
              id: number,
              productName: string,
              quantity: number,
              unitPrice: number
          })
      )
  }),
  reset: func,
};

PopinCart.defaultProps = {
  open: false,
  hidePopinCart: Function.prototype,
  reset: Function.prototype,
  cart: {
    buyerId: "",
    items:[]
  }
};
