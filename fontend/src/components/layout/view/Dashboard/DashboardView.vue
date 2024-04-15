<template>
  <div class="homepage">
    <apex-chart
      type="donut"
      :options="chartOptions"
      :series="series"
    ></apex-chart>
  </div>
</template>
<script>
export default {
  data() {
    return {
      series: [44, 55, 41, 17, 15],
      chartOptions: {
        chart: {
          type: "donut",
        },
        responsive: [
          {
            breakpoint: 480,
            options: {
              chart: {
                width: 200,
              },
              legend: {
                position: "bottom",
              },
            },
          },
        ],
      },
    };
  },
};
</script>
<style>
@import url(./dashboard.scss);
</style>