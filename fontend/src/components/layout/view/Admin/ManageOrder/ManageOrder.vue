<template>
  <div class="manage-order">
    <div class="flex-row between">
      <div class="list-header mt-1 mb-2">Danh sách đơn hàng</div>
      <div class="flex-end">
        <button class="form-btn btn3" @click="openFormAdd">Nhập kho</button>
        <button class="form-btn btn3 ml-1" @click="openFormAdd">
          Xuất kho
        </button>
      </div>
    </div>
    <table class="table">
      <thead>
        <tr>
          <th class="w-10">Ngày đặt hàng</th>
          <th class="w-10">Ngày bắt đầu thuê</th>
          <th class="w-10">Ngày kết thúc thuê</th>
          <th class="right w-15">Giá trị đơn hàng</th>
          <th class="w-15">Trạng thái đơn hàng</th>
          <th class="left">Ghi chú</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="item in listOrder"
          :key="item.order_id"
          @dblclick="viewOrder(item)"
        >
          <td class="w-10">{{ datetimeToDate(item.order_date) }}</td>
          <td class="w-10">{{ datetimeToDate(item.from_date) }}</td>
          <td class="w-10">{{ datetimeToDate(item.to_date) }}</td>
          <td class="right w-15">
            {{ replaceNumber(item.total_order_deposit) }}
          </td>
          <td class="w-15">
            {{ item.order_type == 1 ? checkStatusOrder(item.status) : "" }}
          </td>
          <td class="left">{{ item.description }}</td>
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
  getOrderTypeName,
} from "@/method/methodFormat";
export default {
  data() {
    return {
      listOrder: [],
      pageNumber: 1,
    };
  },
  methods: {
    replaceNumber,
    datetimeToDate,
    checkStatusOrder,
    getOrderTypeName,
    viewOrder(order) {
      if (order.order_type == 1) {
        this.$router.replace(this.$router.path);
        this.$router.push(`/manageorder/${order.order_id}`, {
          params: { id: order.order_id },
        });
      } else {
        this.$router.replace(this.$router.path);
        this.$router.push(`/manageorder/stockorder/${order.order_id}`, {
          params: { id: order.order_id },
        });
      }
    },
    async getPagingOrder() {
      let filter = [];
      filter.push({
        columnName: "status",
        filterValue: 1,
        operatorValue: "!=",
      });
      filter.push({
        columnName: "order_type",
        filterValue: 1,
        operatorValue: "=",
      });
      await apiGetPagingOrder(filter, 20, this.pageNumber).then((response) => {
        this.listOrder = response.data.data;
      });
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