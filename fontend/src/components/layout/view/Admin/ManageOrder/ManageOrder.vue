<template>
  <div class="manage-order">
    <div class="list-header mt-1 mb-1">Danh sách đơn hàng</div>
    <table class="table">
      <thead>
        <tr>
          <th>Ngày đặt hàng</th>
          <th>Ngày bắt đầu thuê</th>
          <th>Ngày kết thúc thuê</th>
          <th>Tổng tiền đơn hàng</th>
          <th>Trạng thái đơn hàng</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="item in listOrder"
          :key="item.order_id"
          @dblclick="viewOrder(item.order_id)"
        >
          <td>{{ datetimeToDate(item.order_date) }}</td>
          <td>{{ datetimeToDate(item.from_date) }}</td>
          <td>{{ datetimeToDate(item.to_date) }}</td>
          <td>{{ replaceNumber(item.total_order_deposit) }}</td>
          <td>{{ checkStatusOrder(item.status) }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
import { apiGetPagingOrder } from "@/api/orderApi";
import {
  replaceNumber,
  datetimeToDate,
  checkStatusOrder,
} from "@/method/methodFormat";
export default {
  data() {
    return {
      listOrder: [],
      pagaSize: 20,
      pageNumber: 1,
    };
  },
  methods: {
    replaceNumber,
    datetimeToDate,
    checkStatusOrder,
    viewOrder(orderId) {
      this.$router.replace(this.$router.path);
      this.$router.push(`/order/${orderId}`, {
        params: { id: orderId },
      });
    },
    async getPagingOrder() {
      let filter = [];
      filter.push({
        columnName: "status",
        filterValue: 1,
        operatorValue: "!=",
      });
      await apiGetPagingOrder(filter, this.pagaSize, this.pageNumber).then(
        (response) => {
          this.listOrder = response.data.data;
        }
      );
    },
  },
  created() {
    this.getPagingOrder();
  },
};
</script>
<style>
@import url(./ManageOrder.scss);
@import url(@/css/layout/table.scss);
</style>