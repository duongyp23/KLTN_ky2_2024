<template>
  <div class="style-product">
    <div
      class="product-img"
      :style="'background-image : url(' + product.image_url + ')'"
    ></div>
    <div class="product-info">
      <div class="product-name">{{ pro }}</div>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {};
  },
  props: {
    product: Object,
  },
  computed: {},
  methods: {},
  watch: {},
  mounted() {},
  created() {},
};
</script>
<style>
@import url(./styleProduct.scss);
</style>