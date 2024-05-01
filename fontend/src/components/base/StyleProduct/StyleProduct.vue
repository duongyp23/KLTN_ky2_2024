<template>
  <div class="style-product">
    <div
      class="product-img"
      :style="'background-image : url(' + getFirstImage() + ')'"
    ></div>
    <div class="product-info col-flex mt-1">
      <div class="product-name">
        {{ product.product_name }} - {{ product.product_code }}
      </div>
      <div class="product-price">
        <div class="title">Giá thuê:</div>
        <div class="amount">
          {{ replaceNumber(product.rental_price_day) }} đ
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { replaceNumber } from "@/method/methodFormat";
export default {
  data() {
    return {};
  },
  props: {
    product: Object,
  },
  computed: {},
  methods: {
    replaceNumber,
    getFirstImage() {
      let arrImg = this.product.product_image_url.split(";");
      return arrImg[0];
    },
  },
  mounted() {},
  created() {},
};
</script>
<style>
@import url(./styleProduct.scss);
</style>