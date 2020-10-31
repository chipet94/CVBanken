import axios from "axios";
import {authHeader} from "@/services/AuthHeader";

const API_URL = process.env.VUE_APP_API_URL
class EducationService {
    getEducationCategories() {
        return axios
            .get(API_URL + 'programme/category',
                {
                    headers:authHeader(),
                    credentials: 'include'}
            )
            .then(response => {
                return response.data;
            });
    }
    getStudentsInProgramme(id) {
        return axios
            .get(API_URL + 'programme/' + id+ "/students",
                {
                    headers:authHeader(),
                    credentials: 'include'}
            )
            .then(response => {
                return response.data;
            });
    }

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
    getEducationByCategory(id) {
        return axios.get(API_URL + "programme/category/" + id, {
            authHeader
        }).then(res => {
            return res.data
        });
    }
}

export default new EducationService();
