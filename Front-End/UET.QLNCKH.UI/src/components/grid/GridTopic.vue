<template>
  <div class="GridTopic">
    <div class="navBar" v-if="loggedIn">
      <div class="headerBtn">
        <DxButton
          :width="120"
          text="Thêm mới"
          type="success"
          styling-mode="contained"
          @click="btnAddOnClick"
        />
      </div>
      <div class="headerBtn" v-if="currentRole == 'Admin'">
        <DxButton
          :width="120"
          text="Sửa"
          type="success"
          styling-mode="contained"
          @click="btnEditOnClick"
        />
      </div>
      <TopicDetail
        :isHide="isHideParent"
        @outIsHide="outIsHide"
        :selectedTopic="selectedTopic"
      />
      <div class="headerBtn" v-if="currentRole == 'Admin'">
        <DxButton
          :width="120"
          text="Xóa"
          type="success"
          styling-mode="contained"
        />
      </div>
    </div>

    <DxDataGrid
      id="dataGrid"
      :data-source="topic"
      :show-borders="true"
      key-expr="researchID"
      @selection-changed="selectUser"
    >
      <DxColumn :width="90" data-field="researchCode" caption="Mã đề tài" />
      <DxColumn data-field="researchName" caption="Tên đề tài" />
      <DxColumn :width="280" data-field="description" caption="Mô tả" />
      <DxColumn :calculate-cell-value="formatStatus" caption="Kết quả" />
      <DxColumn
        :calculate-cell-value="formatProcess"
        caption="Tiến trình nghiên cứu"
      />
      <DxColumn
        :width="120"
        data-field="createdDate"
        data-type="date"
        caption="Ngày tạo"
      />
      <DxColumn
        :width="120"
        data-field="endDate"
        data-type="date"
        caption="Ngày kết thúc"
      />
      <DxColumn
        :width="120"
        data-field="expiredDate"
        data-type="date"
        caption="Ngày nghiệm thu"
      />
      <DxColumn
        :width="180"
        data-field="expense"
        data-type="number"
        caption="Kinh phí"
        format="#,##0"
      />
      <DxPaging :page-size="15" />
      <DxColumnFixing :enabled="true" />
      <DxSelection mode="single" />
      <DxFilterRow :visible="true" />
      <DxExport :enabled="true" />
      <DxSearchPanel :visible="true" />
    </DxDataGrid>

    <!-- <p id="selected-employee" v-if="selectedTopic">
      Selected topic : {{ selectedTopic.researchID }}
    </p> -->
  </div>
</template>
<script>
import * as axios from "axios";
import TopicDetail from "../detail/ListTopicDetail.vue";
import { DxSelectBox } from "devextreme-vue/select-box";
import DxButton from "devextreme-vue/button";
import {
  DxDataGrid,
  DxColumn,
  DxPaging,
  DxSelection,
  DxFilterRow,
  DxEditing,
  DxFormItem,
  DxExport,
  DxGroupItem,
  DxColumnFixing,
  DxSearchPanel,
  DxGroupPanel,
} from "devextreme-vue/data-grid";

export default {
  components: {
    DxSelectBox,
    DxDataGrid,
    DxColumn,
    DxPaging,
    DxSelection,
    DxFilterRow,
    DxEditing,
    DxFormItem,
    DxButton,
    DxExport,
    DxGroupItem,
    DxColumnFixing,
    DxSearchPanel,
    DxGroupPanel,
    TopicDetail,
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
  },
  methods: {
    btnAddOnClick() {
      // Mở form
      this.isHideParent = !this.isHideParent;
      this.selectedTopic = {};
    },
    btnEditOnClick() {
      if (this.selectedTopic == {}) {
        this.$notify({
          title: "THÔNG BÁO",
          text: "Vui lòng chọn đề tài muốn sửa",
        });
        this.isHideParent = true;
      }
      else{
        this.isHideParent = !this.isHideParent;
      }
    },
    outIsHide(e) {
      this.isHideParent = e;
    },
    /**Format trạng thái */
    formatStatus(rowData) {
      if (rowData.status == 1) return "Hoàn thành nhiệm vụ";
      if (rowData.status == 2) return "Không hoàn thành nhiệm vụ";
      if (rowData.status == 3) return "Bị hủy";
      if (rowData.status == 4) return "Chưa cập nhật";
    },

    /**Format tiến trính */
    formatProcess(rowData) {
      if (rowData.process == 1) return "Đợi xét chọn";
      if (rowData.process == 2) return "Đang thực hiện";
      if (rowData.process == 3) return "Đã hết hạn";
    },
    async getTopicList() {
      const config = {
        headers: { Authorization: `Bearer ${currentToken}` },
      };
      await axios
        .get("https://localhost:44323/api/ResearchTopic", config)
        .then((response) => {
          if (response.data) {
            this.$notify({
              type: "success",
              title: "THÔNG BÁO",
              text: "Cập nhật thành công ",
            });
          }
          this.topic = response.data;
        })
        .catch((e) => {
          if (e.response.status == 401) {
            this.$notify({
              // bad request
              type: "error",
              title: "THÔNG BÁO",
              text: "Unauthorized",
            });
          }

          if (e.response.status == 500) {
            this.$notify({
              //Lỗi server
              type: "error",
              title: "THÔNG BÁO",
              text: "Vui lòng liên hệ MISA để được hỗ trợ!",
            });
          }
        });
    },

    /**khi chọn một hàng thì lấy đề tài đã chọn sang */
    selectUser(e) {
      e.component.byKey(e.currentSelectedRowKeys[0]).done((topic) => {
        if (topic) {
          this.selectedTopic = topic;
        }
      });
    },
  },
  data() {
    return {
      allMode: "page",
      checkBoxesMode: "always",
      selectedTopic: {},
      topic: [],
      isHideParent: true,
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
          this.$notify({
            type: "success",
            title: "THÔNG BÁO",
            text: "Cập nhật thành công ",
          });
          this.topic = response.data;
        }
      })
      .catch((e) => {
        if (e.response.status == 401) {
          this.$notify({
            // bad request
            type: "error",
            title: "THÔNG BÁO",
            text: "Unauthorized",
          });
        }

        if (e.response.status == 500) {
          this.$notify({
            //Lỗi server
            type: "error",
            title: "THÔNG BÁO",
            text: "Vui lòng liên hệ MISA để được hỗ trợ!",
          });
        }
      });
  },
};
</script>
<style scoped>
#dataGrid {
  padding: 15px;
}
/* .GridTopic {
  margin: 10px;
} */
.options {
  margin-top: 20px;
  padding: 20px;
  background-color: rgba(191, 191, 191, 0.15);
  position: relative;
}

.caption {
  font-size: 18px;
  font-weight: 500;
}

.option {
  margin-top: 10px;
}

.checkboxes-mode {
  position: absolute;
  right: 20px;
  bottom: 20px;
}

.option > .dx-selectbox {
  width: 150px;
  display: inline-block;
  vertical-align: middle;
}

.option > span {
  margin-right: 10px;
}
.navBar {
  display: flex;
  list-style: none;
  padding: 15px 0;
  margin: 0;
  justify-content: flex-end;
  background: #f5f8fa;
  border-bottom: 1px solid lightgrey;
}
.headerBtn {
  margin-right: 15px;
}
</style>
