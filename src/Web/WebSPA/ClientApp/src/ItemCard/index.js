import React from "react";
import {
  Typography,
  CardContent,
  Card,
  CardMedia,
  CardActions,
  Button,
} from "@material-ui/core";
import { string, number, func } from "prop-types";
import { makeStyles } from "@material-ui/core/styles";
import Price from "../Price";

const useStyles = makeStyles((theme) => ({
  root: {
    margin: `${theme.spacing(2)}px`,
  },
  button: {
    flexGrow: 1,
  },
}));

export default function ItemCard({
  id,
  name,
  description,
  imageUrl,
  price,
  availableStock,
  typeTitle,
  brandName,
  addToCart,
}) {
  const classes = useStyles();

  return (
    <Card className={classes.root}>
      {imageUrl && (
        <CardMedia
          component="img"
          alt={name}
          height="140"
          image={imageUrl}
          title="Item "
        />
      )}
      <CardContent>
        <Typography gutterBottom variant="h5" component="h2">
          {name}
        </Typography>
        <Typography variant="body2" color="textSecondary" component="p">
          {description}
        </Typography>
        <Typography variant="h4" component="p">
          <Price value={price} />
        </Typography>
      </CardContent>
      <CardActions>
        <Button
          className={classes.button}
          variant="contained"
          color="primary"
          onClick={() => {
              addToCart({ id, productName: name, unitPrice: price, quantity: 1, addedAt: Date.now() });
          }}
        >
          Buy
        </Button>
      </CardActions>
    </Card>
  );
}

ItemCard.propTypes = {
  onClick: func,
  id: number,
  name: string.isRequired,
  description: string.isRequired,
  price: number.isRequired,
  imageUrl: string.isRequired,
  availableStock: number.isRequired,
  typeTitle : string.isRequired,
  brandName: string.isRequired
 };

ItemCard.defaultProps = {
  imageUrl: null,
  onClick: Function.prototype,
};
