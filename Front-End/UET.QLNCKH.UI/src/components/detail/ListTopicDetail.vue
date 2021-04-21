
<template>
  <div class="modal" :class="{ isHide: isHide }">
    <div class="dialog-mask">
      <div draggable="true" class="dialog-topic">
        <!-- nút x thoát dialog  -->
        <button class="close-dialog" @click="cancel">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="24"
            height="24"
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
            class="feather feather-x"
          >
            <line x1="18" y1="6" x2="6" y2="18"></line>
            <line x1="6" y1="6" x2="18" y2="18"></line>
          </svg>
        </button>
        <!-- tiêu đề dialog  -->
        <h1 id="title">Đề tài : {{ this.selectedTopic.researchName }}</h1>
        <!-- body dialog  -->
        <div class="dialog-content">
          <!-- khối avatar  -->
          <!-- Khối điền thông tin  -->
          <div class="block-left">
            <h3 style="text-align: center">THÔNG TIN CHUNG</h3>
            <!--  -->
            <div class="block-2-topic">
              <div class="block-1-topic">
                <div class="fieldName">Mã đề tài (<span>*</span>)</div>
                <input type="text" v-model="selectedTopic.researchCode" />
              </div>
              <div class="block-1-topic">
                <div class="fieldName">Tên đề tài (<span>*</span>)</div>
                <input
                  type="text"
                  ref="name"
                  v-model="selectedTopic.researchName"
                />
              </div>
            </div>
            <!--  -->
            <div class="block-2-topic">
              <div class="block-1-topic">
                <div class="fieldName">Kinh phí</div>
                <input type="number" v-model="selectedTopic.expense" />
              </div>
              <div class="block-1-topic">
                <div class="fieldName">Kinh phí yêu cầu VNU tài trợ</div>
                <input type="number" v-model="selectedTopic.sponsorMoney" />
              </div>
            </div>
            <!--  -->
            <div class="block-2-topic">
              <div class="block-1-topic">
                <div class="fieldName">Mô tả</div>
                <DxTextArea
                  :height="100"
                  :width="650"
                  v-model="selectedTopic.description"
                />
              </div>
            </div>
            <!--  -->
            <div class="block-2-topic">
              <div class="block-1-topic">
                <div class="fieldName">Ngày bắt đầu</div>
                <DxDateBox
                  v-model="selectedTopic.createdDate"
                  type="date"
                  display-format="dd/MM/yyyy"
                />
              </div>
              <div class="block-1-topic">
                <div class="fieldName">Ngày kết thúc</div>
                <DxDateBox
                  v-model="selectedTopic.endDate"
                  type="date"
                  display-format="dd/MM/yyyy"
                />
              </div>
            </div>
            <!--  -->
            <div class="block-2-topic">
              <div class="block-1-topic">
                <div class="fieldName">Chủ nhiệm đề tài (<span>*</span>)</div>
                <select v-model="selectedTopic.userID">
                  <option value="0" disabled>Chọn Chủ nhiệm đề tài</option>
                  <option
                    v-for="user in users"
                    :key="user.userID"
                    :value="user.userID"
                  >
                    {{ user.fullName }}
                  </option>
                </select>
              </div>
              <div class="block-1-topic">
                <div class="fieldName">Ngày nghiệm thu</div>
                <DxDateBox
                  v-model="selectedTopic.expiredDate"
                  type="date"
                  display-format="dd/MM/yyyy"
                />
              </div>
            </div>
            <!--  -->
            <div class="block-2-topic">
              <div class="block-1-topic">
                <div class="fieldName">Mã chuyên ngành</div>
                <input type="text" v-model="selectedTopic.specializeCode" />
              </div>
              <div class="block-1-topic">
                <div class="fieldName">Loại đề tài</div>
                <input type="text" v-model="selectedTopic.typeResearch" />
              </div>
            </div>
            <h3 style="text-align: center; margin-top: 20px">
              Thành viên nghiên cứu :
              <!-- {{ selectedTopic.numberResearcher }} -->
            </h3>
            <!-- <div
              v-for="member in members"
              :key="member.userID"
              :value="member.userID"
            >
              <div class="block-2-topic">
                <div class="block-1-topic">
                  <div class="fieldName">Vai trò nghiên cứu:</div>
                  <input type="text" v-model="member.researchRole" />
                </div>
                <div class="block-1-topic">
                  <div class="fieldName">Họ và tên :</div>
                  <select v-model="member.userID">
                    <option value="0" disabled>Chọn tên thành viên</option>
                    <option
                      v-for="user in users"
                      :key="user.userID"
                      :value="user.userID"
                    >
                      {{ user.fullName }}
                    </option>
                  </select>
                </div>
              </div>
              <div class="block-2-topic">
                <div class="block-1-topic">
                  <div class="fieldName">Mã thành viên :</div>
                  <input type="text" v-model="member.userCode" />
                </div>
                <div class="block-1-topic">
                  <div class="fieldName">Số tháng làm việc :</div>
                  <input type="text" v-model="member.workmonth" />
                </div>
              </div>
            </div> -->
            <div class="block-2-topic">
              <DxDataGrid
                id="grid"
                :show-borders="true"
                :data-source="members"
                :repaint-changes-only="true"
              >
                <DxEditing
                  :refresh-mode="refreshMode"
                  :allow-adding="true"
                  :allow-updating="true"
                  :allow-deleting="true"
                  mode="cell"
                />
                <DxColumn data-field="userID" caption="Họ và tên">
                  <DxLookup
                    :data-source="users"
                    value-expr="userID"
                    display-expr="fullName"
                  />
                </DxColumn>
                <DxColumn
                  data-field="researchRole"
                  caption="Vai trò nghiên cứu"
                />
                <DxColumn data-field="workmonth" caption="Số tháng làm việc" />
                <DxColumn
                  data-field="userCode"
                  caption="Mã nhân viên"
                  data-type="text"
                />
              </DxDataGrid>
            </div>
          </div>
          <div class="block-right">
            <h3 style="text-align: center">THÔNG TIN CHI TIẾT QUY TRÌNH</h3>
            <!--  -->
            <div class="block-2-topic">
              <div class="block-1-topic">
                <div class="fieldName">Kết quả nghiên cứu</div>
                <select v-model="selectedTopic.status">
                  <option value="0" disabled>Chọn một kết quả</option>
                  <option
                    v-for="status in statuses"
                    :key="status.id"
                    :value="status.id"
                  >
                    {{ status.name }}
                  </option>
                </select>
              </div>
              <div class="block-1-topic">
                <div class="fieldName">Trạng thái nghiên cứu</div>
                <select v-model="selectedTopic.process">
                  <option value="0" disabled>Chọn một trạng thái</option>
                  <option
                    v-for="process in processArr"
                    :key="process.id"
                    :value="process.id"
                  >
                    {{ process.name }}
                  </option>
                </select>
              </div>
            </div>
            <!--  -->
            <div class="block-2-topic">
              <div class="block-1-topic">
                <div class="fieldName">Thuyết minh đề tài</div>
                <input type="text" v-model="selectedTopic.present" />
              </div>
            </div>

            <div class="block-2-topic">
              <div class="block-1-topic">
                <div class="fieldName">
                  Cơ sở vật chất, trang thiết bị phục vụ cho đề tài:
                </div>
                <DxTextArea
                  :height="100"
                  :width="650"
                  v-model="selectedTopic.infrastructure"
                />
              </div>
            </div>

            <!--  -->
            <h3 style="text-align: center; margin-top: 20px">
              DỰ KIẾN CÔNG BỐ
            </h3>
            <div class="block-2-topic">
              <div class="block-1-topic">
                <div class="fieldName">Bài báo ISI /Scopus</div>
                <input type="number" v-model="selectedTopic.isi" />
              </div>
              <div class="block-1-topic">
                <div class="fieldName">Tạp chí quốc tế có uy tín (Q1,Q2)</div>
                <input type="number" v-model="selectedTopic.journal" />
              </div>
            </div>
            <!--  -->
            <div class="block-2-topic">
              <div class="block-1-topic">
                <div class="fieldName">
                  Công bố phát minh sáng chế/ Giải pháp hữu ích
                </div>
                <input type="number" v-model="selectedTopic.usefullSolution" />
              </div>
              <div class="block-1-topic">
                <div class="fieldName">Sản phẩm công nghệ chuyển giao</div>
                <input type="number" v-model="selectedTopic.transferProduct" />
              </div>
            </div>
            <!--  -->
            <div class="block-2-topic">
              <div class="block-1-topic">
                <div class="fieldName">
                  Báo cáo tư vấn chính sách cho nhà nước
                </div>
                <input type="number" v-model="selectedTopic.reportToState" />
              </div>
              <div class="block-1-topic">
                <div class="fieldName">Sách chuyên khảo</div>
                <input type="number" v-model="selectedTopic.monograph" />
              </div>
            </div>
            <!--  -->
            <div class="block-2-topic">
              <div class="block-1-topic">
                <div class="fieldName">Khác</div>
                <input type="number" v-model="selectedTopic.otherResult" />
              </div>
              <!-- Dẫn chứng  -->
              <div class="block-1-topic">
                <div class="fieldName">Dẫn chứng</div>
                <input type="text" v-model="selectedTopic.otherResult" />
              </div>
            </div>
          </div>
        </div>
        <!-- footer dialog  -->
        <div class="dialog-footer">
          <div class="footer-content">
            <button id="btn-cancel" @click="cancel">Hủy</button>
            <button id="btn-save" @click="btnSaveOnClick">Lưu</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import * as axios from "axios";
