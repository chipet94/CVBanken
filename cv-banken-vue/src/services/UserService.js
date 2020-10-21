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

}

export default new UserService();