
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
        <h3>THÔNG TIN ĐỀ TÀI</h3>
        <!-- body dialog  -->
        <div class="dialog-content">
          <!-- khối avatar  -->
          <!-- Khối điền thông tin  -->
          <div class="block-right">
            <!-- block 2 là khối chưa 2 trường nhập  -->
            <div class="block-2-topic">
              <!-- block 1 là khối chứ 1 trường nhập  -->
              <div class="block-1-topic">
                <div class="fieldName">Mã đề tài (<span>*</span>)</div>
                <!-- nhập mã nhân viên  -->
                <input id="txtEmployeeCode" type="text" />
              </div>
              <!-- Nhập họ tên  -->
              <div class="block-1-topic">
                <div class="fieldName">Tên đề tài (<span>*</span>)</div>
                <input
                  id="txtFullName"
                  fieldName="FullName"
                  class="input-required"
                  type="text"
                />
              </div>
            </div>
            <!-- Nhập ngày sinh  -->
            <div class="block-2-topic">
              <div class="block-1-topic">
                <div class="fieldName">Ngày nghiệm thu</div>
                <input type="date" />
              </div>
              <!-- Nhập giới tính  -->
              <div class="block-1-topic">
                <div class="fieldName">Kinh phí</div>
                <input type="number" />
              </div>
            </div>
            <div class="block-2-topic">
              <div class="block-1-topic">
                <!-- Nhập cmtnd/ căn cước  -->
                <div class="fieldName">Kết quả nghiên cứu(<span>*</span>)</div>
                <select id="cbxPosition" class="m-control">
                  <option>
                    {{}}
                  </option>
                </select>
              </div>
              <!-- Ngầy cấp cmtnd/cc  -->
              <div class="block-1-topic">
                <div class="fieldName">Trạng thái nghiên cứu</div>
                <select id="cbxPosition" class="m-control">
                  <option>
                    {{}}
                  </option>
                </select>
              </div>
            </div>
            <div class="block-2-topic">
              <div class="block-1-topic">
                <!-- Nới cấp cmtnd/cc  -->
                <div class="fieldName">Mô tả</div>
                <textarea
                  style="margin: 0px; width: 650px; height: 107px; resize: none"
                ></textarea>
              </div>
            </div>
            <div class="block-2-topic">
              <div class="block-1-topic">
                <!-- Email  -->
                <div class="fieldName">Ngày bắt đầu (<span>*</span>)</div>
                <input type="date" />
              </div>
              <div class="block-1-topic">
                <!-- Số điện thoại  -->
                <div class="fieldName">Ngày kết thúc (<span>*</span>)</div>
                <input type="date" />
              </div>
            </div>
            <div class="block-2-topic">
              <div class="block-1-topic">
                <!-- Vị trí công việc  -->
                <div class="fieldName">Tác giả</div>
                <input type="text" />
              </div>
            </div>
          </div>
        </div>
        <!-- footer dialog  -->
        <div class="dialog-footer">
          <div class="footer-content">
            <button id="btn-cancel" @click="cancel">Hủy</button>
            <button id="btn-save">Lưu</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import * as axios from "axios";
import service from "../../../modules/data.js";
export default {
  name: "UserDetails",
  props: {
    isHide: {
      type: Boolean,
      default() {
        return true;
      },
    },
  },
  components: {},
  methods: {
    /**Sự kiện hủy */
    cancel() {
      this.$emit("outIsHide", !this.isHide);
    },
    validEmail(email) {
      var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      return re.test(email);
    },

    // formatPrice(value) {
    //   if (value) {
    //     var salary = value.toString();
    //     return salary.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");
    //   }
    // },

    //   saveEmployee() {
    //     if (this.employee.EmployeeId == null && this.checkForm()) {
    //       axios({
    //         method: "POST",
    //         url: "http://api.manhnv.net/api/employees",
    //         data: this.employee,
    //       }).catch((e) => console.log(e));
    //       console.log(this.employee);
    //     }
    //     if (this.employee.EmployeeId != null && this.checkForm()) {
    //       axios({
    //         method: "PUT",
    //         url: "http://api.manhnv.net/api/employees",
    //         data: this.employee,
    //       }).catch((e) => console.log(e));
    //     }
    //     this.closeDialog();
    //   },
  },
  data() {
    return {
      departments: service.getDepartment(),
      positions: service.getPosition(),
    };
  },
};
</script>

<style>
.dialog-topic {
  position: absolute;
  width: 700px;
  left: 33%;
  top: 13%;
  background-color: #fff;
}
.dialog-mask {
  position: fixed;
  z-index: 9998;
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
}

.block-right {
  display: block;
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
}
.block-1-topic input,
.block-1-topic select {
  padding-left: 20px;
  margin-top: 4px;
  width: 300px;
  height: 30px;
  border-radius: 5px;
  border: 0.5px solid rgb(156, 153, 153);
  padding: 5px;
}

.dialog-footer {
  margin-top: 20px;
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
}

#btn-save {
  width: 70px;
  border: solid 1px #ffffff;
}

#btn-cancel {
  border: solid 1px #ffffff;
  width: 70px;
  background-color: Transparent !important;
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
</style>