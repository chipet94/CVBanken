import axios from "axios";
import {authHeader} from "@/services/AuthHeader";

const API_URL = process.env.VUE_APP_API_URL
class UserService {
    getAll() {
        return axios
            .get(API_URL + 'user',)
    }
    adminGetAll() {
        return axios
            .get(API_URL + 'user/super/all',
                {headers: authHeader(),}
            )
    }
    getAllinProgramme(id) {
        return axios
            .get(API_URL + 'user/programme/' + id)
    }
    getAllinCategory(id) {
        return axios
            .get(API_URL + 'user/category/'+ id)
    }

}

export default new UserService();