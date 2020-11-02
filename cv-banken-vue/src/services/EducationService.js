import axios from "axios";
import {authHeader} from "@/services/AuthHeader";

const API_URL = process.env.VUE_APP_API_URL

class EducationService {
    getCategories() {
        return axios
            .get(API_URL + 'category',
                {
                    headers: authHeader(),
                    credentials: 'include'
                }
            )
            .then(response => {
                return response.data;
            });
    }

    getCategoryById(id) {
        return axios
            .get(API_URL + 'category/'+id,
                {
                    headers: authHeader(),
                    credentials: 'include'
                }
            )
            .then(response => {
                return response.data;
            });
    }

    getCategoryByName(name) {
        return axios
            .get(API_URL + 'category/'+name,
                {
                    headers: authHeader(),
                    credentials: 'include'
                }
            )
            .then(response => {
                return response.data;
            });
    }

    getStudentsInProgramme(id) {
        return axios
            .get(API_URL + 'programme/' + id + "/students",
                {
                    headers: authHeader(),
                    credentials: 'include'
                }
            )
            .then(response => {
                return response.data;
            });
    }

    getEducations() {
        return axios
            .get(API_URL + 'programme',
                {
                    headers: authHeader(),
                    credentials: 'include'
                }
            )
            .then(response => {
                return response.data;
            });
    }

    getEducation(id) {
        return axios.get(API_URL + "programme/" + id, {
                headers: authHeader(),
                credentials: 'include'
            }
        ).then(res => {
            return res.data
        });
    }
    getEducationByName(name) {
        return axios.get(API_URL + "programme/" + name, {
                headers: authHeader(),
                credentials: 'include'
            }
        ).then(res => {
            return res.data
        });
    }
    getEducationByCategory(id) {
        return axios.get(API_URL + "programme/category/" + id, {
            headers: authHeader(),
        }).then(res => {
            return res.data
        });
    }
    addProgramme(programme){
        return axios.post(API_URL + "programme", programme,{
            headers: authHeader(),
        }).then(res => {
            return res.data
        });
    }
    updateProgramme(id, programme){
        return axios.put(API_URL + "programme/"+id, programme,{
            headers: authHeader(),
        }).then(res => {
            return res.data
        });
    }
    deleteProgramme(id){
        return axios.delete(API_URL + "programme/"+id,{
            headers: authHeader(),
        }).then(res => {
            return res.data
        });
    }
    addCategory(category){
        return axios.post(API_URL + "category", category,{
            headers: authHeader(),
        }).then(res => {
            return res.data
        });
    }
    updateCategory(id, category){
        return axios.put(API_URL + "category/"+id, category,{
            headers: authHeader(),
        }).then(res => {
            return res.data
        });
    }
    deleteCategory(id){
        return axios.delete(API_URL + "category/" +id,{
            headers: authHeader(),
        }).then(res => {
            return res.data
        });
    }
}

export default new EducationService();
