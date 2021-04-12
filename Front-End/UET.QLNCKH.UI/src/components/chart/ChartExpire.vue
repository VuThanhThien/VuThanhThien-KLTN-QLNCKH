<template>
  <DxChart
    id="chart"
    :data-source="topic"
    title="World Population by Decade"
  >
  <DxCommonSeriesSettings argument-field="status"/>
    <DxArgumentAxis :tick-interval="10">
      <DxLabel format="decimal" />
    </DxArgumentAxis>
    <DxSeries type="bar" value-field="expired"/>
    <DxLegend :visible="false" />
  </DxChart>
</template>
<script>
import DxChart, {
  DxArgumentAxis,
  DxLegend,
  DxLabel,
  DxSeries,
} from "devextreme-vue/chart";

export default {
  name: "Chart-Expire",
  components: {
    DxChart,
    DxArgumentAxis,
    DxLabel,
    DxLegend,
    DxSeries,
  },
  data() {
    return {
      topic :[],
    };
  },
  async created() {
    await axios
      .get("https://localhost:44323/api/ResearchTopic", {
        headers: {
          Authorization: `Bearer ${this.currentToken}`,
        },
      })
      .then((response) => {
        if (response.data) {
          this.topic = response.data;
        }
      })
      .catch((e) => {
        console.log(e);
      });
  },
};
</script>
<style scoped>
#chart {
  height: 440px;
}
</style>
