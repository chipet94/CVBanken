<template>
  <ul>
    <template v-for="item in programmes">
      <li v-if="item.totalStudents > 0" class="ITHS-list" :key="item.id">
        <b-collapse class="card" animation="slide" aria-id="programmeName" :open="false" @open="programmeExpanded(item.id)">
          <div
              slot="trigger"
              slot-scope="props"
              class="card-header ITHS-sub-header"
              role="button"
              aria-controls="programmeName">
            <p class="card-header-title is-centered is-center">
              {{item.name}}
            </p>
              <b-tag class="is-info has-text-weight-bold" title="visible students">
                {{item.publicStudents + " / " + item.totalStudents}}
              </b-tag>

            <a class="card-header-icon">
              <b-icon
                  :icon="props.open ? 'menu-down' : 'menu-up'">
              </b-icon>
            </a>
          </div>
          <div class="card-content">
            <div class="content">
              <students-list :programmeId="item.id"></students-list>
            </div>
          </div>
        </b-collapse>
      </li>
    </template>
  </ul>
</template>

<script>
import StudentsList from "@/components/common/Lists/StudentsList";
export default {
name: "ProgrammeList",
  components: {StudentsList},
  props:{programmes: Array},

  methods:{
  programmeExpanded(prog){
    this.$store.dispatch("edu/getStudentsIn", prog)
  }
  }
}
</script>

<style scoped>
.ITHS-sub-header{
  background-color: whitesmoke;
  text-align: center;
  display: flex;
}
.ITHS-sub-header p {
  display: block;
  position: center;
  float: left;
  text-align: center;
}
.ITHS-sub-header :hover{
  opacity: 0.7;
}
.ITHS-list{
 list-style-type: none;
}
.ITHS-list :hover{

}
</style>