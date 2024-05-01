<template>
  <div class="stock-order-detail flex-column">
    <div class="title">{{ orderType == 2 ? "Nhập kho" : "Xuất kho" }}</div>
    <div class="flex-row">
      <div class="detail-list flex1 flex-column">
        <div class="box-search mb-1">
          <StyleInput
            :rightIcon="'search'"
            :placeholder="'Nhập tên sản phẩm'"
            v-model:value="searchText"
            @change="getProductData"
          ></StyleInput>
          <div class="list-option" v-if="productSearch.length > 0">
            <div
              v-for="(item, index) in productSearch"
              :key="index"
              @click="addOrderDetail(item)"
            >
              {{ item.product_name }}
            </div>
          </div>
        </div>
        <div
          v-for="(orderDetail, index) in orderDetails"
          :key="index"
          class="order-detail-item flex-row"
        >
          <div
            class="product-img"
            :style="
              'background-image : url(' +
              getFirstImage(orderDetail.product_image_url) +
              ')'
            "
          ></div>
          <div class="flex-column">
            <div>{{ orderDetail.product_name }}</div>
            <div class="flex-row">
              <StyleInput
                v-model:value="orderDetail.option_code"
                :label="'Mã chọn'"
              ></StyleInput>
              <InputNumber
                v-model:numberValue="orderDetail.quantity"
                :label="'Số lượng'"
              ></InputNumber>
              <InputNumber
                v-if="orderType == 3"
                v-model:numberValue="orderDetail.product_payment"
                :label="'Tổng tiền'"
                @changeValue="calculateOrderMoney"
              ></InputNumber>
              <InputNumber
                v-if="orderType == 2"
                v-model:numberValue="orderDetail.product_return"
                :label="'Tổng tiền'"
                @changeValue="calculateOrderMoney"
              ></InputNumber>
            </div>
          </div>
        </div>
      </div>
      <div class="total-order flex1 flex-column">
        <div class="amount-row">
          <div>Giá trị đơn hàng</div>
          <div v-if="order.order_type == 2">
            {{ replaceNumber(order.total_order_return) }}
          </div>
          <div v-else>{{ replaceNumber(order.total_order_payment) }}</div>
        </div>
        <div class="flex-row">
          <style-input
            :label="'Người tạo'"
            class="flex1"
            v-model:value="order.user_name"
          ></style-input>
          <style-input
            :label="'Ngày phiếu kho'"
            class="flex1"
            v-model:value="order.order_date"
            type="date"
          ></style-input>
        </div>

        <style-input
          :label="'Ghi chú'"
          type="textarea"
          class="description"
          v-model:value="order.description"
        ></style-input>
      </div>
    </div>
    <div class="flex-row flex-end mt-1">
      <div class="flex-row flex-end">
        <button class="form-btn btn1" v-on:click="closeForm">Hủy</button>
        <button class="form-btn btn2" @click="deleteProduct" v-if="state == 2">
          Xóa
        </button>
        <button class="form-btn btn3" @click="saveOrder">Lưu</button>
      </div>
    </div>
  </div>
</template>
<script>
import StyleInput from "@/components/base/StyleInput/StyleInput.vue";
import InputNumber from "@/components/base/StyleInput/InputNumber.vue";
import Images from "@/assets/icon/images";
import { apiDeleteOrderDetail } from "@/api/orderDetailApi";
import { replaceNumber, datetimeToDate } from "@/method/methodFormat";
import { apiAddStockOrder, apiGetOrder, apiDeleteOrder } from "@/api/orderApi";
import { apiGetPagingProduct } from "@/api/productApi";
export default {
  setup() {},
  data() {
    return {
      state: 0,
      orderType: 0,
      order: {},
      orderDetails: [],
      products: [],
      productSearch: [],
      Images,
      searchText: "",
    };
  },
  components: { StyleInput, InputNumber },
  methods: {
    addOrderDetail(item) {
      this.products.push(item);
      this.orderDetails.push({
        product_id: item.product_id,
        product_code: item.product_code,
        product_name: item.product_name,
        product_image_url: item.product_image_url,
        product_return: 0,
        product_payment: 0,
        option_code: "",
      });
      this.productSearch = [];
    },
    async getProductData() {
      let filter = [];
      if (this.searchText) {
        filter.push({
          columnName: "product_name",
          filterValue: "'%" + this.searchText + "%'",
          operatorValue: "LIKE",
        });
      }
      await apiGetPagingProduct(filter, 10, 1).then((response) => {
        this.productSearch = response.data.data;
      });
    },
    datetimeToDate,
    async removeOrder() {
      await apiDeleteOrder(this.order.order_id).then(() => {
        this.closeForm();
      });
    },
    async saveOrder() {
      await apiAddStockOrder(this.order, this.orderDetails).then(() => {
        this.closeForm();
      });
    },
    async remove(item) {
      await apiDeleteOrderDetail(item.order_detail_id).then(() => {
        this.loadData();
      });
    },
    async loadData() {
      await apiGetOrder(this.$route.params.id).then((response) => {
        this.order = response.data.order;
        this.orderDetails = response.data.orderDetails;
        this.products = response.data.products;
      });
    },
    replaceNumber,
    calculateOrderMoney() {
      this.order.total_order_return = 0;
      this.order.total_order_payment = 0;
      this.orderDetails.forEach((item) => {
        this.order.total_order_return += item.product_return;
        this.order.total_order_payment += item.product_payment;
      });
    },
    closeForm() {
      this.$router.replace(this.$router.path);
      this.$router.push("/managestock");
    },
    getFirstImage(listImage) {
      let arrImg = listImage.split(";");
      return arrImg[0];
    },
  },
  created() {},
  async mounted() {
    this.orderType = this.$route.params.type;
    if (!this.orderType) {
      this.$router.replace(this.$router.path);
      this.$router.push("/managestock");
    }
    this.state = this.$route.params.state;
    if (this.state == 2) {
      await this.loadData();
    } else {
      this.order.order_type = this.orderType;
    }
    this.order.order_date = this.datetimeToDate(new Date());
    this.order.user_name = this.$cookies.get("userName");
  },
  watch: {
    orderDetails: {
      deep: true,
      handler: function () {
        this.calculateOrderMoney();
      },
    },
  },
};
</script>
<style>
@import url(./StockOrderDetail.scss);
</style>