<template>
  <div class="manage-stock">
    <div class="flex-row between">
      <div class="list-header mt-1 mb-1">Quản lý nhập, xuất kho</div>
      <div class="flex-end">
        <button class="form-btn btn3" @click="openFormAdd(2)">Nhập kho</button>
        <button class="form-btn btn3 ml-1" @click="openFormAdd(3)">
          Xuất kho
        </button>
      </div>
    </div>
    <table class="table mt-1">
      <thead>
        <tr>
          <th class="left w-20">Mã sản phẩm</th>
          <th class="left w-20">Tên sản phẩm</th>
          <th class="right w-15">Tổng số lượng</th>
          <th class="right w-15">SL đang thuê</th>
          <th class="right w-15">SL đã bán</th>
          <th class="right w-15">SL còn lại</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in listData" :key="item.order_id">
          <td class="left w-20">{{ item.product_code }}</td>
          <td class="left w-20">{{ item.product_name }}</td>
          <td class="right w-15">{{ replaceNumber(item.total_quantity) }}</td>
          <td class="right w-15">{{ replaceNumber(item.quantity_rental) }}</td>
          <td class="right w-15">{{ replaceNumber(item.quantity_sold) }}</td>
          <td class="right w-15">{{ replaceNumber(item.quantity_remain) }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
import { apiGetPagingProduct } from "@/api/productApi";
import { replaceNumber } from "@/method/methodFormat";
export default {
  data() {
    return {
      listData: [],
      pageNumber: 1,
    };
  },
  components: {},
  methods: {
    replaceNumber,
    async getPagingData() {
      let filter = [];
      await apiGetPagingProduct(filter, 20, this.pageNumber).then(
        (response) => {
          this.listData = response.data.data;
        }
      );
    },
    openFormAdd(orderType) {
      this.$router.push({
        name: "DetailStock",
        params: { type: orderType, state: 1 },
      });
    },
  },
  created() {
    this.getPagingData();
  },
  mounted() {},
};
</script>
<style>
@import url(./ManageStock.scss);
@import url(@/css/layout/table.scss);
</style>