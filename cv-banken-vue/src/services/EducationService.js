import axios from "axios";
import {authHeader} from "@/services/AuthHeader";

const API_URL = process.env.VUE_APP_API_URL
class EducationService {
    getEducations() {
        return axios
            .get(API_URL + 'programme',
                {
                    headers:authHeader(),
                    credentials: 'include'}
            )
            .then(response => {
                return response.data;
            });
    }

    getEducation(id) {
        return axios.get(API_URL + "programme/" + id, {
                headers:authHeader(),
                credentials: 'include'
        }
        ).then(res => {
            return res.data
        });
    }
}

export default new EducationService();
