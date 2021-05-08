<template>
  <div>
    <DxChart
      id="chart"
      :data-source="topic"
      title="Thống kê kinh phí của các đề tài"
    >
      <DxArgumentAxis :tick-interval="20" value-field="expired">
        <DxLabel format="decimal" />
      </DxArgumentAxis>
      <DxSeries
        type="bar"
        argument-field="researchCode"
        value-field="expense"
        name="Kinh phí"
      />
      <DxLegend :visible="true" />
      <DxExport :enabled="true" />
    </DxChart>

    <br />
    <br />
    <br />
    <br />
    <br />

    <div id="chart-demo-1">
      <DxChart
        :data-source="topic"
        palette="Harmony Light"
        title="Biểu đồ kết quả của các đề tài nghiên cứu"
      >
        <DxCommonSeriesSettings :type="type" argument-field="researchCode" />
        <DxSeries value-field="status" name="Kết quả" />
        <DxMargin :bottom="20" />
        <DxArgumentAxis :value-margins-enabled="false" />
        <DxLegend :visible="true" />
        <DxExport :enabled="true" />
      </DxChart>
    </div>
    <ul>
      <li>1: Hoàn thành nhiệm vụ</li>
      <li>2: Chưa hoàn thành nhiệm vụ</li>
      <li>3: Bị hủy</li>
      <li>4: Chưa cập nhật</li>
    </ul>
    <br />
    <br />
    <br />
    <br />
    <br />
    <div id="chart-demo-2">
      <DxChart
        :data-source="topic"
        palette="Ocean"
        title="Biểu đồ tiến trình của các đề tài nghiên cứu"
      >
        <DxCommonSeriesSettings :type="type" argument-field="researchCode" />
        <DxSeries value-field="process" name="Tiến trình" />
        <DxMargin :bottom="20" />
        <DxArgumentAxis :value-margins-enabled="false" />
        <DxLegend :visible="true" />
        <DxExport :enabled="true" />
      </DxChart>
    </div>
    <ul>
      <li>1: Không phê duyệt</li>
      <li>2: Đang thực hiện</li>
      <li>3: Đã hết hạn</li>
      <li>4: Đã nghiệm thu</li>
      <li>5: Đề xuất</li>
    </ul>
    <br />
    <br />
    <br />
    <br />
    <br />
    <!-- <DxChart
      id="char-2"
      :data-source="topic"
      :rotated="true"
      :bar-group-padding="0.2"
    >
      <DxArgumentAxis :categories="categories">
        <DxTick :visible="false" />
      </DxArgumentAxis>
      <DxTitle text="The British Monarchy" subtitle="An Abbreviated Timeline" />
      <DxCommonSeriesSettings
        argument-field="researchCode"
        type="rangebar"
        range-value1-field="createdDate"
        range-value2-field="expiredDate"
      />
      <DxAnimation :enabled="true" />
      <DxLegend vertical-alignment="bottom" horizontal-alignment="center">
        <DxTitle text="Royal Houses" />
      </DxLegend>
    </DxChart> -->
  </div>
</template>
<script>
import DxChart, {
  DxArgumentAxis,
  DxLegend,
  DxLabel,
  DxSeries,
  DxCommonSeriesSettings,
  DxExport,
  DxMargin,
  DxAnimation,
  DxTick,
  DxSeriesTemplate,
  DxTitle,
} from "devextreme-vue/chart";
import * as axios from "axios";
export default {
  name: "Chart-Expire",
  components: {
    DxChart,
    DxArgumentAxis,
    DxLabel,
    DxLegend,
    DxSeries,
    DxCommonSeriesSettings,
    DxExport,
    DxMargin,
    DxAnimation,
    DxTick,
    DxSeriesTemplate,
    DxTitle,
  },
  computed: {
    currentRole() {
      return this.$store.getters.currentRole;
    },
    loggedIn() {
      return this.$store.getters.loggedIn;
    },
    currentToken() {
      return this.$store.getters.currentToken;
    },
    currenID() {
      return this.$store.getters.currentID;
    },
  },
  data() {
    return {
      topic: [],
      type: "bar",
      categories: ["Royal Houses"],
    };
  },
  methods: {},
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
        if (e.response.status == 401) {
          this.$notify({
            //Lỗi server
            type: "error",
            title: "THÔNG BÁO",
            text: "Vui lòng đăng nhập lại",
          });

          setTimeout(() => {
            this.$router.push("Login");
          }, 1000);
        }
      });
  },
};
</script>
<style scoped>
#chart {
  height: 440px;
  width: 1700px !important;
  padding-left: 200px;
}
#chart-demo-1,
#chart-demo-2 {
  height: 440px;
  width: 1700px !important;
  padding-left: 200px;
}
ul {
  margin-left: 230px;
}
li {
  font-size: medium;
}
</style>