import DxDateBox from "devextreme-vue/date-box";
import DxTextArea from "devextreme-vue/text-area";
import service from "../../../modules/data.js";
import DxButton from "devextreme-vue/button";
import {
  DxDataGrid,
  DxColumn,
  DxEditing,
  DxLookup,
  DxTotalItem,
} from "devextreme-vue/data-grid";
import CustomStore from "devextreme/data/custom_store";
import "whatwg-fetch";

// const URL = "https://localhost:44323/api/MemberTopic";
export default {
  name: "UserDetails",
  props: {
    isHide: {
      type: Boolean,
      default() {
        return true;
      },
    },
    selectedTopic: {
      type: Object,
      default() {
        return {};
      },
    },
    canEdit: {
      type: Boolean,
      default() {
        return false;
      },
    },
    members: {
      type: Array,
      default() {
        return [];
      },
    },
  },
  components: {
    DxDateBox,
    DxTextArea,
    DxButton,
    DxDataGrid,
    DxColumn,
    DxEditing,
    DxLookup,
    DxTotalItem,
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
    validateTopic() {
      let returnData = {
        error: false,
        msg: "",
        typeError: "",
      };

      if (
        this.selectedTopic.researchName === "" ||
        this.selectedTopic.researchName == null
      ) {
        returnData = {
          error: true,
          msg: "Vui lòng nhập tên đề tài",
          typeError: "name",
        };
      }
      if (
        this.selectedTopic.researchCode === "" ||
        this.selectedTopic.researchCode == null
      ) {
        returnData = {
          error: true,
          msg: "Vui lòng nhập mã đề tài",
          typeError: "code",
        };
      }
      if (this.selectedTopic.userID == 0) {
        returnData = {
          error: true,
          msg: "Vui lòng chọn Chủ nhiệm đề tài",
          typeError: "user",
        };
      }

      return returnData;
    },
  },
  methods: {
    /**Sự kiện hủy */
    cancel() {
      this.$emit("outIsHide", !this.isHide);
    },

    /**Hàm insert */
    postTopic() {
      const config = {
        headers: { Authorization: `Bearer ${this.currentToken}` },
      };

      const bodyParameters = this.selectedTopic;
      axios
        .post(
          "https://localhost:44323/api/ResearchTopic",
          bodyParameters,
          config
        )
        .then((response) => {
          if (response.data) {
            // this.topic = response.data;
            this.cancel();
            this.$notify({
              type: "success",
              title: "THÔNG BÁO",
              text:
                "Thêm mới thành công đề tài " + this.selectedTopic.researchName,
            });
          }
        })
        .catch((e) => {
          if (e.response.status == 401) {
            this.$notify({
              // bad request
              type: "error",
              title: "THÔNG BÁO",
              text: "Phiên hết hạn! Vui lòng đăng nhập lại!",
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

          if (e.response.status == 400) {
            this.$notify({
              //Lỗi server
              type: "error",
              title: "THÔNG BÁO",
              text: e.response.data,
            });
          }
        });
    },

    /**Hàm sửa */
    putTopic() {
      if (this.canEdit == true) {
        const config = {
          headers: { Authorization: `Bearer ${this.currentToken}` },
        };
        const bodyParameters = this.selectedTopic;
        axios
          .put(
            "https://localhost:44323/api/ResearchTopic/" +
              this.selectedTopic.researchID,
            bodyParameters,
            config
          )
          .then((response) => {
            if (response.data) {
              // this.topic = response.data;
              this.cancel();
              this.$notify({
                type: "success",
                title: "THÔNG BÁO",
                text:
                  "Cập nhật thành công đề tài " +
                  this.selectedTopic.researchName,
              });
            }
          })
          .catch((e) => {
            if (e.response.status == 400) {
              this.$notify({
                //Lỗi server
                type: "error",
                title: "THÔNG BÁO",
                text: e.response.data,
              });
            }

            if (e.response.status == 403) {
              this.$notify({
                // bad request
                type: "error",
                title: "THÔNG BÁO",
                text:
                  "Bạn không đủ quyền để chỉnh sửa, vui lòng liên hệ Admin!",
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
      } else {
        alert("Đề tài đã hết hạn chỉnh sửa");
        this.cancel();
      }
    },

    /**Sự kiện nút lưu */
    btnSaveOnClick() {
      if (this.validateTopic.error) {
        this.$notify({
          title: "THÔNG BÁO",
          type: "warn",
          text: this.validateTopic.msg,
        });
        //focus
        if (this.validateTopic.typeError == "name") {
          this.$refs.name.focus();
        }
      } else {
        if (this.selectedTopic.researchID == null) {
          this.selectedTopic.researchID =
            "00000000-0000-0000-0000-000000000000";
          this.postTopic();
        } else {
          this.putTopic();
        }
      }
    },
  },
  data() {
    return {
      users: [],
      processArr: service.getProcess(),
      statuses: service.getStatus(),
      refreshMode: "reshape",
    };
  },

  async created() {
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
};
</script>

<style scoped>
#title {
  margin-top: 20px;
  text-align: center;
}
.dx-datebox {
  max-width: 90% !important;
}
.dx-datebox.dx-auto-width .dx-texteditor-input,
.dx-datebox:not(.dx-texteditor-empty).dx-auto-width .dx-texteditor-input {
  padding-right: 26px;
  width: 267px !important;
}
.dialog-topic {
  position: absolute;
  width: 100%;
  height: 100vh;
  background-color: #fff;
  padding: 10px;
}
.dialog-mask {
  position: fixed;
  z-index: 3;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: opacity 0.3s ease;
}
.isHide {
  display: none;
}
.close-dialog {
  margin: 5px;
  float: right;
  top: 0;
  border: none;
  background-color: #fff !important;
  cursor: pointer;
}
.dialog-content {
  display: flex;
  justify-content: space-between;
  height: 845px;
  overflow-y: scroll;
}

.block-left {
  display: block;
  margin: 30px 20px 30px 150px;
}
.block-right {
  display: block;
  margin: 30px 150px 30px 20px;
}
.block-2-topic {
  display: flex;
  justify-content: space-between;
  width: 700px;
  align-items: center;
}
.block-1-topic {
  width: 350px;
  height: calc(100% - 2px);
  margin-left: 15px;
  margin-top: 10px;
}
.block-1-topic span {
  color: #ff0000;
}
.fieldName {
  font-size: 13px;
  font-weight: bold;
  margin-bottom: 3px;
  margin-top: 8px;
}
.block-1-topic input,
.block-1-topic select {
  padding-left: 20px;
  margin-top: 4px;
  width: 300px;
  height: 30px;
  border-radius: 5px;
  border: 0.5px solid rgb(204, 204, 204);
  padding: 5px;
}
input:hover,
select:hover {
  border: 0.5px solid rgb(183, 192, 245);
}
.dialog-footer {
  position: fixed;
  bottom: 0;
  width: 100%;
  height: 43px;
  background-color: #e9ebee;
  border-radius: 0 0 5px 5px;
  display: flex;
  align-items: center;
  justify-items: right;
  border-radius: 2px;
}

.dialog-footer .footer-content {
  position: absolute;
  right: 0px;
  margin-right: 9px;
}

.dialog-footer button {
  margin-right: 20px;
  height: 31px;
  padding-left: 10px;
  padding-right: 10px;
  background-color: #5cb85c;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  color: white;
}
.dialog-footer button:hover {
  color: black;
}
#btn-save {
  width: 70px;
  border: solid 1px #ffffff;
}

#btn-cancel {
  border: solid 1px #ffffff;
  width: 70px;
}

.currency-for-input {
  position: absolute;
  right: 40px;
  line-height: 40px;
  font-size: xx-small;
  font-style: italic;
  color: black !important;
}

.dialog-footer button:hover {
  background-color: azure;
}
textarea {
  border-radius: 3px;
  padding-left: 5px;
}
</style>