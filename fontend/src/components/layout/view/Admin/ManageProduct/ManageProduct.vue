<template>
  <div class="manage-product">
    <div class="flex-row between">
      <div class="list-header mt-1 mb-1">Danh sách sản phẩm</div>
      <button class="form-btn btn3" @click="openFormAdd">Thêm sản phẩm</button>
    </div>
    <table class="table">
      <thead>
        <tr>
          <th>Mã sản phẩm</th>
          <th>Tên sản phẩm</th>
          <th>Số lượng sản phẩm</th>
          <th>Giá sản phẩm</th>
          <th>Diễn giải</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="item in listData"
          :key="item.product_id"
          @dblclick="openFormEdit(item)"
        >
          <td>{{ item.product_code }}</td>
          <td>{{ item.product_name }}</td>
          <td>{{ replaceNumber(item.quantity) }}</td>
          <td>{{ replaceNumber(item.product_price) }}</td>
          <td>{{ item.description }}</td>
        </tr>
      </tbody>
    </table>
    <ProductForm></ProductForm>
  </div>
</template>
<script>
import { apiGetPagingProduct } from "@/api/productApi";
import { replaceNumber } from "@/method/methodFormat";
import ProductForm from "./ProductForm.vue";
export default {
  data() {
    return {
      listData: [],
      pagaSize: 20,
      pageNumber: 1,
    };
  },
  components: { ProductForm },
  methods: {
    replaceNumber,
    async getPagingData() {
      let filter = [];
      await apiGetPagingProduct(filter, this.pagaSize, this.pageNumber).then(
        (response) => {
          this.listData = response.data.data;
        }
      );
    },
    openFormEdit(item) {
      this.emitter.emit("updateProduct", item);
    },
    openFormAdd() {
      this.emitter.emit("addNewProduct");
    },
  },
  created() {
    this.getPagingData();
  },
  mounted() {
    this.emitter.on("reloadProductList", () => {
      this.getPagingData();
    });
  },
};
</script>
<style>
@import url(./ManageProduct.scss);
@import url(@/css/layout/table.scss);
</style>