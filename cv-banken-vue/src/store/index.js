import Vue from 'vue';
import Vuex from 'vuex';

import { auth } from './auth.module';
import { edu } from './edu.module'
import {files} from "@/store/files.module";
import {profile} from "@/store/profile.module";
import {user} from "@/store/user.module";
import {Site} from "@/store/site.module";

Vue.use(Vuex);

export default new Vuex.Store({
    modules: {
        auth, edu, files, profile, user, Site
    }
});
