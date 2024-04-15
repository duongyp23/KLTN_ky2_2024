<template>
  <div class="order-list">
    <div class="list-order">
      <div class="order-item">
        <div class="table-header">Ngày đặt hàng</div>
        <div class="table-header">Ngày bắt đầu thuê</div>
        <div class="table-header">Ngày kết thúc thuê</div>
        <div class="table-header">Tổng tiền thanh toán</div>
        <div class="table-header">Trạng thái đơn hàng</div>
      </div>
      <div
        class="order-item"
        v-for="item in listOrder"
        :key="item.order_id"
        @dblclick="viewOrder(item.order_id)"
      >
        <div>{{ datetimeToDate(item.order_date) }}</div>
        <div>{{ datetimeToDate(item.from_date) }}</div>
        <div>{{ datetimeToDate(item.to_date) }}</div>
        <div>{{ replaceNumber(item.total_order_deposit) }}</div>
        <div>{{ checkStatusOrder(item.status) }}</div>
      </div>
    </div>
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
@import url(./orderList.scss);
</style>