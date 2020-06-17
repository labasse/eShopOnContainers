import { arrayOf, shape, func } from "prop-types";
import { isNilOrEmpty } from "ramda-adjunct";
import { Typography, Box } from "@material-ui/core";
import Grid from "@material-ui/core/Grid";
import ItemCard from "../ItemCard";
import React from "react";
import * as CONF from '../conf';

export default function ItemList({ data, addToCart }) {
  if (isNilOrEmpty(data))
    return (
      <Typography variant="body1" component="p">
        Pas de Item en ce moment...
      </Typography>
    );
  console.log(data);
  return (
      <Grid container justify="center" justify="space-between">
          <Box xs={12}>
              <img src={`${CONF.CDN_URL}/img/main_banner.png`} />
          </Box>
        {data.map(({ id, name, description, pictureFileName, price, availableStock, typeTitle, brandName }) => (
        <Grid key={id} item xs={3} xl={2}>
          <ItemCard
            name={name}
            description={description}
            imageUrl={`${CONF.CDN_URL}/items/${pictureFileName}`}
            price={price}
            availableStock={availableStock}
            typeTitle={typeTitle}
            brandName={brandName}
            addToCart={addToCart}
          />
        </Grid>
      ))}
    </Grid>
  );
}

ItemList.protypes = {
  data: arrayOf(shape(ItemCard.propTypes)),
  addToCart: func,
};

ItemList.defaultProps = {
  data: [],
  addToCart: Function.prototype,
};
