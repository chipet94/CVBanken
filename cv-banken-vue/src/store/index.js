import Vue from 'vue';
import Vuex from 'vuex';

import { auth } from './auth.module';
import { edu } from './edu.module'
import {files} from "@/store/files.module";

Vue.use(Vuex);

export default new Vuex.Store({
    modules: {
        auth, edu, files
    }
});
