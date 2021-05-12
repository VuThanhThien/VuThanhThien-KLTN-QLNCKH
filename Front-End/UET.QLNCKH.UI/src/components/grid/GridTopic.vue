<template>
  <div class="GridTopic" id="gridTopic">
    <div class="navBar" v-if="loggedIn && currentRole == 'Admin'">
      <div class="headerBtn">
        <DxButton
          :width="120"
          text="Thêm mới"
          type="success"
          styling-mode="contained"
          @click="btnAddOnClick"
        />
      </div>
      <div class="headerBtn">
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
        :members="members"
        :canEdit="canEdit"
      />
      <div class="headerBtn">
        <DxButton
          :width="120"
          text="Xóa"
          type="success"
          styling-mode="contained"
          @click="btnDeleteOnClick"
        />
      </div>
    </div>
    <DxLoadPanel
      :position="positionLoading"
      :visible="loadingVisible"
      :show-indicator="true"
      :show-pane="true"
      :shading="true"
      :close-on-outside-click="false"
      :on-shown="onShown"
      shading-color="rgba(0,0,0,0.4)"
    />
    <h2 id="tableTitle">DANH SÁCH ĐỀ TÀI NGHIÊN CỨU KHOA HỌC UET</h2>
    <DxDataGrid
      id="dataGrid"
      :data-source="topic"
      :show-borders="true"
      :allow-column-resizing="true"
      :column-auto-width="true"
      :allow-column-reordering="false"
      key-expr="researchID"
      @selection-changed="selectTopic"
    >
      <DxColumn
        :width="90"
        data-field="researchCode"
        sort-order="asc"
        caption="Mã đề tài"
      />
      <DxColumn :width="280" data-field="researchName" caption="Tên đề tài" />
      <DxColumn data-field="description" caption="Mô tả" />
      <DxColumn :width="180" data-field="status" caption="Kết quả nghiên cứu">
        <DxLookup :data-source="statuses" display-expr="name" value-expr="id" />
      </DxColumn>
      <DxColumn :width="180" data-field="process" caption="Tình trạng đề tài">
        <DxLookup
          :data-source="processArr"
          display-expr="name"
          value-expr="id"
        />
      </DxColumn>
      <DxColumn :width="200" data-field="userID" caption="Tác giả">
        <DxLookup
          :data-source="users"
          display-expr="fullName"
          value-expr="userID"
        />
      </DxColumn>
      <DxColumn
        :width="150"
        data-field="createdDate"
        data-type="date"
        caption="Ngày bắt đầu"
        format="dd/MM/yyyy"
      />
      <DxColumn
        :width="150"
        data-field="expiredDate"
        data-type="date"
        caption="Ngày hết hạn"
        format="dd/MM/yyyy"
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
      <DxGroupPanel
        :visible="true"
        empty-panel-text="Kéo cột muốn nhóm lại vào đây !"
      />
      <DxSummary>
        <DxGroupItem summary-type="count" :customize-text="customizeText" />
      </DxSummary>
    </DxDataGrid>
    <notifications position="bottom right" clean: true style="margin-bottom:
    20px"/>
    <vue-confirm-dialog></vue-confirm-dialog>
  </div>
</template>
<script>
import * as axios from "axios";
import TopicDetail from "../detail/ListTopicDetail.vue";
import DxButton from "devextreme-vue/button";
import { DxLoadPanel } from "devextreme-vue/load-panel";
import service from "../../../modules/data.js";
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
  DxLookup,
  DxSummary,
} from "devextreme-vue/data-grid";

