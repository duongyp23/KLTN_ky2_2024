<template>
  <div class="dictionary-page">
    <CategoryTable class="left-page" :show-button="true"></CategoryTable>
    <ProductList class="right-page"></ProductList>
  </div>
</template>
<script>
import ProductList from "../ListProduct/ProductList.vue";
import CategoryTable from "../ListCategory/CategoryTable.vue";
export default {
  data() {
    return {};
  },
  components: { ProductList, CategoryTable },
};
</script>
<style>
@import url(./dictionaryPage.scss);
</style>