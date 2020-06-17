import { number, string } from "prop-types";

export default function Price({ value, currency, language }) {
  const priceFormatter = new Intl.NumberFormat(language, {
    style: "currency",
    currency,
  });

  return priceFormatter.format(value);
}

Price.prototype = {
  value: number.isRequired,
  currency: string,
  language: string,
};

Price.defaultProps = {
  currency: "EUR",
  language: "fr-FR",
};
