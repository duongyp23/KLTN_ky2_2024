<template>
  <div class="style-product">
    <div
      class="product-img"
      :style="'background-image : url(' + product.product_image_url + ')'"
    ></div>
    <div class="product-info col-flex mt-1">
      <div class="product-name">{{ product.product_name }}</div>
      <div class="product-price">
        <div class="title">GIÁ BÁN</div>
        <div class="amount">{{ replaceNumber(product.sell_price) }} đ</div>
      </div>
      <div class="product-price">
        <div class="title">GIÁ THUÊ</div>
        <div class="amount">{{ replaceNumber(product.rental_price) }} đ</div>
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
  },
  watch: {},
  mounted() {},
  created() {},
};
</script>
<style>
@import url(./styleProduct.scss);
</style>