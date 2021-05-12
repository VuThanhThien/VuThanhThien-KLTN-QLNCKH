
<template>
  <div class="modal">
    <div class="dialog-mask">
      <div draggable="true" class="dialog">
        <!-- nút x thoát dialog  -->
        <router-link :to="{ name: 'ListAuthor' }">
          <button class="close-dialog">
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
        </router-link>

        <!-- body dialog  -->
        <div class="dialog-content">
          <h2 style="text-align: center">Lập lịch gửi Email</h2>
          <br />
          <br />
          <ul class="a">
            <li>
              Gửi Email thông báo cho những chủ nhiệm đề tài có đề tài sắp tới
              hạn nghiệm thu,<br />
              trước <input type="number" v-model="perday" /> ngày so với hạn
              nghiệm thu đăng ký.<br />Lặp lại vào lúc 24:00 hằng ngày.
            </li>
          </ul>
        </div>
        <!-- footer dialog  -->
        <div class="dialog-footer">
          <div class="footer-content">
            <router-link :to="{ name: 'ListAuthor' }">
              <button id="btn-cancel">Hủy</button>
            </router-link>
            <button id="btn-save" @click="savePerDay">Lưu</button>
            <button id="btn-save" @click="sendNow">Gửi</button>
          </div>
        </div>
      </div>
    </div>
    <notifications position="bottom right" clean: true style="margin-bottom:
    20px"/>
  </div>
</template>

<script>
import * as axios from "axios";
export default {
  name: "CronJob",
  methods: {
    savePerDay() {
      axios
        .post(
          "https://localhost:44385/api/Hangfire/research-expire?perDay=" +
            this.perday
        )
        .then((response) => {
          if (response.status == 200) {
            this.$notify({
              type: "success",
              title: "THÔNG BÁO",
              text:
                "Cronjob chạy thành công theo chu kỳ 1 ngày với khoảng thời gian là " +
                this.perDay +
                " ngày đến lúc hết hạn !",
            });
          }
        })
        .catch((e) => {
          console.log(e);
        });
      setTimeout(() => {
        this.$router.push("ListTopic");
      }, 1500);
    },

    sendNow() {
      axios
        .post(
          "https://localhost:44323/api/CronJob/research-expire?perDay=" +
            this.perday
        )
        .then((response) => {
          if (response.data) {
            this.$notify({
              type: "success",
              title: "THÔNG BÁO",
              text: "gửi thành công !",
            });
          }
        })
        .catch((e) => {
          console.log(e);
        });
      setTimeout(() => {
        this.$router.push("ListTopic");
      }, 1500);
    },
  },
  data() {
    return {
      perday: 0,
    };
  },
};
</script>

<style scoped>
li {
  font-size: medium;
}
ul.a {
  list-style-type: circle;
}
.dialog {
  padding: 10px;
  position: absolute;
  width: 700px;
  left: 33%;
  top: 4%;
  background-color: #fff;
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
h3 {
  padding: 20px;
  font-weight: bold;
  text-transform: uppercase;
}
h4 {
  padding-top: 10px;
  margin-left: 14px;
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
  flex-direction: column;
  justify-content: space-between;
}

input,
select {
  padding-left: 20px;
  margin-top: 4px;
  width: 200px;
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
option {
  text-overflow: ellipsis;
}
</style>