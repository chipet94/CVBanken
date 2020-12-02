<template>
  <ul>
    <template v-for="item in programmes">
      <li :key="item.id" class="ITHS-list">
        <b-collapse :open="false" animation="slide" aria-id="programmeName" class="card"
                    @open="programmeExpanded(item.id)">
          <div
              slot="trigger"
              slot-scope="props"
              aria-controls="programmeName"
              class="card-header ITHS-sub-header"
              role="button">
            
            <div class="columns">
              <div class="column start" title="Stad">
                  {{item.location}}
              </div>
              <div class="column middle">
                <p class="card-header-title is-centered is-center">
                  {{ item.name }}
                </p>
 
              </div>
              <div class="column end">
                ({{item.start}} - {{item.end}})
                <b-tag class="is-info has-text-weight-bold" title="visible students">
                  {{ item.publicStudents + " / " + item.totalStudents }}
                </b-tag>
                
              </div>
            </div>
            <a class="card-header-icon">
              <b-icon
                  :icon="props.open ? 'menu-down' : 'menu-up'">
              </b-icon>
            </a>
            <router-link :to="{ name: 'Klass', params: {name: item.name}}" class="card-header-icon ITHS-text"
                         title="Gå till klass">
              <b-icon
                  icon="link">
              </b-icon>
            </router-link>


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
  props: {programmes: Array},

  methods: {
    async programmeExpanded(prog) {
      if (this.$store.getters["edu/studentsInProgramme"](prog) < 1) {
        await this.$store.dispatch("edu/getStudentsIn", prog)
      }
    }
  }
}
</script>

<style scoped>
.ITHS-sub-header {
  background-color: whitesmoke;
  text-align: center;
  align-content: center;
  vertical-align: center;
  display: flex;
}

.ITHS-sub-header p {
  display: block;
  position: center;
  float: left;
  text-align: center;
}
.ITHS-sub-header * {
  margin-bottom: auto;
  margin-top: auto;
  position: center;
  text-align: center;
  vertical-align: center;
}
.columns {
  width: 100%;
}
.columns.start {
  float: left;
}
.columns.middle {
  
}
.columns.end {
  float: right;
  display: flex;
}
.ITHS-sub-header :hover {
  opacity: 0.7;
}

.ITHS-list {
  list-style-type: none;
}

.ITHS-list :hover {

}
</style>