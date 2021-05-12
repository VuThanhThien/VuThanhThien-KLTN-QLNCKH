<template>
  <div>
    <div class="navBar" v-if="loggedIn">
      <div v-if="loggedIn && currentRole == 'Admin'" class="headerBtn">
        <DxSelectBox
          placeholder="Chọn tác vụ"
          :data-source="option"
          display-expr="name"
          value-expr="id"
          @value-changed="onValueChanged"  
        />
      </div>
      <div v-if="loggedIn && currentRole == 'User'" class="headerBtn">
        <DxSelectBox
          placeholder="Chọn tác vụ"
          :data-source="optionsForUser"
          display-expr="name"
          value-expr="id"
          @value-changed="onValueChanged"  
        />
      </div>
    </div>
  </div>
</template>

<script>
import DxButton from "devextreme-vue/button";
import { DxSelectBox } from "devextreme-vue/select-box";
import service from "../../../modules/data.js";
export default {
  components: {
    DxButton,DxSelectBox
  },
  computed: {
    loggedIn() {
      return this.$store.getters.loggedIn;
    },
    currentRole() {
      return this.$store.getters.currentRole;
    },
  },
  data(){
    return{
      option: service.getOption(),
      optionsForUser: service.getOptionsForUser(),
    };
  },
  methods:{
    onValueChanged(e) {
            const item = this.option.filter(i => i.id === e.value)[0];
            this.$router.push({ name: item.route });
        }
  }
};
</script>

<style lang="scss" scoped>
.navBar {
  display: flex;
  list-style: none;
  padding: 10px 0;
  margin: 0;
  justify-content: flex-end;
  background: #f5f8fa;
  border-bottom: 1px solid lightgrey;
}
.headerBtn {
  margin-right: 15px;
}
</style>
