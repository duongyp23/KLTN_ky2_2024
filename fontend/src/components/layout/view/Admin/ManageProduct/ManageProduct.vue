<template>
  <div class="manage-product">
    <div class="flex-row between">
      <div class="list-header mt-1 mb-1">Danh sách sản phẩm</div>
      <button class="form-btn btn3" @click="openFormAdd">Thêm sản phẩm</button>
    </div>
    <div class="category-product">
      <CategoryTable></CategoryTable>
      <table class="table">
        <thead>
          <tr>
            <th class="left w-15">Mã sản phẩm</th>
            <th class="left w-15">Tên sản phẩm</th>
            <th class="right w-15">Giá sản phẩm</th>
            <th class="left">Diễn giải</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="item in listData"
            :key="item.product_id"
            @dblclick="openFormEdit(item)"
          >
            <td class="left w-15">{{ item.product_code }}</td>
            <td class="left w-15">{{ item.product_name }}</td>
            <td class="right w-15">{{ replaceNumber(item.product_price) }}</td>
            <td class="left">{{ item.description }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
<script>
import { apiGetPagingProduct } from "@/api/productApi";
import { replaceNumber } from "@/method/methodFormat";
import CategoryTable from "../../ListCategory/CategoryTable.vue";
export default {
  data() {
    return {
      listData: [],
      pagaSize: 20,
      pageNumber: 1,
    };
  },
  components: { CategoryTable },
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
      this.$router.replace(this.$router.path);
      this.$router.push(`/manageproduct/editproduct/${item.product_id}`, {
        params: { id: item.product_id },
      });
    },
    openFormAdd() {
      this.$router.replace(this.$router.path);
      this.$router.push(`/manageproduct/addproduct`);
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