export default {
  components: {
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
    DxLoadPanel,
    DxLookup,
    DxSummary,
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
    currentID() {
      return this.$store.getters.currentID;
    },
  },
  methods: {
    /**custome text của groupitem */
    customizeText(e) {
      return "Tổng: " + e.value;
    },

    /**Hiển thị panel loading */
    showLoadPanel() {
      this.loadingVisible = true;
      this.getTopicList();
      // this.$router.push("MyTopic");
      // setTimeout(() => {
      //   this.$router.push("ListTopic");
      // }, 100);
    },

    /**Thời gian hiển thị loading panel */
    onShown() {
      setTimeout(() => {
        this.loadingVisible = false;
      }, 500);
    },

    /**Sự kiện nút thêm mới
     * created by VTT 09/04/21
     */
    btnAddOnClick() {
      // Mở form
      this.isHideParent = !this.isHideParent;
      this.selectedTopic = {};
      this.members = [];
    },

    /**Sự kiện nút sửa
     * Createdby VTT 09/04/21
     */
    btnEditOnClick() {
      if (this.selectedTopic.researchID == null) {
        this.$notify({
          title: "THÔNG BÁO",
          text: "Vui lòng chọn đề tài muốn sửa",
        });
        this.isHideParent = true;
      } else {
        this.isHideParent = !this.isHideParent;
      }
    },

    /**Sự kiện nút xóa */
    btnDeleteOnClick() {
      if (this.selectedTopic.researchID == null) {
        this.$notify({
          title: "THÔNG BÁO",
          text: "Vui lòng chọn đề tài muốn xóa",
        });
      } else {
        this.$confirm({
          message:
            `Bạn có chắc chắn muốn xóa đề tài ` +
            this.selectedTopic.researchName +
            ` không?`,
          button: {
            no: "Hủy",
            yes: "Chắc chắn",
          },
          callback: (confirm) => {
            if (confirm) {
              // ... do something
              const config = {
                headers: { Authorization: `Bearer ${this.currentToken}` },
              };
              axios
                .delete(
                  "https://localhost:44323/api/ResearchTopic/" +
                    this.selectedTopic.researchID,
                  config
                )
                .then((response) => {
                  if (response.data) {
                    this.showLoadPanel();
                    this.$notify({
                      type: "success",
                      title: "THÔNG BÁO",
                      text:
                        "Xóa thành công đề tài " +
                        this.selectedTopic.researchName,
                    });
                  }
                })
                .catch((e) => {
                  console.log(e);
                });
            }
          },
        });
      }
    },

    /**Đóng form detail */
    outIsHide(e) {
      this.isHideParent = e;
      this.showLoadPanel();
    },

    /**Lấy danh sách đề tài */
    async getTopicList() {
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
              text: "Vui lòng liên hệ Admin để được hỗ trợ!",
            });
          }
        });
    },

    /**Lấy danh sách tác giả */
    async getAuthorList() {
      const config = {
        headers: { Authorization: `Bearer ${this.currentToken}` },
      };
      await axios
        .get("https://localhost:44323/api/User", config)
        .then((response) => {
          if (response.data) {
            this.users = response.data;
          }
        })
        .catch((e) => {
          console.log(e);
        });
    },

    async getMemberList() {
      const config = {
        headers: { Authorization: `Bearer ${this.currentToken}` },
      };
      await axios
        .get(
          "https://localhost:44323/api/ResearchTopic/" +
            this.selectedTopic.researchID +
            "/members",
          config
        )
        .then((response) => {
          if (response.data) {
            this.members = response.data;
          }
        })
        .catch((e) => {
          console.log(e);
        });
    },
    /**khi chọn một hàng thì lấy đề tài đã chọn sang */
    selectTopic({ selectedRowsData }) {
      const data = selectedRowsData[0];
      this.selectedTopic = data;
      if (
        (this.selectedTopic.process == 5 && this.currentRole == "User") ||
        this.currentRole == "Admin"
      ) {
        this.canEdit = true;
      } else {
        this.canEdit = false;
      }
    },
  },
  data() {
    return {
      canEdit: false,
      allMode: "page",
      checkBoxesMode: "always",
      selectedTopic: {},
      topic: [],
      isHideParent: true,
      loadingVisible: false,
      positionLoading: { of: "#gridTopic" },
      statuses: service.getStatus(),
      processArr: service.getProcess(),
      isHidePopupParent: true,
      users: [],
      members: [],
    };
  },
  watch: {
    selectedTopic: {
      handler: function (value) {
        if (value) {
          this.getMemberList();
        } else {
          this.members = [];
        }
      },
    },
  },
  async created() {
    //tất cả đề tài
    await axios
      .get("https://localhost:44323/api/ResearchTopic", {
        headers: {
          Authorization: `Bearer ${this.currentToken}`,
        },
      })
      .then((response) => {
        if (response.data) {
          this.topic = response.data;
          this.getAuthorList();
        }
      })
      .catch((e) => {
        if (e.response.status == 401) {
          this.$notify({
            type: "error",
            title: "THÔNG BÁO",
            text: "Vui lòng đăng nhập lại",
          });
          setTimeout(() => {
            this.$router.push("Login");
          }, 1000);
        }
        if (e.response.status == 500) {
          this.$notify({
            type: "error",
            title: "THÔNG BÁO",
            text: "Vui lòng liên hệ để được hỗ trợ!",
          });
        }
      });
  },
};
</script>
<style scoped>
#tableTitle {
  margin-top: 20px;
  margin-left: 20px;
}
#gridTopic {
  height: 820px;
  overflow: auto;
}
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
.dx-datagrid-text-content {
  color: black !important ;
}
.option > span {
  margin-right: 10px;
}
.navBar {
  display: flex;
  list-style: none;
  padding: 5px 0;
  margin: 0;
  justify-content: flex-end;
  background: #f5f8fa;
  border-bottom: 1px solid lightgrey;
}
.headerBtn {
  margin-right: 15px;
}
</style>
